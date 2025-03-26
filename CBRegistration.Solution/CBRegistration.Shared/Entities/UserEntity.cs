using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRegistration.Shared.Entities
{
    public class UserEntity : BaseEntity
    {
        public int ICNumber { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int MobileNumber { get; set; }
        public string Email { get; set; } = string.Empty;
        public int Pin { get; set; }
        public bool IsBiometricLoginEnabled { get; set; } = false;
        public bool HasAcceptedTermsConditions { get; set; } = false;
    }
}
