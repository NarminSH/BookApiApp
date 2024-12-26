using Book.BL.ExternalServices.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.BL.ExternalServices.Implementations
{
    public class JwtTokenService : IJwtToken
    {
        IConfiguration _config;

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(string username)
        {
            throw new NotImplementedException();
        }
    }
}
