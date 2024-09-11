using AppToken.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppToken.Infra.Interface
{
    public  interface ITokenUtils
    {
        public string GenerateAccessToken(User user, string secret);
        public string GenerateRefreshToken();
        public string GenerateAccessTokenFromRefreshToken(string Token, string refreshToken, string secret);
    }
}
