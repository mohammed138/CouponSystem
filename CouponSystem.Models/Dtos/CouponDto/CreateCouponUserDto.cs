using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystem.Models.Dtos.CouponDto
{
    public class CreateCouponUserDto
    {

        [Required]
        public string UserId { get; set; }
        public string FullName { get; set; }
        public int Usage { get; set; } = 0;
    }
}
