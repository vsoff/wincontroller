using System;
using System.Drawing;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Config;
using Vsoff.WC.Core.Common.Workers;

namespace Vsoff.WC.Common.Modules.System.Services
{
    public interface IUserMonitoringService
    {
        void StartMonitoring();
        void StopMonitoring();
        DateTime GetLastActivityTime();
    }

    public class UserMonitoringService : IUserMonitoringService
    {
        /// <summary>
        /// Периодичность с которой проверяется активность пользователя.
        /// </summary>
        private readonly TimeSpan _stateCheckPeriod = TimeSpan.FromSeconds(3);

        private readonly IAppConfigService _appConfigService;
        private readonly IMessenger _messenger;
        private readonly IWorker _worker;

        private DateTime _cursorLastPositionTime;
        private Point _cursorLastPosition;
        private bool _isUserActive;

        public UserMonitoringService(
            IAppConfigService appConfigService,
            IWorkerController workerController,
            IMessenger messenger)
        {
            _appConfigService = appConfigService ?? throw new ArgumentNullException(nameof(appConfigService));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));

            _worker = workerController?.CreateWorker(UpdateUserActivity) ?? throw new ArgumentNullException(nameof(workerController));
        }

        public DateTime GetLastActivityTime() => _cursorLastPositionTime;

        public void StartMonitoring()
        {
            _cursorLastPosition = WinApi.GetCursorPosition();
            _cursorLastPositionTime = DateTime.Now;
            _isUserActive = false;

            _worker.Start(_stateCheckPeriod);
        }

        public void StopMonitoring()
        {
            _worker?.Stop();
        }

        private void UpdateUserActivity()
        {
            Point currenCursorPosition = WinApi.GetCursorPosition();
            DateTime currentTime = DateTime.Now;

            // Проверяем, поменялись ли координаты курсора.
            if (currenCursorPosition != _cursorLastPosition)
            {
                if (!_isUserActive)
                {
                    _messenger.Send("Пользователь снова стал проявлять активность.");
                }

                _cursorLastPosition = currenCursorPosition;
                _cursorLastPositionTime = currentTime;
                _isUserActive = true;

                return;
            }

            // Проверяем время последней активности.
            TimeSpan sessionDuration = _appConfigService.GetConfig().UserActivitySessionDuration;
            if (currentTime - _cursorLastPositionTime <= sessionDuration)
                return;

            if (_isUserActive)
                _messenger.Send("Пользователь перестал проявлять активность.");

            _isUserActive = false;
        }
    }
}