using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vsoff.WC.Common.Modules.System
{
    public interface IAutorunService
    {
        void Register();
        bool IsRegisterExists();
        void Unregister();
    }

    public class AutorunScheduleService : IAutorunService
    {
        private const string _appName = @"WinController";

        private readonly ISystemService _systemService;

        public AutorunScheduleService(ISystemService systemService)
        {
            _systemService = systemService;
        }

        public bool IsRegisterExists()
        {
            using (TaskService ts = new TaskService())
            {
                var collection = ts.RootFolder.GetTasks(new Regex(_appName));
                return collection.Count != 0;
            }
        }

        public void Register()
        {
            using (TaskService ts = new TaskService())
            {
                // Создаём и настраиваем таску.
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Windows controller application";
                td.Triggers.Add(new LogonTrigger());
                td.Actions.Add(new ExecAction(Application.ExecutablePath));
                td.Settings.Enabled = true;
                td.Settings.StartWhenAvailable = true;
                td.Settings.StopIfGoingOnBatteries = false;
                td.Principal.RunLevel = TaskRunLevel.Highest;

                // Добавляем таску в шедулер.
                ts.RootFolder.RegisterTaskDefinition(_appName, td);
            }
        }

        public void Unregister()
        {
            using (TaskService ts = new TaskService())
            {
                ts.RootFolder.DeleteTask(_appName);
            }
        }
    }
}
