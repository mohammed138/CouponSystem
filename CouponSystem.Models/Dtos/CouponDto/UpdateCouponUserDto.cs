using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystem.Models.Dtos.CouponDto
{
    public class UpdateCouponUserDto : CreateCouponUserDto
    {
        public Guid Id { get; set; }
    }
}
