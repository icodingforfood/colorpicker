using System;
using System.Runtime.InteropServices;

namespace ColorPicker.Utilities
{
    public class Win32Helper
    {
        /// <summary>
        /// 获取坐标点的像素的RGB值
        /// </summary>
        /// <param name="hDC"></param>
        /// <param name="nXPos"></param>
        /// <param name="nYPos"></param>
        /// <returns></returns>
        [DllImport("gdi32")]
        public static extern uint GetPixel(IntPtr hDC, int nXPos, int nYPos);

        /// <summary>
        /// 获取设备上下文环境
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        /// <summary>
        /// 释放设备上下文环境（DC）
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hDC"></param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("User32")]
        public extern static bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, ColorPicker.Utilities.KeyboardHook.LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
    }
}
