using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystem.Models.Dtos.Helpers
{

    public class ResponseData<T>
    {
        public string Message { get; set; } = "Success";
        public bool IsSuccess { get; set; } = true;
        public T? Data { get; set; }
    }
    public class ResponsDataByPage<T> : ResponseData<T>
    {
        public Pagination? meta { get; set; }
    }
    public class Pagination
    {
        public int last_page { get; set; } = 1;
        public int per_page { get; set; } = 10;
        public int total { get; set; } = 0;
        public int current_page { get; set; } = 1;
    }

}
