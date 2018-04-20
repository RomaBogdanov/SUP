using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SupRealClient
{
    public class InputProvider : IDisposable
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        const int WH_KEYBOARD = 2;
        const int WH_MOUSE = 7;
        const int WH_KEYBOARD_LL = 13;
        const int WH_MOUSE_LL = 14;

        const int WM_KEYDOWN = 0x0100;
        const int WM_MOUSEMOVE = 0x0200;
        const int WM_LBUTTONDOWN = 0x0201;

        //private LowLevelKeyboardProc _keyboardHnd = KeyboardCallback;
        //private LowLevelKeyboardProc _mouseHand = MouseCallback;

        private IntPtr _keyboardHookId = IntPtr.Zero;
        private IntPtr _mouseHookId = IntPtr.Zero;

        private static InputProvider inputProvider;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public delegate void OnInput();

        private OnInput onInput;

        public static InputProvider GetInputProvider()
        {
            if (inputProvider == null)
            {
                inputProvider = new InputProvider();
            }
            return inputProvider;
        }

        public void Init(OnInput onInput)
        {
            this.onInput += onInput;
            _keyboardHookId = SetHook(KeyboardCallback, WH_KEYBOARD_LL);
            _mouseHookId = SetHook(MouseCallback, WH_MOUSE_LL);
        }

        public void Close(OnInput onInput)
        {
            this.onInput -= onInput;
        }

        public void Dispose()
        {
            if (_keyboardHookId != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_keyboardHookId);
            }
            if (_mouseHookId != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_mouseHookId);
            }
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc, int id)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(id, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr KeyboardCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            //{
            //    int vkCode = Marshal.ReadInt32(lParam);
            //    Console.WriteLine((System.Windows.Forms.Keys)vkCode);
            //}
            if (nCode >= 0 && this.onInput != null)
            {
                this.onInput();
            }

            return CallNextHookEx(_keyboardHookId, nCode, wParam, lParam);
        }

        private IntPtr MouseCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            /*if (nCode >= 0 && wParam == (IntPtr)WM_MOUSEMOVE)
            {
                Console.WriteLine("Mv");
            }
            else if (nCode >= 0 && wParam == (IntPtr)WM_LBUTTONDOWN)
            {
                Console.WriteLine("Lb");
            }
            else
            {
                Console.WriteLine("Moo");
            }*/
            if (nCode >= 0 && this.onInput != null)
            {
                this.onInput();
            }

            return CallNextHookEx(_mouseHookId, nCode, wParam, lParam);
        }
    }
}
