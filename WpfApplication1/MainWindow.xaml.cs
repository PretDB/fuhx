using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Boolean isFullscreen = false;
        public struct State
        {
            public State(WindowState state, WindowStyle style, ResizeMode rszMode, Boolean istop, double left, double top, double width, double height)
            {
                this.winState = state;
                this.winStyle = style;
                this.resizeMode = rszMode;
                this.isTopmost = istop;
                this.left = left;
                this.top = top;
                this.width = width;
                this.height = height;
            }
            public WindowState winState;
            public WindowStyle winStyle;
            public ResizeMode resizeMode;
            public Boolean isTopmost;
            public double left;
            public double top;
            public double width;
            public double height;
        }
        private State state;
        public MainWindow()
        {
            InitializeComponent();
            this.state = new State(this.WindowState, this.WindowStyle, this.ResizeMode, this.Topmost, this.Left, this.Top, this.Width, this.Height);
            
#if RELEASE
            this.FullScreen();
#endif
        }

        private void FullScreen()
        {
            this.WindowState = System.Windows.WindowState.Normal;
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
            this.Topmost = true;
            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
        }
        private void ResumeWindowState()
        {
            this.WindowState = this.state.winState;
            this.WindowStyle = this.state.winStyle;
            this.ResizeMode = this.state.resizeMode;
            this.Topmost = this.state.isTopmost;
            this.Left = this.state.left;
            this.Top = this.state.top;
            this.Width = this.state.width;
            this.Height = this.state.height;
        }
        private void DumpWindowState()
        {
            this.state.winState = this.WindowState;
            this.state.winStyle = this.WindowStyle;
            this.state.isTopmost = this.Topmost;
            this.state.resizeMode = this.ResizeMode;
            this.state.left = this.Left;
            this.state.top = this.Top;
            this.state.width = this.Width;
            this.state.height = this.Height;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!this.isFullscreen)
            {
                this.DumpWindowState();
                this.FullScreen();
            }
            else
            {
                this.ResumeWindowState();
            }
            this.isFullscreen = !this.isFullscreen;
        }
    }
}
