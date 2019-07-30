using System;
using System.Collections.Generic;
using System.Text;
using Vsoff.WC.Common.Enums;

namespace Vsoff.WC.Domain.Main
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public RoleTypes Role { get; set; }
    }
}
