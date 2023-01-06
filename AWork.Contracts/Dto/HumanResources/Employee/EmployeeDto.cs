using AWork.Contracts.Dto.PersonModule;
using AWork.Contracts.Dto.Sales.SalesPerson;
﻿using AWork.Contracts.Dto.HumanResources.EmployeePayHistory;
using AWork.Contracts.Dto.HumanResources.JobCandidate;
using AWork.Contracts.Dto.PersonModule;
using AWork.Domain.Models;
using System;
using System.Collections.Generic;

namespace AWork.Contracts.Dto.HumanResources.Employee
{
    public class EmployeeDto
    {
        /*public Employee()
        {
            EmployeeDepartmentHistories = new HashSet<EmployeeDepartmentHistory>();
            EmployeePayHistories = new HashSet<EmployeePayHistory>();
            JobCandidates = new HashSet<JobCandidate>();
            PurchaseOrderHeaders = new HashSet<PurchaseOrderHeader>();
        }
*/
        public int BusinessEntityId { get; set; }
        public string NationalIdnumber { get; set; }
        public string LoginId { get; set; }
        public short? OrganizationLevel { get; set; }
        public string JobTitle { get; set; }
        public DateTime BirthDate { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public DateTime HireDate { get; set; }
        public bool? SalariedFlag { get; set; }
        public short VacationHours { get; set; }
        public short SickLeaveHours { get; set; }
        public bool? CurrentFlag { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<EmployeeDepartmentHistoryDto> EmployeeDepartmentHistories { get; set; }
        public virtual EmployeeDepartmentHistoryDto EmployeeDepartmentHistoryDtos { get; set; }

        public virtual ICollection<EmployeePayHistoryDto> EmployeePayHistories { get; set; }
        public virtual EmployeePayHistoryDto EmployeePayHistoryDtos { get; set; }
        
        public virtual PersonDto BusinessEntity { get; set; }
        public virtual SalesPersonDto SalesPerson { get; set; }
/*        public virtual ICollection<SalesPersonDto> SalesPersonDto { get; set; }*/
        /*public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; }
        public virtual ICollection<EmployeePayHistory> EmployeePayHistories { get; set; }
        public virtual ICollection<JobCandidate> JobCandidates { get; set; }
        public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }*/

        /*public virtual ICollection<EmployeePayHistoryDto> EmployeePayHistoriesDto { get; set; }
        public virtual ICollection<JobCandidateDto> JobCandidatesDto { get; set; }*/

        //public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
        //public virtual SalesPerson SalesPerson { get; set; }
        //public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; }

    }
}
