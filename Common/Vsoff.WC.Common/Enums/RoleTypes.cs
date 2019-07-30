using System;

namespace Vsoff.WC.Common.Enums
{
    [Flags]
    public enum RoleTypes : byte
    {
        None = 0,
        Admin = 1,
        User = 2,
        Machine = 3
    }
}
