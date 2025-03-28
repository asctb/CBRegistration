using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRegistration.Shared.Models
{
    public class PinModel
    {
        public int UserId { get; set; }
        public string Pin { get; set; } = string.Empty;
    }
}
