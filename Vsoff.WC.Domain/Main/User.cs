﻿using System;
using System.Collections.Generic;
using System.Text;
using Vsoff.WC.Common.Enums;

namespace Vsoff.WC.Domain.Main
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }

        // TODO Удалить поле пароля, оно должно храниться только в UserDto в виде хеша.
        public string Password { get; set; }
        public int TelegramId { get; set; }
        public RoleTypes Role { get; set; }
        public bool IsBlocked { get; set; }
    }
}