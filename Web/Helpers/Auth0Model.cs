using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nXs.Web.Helpers
{
    public class Auth0Model
    {
        public string id_token { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
    }
}