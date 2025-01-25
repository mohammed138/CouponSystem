using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystem.Models.Dtos.UserDto
{
    public class UpdateUserDto : CreateUserDto
    {
        public string Id { get; set; }
    }
}
