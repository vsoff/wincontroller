using System;
using System.Threading.Tasks;
using Vsoff.WC.Client.Modules.Commands.Types;
using Vsoff.WC.Core.Modules.Commands;
using Vsoff.WC.Core.Modules.Commands.Types;
using Vsoff.WC.Core.Modules.Configs;
using Vsoff.WC.Domain.Main;
using Vsoff.WC.Server.Modules.Commands.Converters;
using Vsoff.WC.Server.Modules.Configs;
using Vsoff.WC.Server.Services;

namespace Vsoff.WC.Server.Modules.Commands
{
    public class ConsoleCommandReceiver : ICommandReceiver
    {
        private readonly Task _consoleReadTask;

        private readonly IConfigService<ServerConfig> _appConfigService;
        private readonly ICommandConverter _commandConverter;
        private readonly ICommandInvoker _commandInvoker;
        private readonly IUserService _userService;

        public ConsoleCommandReceiver(
            IConfigService<ServerConfig> appConfigService,
            ICommandConverter commandConverter,
            ICommandInvoker commandInvoker,
            IUserService userService)
        {
            _appConfigService = appConfigService ?? throw new ArgumentNullException(nameof(appConfigService));
            _commandConverter = commandConverter ?? throw new ArgumentNullException(nameof(commandConverter));
            _commandInvoker = commandInvoker ?? throw new ArgumentNullException(nameof(commandInvoker));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));

            _consoleReadTask = new Task(() =>
            {
                while (true)
                {
                    HandleMessage();
                }
            });

            Start();
        }

        public void Start()
        {
            _consoleReadTask.Start();
        }

        public void Stop()
        {
        }

        private void HandleMessage()
        {
            string text = Console.ReadLine();

            int adminId = _appConfigService.GetConfig().AdminTelegramId;
            User user = _userService.GetUserByTelegramId(adminId);

            Command command = _commandConverter.Convert(text);

            _commandInvoker.InvokeCommand(new CommandInfo(user, command));
        }
    }
}