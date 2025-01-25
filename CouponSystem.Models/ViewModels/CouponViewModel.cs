using CouponSystem.Models.Entities;
using CouponSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystem.Models.ViewModels
{
    public class CouponViewModel
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public List<CouponUserViewModel>? CouponUsers { get; set; }
        public List<CouponProductViewModel>? CouponProducts { get; set; }
        public decimal MinimumOrderPrice { get; set; }
        public int UseCount { get; set; }
        public int Value { get; set; }
        public TypeCoupon Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfUsePerCustomer { get; set; }
        public bool FreeShipping { get; set; }
        public bool CODNotIncluded { get; set; }
    }
}
