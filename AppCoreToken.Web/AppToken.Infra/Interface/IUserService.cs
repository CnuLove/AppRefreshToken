using AppToken.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppToken.Infra.Interface
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        // void SaveRefreshToken(int userId, string refreshToken);
        // string GetRefreshToken(int userId);
    }
}
