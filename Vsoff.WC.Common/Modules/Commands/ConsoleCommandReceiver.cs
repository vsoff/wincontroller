using System;
using System.Threading.Tasks;
using Vsoff.WC.Common.Modules.Commands.Converters;
using Vsoff.WC.Common.Modules.Commands.Types;

namespace Vsoff.WC.Common.Modules.Commands
{
    public class ConsoleCommandReceiver : ICommandReceiver
    {
        private Task _consoleReadTask;

        private readonly ICommandConverter _commandConverter;
        private readonly ICommandService _commandService;

        public ConsoleCommandReceiver(
            ICommandConverter commandConverter,
            ICommandService commandService)
        {
            _commandConverter = commandConverter ?? throw new ArgumentNullException(nameof(commandConverter));
            _commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            _consoleReadTask = new Task(() =>
            {
                while (true) HandleMessage();
            });
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

            if (string.IsNullOrWhiteSpace(text))
                return;

            ICommand command = _commandConverter.Convert(text);
            _commandService.InvokeCommand(command);
        }
    }
}