using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using Vsoff.WC.Common.Modules.System.Services;

namespace Vsoff.WC.Common.Modules.Config
{
    public interface IAppConfigService
    {
        void SetSettingFieldValue<T>(Expression<Func<AppConfig, T>> field, T value);
        AppConfig GetConfig();
    }

    public class AppConfigService : IAppConfigService
    {
        private const string _configName = "config.json";
        private readonly string _configFullPath;

        private readonly object _saveLocker = new object();

        private AppConfig _config;

        public AppConfigService(ISystemService systemService)
        {
            _configFullPath = Path.Combine(systemService.ApplicationFolder, _configName);
            OpenOrCreateConfig();
        }

        public AppConfig GetConfig() => _config.GetClone();

        public void SetSettingFieldValue<T>(Expression<Func<AppConfig, T>> field, T value)
        {
            // Компилируем action.
            var newValue = Expression.Parameter(field.Body.Type);
            var assign = Expression.Lambda<Action<AppConfig, T>>(
                Expression.Assign(field.Body, newValue),
                field.Parameters[0], newValue);

            // Изменяем параметр.
            var setter = assign.Compile();
            setter(_config, value);

            // Сохраняем изменения в файл конфига.
            SaveConfigInFile();
        }

        private void SaveConfigInFile()
        {
            string newConfigContent = JsonConvert.SerializeObject(_config);
            lock (_saveLocker)
            {
                File.WriteAllText(_configFullPath, newConfigContent, Encoding.Unicode);
            }
        }

        private void OpenOrCreateConfig()
        {
            if (!File.Exists(_configFullPath))
            {
                _config = new AppConfig();
                SaveConfigInFile();
                return;
            }

            string configContent = File.ReadAllText(_configFullPath);
            _config = JsonConvert.DeserializeObject<AppConfig>(configContent);
        }
    }
}