using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Production
{
    public class UploadImagesEdit
    {
        //public int ProductId { get; set; }

        //public ProductDto Product { get; set; }
        public ProductDto productDto { get; set; }
        public ProductPhotoDto productPhotoDto { get; set; }
        public ProductProductPhotoDto ProductProductPhotoDto { get; set; }
        public List<IFormFile> FileImages { get; set; }
        public IFormFile Photo1 { get; set; }
        public IFormFile Photo2 { get; set; }
        public IFormFile Photo3 { get; set; }

    }
}
