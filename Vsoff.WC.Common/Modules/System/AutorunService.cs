using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vsoff.WC.Common.Modules.System
{
    public interface IAutorunService
    {
        void Register();
        void Unregister();
    }

    public class AutorunRegistryService : IAutorunService
    {
        private const string _registryKeyName = @"WinController";
        private const string _registryPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        private readonly RegistryKey _registryKey;

        public AutorunRegistryService()
        {
            _registryKey = Registry.LocalMachine.OpenSubKey(_registryPath, true);
        }

        public void Register()
        {
            _registryKey.SetValue(_registryKeyName, Application.ExecutablePath);
        }

        public void Unregister()
        {
            if (IsStartupItemExists())
                _registryKey.DeleteValue(_registryKeyName, false);
        }

        private bool IsStartupItemExists() => _registryKey.GetValue(_registryKeyName) != null;
    }

    public class AutorunScheduleService : IAutorunService
    {
        private const string _appName = @"WinController";

        private readonly ISystemService _systemService;

        public AutorunScheduleService(ISystemService systemService)
        {
            _systemService = systemService;
        }

        public void Register() => Invoke("schtasks", $"/create /sc onlogon /tn {_appName} /rl highest /tr \"{Application.ExecutablePath}\"");

        public void Unregister() => Invoke("schtasks", $"/delete /tn {_appName}");

        private void Invoke(string cmd, string arguments)
        {
            Process process = _systemService.ExecuteCmd(cmd, arguments);
            process.WaitForExit();
            if (process.ExitCode != 0)
                throw new Exception($"Во время добавления/удаления приложения в автозапуск произошла ошибка с кодом {process.ExitCode}");
        }
    }
}
