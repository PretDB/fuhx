using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Timers;
using System.Threading;

namespace 孵化
{
    public enum MainPic
    {
        init,
        first,
        second
    }
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
    {
        private bool isFullscreen = false;
        private bool[] isModelIn = new bool[5];
        private MainPic currentMainPic = MainPic.init;


        private System.Timers.Timer timer = new System.Timers.Timer(60000);

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
            this.timer.Elapsed += this.Timer_Elapsed;
            this.timer.AutoReset = true;

#if !DEBUG
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
            if(this.currentMainPic == MainPic.first || this.isModelIn[0] == false)
            {
                this.isModelIn[0] = true;
                this.Model0EnterButton.IsEnabled = false;

                BitmapImage i = new BitmapImage();
                i.BeginInit();
                i.UriSource = new Uri("res/content0/01.png", UriKind.RelativeOrAbsolute);
                i.EndInit();
                this.content0.Source = i;
                this.content0.Visibility = Visibility.Visible;
            }
		}

		private void Model1EnterButton_Click(object sender, RoutedEventArgs e)
		{
            if(this.currentMainPic == MainPic.first || this.isModelIn[1] == false)
            {
                this.isModelIn[1] = true;
                this.Model1EnterButton.IsEnabled = false;

                BitmapImage i = new BitmapImage();
                i.BeginInit();
                i.UriSource = new Uri("res/content1/01.png", UriKind.RelativeOrAbsolute);
                i.EndInit();
                this.content1.Source = i;
            }

		}

		private void Model2EnterButton_Click(object sender, RoutedEventArgs e)
		{
            if(this.currentMainPic == MainPic.first || this.isModelIn[2] == false)
            {
                this.isModelIn[2] = true;
                this.Model2EnterButton.IsEnabled = false;

                BitmapImage i = new BitmapImage();
                i.BeginInit();
                i.UriSource = new Uri("res/content2/01.png", UriKind.RelativeOrAbsolute);
                i.EndInit();
                this.content1.Source = i;

            }

		}

        private void Model3EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentMainPic == MainPic.first || this.isModelIn[3] == false)
            {
                this.isModelIn[3] = true;
            }
            else if (this.currentMainPic == MainPic.second && this.isModelIn[4] == false)
            {
                this.isModelIn[4] = true;
            }
            this.Model3EnterButton.IsEnabled = false;
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            if (this.currentMainPic == MainPic.init || this.currentMainPic == MainPic.second)
            {
                i.UriSource = new Uri("res/1.png", UriKind.RelativeOrAbsolute);
                this.currentMainPic = MainPic.first;
            }
            else
            {
                i.UriSource = new Uri("res/2.png", UriKind.RelativeOrAbsolute);
                this.currentMainPic = MainPic.second;
            }
            i.EndInit();
            this.MainPicImage.Source = i;

            this.content0.Visibility = Visibility.Hidden;
            this.content1.Visibility = Visibility.Hidden;
            this.content2.Visibility = Visibility.Hidden;
            this.content3.Visibility = Visibility.Hidden;

            this.Model0EnterButton.IsEnabled = true;
            this.Model1EnterButton.IsEnabled = true;
            this.Model2EnterButton.IsEnabled = true;
            this.Model3EnterButton.IsEnabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.currentMainPic = MainPic.init;
            this.content0.Visibility = Visibility.Hidden;
            this.content1.Visibility = Visibility.Hidden;
            this.content2.Visibility = Visibility.Hidden;
            this.content3.Visibility = Visibility.Hidden;

            this.Model0EnterButton.IsEnabled = true;
            this.Model1EnterButton.IsEnabled = true;
            this.Model2EnterButton.IsEnabled = true;
            this.Model3EnterButton.IsEnabled = true;
        }

        private void Nav0IntroButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.UriSource = new Uri("res/content0/01.png", UriKind.RelativeOrAbsolute);
            i.EndInit();
            this.content0.Source = i;
        }

        private void Nav0BackgroundButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.UriSource = new Uri("res/content0/02.png", UriKind.RelativeOrAbsolute);
            i.EndInit();
            this.content0.Source = i;
        }

        private void Nav0CoreButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.UriSource = new Uri("res/content0/03.png", UriKind.RelativeOrAbsolute);
            i.EndInit();
            this.content0.Source = i;
        }

        private void Nav0BaseButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.UriSource = new Uri("res/content0/05.png", UriKind.RelativeOrAbsolute);
            i.EndInit();
            this.content0.Source = i;

        }

        private void Nav0StructureButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.UriSource = new Uri("res/content0/07.png", UriKind.RelativeOrAbsolute);
            i.EndInit();
            this.content0.Source = i;

        }

        private void Nav0PlanButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.UriSource = new Uri("res/content0/08.png", UriKind.RelativeOrAbsolute);
            i.EndInit();
            this.content0.Source = i;

        }

        private void Nav0ADASButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.UriSource = new Uri("res/content0/09.png", UriKind.RelativeOrAbsolute);
            i.EndInit();
            this.content0.Source = i;

        }

        private void Nav0Auto_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.UriSource = new Uri("res/content0/12.png", UriKind.RelativeOrAbsolute);
            i.EndInit();
            this.content0.Source = i;

        }

        private void Nav0ProgressButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.UriSource = new Uri("res/content0/14.png", UriKind.RelativeOrAbsolute);
            i.EndInit();
            this.content0.Source = i;

        }

        private void Nav0PartnerButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.UriSource = new Uri("res/content0/15.png", UriKind.RelativeOrAbsolute);
            i.EndInit();
            this.content0.Source = i;

        }

        private void Nav0ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.UriSource = new Uri("res/content0/01.png", UriKind.RelativeOrAbsolute);
            i.EndInit();
            this.content0.Source = i;
            this.content0.Visibility = Visibility.Hidden;
            this.Model0EnterButton.IsEnabled = true;
        }

        private void Nav1PatternButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nav1TeamButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nav1IntroButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nav1BackgroundButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nav1Plan_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nav1ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.UriSource = new Uri("res/content1/01.png", UriKind.RelativeOrAbsolute);
            i.EndInit();
            this.content1.Source = i;
            this.content1.Visibility = Visibility.Hidden;
            this.Model0EnterButton.IsEnabled = true;
        }

        private void Nav2IntroButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nav2NewButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nav2PlanButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nav2ProgressButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nav2ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.UriSource = new Uri("res/content2/01.png", UriKind.RelativeOrAbsolute);
            i.EndInit();
            this.content2.Source = i;
            this.content2.Visibility = Visibility.Hidden;
            this.Model0EnterButton.IsEnabled = true;
        }

        private void Nav31IntroButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nav31AffarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nav31ProductButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nav31ServiceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nav31GoodnessButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nav31PartnerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nav31ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.UriSource = new Uri("res/content1/01.png", UriKind.RelativeOrAbsolute);
            i.EndInit();
            this.content1.Source = i;
            this.content1.Visibility = Visibility.Hidden;
            this.Model0EnterButton.IsEnabled = true;
        }
    }
}
