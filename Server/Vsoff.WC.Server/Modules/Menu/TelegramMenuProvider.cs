using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace Vsoff.WC.Server.Modules.Menu
{
    public interface ITelegramMenuProvider
    {
        IReplyMarkup BuildMenuMarkup(MenuType menuType);
    }

    public class TelegramMenuProvider : ITelegramMenuProvider
    {
        public IReplyMarkup BuildMenuMarkup(MenuType menuType)
        {
            Builder builder = new Builder();

            switch (menuType)
            {
                case MenuType.Main:
                    return builder
                        .AddButtons("/start")
                        .AddButtons("/menu", "/status")
                        .AddButtons("<< Назад")
                        .Build();
                default: throw new Exception($"Unknown MenuType {menuType}");
            }
        }

        private class Builder
        {
            private readonly List<List<KeyboardButton>> _markup;

            public Builder()
            {
                _markup = new List<List<KeyboardButton>>();
            }

            public Builder AddButtons(params string[] buttons)
            {
                var line = new List<KeyboardButton>();
                foreach (var button in buttons)
                    line.Add(new KeyboardButton(button));

                _markup.Add(line);
                return this;
            }

            public ReplyKeyboardMarkup Build() => new ReplyKeyboardMarkup(_markup.Select(x => x.ToArray()).ToArray());
        }
    }
}