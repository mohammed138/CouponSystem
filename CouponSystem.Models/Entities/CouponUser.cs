using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystem.Models.Entities
{
    public class CouponUser
    {
        public long Id { get; set; }
        [ForeignKey(nameof(User))]
        [NotNull]
        public string? UserId { get; set; }  
        public ApplicationUser? User { get; set; }

        [ForeignKey(nameof(Coupon))]
        public Guid? CouponId { get; set; }
        public Coupon? Coupon { get; set; }
        public int Usage { get; set; } = 0;

    }
}
