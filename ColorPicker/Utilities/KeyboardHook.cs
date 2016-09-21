using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace ColorPicker.Utilities
{
    public class KeyboardHook : IDisposable
    {
        private const int WH_KEYBOARD_LL = 13; //WH_KEYBOARD_LL和WH_KEYBOARD是很大的区别的
        private const int WM_KEYDOWN = 0x0100;
        private LowLevelKeyboardProc _keyboardProc;
        private IntPtr _hookId = IntPtr.Zero;
        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_SHOWWINDOW = 0x0040;

        public Key SetupKey { get; set; }
        public event EventHandler KeyCombinationPressed;

        public KeyboardHook()
        {
            _keyboardProc = HookCallBack;
            _hookId = SetHook(_keyboardProc);
        }

        private IntPtr HookCallBack(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                var keyPressed = KeyInterop.KeyFromVirtualKey(vkCode);
                if (keyPressed == SetupKey && Keyboard.Modifiers == ModifierKeys.Control)
                {
                    if (KeyCombinationPressed != null)
                    {
                        KeyCombinationPressed(null, new EventArgs());
                    }
                }
            }

            return Win32Helper.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return Win32Helper.SetWindowsHookEx(WH_KEYBOARD_LL, proc, Win32Helper.GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        public void Dispose()
        {
            Win32Helper.UnhookWindowsHookEx(_hookId);
        }
    }
}
