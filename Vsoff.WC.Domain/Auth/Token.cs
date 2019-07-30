using System;
using System.Collections.Generic;
using System.Text;

namespace Vsoff.WC.Domain.Auth
{
    public class Token
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }
        public DateTime Expires { get; set; }
    }
}
