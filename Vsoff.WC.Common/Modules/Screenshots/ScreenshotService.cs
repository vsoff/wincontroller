﻿using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Vsoff.WC.Common.Modules.Screenshots
{
    public interface IScreenshotService
    {
        byte[] GetScreenshot();
    }

    public class ScreenshotService : IScreenshotService
    {
        public byte[] GetScreenshot()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(bmp))
                g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);

            using (var ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
    }
}
