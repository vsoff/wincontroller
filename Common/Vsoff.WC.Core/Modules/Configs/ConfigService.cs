using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Vsoff.WC.Core.Modules.Configs
{
    public abstract class ConfigService<TConfig> : IConfigService<TConfig> where TConfig : ConfigBase, new()
    {
        private readonly string _configFullPath;
        private readonly object _saveLocker;
        private TConfig _config;

        protected abstract string ConfigName { get; }

        protected ConfigService()
        {
            if (string.IsNullOrWhiteSpace(ConfigName))
                throw new Exception("Название файла конфигурации не должно быть пустым");

            _saveLocker = new object();
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _configFullPath = Path.Combine(path ?? string.Empty, ConfigName);
            OpenOrCreateConfig();
        }

        public TConfig GetConfig() => (TConfig) _config.Clone();

        public void SetSettingFieldValue<T>(Expression<Func<TConfig, T>> field, T value)
        {
            // Компилируем action.
            var newValue = Expression.Parameter(field.Body.Type);
            var assign = Expression.Lambda<Action<TConfig, T>>(
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
                _config = new TConfig();
                SaveConfigInFile();
                return;
            }

            string configContent = File.ReadAllText(_configFullPath);
            _config = JsonConvert.DeserializeObject<TConfig>(configContent);
        }
    }
}