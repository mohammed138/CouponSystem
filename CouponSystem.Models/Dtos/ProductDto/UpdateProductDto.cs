using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystem.Models.Dtos.ProductDto
{
    public class UpdateProductDto : CreateProductDto
    {
        public Guid Id { get; set; }
    }
}
