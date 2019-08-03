using System;
using System.Linq;
using Vsoff.WC.Client.Modules.Commands.Types;
using Vsoff.WC.Core.Modules.Commands.Types;
using Vsoff.WC.Server.Modules.Commands.Types;
using Vsoff.WC.Server.Modules.Menu;

namespace Vsoff.WC.Server.Modules.Commands.Converters
{
    public interface ICommandConverter
    {
        Command Convert(string text);
    }

    public class CommandConverter : ICommandConverter
    {
        private readonly string[] _shutdownAbortWords = {"a", "abort", "s", "stop"};

        public Command Convert(string text)
        {
            SplitText(text, out var cmd, out var argument);

            switch (cmd)
            {
                case "/start": return new MenuCommand("Меню", MenuType.Main);
                case "/status": return new StatusCommand();

                //case "/au":
                //case "/autorun": return ConvertAutorunCommand(argument.ToLower());

                //#region Volume module

                //case "/vd":
                //case "/voldown": return new VolumeCommand(VolumeCommand.Command.VolumeDown);
                //case "/vu":
                //case "/volup": return new VolumeCommand(VolumeCommand.Command.VolumeUp);
                //case "/vol":
                //case "/volume": return ConvertVolumeCommand(argument);
                //case "/mute": return new VolumeCommand(VolumeCommand.Command.Mute);

                //#endregion

                //case "/left": return new KeyboardCommand("{left}");
                //case "/right": return new KeyboardCommand("{right}");
                //case "/enter": return new KeyboardCommand("\n");
                //case "/space": return new KeyboardCommand(" ");
                //case "/key": return new KeyboardCommand(argument);

                //case "/lock": return new LockCommand();
                //case "/screen": return new TakeScreenshotCommand();
                //case "/status": return new StatusCommand();
                //case "/shutdown": return ConvertShutdownCommand(argument);
            }

            return new UndefinedCommand("Неизвестная команда");
        }

        private Command ConvertAutorunCommand(string argument)
        {
            if (argument == "on")
                return new AutorunCommand(AutorunCommand.Command.AddAutorun);

            if (argument == "off")
                return new AutorunCommand(AutorunCommand.Command.RemoveAutorun);

            return new UndefinedCommand("Неправильно указаны аргументы");
        }

        private Command ConvertVolumeCommand(string argument)
        {
            if (argument == "+")
                return new VolumeCommand(VolumeCommand.Command.VolumeUp);

            if (argument == "-")
                return new VolumeCommand(VolumeCommand.Command.VolumeDown);

            return new UndefinedCommand("Неправильно указаны аргументы");
        }

        private Command ConvertShutdownCommand(string argument)
        {
            if (_shutdownAbortWords.Contains(argument))
                return new ShutdownCommand
                {
                    Delay = TimeSpan.Zero,
                    IsAbort = true
                };

            if (TimeSpan.TryParse(argument, out var delay))
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