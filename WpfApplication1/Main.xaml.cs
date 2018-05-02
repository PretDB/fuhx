using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace 孵化
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
#if DEBUG
#else
            MainWindow.FullScreen(this);
#endif
        }

        public static void FullScreen(object sender)
        {
            (sender as Window).WindowState = System.Windows.WindowState.Normal;
            (sender as Window).WindowStyle = System.Windows.WindowStyle.None;
            (sender as Window).ResizeMode = System.Windows.ResizeMode.NoResize;
            (sender as Window).Topmost = true;
            (sender as Window).Left = 0.0;
            (sender as Window).Top = 0.0;
			(sender as Window).Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            (sender as Window).Height = System.Windows.SystemParameters.PrimaryScreenHeight;
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

		private void Model0EnterButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void Model1EnterButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void Model2EnterButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void Model3EnterButton_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
