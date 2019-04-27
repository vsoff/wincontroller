﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Vsoff.WC.Common.Modules.System
{
    public interface IVolumeController
    {
        void VolumeDown();
        void VolumeUp();
        void Mute();
    }

    public class VolumeController : IVolumeController
    {
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;

        private IntPtr CurrentWindowHandle => Process.GetCurrentProcess().MainWindowHandle;

        public void VolumeDown() => WinApi.SendMessageW(CurrentWindowHandle, WM_APPCOMMAND, CurrentWindowHandle, (IntPtr)APPCOMMAND_VOLUME_DOWN);
        public void VolumeUp() => WinApi.SendMessageW(CurrentWindowHandle, WM_APPCOMMAND, CurrentWindowHandle, (IntPtr)APPCOMMAND_VOLUME_UP);
        public void Mute() => WinApi.SendMessageW(CurrentWindowHandle, WM_APPCOMMAND, CurrentWindowHandle, (IntPtr)APPCOMMAND_VOLUME_MUTE);
    }
}
