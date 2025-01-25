using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CouponSystem.Models.Entities
{
    public class CouponProduct
    {

        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Coupon")]
        public Guid CouponId { get; set; }
        [JsonIgnore]
        public Coupon? Coupon { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        [NotMapped]
        [NotNull]
        public CouponProduct? Model { get; set; }
        public CouponProduct(CouponProduct model)
        {
            this.Model = model;
        }
        public CouponProduct() { }
    }
}
