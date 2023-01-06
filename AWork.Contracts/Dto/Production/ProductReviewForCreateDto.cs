using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWork.Contracts.Dto.Production
{
    public class ProductReviewForCreateDto
    {
        public int ProductId { get; set; }
        public string ReviewerName { get; set; }
        public DateTime ReviewDate { get; set; }
        public string EmailAddress { get; set; }
        /*[Required]
        [StringLength(5, ErrorMessage = "Rating 1 - 5")]*/
        public int Rating { get; set; }
        public string Comments { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ProductDto Product { get; set; }
    }
}
