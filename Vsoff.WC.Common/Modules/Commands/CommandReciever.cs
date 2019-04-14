using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Vsoff.WC.Common.Messengers;

namespace Vsoff.WC.Common.Modules.Commands
{
    public interface IReceiver
    {
        void Start();
        void Stop();
    }

    public class CommandReceiver : IReceiver
    {
        private readonly TimeSpan _oldMessagesDelay = TimeSpan.FromSeconds(40);

        private readonly ICommandService _commandService;
        private readonly IMessenger _messenger;

        private readonly TelegramBotClient _client;

        public CommandReceiver(
            ICommandService commandService,
            IMessenger messenger)
        {
            _commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));

            IWebProxy proxy = new WebProxy(TempConfig.ProxyIp);
            _client = new TelegramBotClient(TempConfig.TelegramToken, proxy);
            _client.OnMessage += OnMessageReceived;
        }

        public void Start() => _client.StartReceiving();

        public void Stop() => _client.StopReceiving();

        private void OnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Chat.Id != TempConfig.AdminId)
                return;

            if (DateTime.UtcNow - e.Message.Date > TimeSpan.FromSeconds(60))
                return;

            string text = e.Message.Text;
            int spaceIndex = text.IndexOf(" ");

            string cmd, argument;
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

            InvokeCommand(cmd, argument);
        }

        private void InvokeCommand(string cmd, string argument)
        {
            CommandType cmdType = CommandType.Unknown;

            if (cmd.StartsWith("/screen"))
                cmdType = CommandType.Screenshot;
            else if (cmd.StartsWith("/status"))
                cmdType = CommandType.Status;
            else if (cmd.StartsWith("/shutdownabort"))
                cmdType = CommandType.ShutdownAbort;
            else if (cmd.StartsWith("/shutdown"))
                cmdType = CommandType.Shutdown;

            _commandService.InvokeCommand(cmdType, argument);
        }
    }
}
