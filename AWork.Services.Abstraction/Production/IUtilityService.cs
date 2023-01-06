using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Services.Abstraction.Production
{
    public interface IUtilityService
    {
        string UploadSingleFile(IFormFile formFile);
    }
}