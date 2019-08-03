using System;
using System.Linq.Expressions;

namespace Vsoff.WC.Core.Modules.Configs
{
    public interface IConfigService<TConfig> where TConfig : ConfigBase, new()
    {
        void SetSettingFieldValue<T>(Expression<Func<TConfig, T>> field, T value);
        TConfig GetConfig();
    }
}