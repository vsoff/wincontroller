using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Commands.Converters;
using Vsoff.WC.Common.Modules.Commands.Types;

namespace Vsoff.WC.Common.Modules.Commands
{
    /// <summary>
    /// Ретранслирует новые команды в приложение.
    /// </summary>
    public interface ICommandReceiver
    {
        void Start();
        void Stop();
    }

    public class CommandReceiver : ICommandReceiver
    {
        private readonly TimeSpan _oldMessagesDelay = TimeSpan.FromSeconds(60);

        private readonly ICommandConverter _commandConverter;
        private readonly ICommandService _commandService;
        private readonly IMessenger _messenger;

        private readonly TelegramBotClient _client;

        public CommandReceiver(
            ICommandConverter commandConverter,
            ICommandService commandService,
            IMessenger messenger)
        {
            _commandConverter = commandConverter ?? throw new ArgumentNullException(nameof(commandConverter));
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

            if (DateTime.UtcNow - e.Message.Date > _oldMessagesDelay)
                return;

            ICommand command = _commandConverter.Convert(e.Message.Text);
            _commandService.InvokeCommand(command);
        }
    }
}
