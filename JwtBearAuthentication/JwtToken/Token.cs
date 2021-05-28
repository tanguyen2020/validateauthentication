using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtBearAuthentication.JwtToken
{
    public class Token
    {
        public string token_type { get; set; }
        public string expiration { get; set; }
        public string access_token { get; set; }
    }
}
