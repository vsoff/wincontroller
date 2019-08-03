using Telegram.Bot.Types.Enums;
using Vsoff.WC.Server.Modules.Menu;

namespace Vsoff.WC.Server.Modules.Messengers
{
    public class NotifyMessage
    {
        public DataType Type { get; set; }
        public string Text { get; set; }
        public MenuType MenuType { get; set; }
        public byte[] Photo { get; set; }

        public NotifyMessage(string text, byte[] photo = null)
        {
            Text = text;
            Photo = photo;
            Type = photo == null ? DataType.Text : DataType.Photo;
        }

        public NotifyMessage(string text, MenuType menuType)
        {
            Text = text;
            Type = DataType.Menu;
            MenuType = menuType;
        }

        public enum DataType
        {
            Text,
            Photo,
            Menu
        }
    }
}