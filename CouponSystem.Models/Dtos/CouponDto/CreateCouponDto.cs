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
        [Required(ErrorMessage = " this Field is required.")]
        public string? Code { get; set; }

        [Required(ErrorMessage = " this Field is required.")]

        public string? Name { get; set; }
         
        public List<CreateCouponUserDto>  CouponUsers { get; set; } 
        public List<CreateCouponProductDto> CouponProducts { get; set; }



        [Required(ErrorMessage = " this Field is required.")]
        public List<Guid> SelectedProductIds { get; set; }  // List of selected product IDs

        [Required(ErrorMessage = " this Field is required.")]
        public List<UserUsageDto> SelectedUsers { get; set; }  // List of users and their usage count



        [Required(ErrorMessage = " this Field is required.")]
        public decimal MinimumOrderPrice { get; set; }

        [Required(ErrorMessage = " this Field is required.")]
        public int UseCount { get; set; }

        [Required(ErrorMessage = " this Field is required.")]
        public int Value { get; set; }

        [Required(ErrorMessage = " this Field is required.")]
        public TypeCoupon Type { get; set; }

        [Required(ErrorMessage = " this Field is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = " this Field is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = " this Field is required.")]
        public int NumberOfUsePerCustomer { get; set; }

        [Required(ErrorMessage = " this Field is required.")]
        public bool FreeShipping { get; set; }

        [Required(ErrorMessage = " this Field is required.")]
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
