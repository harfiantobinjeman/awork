using AWork.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Production
{
    public class ProductProductPhotoForCreateDto
    {
        //public List<IFormFile> FilesPhoto { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "set Primary")]
        public bool Primary { get; set; }
        public int ProductPhotoId { get; set; }
        public DateTime ModifiedDate { get; set; }

        /*  public virtual ProductDto Product { get; set; }
          public virtual ProductPhotoDto ProductPhoto { get; set; }*/

    }
}
