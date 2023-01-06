using AWork.Domain.Models;
using AWork.Contracts.Dto.HumanResources;
using AWork.Domain.Repositories.HumanResource;
using AWork.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using AWork.Contracts.Dto.HumanResources.Employee;
using AWork.Domain.Dto;

namespace AWork.Persistence.Repositories.HumanResource
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AdventureWorks2019Context dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Employee>> GetAllEmployee(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.BusinessEntityId)
                .Include( p => p.BusinessEntity)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int businessEntityId, bool trackChanges)
        {
            return await FindByCondition(c => c.BusinessEntityId.Equals(businessEntityId), trackChanges)
                .Include(p => p.BusinessEntity)
                .Include(q => q.EmployeeDepartmentHistories)
                .Include(w => w.EmployeePayHistories)
                .SingleOrDefaultAsync();
        }

        public void Insert(Employee employee)
        {
            Create(employee);
        }

        public void Edit(Employee employee)
        {
            Update(employee);
        }

        public void Remove(Employee employee)
        {
            Delete(employee);
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByPerson(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Include(c => c.BusinessEntity)
                .Include(p => p.BusinessEntityId)
                .Where(x => (x.BusinessEntity.PersonType == "EM" || x.BusinessEntity.PersonType == "SP") && x.NationalIdnumber == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeePaged(int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(p => p.BusinessEntityId)
                .Include(b => b.BusinessEntity)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetPersonByType(bool trackChange)
        {
            var personEmployee = await _dbContext.People
                .Where(p => (p.PersonType == "EM" || p.PersonType == "SP") && !_dbContext.Employees.Any(e => e.BusinessEntityId == p.BusinessEntityId))
                .Select(p => new Person
                {
                    BusinessEntityId = p.BusinessEntityId,
                    FirstName = p.FirstName,
                    LastName = p.LastName
                })
                .ToListAsync();
            return personEmployee;   
        }

        public async Task<IEnumerable<JobTitleType>> GetJobTitle(bool trackChange)
        {
            var jobList = _dbContext.JobTitleTypes.FromSqlRaw("select distinct JobTitle from HumanResources.Employee")
                                .Select(j => new JobTitleType
                                {
                                    JobTitle = j.JobTitle
                                })
                                .ToList();

            return jobList;
        }

        public IEnumerable<JmlhEmployeTiapDepartement> GetEmployeeTiapDepartement()
        {
            var empTiapDepart = _dbContext.JmlhEmp.FromSqlRaw("select e.Name, count(d.BusinessEntityID) as Jumlah_Employee "
                               + "from HumanResources.Department as e "
                               + "join HumanResources.EmployeeDepartmentHistory as ed on ed.DepartmentID = e.DepartmentID "
                               + "join HumanResources.Employee as d on ed.BusinessEntityID = d.BusinessEntityID "
                               + "group by e.DepartmentID, e.Name")
                                .Select(j => new JmlhEmployeTiapDepartement
                                {
                                    Name = j.Name,
                                    Jumlah_Employee = j.Jumlah_Employee
                                })
                                .ToList();
            return empTiapDepart;
        }
        public IEnumerable<SalaryRateEmployeeDto> GetSalaryRateEmp()
        {
            var salaryEmp = _dbContext.TotalEmp.FromSqlRaw("select t.Rate, sum(t.jumlahEmployee) as totalEmployee From (\r\nselect\r\ncase\r\nwhen Rate > 5 and Rate <= 10 then 'Rate 05-10'\r\nwhen Rate > 10 and Rate <= 15 then 'Rate 11-15'\r\nwhen Rate > 15 and Rate <= 20 then 'Rate 16-20'\r\nwhen Rate > 20 and Rate <= 45 then 'Rate 20-45'\r\nwhen Rate > 45 then 'Rate More then 45'\r\nend Rate,\r\ncase\r\nwhen Rate > 5 and Rate <= 10 then count(BusinessEntityID)\r\nwhen Rate > 10 and Rate <= 15 then count(BusinessEntityID)\r\nwhen Rate > 15 and Rate <= 20 then count(BusinessEntityID)\r\nwhen Rate > 20 and Rate <= 45 then count(BusinessEntityID)\r\nwhen Rate > 45 then count(BusinessEntityID)\r\nend as jumlahEmployee\r\nfrom HumanResources.EmployeePayHistory\r\ngroup by Rate) t \r\ngroup by t.Rate")
                                .Select(x => new SalaryRateEmployeeDto
                                {
                                    Rate = x.Rate,
                                    TotalEmployee = x.TotalEmployee
                                })
                                .ToList();
            return salaryEmp;
        }
    }
}


/*var personEmployee = _dbContext.PersonEmployeeTypes.FromSqlRaw("select BusinessEntityID, FirstName, MiddleName, LastName, Suffix from person.person P where not exists(select * from HumanResources.Employee where BusinessEntityID = p.BusinessEntityID ) and P.PersonType = 'EM'")
                    .Select(x => new PersonEmployeeType
                    {
                        BusinessEntityId = x.BusinessEntityId,
                        FirstName = x.FirstName,
                        MiddleName = x.MiddleName,
                        LastName = x.LastName,
                        Suffix = x.Suffix
                    })
                    .ToList();*/


// _dbContext.PersonEmployeeTypes.FromSqlRaw("select BusinessEntityID, FirstName, MiddleName, LastName, Suffix from Person.Person p where not exists(select 1 from HumanResources.Employee e where e.BusinessEntityID = p.BusinessEntityID) and p.PersonType = 'EM'")

/*return await _dbContext.People
    .LeftJoin(_dbContext.Employees,
    p => p.BusinessEntityId,
    e => e.BusinessEntityId,
    (p, e) => new { People = p, Employees = e })
    .Where(e => (e.People.PersonType == "EM" || e.People.PersonType == "SP") && e.Employees.NationalIdnumber == null)
    .Select(x => x.People)
    .ToListAsync();*/

/*return await _dbContext.People
                  .Where(x => (x.PersonType == "EM" || x.PersonType == "SP") &&
                  !_dbContext.Employees.Any(sp => sp.NationalIdnumber == null))
                  .ToListAsync();*/
/*return await _dbContext.People
    .Join(_dbContext.Employees,
    p => p.BusinessEntityId,
    e => e.BusinessEntityId,
    (p, e) => new { People = p, Employees = e })
    .Where(x => x.People.PersonType == "SP" && !_dbContext.SalesPeople.Any(sp => sp.BusinessEntityId == x.People.BusinessEntityId))
    .Select(x => x.People)
    .ToListAsync();*/