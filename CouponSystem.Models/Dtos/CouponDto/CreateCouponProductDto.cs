using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystem.Models.Dtos.CouponDto
{
    public class CreateCouponProductDto
    {
        [Required]
        public Guid  Id { get; set; }
        public string? Name { get; set; }
    }
}
