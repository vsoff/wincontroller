using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using Vsoff.WC.Common.Enums;
using Vsoff.WC.Common.Workers;
using Vsoff.WC.Core.Modules.Commands.Types;
using Vsoff.WC.Core.Modules.Configs;
using Vsoff.WC.Domain.Main;
using Vsoff.WC.Server.Modules.Commands;
using Vsoff.WC.Server.Modules.Commands.Converters;
using Vsoff.WC.Server.Modules.Configs;
using Vsoff.WC.Server.Modules.Menu;
using Vsoff.WC.Server.Services;

namespace Vsoff.WC.Server.Modules.Messengers
{
    public interface IMessenger
    {
        void Send(int userId, string text, byte[] photo = null);
        void Send(int userId, MenuType menuType);

        event EventHandler<CommandEventArgs> OnCommand;
    }

    public class TelegramMessenger : IMessenger
    {
        /// <summary>
        /// Максимальное количество отправляемых соощений за итерацию.
        /// </summary>
        private const int MaxMessagesHandlePerIteration = 5;

        /// <summary>
        /// Время жизни (актуальности) сообщения.
        /// </summary>
        private readonly TimeSpan _messagesTtl = TimeSpan.FromSeconds(60);

        /// <summary>
        /// Минимальный интервал между обработками порций сообщений.
        /// </summary>
        private readonly TimeSpan _messagesHandleInterval = TimeSpan.FromSeconds(1);

        /// <summary>
        /// Очередь сообщений.
        /// </summary>
        private readonly Queue<NotifyMessage> _messagesQueue;

        /// <summary>
        /// Telegram клиент.
        /// </summary>
        private readonly TelegramBotClient _client;

        /// <summary>
        /// Воркер отправки сообщений.
        /// </summary>
        private readonly IWorker _worker;

        private readonly IConfigService<ServerConfig> _appConfigService;
        private readonly ITelegramMenuProvider _telegramMenuProvider;
        private readonly IAccountContextService _accountContextService;
        private readonly ICommandConverter _commandConverter;
        private readonly IAccountService _accountService;

        public event EventHandler<CommandEventArgs> OnCommand;

        public TelegramMessenger(
            IConfigService<ServerConfig> appConfigService,
            ITelegramMenuProvider telegramMenuProvider,
            IAccountContextService accountContextService,
            ICommandConverter commandConverter,
            IWorkerController workerController,
            IAccountService accountService)
        {
            if (workerController == null) throw new ArgumentNullException(nameof(workerController));
            _appConfigService = appConfigService ?? throw new ArgumentNullException(nameof(appConfigService));
            _telegramMenuProvider =
                telegramMenuProvider ?? throw new ArgumentNullException(nameof(telegramMenuProvider));
            _accountContextService =
                accountContextService ?? throw new ArgumentNullException(nameof(accountContextService));
            _commandConverter = commandConverter ?? throw new ArgumentNullException(nameof(commandConverter));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));

            _worker = workerController.StartWorker(HandleMessages, _messagesHandleInterval, false);
            _messagesQueue = new Queue<NotifyMessage>();

            // Настраиваем telegram клиент.
            var config = _appConfigService.GetConfig();
            WebProxy proxy = new WebProxy(config.ProxyIp);
            _client = new TelegramBotClient(config.TelegramToken, proxy);
            _client.OnMessage += OnMessageReceived;
            _client.StartReceiving();
        }

        public void Send(int userId, string text, byte[] photo = null)
            => _messagesQueue.Enqueue(new NotifyMessage(userId, text, photo));

        public void Send(int userId, MenuType menuType)
            => _messagesQueue.Enqueue(new NotifyMessage(userId, menuType));

        private void HandleMessages()
        {
            int i = 0;
            while (_messagesQueue.Count != 0 || i++ >= MaxMessagesHandlePerIteration)
            {
                var message = _messagesQueue.Dequeue();
                try
                {
                    Notify(message);
                }
                catch (Exception ex)
                {
                    _messagesQueue.Enqueue(message);
                }
            }
        }

        private void Notify(NotifyMessage msg)
        {
            var adminId = _appConfigService.GetConfig().AdminTelegramId;
            switch (msg.Type)
            {
                case NotifyMessage.DataType.Text:
                    _client.SendTextMessageAsync(adminId, msg.Text).Wait();
                    break;
                case NotifyMessage.DataType.Photo:
                    _client.SendDocumentAsync(adminId,
                        new InputOnlineFile(new MemoryStream(msg.Photo), "screenshot.jpeg"));
                    break;
                case NotifyMessage.DataType.Menu:
                    var markup = _telegramMenuProvider.BuildMenuMarkup(msg.MenuType);
                    // todo Получение текста меню.
                    _client.SendTextMessageAsync(adminId, "DEFAULT MENU TEXT", replyMarkup: markup).Wait();
                    break;
            }
        }

        private void OnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            // Принимаем только личные сообщения.
            if (e.Message.From.Id != e.Message.Chat.Id)
                return;

            // Отбрасываем старые сообщения.
            if (DateTime.UtcNow - e.Message.Date > _messagesTtl)
                return;

            // Принимаем сообщения только от админа.
            Account account = _accountService.GetUserAccountByTelegramId(e.Message.From.Id);
            if (account == null || account.Role != RoleType.Admin)
                return;

            // Получаем команду.
            Command command = _commandConverter.Convert(e.Message.Text);

            // Получаем текущий контекст пользователя.
            AccountContext accountContext = _accountContextService.GetAccountContext(account.Id);

            var userCommand = new UserCommand(account.Id, accountContext.MachineId, command);
            OnCommand?.Invoke(this, new CommandEventArgs(userCommand));
        }
    }
}