using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRegistration.Shared.Models
{
    public class BaseResponseModel
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
    }

    public class BaseResponseModel<T> : BaseResponseModel
    {
        public T? Data { get; set; }
    }
}
