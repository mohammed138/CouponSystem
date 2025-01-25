using CouponSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystem.Models.Entities
{
    public class Coupon : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Code { get; set; }
        public string? Name { get; set; }
        public List<CouponUser>? CouponUsers { get; set; }
        public List<CouponProduct>? CouponProducts { get; set; }
        public decimal MinimumOrderPrice { get; set; }

        [Required]
        public int UseCount { get; set; }

        [Required] 
        public int Value { get; set; }

        [Required]
        public TypeCoupon Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfUsePerCustomer { get; set; }
        public bool FreeShipping { get; set; }
        public bool CODNotIncluded { get; set; }

    }
}
