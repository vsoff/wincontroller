using System;
using System.Collections.Generic;
using System.Text;
using Vsoff.WC.Common.Enums;

namespace Vsoff.WC.Server.Data.Entities
{
    public class UserDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public RoleTypes Role { get; set; }
    }
}
