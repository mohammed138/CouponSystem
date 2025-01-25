using CouponSystem.Models.Entities;
using CouponSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystem.Models.Dtos.CouponDto
{
    public class CreateCouponDto
    {
        [Required]
        public string? Code { get; set; }

        [Required]
        public string? Name { get; set; }
         
        public List<CreateCouponUserDto>  CouponUsers { get; set; } 
        public List<CreateCouponProductDto> CouponProducts { get; set; }



        [Required]
        public List<Guid> SelectedProductIds { get; set; }  // List of selected product IDs

        [Required]
        public List<UserUsageDto> SelectedUsers { get; set; }  // List of users and their usage count



        [Required]
        public decimal MinimumOrderPrice { get; set; }

        [Required]
        public int UseCount { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public TypeCoupon Type { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int NumberOfUsePerCustomer { get; set; }

        [Required]
        public bool FreeShipping { get; set; }

        [Required]
        public bool CODNotIncluded { get; set; }
    }
    public class UserUsageDto
    {
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public int Usage { get; set; }
    }
}
