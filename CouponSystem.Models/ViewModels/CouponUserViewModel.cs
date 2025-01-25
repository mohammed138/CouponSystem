using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystem.Models.ViewModels
{
    public class CouponUserViewModel
    {
        public string? UserId{ get; set; }
        public string? EmailAddress{ get; set; }
        public int Usage { get; set; }
        public string? FullName { get; set; }
    }
}
