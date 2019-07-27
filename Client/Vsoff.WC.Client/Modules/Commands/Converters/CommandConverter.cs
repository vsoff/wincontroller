using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Client.Modules.Commands.Types;

namespace Vsoff.WC.Client.Modules.Commands.Converters
{
    public class CommandConverter : ICommandConverter
    {
        private readonly string[] _shutdownAbortWords = new string[] { "a", "abort", "s", "stop" };

        public ICommand Convert(string text)
        {
            string cmd, argument;
            SplitText(text, out cmd, out argument);

            switch (cmd)
            {
                case "/au":
                case "/autorun": return ConvertAutorunCommand(argument.ToLower());

                #region Volume module
                case "/vd":
                case "/voldown": return new VolumeCommand(VolumeCommand.Command.VolumeDown);
                case "/vu":
                case "/volup": return new VolumeCommand(VolumeCommand.Command.VolumeUp);
                case "/vol":
                case "/volume": return ConvertVolumeCommand(argument);
                case "/mute": return new VolumeCommand(VolumeCommand.Command.Mute);
                #endregion

                case "/left": return new KeyboardCommand("{left}");
                case "/right": return new KeyboardCommand("{right}");
                case "/enter": return new KeyboardCommand("\n");
                case "/space": return new KeyboardCommand(" ");
                case "/key": return new KeyboardCommand(argument);

                case "/lock": return new LockCommand();
                case "/screen": return new TakeScreenshotCommand();
                case "/status": return new StatusCommand();
                case "/shutdown": return ConvertShutdownCommand(argument);
            }

            return new UndefinedCommand("Неизвестная команда");
        }

        private ICommand ConvertAutorunCommand(string argument)
        {
            if (argument == "on")
                return new AutorunCommand(AutorunCommand.Command.AddAutorun);

            if (argument == "off")
                return new AutorunCommand(AutorunCommand.Command.RemoveAutorun);

            return new UndefinedCommand("Неправильно указаны аргументы");
        }

        private ICommand ConvertVolumeCommand(string argument)
        {
            if (argument == "+")
                return new VolumeCommand(VolumeCommand.Command.VolumeUp);

            if (argument == "-")
                return new VolumeCommand(VolumeCommand.Command.VolumeDown);

            return new UndefinedCommand("Неправильно указаны аргументы");
        }

        private ICommand ConvertShutdownCommand(string argument)
        {
            if (_shutdownAbortWords.Contains(argument))
                return new ShutdownCommand
                {
                    Delay = TimeSpan.Zero,
                    IsAbort = true
                };

            TimeSpan delay;
            if (TimeSpan.TryParse(argument, out delay))
            {
                return new ShutdownCommand
                {
                    Delay = delay,
                    IsAbort = false
                };
            }

            return new UndefinedCommand("Неправильно указаны аргументы");
        }

        private void SplitText(string text, out string cmd, out string argument)
        {
            int spaceIndex = text.IndexOf(" ");

            if (spaceIndex != -1)
            {
                cmd = text.Substring(0, spaceIndex);
                argument = text.Length >= spaceIndex + 1
                    ? text.Substring(spaceIndex + 1)
                    : string.Empty;
            }
            else
            {
                cmd = text;
                argument = string.Empty;
            }
        }
    }
}
