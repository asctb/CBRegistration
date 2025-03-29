using CBRegistration.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRegistration.BLL.Interfaces
{
    public interface IUserVerificationService
    {
        BaseResponseModel VerifyUserPhoneByCode(int userId);
        BaseResponseModel VerifyUserEmailByCode(int userId);
    }
}
