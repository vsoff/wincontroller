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
using Vsoff.WC.Common.Modules.Config;

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

        private readonly IAppConfigService _appConfigService;
        private readonly ICommandConverter _commandConverter;
        private readonly ICommandService _commandService;
        private readonly IMessenger _messenger;

        private readonly TelegramBotClient _client;

        public CommandReceiver(
            IAppConfigService appConfigService,
            ICommandConverter commandConverter,
            ICommandService commandService,
            IMessenger messenger)
        {
            _appConfigService = appConfigService ?? throw new ArgumentNullException(nameof(appConfigService));
            _commandConverter = commandConverter ?? throw new ArgumentNullException(nameof(commandConverter));
            _commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));

            var config = _appConfigService.GetConfig();
            _client = new TelegramBotClient(config.TelegramToken);
            _client.OnMessage += OnMessageReceived;
        }

        public void Start() => _client.StartReceiving();

        public void Stop() => _client.StopReceiving();

        private void OnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Chat.Id != _appConfigService.GetConfig().AdminId)
                return;

            if (DateTime.UtcNow - e.Message.Date > _oldMessagesDelay)
                return;

            ICommand command = _commandConverter.Convert(e.Message.Text);
            _commandService.InvokeCommand(command);
        }
    }
}
