using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRegistration.Shared.Models
{
    public class SetTermsAndCondsRequestModel
    {
        public int UserId { get; set; }
        public bool IsAccepted { get; set; }

    }
}
