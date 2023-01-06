using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Production
{
    public class UploadImages
    {
        //public int ProductId { get; set; }

        //public ProductDto Product { get; set; }
        public ProductForCreateDto ProductFCDto { get; set; }
        public ProductPhotoForCreateDto productPhotoFCDto { get; set; }
        public ProductProductPhotoForCreateDto ProductProductPhotoFCDto { get; set; }
        public List<IFormFile> FileImages { get; set; }
        public IFormFile Photo1 { get; set; }
        public IFormFile Photo2 { get; set; }
        public IFormFile Photo3 { get; set; }

    }
}
