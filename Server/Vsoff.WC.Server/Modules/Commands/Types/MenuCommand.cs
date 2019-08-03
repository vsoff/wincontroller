using System;
using System.Collections.Generic;
using System.Text;
using Vsoff.WC.Client.Modules.Commands.Types;
using Vsoff.WC.Core.Modules.Commands.Types;
using Vsoff.WC.Server.Modules.Menu;

namespace Vsoff.WC.Server.Modules.Commands.Types
{
    public class MenuCommand : Command
    {
        public string Text { get; set; }
        public MenuType Menu { get; set; }

        public MenuCommand(string text, MenuType menu)
        {
            Text = text;
            Menu = menu;
        }
    }
}