using AppToken.Infra.Common;
using AppToken.Infra.Interface;
using AppToken.Infra.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppToken.Infra.RegisterService
{
    public static class Registerservice
    {

        
        public static IServiceCollection AddinfrastructureRegister(this IServiceCollection services)
        {


            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient(typeof(ITokenUtils), typeof(TokenUtils));
            //var secret = "endrumanbudanseenuendrumanbudanseenu"; //Configuration["Jwt:Secret"];
            //var key = Encoding.ASCII.GetBytes(secret);

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //                            {
            //                                options.TokenValidationParameters = new TokenValidationParameters
            //                                {
            //                                    ValidateIssuer = false,
            //                                    ValidateAudience = false,
            //                                    ValidateLifetime = true,
            //                                    ValidateIssuerSigningKey = true,
            //                                    IssuerSigningKey = new SymmetricSecurityKey(key)
            //                                };
            //                            }
            //                    );

            return services;
        }
    }
}
