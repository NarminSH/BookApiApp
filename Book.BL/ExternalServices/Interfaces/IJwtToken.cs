using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.BL.ExternalServices.Interfaces
{
    public interface IJwtToken
    {
        string GenerateToken(string username);
    }
}
