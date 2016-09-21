using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ColorPicker.Utilities;

namespace ColorPicker
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private IntPtr _hdc = IntPtr.Zero;
        private IntPtr _hwn = IntPtr.Zero;
        private KeyboardHook _hook;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _hook = new KeyboardHook { SetupKey = Key.F1 };
            if (_hook != null)
            {
                _hook.KeyCombinationPressed += new EventHandler(hook_KeyCombinationPressed);
            }
        }

        private void OnPicker(object sender, RoutedEventArgs e)
        {
        }

        void hook_KeyCombinationPressed(object sender, EventArgs e)
        {
            POINT p = new POINT();
            Win32Helper.GetCursorPos(out p);

            Console.WriteLine(p.X.ToString() + "---" + p.Y.ToString());
            _hdc = Win32Helper.GetDC(_hwn);
            uint color = Win32Helper.GetPixel(_hdc, p.X, p.Y);
            byte red = ColorHelper.GetRValue(color);
            byte green = ColorHelper.GetGValue(color);
            byte blue = ColorHelper.GetBValue(color);
            redTextBox.Text = red.ToString();
            greenTextBox.Text = green.ToString();
            blueTextBox.Text = blue.ToString();
            hexValueTextBox.Text = "#" + red.ToString("x").PadLeft(2, '0') + green.ToString("x").PadLeft(2, '0') + blue.ToString("x").PadLeft(2, '0');
            showColorBorder.Background = new SolidColorBrush(Color.FromRgb(red, green, blue));
            Win32Helper.ReleaseDC(_hwn, _hdc);
        }



    }
}
