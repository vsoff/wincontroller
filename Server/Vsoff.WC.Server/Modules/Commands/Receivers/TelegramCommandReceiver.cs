using System;
using System.Net;
using Telegram.Bot;
using Vsoff.WC.Client.Modules.Commands.Types;
using Vsoff.WC.Common.Enums;
using Vsoff.WC.Core.Modules.Commands;
using Vsoff.WC.Core.Modules.Commands.Types;
using Vsoff.WC.Core.Modules.Configs;
using Vsoff.WC.Domain.Main;
using Vsoff.WC.Server.Modules.Commands.Converters;
using Vsoff.WC.Server.Modules.Configs;
using Vsoff.WC.Server.Modules.Messengers;
using Vsoff.WC.Server.Services;

namespace Vsoff.WC.Server.Modules.Commands
{
    public class TelegramCommandReceiver : ICommandReceiver
    {
        private readonly TimeSpan _oldMessagesDelay = TimeSpan.FromSeconds(60);

        private readonly ICommandConverter _commandConverter;
        private readonly ICommandInvoker _commandInvoker;
        private readonly IUserService _userService;

        private readonly TelegramBotClient _client;

        public TelegramCommandReceiver(
            IConfigService<ServerConfig> appConfigService,
            ICommandConverter commandConverter,
            ICommandInvoker commandInvoker,
            IUserService userService)
        {
            _commandConverter = commandConverter ?? throw new ArgumentNullException(nameof(commandConverter));
            _commandInvoker = commandInvoker ?? throw new ArgumentNullException(nameof(commandInvoker));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));

            ServerConfig config = appConfigService.GetConfig();
            WebProxy proxy = new WebProxy(config.ProxyIp);
            _client = new TelegramBotClient(config.TelegramToken, proxy);
            _client.OnMessage += OnMessageReceived;
            _client.OnCallbackQuery += OnCallbackQuery;
            //var a = _client.GetMeAsync().Result;
            // Запускаем получение новых сообщений.
            Start();
        }

        private void OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
        }

        private void OnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            // Принимаем только личные сообщения.
            if (e.Message.From.Id != e.Message.Chat.Id)
                return;

            // Отбрасываем старые сообщения.
            if (DateTime.UtcNow - e.Message.Date > _oldMessagesDelay)
                return;

            // Принимаем сообщения только от админа.
            User user = _userService.GetUserByTelegramId(e.Message.From.Id);
            if (user == null || user.Role != RoleTypes.Admin)
                return;

            // Получаем команду.
            Command command = _commandConverter.Convert(e.Message.Text);

            _commandInvoker.InvokeCommand(new CommandInfo(user, command));
        }

        public void Start() => _client.StartReceiving();

        public void Stop() => _client.StopReceiving();
    }
}