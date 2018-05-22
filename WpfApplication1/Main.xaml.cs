using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Timers;
using System.Threading;
using System.Windows.Threading;
using System.Runtime.InteropServices;

namespace 孵化
{
    [StructLayout(LayoutKind.Sequential)]
    struct PLASTINPUTINFO
    {
        public static readonly int Sizeof = Marshal.SizeOf(typeof(PLASTINPUTINFO));

        [MarshalAs(UnmanagedType.U4)]
        public Int32 cbSize;
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 dwTime;
    }

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
        private int model0CurrentPage = 1;
        private int model1CurrentPage = 1;
        private int model2CurrentPage = 1;
        private int model3CurrentPage = 1;
        private int model4CurrentPage = 1;
        private int content0CurrentPage
        {
            set
            {
                this.model0CurrentPage = value;
                BitmapImage i = new BitmapImage();
                i.BeginInit();
                string s = string.Format("res/content0/{0:D2}.png", value);
                i.UriSource = new Uri(s, UriKind.RelativeOrAbsolute);
                i.DecodePixelWidth = 1920;
                i.EndInit();
                this.content0.Source = i;

            }
            get
            {
                return this.model0CurrentPage;
            }
        }
        private int content1CurrentPage
        {
            set
            {
                this.model1CurrentPage = value;
                BitmapImage i = new BitmapImage();
                i.BeginInit();
                string s = string.Format("res/content1/{0:D2}.png", value);
                i.UriSource = new Uri(s, UriKind.RelativeOrAbsolute);
                i.DecodePixelWidth = 1920;
                i.EndInit();
                this.content1.Source = i;
            }
            get
            {
                return this.model1CurrentPage;
            }
        }
        private int content2CurrentPage
        {
            set
            {
                this.model2CurrentPage = value;
                BitmapImage i = new BitmapImage();
                i.BeginInit();
                string s = string.Format("res/content2/{0:D2}.png", value);
                i.UriSource = new Uri(s, UriKind.RelativeOrAbsolute);
                i.DecodePixelWidth = 1920;
                i.EndInit();
                this.content2.Source = i;
            }
            get
            {
                return this.model2CurrentPage;
            }
        }
        private int content3CurrentPage
        {
            set
            {
                this.model3CurrentPage = value;
                BitmapImage i = new BitmapImage();
                i.BeginInit();
                if (this.currentMainPic == MainPic.first)
                {
                    string s = string.Format("res/content3/{0:D2}.png", value);
                    i.UriSource = new Uri(s, UriKind.RelativeOrAbsolute);
                }
                else
                {
                    string s = string.Format("res/content4/{0:D2}.png", value);
                    i.UriSource = new Uri(s, UriKind.RelativeOrAbsolute);
                }
                i.DecodePixelWidth = 1920;
                i.EndInit();
                this.content3.Source = i;
            }
            get
            {
                return this.model3CurrentPage;
            }
        }

        private DispatcherTimer timer;
        private readonly static int defaultCheckCount = 2;  // Unit in second
        private static int _checkCount = defaultCheckCount;
        private static int checkCount
        {
            get
            {
                return _checkCount;
            }
            set
            {
                _checkCount = value;
            }
        }

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
            this.timer = new DispatcherTimer();
            this.timer.Interval = new TimeSpan(0, 0, 1);
            this.timer.Tick += new EventHandler(this.Timer_Tick);
            timer.Start();


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
            if (this.currentMainPic == MainPic.first && this.isModelIn[0] == false)
            {
                this.isModelIn[0] = true;
                this.Model0EnterButton.IsEnabled = false;

                this.content0CurrentPage = 1;
                this.content0.Visibility = Visibility.Visible;
            }
        }

        private void Model1EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentMainPic == MainPic.first && this.isModelIn[1] == false)
            {
                this.isModelIn[1] = true;
                this.Model1EnterButton.IsEnabled = false;

                this.content1CurrentPage = 1;
                this.content1.Visibility = Visibility.Visible;
            }

        }

        private void Model2EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentMainPic == MainPic.first && this.isModelIn[2] == false)
            {
                this.isModelIn[2] = true;
                this.Model2EnterButton.IsEnabled = false;

                this.Nav2IntroButton_Click(sender, e);
                this.content2.Visibility = Visibility.Visible;

            }

        }

        private void Model3EnterButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            if (this.currentMainPic == MainPic.second && this.isModelIn[4] == false)
            {
                this.isModelIn[4] = true;
                i.UriSource = new Uri("res/content4/01.png", UriKind.RelativeOrAbsolute);
                this.Nav32.Visibility = Visibility.Visible;
            }
            else
            {
                this.isModelIn[3] = true;
                i.UriSource = new Uri("res/content3/01.png", UriKind.RelativeOrAbsolute);
                this.Nav31.Visibility = Visibility.Visible;
            }
            i.EndInit();
            this.content3.Source = i;
            this.Model3EnterButton.IsEnabled = false;
            this.content3.Visibility = Visibility.Visible;
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            if (this.currentMainPic == MainPic.init || this.currentMainPic == MainPic.second)
            {
                i.UriSource = new Uri("res/1.png", UriKind.RelativeOrAbsolute);
                i.DecodePixelWidth = 1920;
                this.currentMainPic = MainPic.first;
            }
            else
            {
                i.UriSource = new Uri("res/2.png", UriKind.RelativeOrAbsolute);
                i.DecodePixelWidth = 1980;
                this.currentMainPic = MainPic.second;
            }
            i.EndInit();
            this.MainPicImage.Source = i;

            ResumeScreenState(sender, e);
        }

        private void GoBackToFirstPic(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.UriSource = new Uri("res/0.png", UriKind.RelativeOrAbsolute);
            i.DecodePixelWidth = 7680;
            i.EndInit();
            this.currentMainPic = MainPic.init;
            this.MainPicImage.Source = i;
            this.ResumeScreenState(sender, e);
        }

        private void ResumeScreenState(object sender, RoutedEventArgs e)
        {
            this.content0.Visibility = Visibility.Hidden;
            this.content1.Visibility = Visibility.Hidden;
            this.content2.Visibility = Visibility.Hidden;
            this.content3.Visibility = Visibility.Hidden;

            this.Model0EnterButton.IsEnabled = true;
            this.Model1EnterButton.IsEnabled = true;
            this.Model2EnterButton.IsEnabled = true;
            this.Model3EnterButton.IsEnabled = true;

            for (int a = 0; a < this.isModelIn.Length - 1; a++)
            {
                this.isModelIn[a] = false;
            }
            this.Nav31.Visibility = Visibility.Hidden;
            this.Nav32.Visibility = Visibility.Hidden;

            this.Nav0ReturnButton_Click(sender, e);
            this.Nav1ReturnButton_Click(sender, e);
            this.Nav2ReturnButton_Click(sender, e);
            this.Nav31ReturnButton_Click(sender, e);
            this.Nav32ReturnButton_Click(sender, e);
        }


        private void Nav0IntroButton_Click(object sender, RoutedEventArgs e)
        {
            this.content0CurrentPage = 1;
            this.ButArea0Next.Visibility = Visibility.Hidden;
            this.ButArea0Previous.Visibility = Visibility.Hidden;
        }

        private void Nav0BackgroundButton_Click(object sender, RoutedEventArgs e)
        {
            this.content0CurrentPage = 2;
        }

        private void Nav0CoreButton_Click(object sender, RoutedEventArgs e)
        {
            this.content0CurrentPage = 3;
            this.ButArea0Next.Visibility = Visibility.Visible;
            this.ButArea0Previous.Visibility = Visibility.Hidden;

            this.ButArea0.RowDefinitions[0].Height = new GridLength(89, GridUnitType.Star);
            this.ButArea0.RowDefinitions[1].Height = new GridLength(13, GridUnitType.Star);
            this.ButArea0.RowDefinitions[2].Height = new GridLength(173, GridUnitType.Star);
            this.ButArea0.ColumnDefinitions[0].Width = new GridLength(121, GridUnitType.Star);
            this.ButArea0.ColumnDefinitions[1].Width = new GridLength(16, GridUnitType.Star);
            this.ButArea0.ColumnDefinitions[2].Width = new GridLength(19, GridUnitType.Star);
            this.ButArea0.ColumnDefinitions[3].Width = new GridLength(34, GridUnitType.Star);
        }

        private void Nav0BaseButton_Click(object sender, RoutedEventArgs e)
        {
            this.content0CurrentPage = 5;

            this.ButArea0.RowDefinitions[0].Height = new GridLength(89, GridUnitType.Star);
            this.ButArea0.RowDefinitions[1].Height = new GridLength(13, GridUnitType.Star);
            this.ButArea0.RowDefinitions[2].Height = new GridLength(173, GridUnitType.Star);
            this.ButArea0.ColumnDefinitions[0].Width = new GridLength(62, GridUnitType.Star);
            this.ButArea0.ColumnDefinitions[1].Width = new GridLength(4, GridUnitType.Star);
            this.ButArea0.ColumnDefinitions[2].Width = new GridLength(12, GridUnitType.Star);
            this.ButArea0.ColumnDefinitions[3].Width = new GridLength(17, GridUnitType.Star);

            this.ButArea0Next.Visibility = Visibility.Visible;
            this.ButArea0Previous.Visibility = Visibility.Hidden;
        }

        private void Nav0StructureButton_Click(object sender, RoutedEventArgs e)
        {
            this.content0CurrentPage = 7;
            this.ButArea0Next.Visibility = Visibility.Hidden;
            this.ButArea0Previous.Visibility = Visibility.Hidden;
        }

        private void Nav0PlanButton_Click(object sender, RoutedEventArgs e)
        {
            this.content0CurrentPage = 8;
            this.ButArea0Next.Visibility = Visibility.Hidden;
            this.ButArea0Previous.Visibility = Visibility.Hidden;
        }

        private void Nav0ADASButton_Click(object sender, RoutedEventArgs e)
        {
            this.content0CurrentPage = 9;

            this.ButArea0.RowDefinitions[0].Height = new GridLength(99, GridUnitType.Star);
            this.ButArea0.RowDefinitions[1].Height = new GridLength(14, GridUnitType.Star);
            this.ButArea0.RowDefinitions[2].Height = new GridLength(162, GridUnitType.Star);
            this.ButArea0.ColumnDefinitions[0].Width = new GridLength(75, GridUnitType.Star);
            this.ButArea0.ColumnDefinitions[1].Width = new GridLength(59, GridUnitType.Star);
            this.ButArea0.ColumnDefinitions[2].Width = new GridLength(34, GridUnitType.Star);
            this.ButArea0.ColumnDefinitions[3].Width = new GridLength(22, GridUnitType.Star);

            this.ButArea0Next.Visibility = Visibility.Visible;
            this.ButArea0Previous.Visibility = Visibility.Hidden;
        }

        private void Nav0Auto_Click(object sender, RoutedEventArgs e)
        {
            this.content0CurrentPage = 12;

            this.ButArea0.RowDefinitions[0].Height = new GridLength(122, GridUnitType.Star);
            this.ButArea0.RowDefinitions[1].Height = new GridLength(11, GridUnitType.Star);
            this.ButArea0.RowDefinitions[2].Height = new GridLength(142, GridUnitType.Star);
            this.ButArea0.ColumnDefinitions[0].Width = new GridLength(57, GridUnitType.Star);
            this.ButArea0.ColumnDefinitions[1].Width = new GridLength(12, GridUnitType.Star);
            this.ButArea0.ColumnDefinitions[2].Width = new GridLength(19, GridUnitType.Star);
            this.ButArea0.ColumnDefinitions[3].Width = new GridLength(17, GridUnitType.Star);

            this.ButArea0Next.Visibility = Visibility.Visible;
            this.ButArea0Previous.Visibility = Visibility.Hidden;

        }

        private void Nav0ProgressButton_Click(object sender, RoutedEventArgs e)
        {
            this.content0CurrentPage = 14;
            this.ButArea0Next.Visibility = Visibility.Hidden;
            this.ButArea0Previous.Visibility = Visibility.Hidden;
        }

        private void Nav0PartnerButton_Click(object sender, RoutedEventArgs e)
        {
            this.content0CurrentPage = 15;
            this.ButArea0Next.Visibility = Visibility.Hidden;
            this.ButArea0Previous.Visibility = Visibility.Hidden;
        }

        private void Nav0ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.content0CurrentPage = 1;
            this.content0.Visibility = Visibility.Hidden;
            this.Model0EnterButton.IsEnabled = true;
            this.isModelIn[0] = false;
        }

        private void Nav1PatternButton_Click(object sender, RoutedEventArgs e)
        {
            this.content1CurrentPage = 16;

            this.ButArea1.RowDefinitions[0].Height = new GridLength(137, GridUnitType.Star);
            this.ButArea1.RowDefinitions[1].Height = new GridLength(9, GridUnitType.Star);
            this.ButArea1.RowDefinitions[2].Height = new GridLength(129, GridUnitType.Star);
            this.ButArea1.ColumnDefinitions[0].Width = new GridLength(155, GridUnitType.Star);
            this.ButArea1.ColumnDefinitions[1].Width = new GridLength(13, GridUnitType.Star);
            this.ButArea1.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);
            this.ButArea1.ColumnDefinitions[3].Width = new GridLength(13, GridUnitType.Star);

            this.ButArea1Next.Visibility = Visibility.Visible;
            this.ButArea1Previous.Visibility = Visibility.Hidden;
        }

        private void Nav1TeamButton_Click(object sender, RoutedEventArgs e)
        {
            this.content1CurrentPage = 15;
        }

        private void Nav1IntroButton_Click(object sender, RoutedEventArgs e)
        {
            this.content1CurrentPage = 4;

            this.ButArea1.RowDefinitions[0].Height = new GridLength(88, GridUnitType.Star);
            this.ButArea1.RowDefinitions[1].Height = new GridLength(14, GridUnitType.Star);
            this.ButArea1.RowDefinitions[2].Height = new GridLength(173, GridUnitType.Star);
            this.ButArea1.ColumnDefinitions[0].Width = new GridLength(9, GridUnitType.Star);
            this.ButArea1.ColumnDefinitions[1].Width = new GridLength(10, GridUnitType.Star);
            this.ButArea1.ColumnDefinitions[2].Width = new GridLength(5, GridUnitType.Star);
            this.ButArea1.ColumnDefinitions[3].Width = new GridLength(71, GridUnitType.Star);

            this.ButArea1Next.Visibility = Visibility.Visible;
            this.ButArea1Previous.Visibility = Visibility.Hidden;
        }

        private void Nav1BackgroundButton_Click(object sender, RoutedEventArgs e)
        {
            this.content1CurrentPage = 1;

            this.ButArea1.RowDefinitions[0].Height = new GridLength(117, GridUnitType.Star);
            this.ButArea1.RowDefinitions[1].Height = new GridLength(11, GridUnitType.Star);
            this.ButArea1.RowDefinitions[2].Height = new GridLength(147, GridUnitType.Star);
            this.ButArea1.ColumnDefinitions[0].Width = new GridLength(141, GridUnitType.Star);
            this.ButArea1.ColumnDefinitions[1].Width = new GridLength(20, GridUnitType.Star);
            this.ButArea1.ColumnDefinitions[2].Width = new GridLength(7, GridUnitType.Star);
            this.ButArea1.ColumnDefinitions[3].Width = new GridLength(22, GridUnitType.Star);

            this.ButArea1Next.Visibility = Visibility.Visible;
            this.ButArea1Previous.Visibility = Visibility.Hidden;
        }

        private void Nav1Plan_Click(object sender, RoutedEventArgs e)
        {
            this.content1CurrentPage = 19;

            this.ButArea1.RowDefinitions[0].Height = new GridLength(172, GridUnitType.Star);
            this.ButArea1.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Star);
            this.ButArea1.RowDefinitions[2].Height = new GridLength(93, GridUnitType.Star);
            this.ButArea1.ColumnDefinitions[0].Width = new GridLength(127, GridUnitType.Star);
            this.ButArea1.ColumnDefinitions[1].Width = new GridLength(17, GridUnitType.Star);
            this.ButArea1.ColumnDefinitions[2].Width = new GridLength(8, GridUnitType.Star);
            this.ButArea1.ColumnDefinitions[3].Width = new GridLength(38, GridUnitType.Star);

            this.ButArea1Next.Visibility = Visibility.Visible;
            this.ButArea1Previous.Visibility = Visibility.Hidden;
        }

        private void Nav1ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.content1CurrentPage = 1;
            this.content1.Visibility = Visibility.Hidden;
            this.Model1EnterButton.IsEnabled = true;
            this.isModelIn[1] = false;
        }

        private void Nav2IntroButton_Click(object sender, RoutedEventArgs e)
        {
            this.content2CurrentPage = 1;

            this.ButArea2.RowDefinitions[0].Height = new GridLength(250, GridUnitType.Star);
            this.ButArea2.RowDefinitions[1].Height = new GridLength(13, GridUnitType.Star);
            this.ButArea2.RowDefinitions[2].Height = new GridLength(159, GridUnitType.Star);
            this.ButArea2.ColumnDefinitions[0].Width = new GridLength(149, GridUnitType.Star);
            this.ButArea2.ColumnDefinitions[1].Width = new GridLength(10, GridUnitType.Star);
            this.ButArea2.ColumnDefinitions[2].Width = new GridLength(31, GridUnitType.Star);

            this.ButArea2Next.Visibility = Visibility.Visible;
        }

        private void Nav2NewButton_Click(object sender, RoutedEventArgs e)
        {
            this.content2CurrentPage = 10;

            this.ButArea2.RowDefinitions[0].Height = new GridLength(123, GridUnitType.Star);
            this.ButArea2.RowDefinitions[1].Height = new GridLength(9, GridUnitType.Star);
            this.ButArea2.RowDefinitions[2].Height = new GridLength(79, GridUnitType.Star);
            this.ButArea2.ColumnDefinitions[0].Width = new GridLength(73, GridUnitType.Star);
            this.ButArea2.ColumnDefinitions[1].Width = new GridLength(13, GridUnitType.Star);
            this.ButArea2.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);

        }

        private void Nav2PlanButton_Click(object sender, RoutedEventArgs e)
        {
            this.content2CurrentPage = 15;

            this.ButArea2.RowDefinitions[0].Height = new GridLength(242, GridUnitType.Star);
            this.ButArea2.RowDefinitions[1].Height = new GridLength(27, GridUnitType.Star);
            this.ButArea2.RowDefinitions[2].Height = new GridLength(153, GridUnitType.Star);
            this.ButArea2.ColumnDefinitions[0].Width = new GridLength(73, GridUnitType.Star);
            this.ButArea2.ColumnDefinitions[1].Width = new GridLength(13, GridUnitType.Star);
            this.ButArea2.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);
        }

        private void Nav2ProgressButton_Click(object sender, RoutedEventArgs e)
        {
            this.content2CurrentPage = 16;

            this.ButArea2.RowDefinitions[0].Height = new GridLength(74, GridUnitType.Star);
            this.ButArea2.RowDefinitions[1].Height = new GridLength(11, GridUnitType.Star);
            this.ButArea2.RowDefinitions[2].Height = new GridLength(337, GridUnitType.Star);
            this.ButArea2.ColumnDefinitions[0].Width = new GridLength(72, GridUnitType.Star);
            this.ButArea2.ColumnDefinitions[1].Width = new GridLength(13, GridUnitType.Star);
            this.ButArea2.ColumnDefinitions[2].Width = new GridLength(105, GridUnitType.Star);
        }

        private void Nav2ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.content2CurrentPage = 1;
            this.content2.Visibility = Visibility.Hidden;
            this.Model2EnterButton.IsEnabled = true;
            this.isModelIn[2] = false;
        }

        private void Nav31IntroButton_Click(object sender, RoutedEventArgs e)
        {
            this.content3CurrentPage = 1;
        }

        private void Nav31AffarButton_Click(object sender, RoutedEventArgs e)
        {
            this.content3CurrentPage = 2;

            this.ButArea3.RowDefinitions[0].Height = new GridLength(90, GridUnitType.Star);
            this.ButArea3.RowDefinitions[1].Height = new GridLength(8, GridUnitType.Star);
            this.ButArea3.RowDefinitions[2].Height = new GridLength(177, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[0].Width = new GridLength(35, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[1].Width = new GridLength(12, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[2].Width = new GridLength(84, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[3].Width = new GridLength(59, GridUnitType.Star);

            this.ButArea3Next.Visibility = Visibility.Visible;
            this.ButArea3Previous.Visibility = Visibility.Hidden;
        }

        private void Nav31ProductButton_Click(object sender, RoutedEventArgs e)
        {
            this.content3CurrentPage = 4;

            this.ButArea3.RowDefinitions[0].Height = new GridLength(132, GridUnitType.Star);
            this.ButArea3.RowDefinitions[1].Height = new GridLength(12, GridUnitType.Star);
            this.ButArea3.RowDefinitions[2].Height = new GridLength(131, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[0].Width = new GridLength(32, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[1].Width = new GridLength(14, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[2].Width = new GridLength(33, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[3].Width = new GridLength(111, GridUnitType.Star);

            this.ButArea3Next.Visibility = Visibility.Visible;
            this.ButArea3Previous.Visibility = Visibility.Hidden;
        }

        private void Nav31ServiceButton_Click(object sender, RoutedEventArgs e)
        {
            this.content3CurrentPage = 7;

            this.ButArea3Next.Visibility = Visibility.Hidden;
            this.ButArea3Previous.Visibility = Visibility.Hidden;
        }

        private void Nav31GoodnessButton_Click(object sender, RoutedEventArgs e)
        {
            this.content3CurrentPage = 8;

            this.ButArea3.RowDefinitions[0].Height = new GridLength(34, GridUnitType.Star);
            this.ButArea3.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Star);
            this.ButArea3.RowDefinitions[2].Height = new GridLength(231, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[0].Width = new GridLength(152, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[1].Width = new GridLength(17, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[2].Width = new GridLength(16, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[3].Width = new GridLength(5, GridUnitType.Star);

            this.ButArea3Next.Visibility = Visibility.Visible;
            this.ButArea3Previous.Visibility = Visibility.Hidden;
        }

        private void Nav31PartnerButton_Click(object sender, RoutedEventArgs e)
        {
            this.content3CurrentPage = 13;
            this.ButArea3Next.Visibility = Visibility.Hidden;
            this.ButArea3Previous.Visibility = Visibility.Hidden;
        }

        private void Nav31ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.content3CurrentPage = 1;
            this.content3.Visibility = Visibility.Hidden;
            this.Model3EnterButton.IsEnabled = true;
            this.Nav31.Visibility = Visibility.Hidden;
            this.isModelIn[3] = false;
        }

        private void Nav32IntroButton_Click(object sender, RoutedEventArgs e)
        {
            this.content3CurrentPage = 1;
        }

        private void Nav32AffarButton_Click(object sender, RoutedEventArgs e)
        {
            this.content3CurrentPage = 2;
        }

        private void Nav32PartnerButton_Click(object sender, RoutedEventArgs e)
        {
            this.content3CurrentPage = 3;
        }

        private void Nav32ProductButton_Click(object sender, RoutedEventArgs e)
        {
            this.content3CurrentPage = 4;

            this.ButArea3.RowDefinitions[0].Height = new GridLength(143, GridUnitType.Star);
            this.ButArea3.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Star);
            this.ButArea3.RowDefinitions[2].Height = new GridLength(122, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[0].Width = new GridLength(24, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[1].Width = new GridLength(16, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[2].Width = new GridLength(127, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[3].Width = new GridLength(23, GridUnitType.Star);

            this.ButArea3Next.Visibility = Visibility.Visible;
            this.ButArea3Previous.Visibility = Visibility.Hidden;

        }

        private void Nav32CharacterButton_Click(object sender, RoutedEventArgs e)
        {
            this.content3CurrentPage = 6;

            this.ButArea3.RowDefinitions[0].Height = new GridLength(49, GridUnitType.Star);
            this.ButArea3.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Star);
            this.ButArea3.RowDefinitions[2].Height = new GridLength(216, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[0].Width = new GridLength(146, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[1].Width = new GridLength(16, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[2].Width = new GridLength(19, GridUnitType.Star);
            this.ButArea3.ColumnDefinitions[3].Width = new GridLength(9, GridUnitType.Star);

            this.ButArea3Next.Visibility = Visibility.Visible;
            this.ButArea3Previous.Visibility = Visibility.Hidden;
        }

        private void Nav32ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.UriSource = new Uri("res/content4/01.png", UriKind.RelativeOrAbsolute);
            i.EndInit();
            this.content3.Source = i;
            this.content3.Visibility = Visibility.Hidden;
            this.Model3EnterButton.IsEnabled = true;
            this.Nav32.Visibility = Visibility.Hidden;
            this.isModelIn[4] = false;
        }

        private void ButArea0Next_Click(object sender, RoutedEventArgs e)
        {
            switch (this.content0CurrentPage)
            {
                case 3:

                    this.content0CurrentPage = 4;
                    this.ButArea0Next.Visibility = Visibility.Hidden;
                    this.ButArea0Previous.Visibility = Visibility.Hidden;

                    break;
                case 5:
                    this.content0CurrentPage = 6;

                    this.ButArea0.RowDefinitions[0].Height = new GridLength(89, GridUnitType.Star);
                    this.ButArea0.RowDefinitions[1].Height = new GridLength(12, GridUnitType.Star);
                    this.ButArea0.RowDefinitions[2].Height = new GridLength(174, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[0].Width = new GridLength(131, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[1].Width = new GridLength(24, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[2].Width = new GridLength(25, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[3].Width = new GridLength(10, GridUnitType.Star);

                    this.ButArea0Next.Visibility = Visibility.Hidden;
                    this.ButArea0Previous.Visibility = Visibility.Visible;

                    break;
                case 9:
                    this.content0CurrentPage = 10;

                    this.ButArea0.RowDefinitions[0].Height = new GridLength(90, GridUnitType.Star);
                    this.ButArea0.RowDefinitions[1].Height = new GridLength(11, GridUnitType.Star);
                    this.ButArea0.RowDefinitions[2].Height = new GridLength(174, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[0].Width = new GridLength(114, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[1].Width = new GridLength(33, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[2].Width = new GridLength(29, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[3].Width = new GridLength(14, GridUnitType.Star);

                    this.ButArea0Next.Visibility = Visibility.Visible;
                    this.ButArea0Previous.Visibility = Visibility.Visible;

                    break;
                case 10:
                    this.content0CurrentPage = 11;

                    this.ButArea0.RowDefinitions[0].Height = new GridLength(90, GridUnitType.Star);
                    this.ButArea0.RowDefinitions[1].Height = new GridLength(11, GridUnitType.Star);
                    this.ButArea0.RowDefinitions[2].Height = new GridLength(174, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[0].Width = new GridLength(114, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[1].Width = new GridLength(33, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[2].Width = new GridLength(29, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[3].Width = new GridLength(14, GridUnitType.Star);

                    this.ButArea0Previous.Visibility = Visibility.Visible;
                    this.ButArea0Next.Visibility = Visibility.Hidden;
                    break;
                case 12:
                    this.content0CurrentPage = 13;

                    this.ButArea0.RowDefinitions[0].Height = new GridLength(122, GridUnitType.Star);
                    this.ButArea0.RowDefinitions[1].Height = new GridLength(11, GridUnitType.Star);
                    this.ButArea0.RowDefinitions[2].Height = new GridLength(142, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[0].Width = new GridLength(114, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[1].Width = new GridLength(55, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[2].Width = new GridLength(7, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[3].Width = new GridLength(14, GridUnitType.Star);

                    this.ButArea0Previous.Visibility = Visibility.Visible;
                    this.ButArea0Next.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void ButArea0Previous_Click(object sender, RoutedEventArgs e)
        {
            switch (this.model0CurrentPage)
            {
                case 4:
                    break;
                case 6:
                    this.Nav0BaseButton_Click(sender, e);
                    break;
                case 10:
                    this.Nav0ADASButton_Click(sender, e);
                    break;
                case 11:
                    this.content0CurrentPage = 10;

                    this.ButArea0.RowDefinitions[0].Height = new GridLength(90, GridUnitType.Star);
                    this.ButArea0.RowDefinitions[1].Height = new GridLength(11, GridUnitType.Star);
                    this.ButArea0.RowDefinitions[2].Height = new GridLength(174, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[0].Width = new GridLength(114, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[1].Width = new GridLength(33, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[2].Width = new GridLength(29, GridUnitType.Star);
                    this.ButArea0.ColumnDefinitions[3].Width = new GridLength(14, GridUnitType.Star);

                    this.ButArea0Next.Visibility = Visibility.Visible;
                    this.ButArea0Previous.Visibility = Visibility.Visible;
                    break;
                case 13:
                    this.content0CurrentPage = 12;
                    this.Nav0Auto_Click(sender, e);
                    break;
            }
        }

        private void ButArea1Next_Click(object sender, RoutedEventArgs e)
        {
            switch (this.content1CurrentPage)
            {
                case 1:
                    this.content1CurrentPage = 2;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(86, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(8, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(181, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(116, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(19, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(16, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(39, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Visible;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;

                case 2:
                    this.content1CurrentPage = 3;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(72, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(14, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(189, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(18, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(15, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(15, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(142, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Hidden;
                    this.ButArea1Previous.Visibility = Visibility.Visible;
                    break;

                case 4:
                    this.content1CurrentPage = 5;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(150, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(12, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(113, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(116, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(9, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(56, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Visible;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;

                case 5:
                    this.content1CurrentPage = 6;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(30, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(8, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(237, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(145, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(12, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(11, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(22, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Visible;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;

                case 6:
                    this.content1CurrentPage = 7;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(30, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(8, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(237, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(145, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(12, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(11, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(22, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Visible;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;

                case 7:
                    this.content1CurrentPage = 8;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(30, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(8, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(237, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(145, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(12, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(11, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(22, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Visible;
                    this.ButArea1Previous.Visibility = Visibility.Visible;
                    break;

                case 8:
                    this.content1CurrentPage = 9;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(184, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(81, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(145, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(14, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(22, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Visible;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;

                case 9:
                    this.content1CurrentPage = 10;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(193, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(12, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(70, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(145, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(14, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(22, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Visible;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;

                case 10:
                    this.content1CurrentPage = 11;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(193, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(12, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(70, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(145, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(14, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(22, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Visible;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;

                case 11:
                    this.content1CurrentPage = 12;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(193, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(12, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(70, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(145, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(14, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(22, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Visible;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;

                case 12:
                    this.content1CurrentPage = 13;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(28, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(11, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(236, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(155, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(9, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(10, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(16, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Visible;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;

                case 13:
                    this.content1CurrentPage = 14;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(34, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(9, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(232, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(155, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(13, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(13, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Hidden;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;

                case 16:
                    this.content1CurrentPage = 17;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(137, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(9, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(129, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(148, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(11, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(10, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(21, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Visible;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;

                case 17:
                    this.content1CurrentPage = 18;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(167, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(11, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(97, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(127, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(11, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(14, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(38, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Hidden;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;

                case 19:
                    this.content1CurrentPage = 20;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(172, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(93, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(127, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(17, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(8, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(38, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Visible;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;

                case 20:
                    this.content1CurrentPage = 21;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(160, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(8, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(107, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(28, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(11, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(11, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(140, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Visible;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;

                case 21:
                    this.content1CurrentPage = 22;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(191, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(8, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(76, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(137, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(10, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(12, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(31, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Visible;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;

                case 22:
                    this.content1CurrentPage = 23;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(185, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(8, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(83, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(132, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(10, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(10, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(39, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Visible;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;

                case 23:
                    this.content1CurrentPage = 24;

                    this.ButArea1.RowDefinitions[0].Height = new GridLength(191, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[1].Height = new GridLength(9, GridUnitType.Star);
                    this.ButArea1.RowDefinitions[2].Height = new GridLength(75, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[0].Width = new GridLength(122, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[1].Width = new GridLength(9, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[2].Width = new GridLength(13, GridUnitType.Star);
                    this.ButArea1.ColumnDefinitions[3].Width = new GridLength(46, GridUnitType.Star);

                    this.ButArea1Next.Visibility = Visibility.Hidden;
                    this.ButArea1Previous.Visibility = Visibility.Visible;

                    break;
            }

        }

        private void ButArea1Previous_Click(object sender, RoutedEventArgs e)
        {
            switch (this.content1CurrentPage)
            {
                case 2:
                    this.Nav1BackgroundButton_Click(sender, e);

                    break;
                case 3:
                    /// go to page 2
                    this.content1CurrentPage = 1;
                    this.ButArea1Next_Click(sender, e);

                    break;

                case 5:
                    this.Nav1IntroButton_Click(sender, e);

                    break;

                case 6:
                    this.content1CurrentPage = 4;
                    this.ButArea1Next_Click(sender, e);

                    break;
                case 7:
                    this.content1CurrentPage = 5;
                    this.ButArea1Next_Click(sender, e);

                    break;
                case 8:
                    this.content1CurrentPage = 6;
                    this.ButArea1Next_Click(sender, e);

                    break;

                case 9:
                    this.content1CurrentPage = 7;
                    this.ButArea1Next_Click(sender, e);

                    break;

                case 10:
                    this.content1CurrentPage = 8;
                    this.ButArea1Next_Click(sender, e);

                    break;

                case 11:
                    this.content1CurrentPage = 9;
                    this.ButArea1Next_Click(sender, e);

                    break;

                case 12:
                    this.content1CurrentPage = 10;
                    this.ButArea1Next_Click(sender, e);

                    break;

                case 13:
                    this.content1CurrentPage = 11;
                    this.ButArea1Next_Click(sender, e);

                    break;

                case 14:
                    this.content1CurrentPage = 12;
                    this.ButArea1Next_Click(sender, e);

                    break;

                case 16:
                    this.Nav1PatternButton_Click(sender, e);

                    break;

                case 17:
                    this.Nav1PatternButton_Click(sender, e);

                    break;

                case 18:
                    this.content1CurrentPage = 16;
                    this.ButArea1Next_Click(sender, e);

                    break;

                case 20:
                    this.Nav1Plan_Click(sender, e);

                    break;

                case 21:
                    this.content1CurrentPage = 19;
                    this.ButArea1Next_Click(sender, e);

                    break;

                case 22:
                    this.content1CurrentPage = 20;
                    this.ButArea1Next_Click(sender, e);

                    break;
                case 23:
                    this.content1CurrentPage = 21;
                    this.ButArea1Next_Click(sender, e);

                    break;

                case 24:
                    this.content1CurrentPage = 22;
                    this.ButArea1Next_Click(sender, e);
                    break;
            }

        }

        private void ButArea2Next_Click(object sender, RoutedEventArgs e)
        {
            switch (this.content2CurrentPage)
            {
                case 1:
                    this.content2CurrentPage = 2;

                    this.ButArea2.RowDefinitions[0].Height = new GridLength(79, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[1].Height = new GridLength(11, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[2].Height = new GridLength(332, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[0].Width = new GridLength(66, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[1].Width = new GridLength(15, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[2].Width = new GridLength(109, GridUnitType.Star);

                    break;
                case 2:
                    this.content2CurrentPage = 3;

                    this.ButArea2.RowDefinitions[0].Height = new GridLength(79, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[1].Height = new GridLength(11, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[2].Height = new GridLength(332, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[0].Width = new GridLength(66, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[1].Width = new GridLength(15, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[2].Width = new GridLength(109, GridUnitType.Star);

                    break;
                case 3:
                    this.content2CurrentPage = 4;

                    this.ButArea2.RowDefinitions[0].Height = new GridLength(120, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[1].Height = new GridLength(6, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[2].Height = new GridLength(85, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[0].Width = new GridLength(153, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[1].Width = new GridLength(19, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[2].Width = new GridLength(18, GridUnitType.Star);

                    break;

                case 4:
                    this.content2CurrentPage = 5;

                    this.ButArea2.RowDefinitions[0].Height = new GridLength(120, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[1].Height = new GridLength(7, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[2].Height = new GridLength(84, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[0].Width = new GridLength(73, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[1].Width = new GridLength(13, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);

                    break;

                case 5:
                    this.content2CurrentPage = 6;

                    this.ButArea2.RowDefinitions[0].Height = new GridLength(126, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[1].Height = new GridLength(5, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[2].Height = new GridLength(80, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[0].Width = new GridLength(73, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[1].Width = new GridLength(13, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);

                    break;

                case 6:
                    this.content2CurrentPage = 7;

                    this.ButArea2.RowDefinitions[0].Height = new GridLength(126, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[1].Height = new GridLength(5, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[2].Height = new GridLength(80, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[0].Width = new GridLength(73, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[1].Width = new GridLength(13, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);

                    break;
                case 7:
                    this.content2CurrentPage = 8;

                    this.ButArea2.RowDefinitions[0].Height = new GridLength(126, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[1].Height = new GridLength(5, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[2].Height = new GridLength(80, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[0].Width = new GridLength(73, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[1].Width = new GridLength(13, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);

                    break;

                case 8:
                    this.content2CurrentPage = 9;

                    this.ButArea2.RowDefinitions[0].Height = new GridLength(124, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[1].Height = new GridLength(5, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[2].Height = new GridLength(83, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[0].Width = new GridLength(73, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[1].Width = new GridLength(13, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);

                    break;

                case 9:
                    this.Nav2NewButton_Click(sender, e);

                    break;

                case 10:
                    this.content2CurrentPage = 11;

                    this.ButArea2.RowDefinitions[0].Height = new GridLength(123, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[1].Height = new GridLength(9, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[2].Height = new GridLength(79, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[0].Width = new GridLength(73, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[1].Width = new GridLength(13, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);

                    break;

                case 11:
                    this.content2CurrentPage = 12;

                    this.ButArea2.RowDefinitions[0].Height = new GridLength(121, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[1].Height = new GridLength(5, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[2].Height = new GridLength(85, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[0].Width = new GridLength(73, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[1].Width = new GridLength(13, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);

                    break;

                case 12:
                    this.content2CurrentPage = 13;

                    this.ButArea2.RowDefinitions[0].Height = new GridLength(121, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[1].Height = new GridLength(8, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[2].Height = new GridLength(82, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[0].Width = new GridLength(73, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[1].Width = new GridLength(13, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[2].Width = new GridLength(9, GridUnitType.Star);

                    break;

                case 13:
                    this.content2CurrentPage = 14;

                    this.ButArea2.RowDefinitions[0].Height = new GridLength(49, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Star);
                    this.ButArea2.RowDefinitions[2].Height = new GridLength(363, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[0].Width = new GridLength(52, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[1].Width = new GridLength(15, GridUnitType.Star);
                    this.ButArea2.ColumnDefinitions[2].Width = new GridLength(123, GridUnitType.Star);

                    break;

                case 14:
                    this.Nav2PlanButton_Click(sender, e);

                    break;

                case 15:
                    this.Nav2ProgressButton_Click(sender, e);

                    break;

                case 16:
                    this.content2CurrentPage = 14;

                    this.ButArea2Next_Click(sender, e);

                    break;
            }
        }

        private void ButArea3Next_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentMainPic == MainPic.first)
            {
                switch (this.content3CurrentPage)
                {
                    case 2:
                        this.content3CurrentPage = 3;

                        this.ButArea3.RowDefinitions[0].Height = new GridLength(154, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[2].Height = new GridLength(111, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[0].Width = new GridLength(12, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[1].Width = new GridLength(15, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[2].Width = new GridLength(15, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[3].Width = new GridLength(148, GridUnitType.Star);

                        this.ButArea3Next.Visibility = Visibility.Hidden;
                        this.ButArea3Previous.Visibility = Visibility.Visible;

                        break;

                    case 3:
                        this.content3CurrentPage = 4;

                        this.ButArea3.RowDefinitions[0].Height = new GridLength(133, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[2].Height = new GridLength(132, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[0].Width = new GridLength(32, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[1].Width = new GridLength(13, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[2].Width = new GridLength(135, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[3].Width = new GridLength(10, GridUnitType.Star);

                        this.ButArea3Next.Visibility = Visibility.Visible;
                        this.ButArea3Previous.Visibility = Visibility.Hidden;

                        break;

                    case 4:
                        this.content3CurrentPage = 5;

                        this.ButArea3.RowDefinitions[0].Height = new GridLength(159, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[1].Height = new GridLength(9, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[2].Height = new GridLength(107, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[0].Width = new GridLength(155, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[1].Width = new GridLength(12, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[2].Width = new GridLength(13, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[3].Width = new GridLength(10, GridUnitType.Star);

                        this.ButArea3Next.Visibility = Visibility.Visible;
                        this.ButArea3Previous.Visibility = Visibility.Visible;

                        break;

                    case 5:
                        this.content3CurrentPage = 6;

                        this.ButArea3.RowDefinitions[0].Height = new GridLength(151, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[2].Height = new GridLength(114, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[0].Width = new GridLength(137, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[1].Width = new GridLength(17, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[2].Width = new GridLength(13, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[3].Width = new GridLength(23, GridUnitType.Star);

                        this.ButArea3Previous.Visibility = Visibility.Visible;
                        this.ButArea3Next.Visibility = Visibility.Hidden;

                        break;

                    case 8:
                        this.content3CurrentPage = 9;

                        this.ButArea3.RowDefinitions[0].Height = new GridLength(24, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[1].Height = new GridLength(9, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[2].Height = new GridLength(242, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[0].Width = new GridLength(152, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[1].Width = new GridLength(17, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[2].Width = new GridLength(16, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[3].Width = new GridLength(5, GridUnitType.Star);

                        this.ButArea3Next.Visibility = Visibility.Visible;
                        this.ButArea3Previous.Visibility = Visibility.Visible;

                        break;

                    case 9:
                        this.content3CurrentPage = 10;

                        this.ButArea3.RowDefinitions[0].Height = new GridLength(183, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[1].Height = new GridLength(12, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[2].Height = new GridLength(80, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[0].Width = new GridLength(152, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[1].Width = new GridLength(13, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[2].Width = new GridLength(20, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[3].Width = new GridLength(5, GridUnitType.Star);

                        this.ButArea3Next.Visibility = Visibility.Visible;
                        this.ButArea3Previous.Visibility = Visibility.Visible;

                        break;

                    case 10:
                        this.content3CurrentPage = 11;

                        this.ButArea3.RowDefinitions[0].Height = new GridLength(198, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[1].Height = new GridLength(13, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[2].Height = new GridLength(64, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[0].Width = new GridLength(152, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[1].Width = new GridLength(13, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[2].Width = new GridLength(20, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[3].Width = new GridLength(5, GridUnitType.Star);

                        this.ButArea3Next.Visibility = Visibility.Visible;
                        this.ButArea3Previous.Visibility = Visibility.Visible;

                        break;

                    case 11:
                        this.content3CurrentPage = 12;

                        this.ButArea3.RowDefinitions[0].Height = new GridLength(198, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[1].Height = new GridLength(13, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[2].Height = new GridLength(64, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[0].Width = new GridLength(133, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[1].Width = new GridLength(19, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[2].Width = new GridLength(15, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[3].Width = new GridLength(23, GridUnitType.Star);

                        this.ButArea3Next.Visibility = Visibility.Hidden;
                        this.ButArea3Previous.Visibility = Visibility.Visible;

                        break;

                }
            }
            else
            {
                switch (this.content3CurrentPage)
                {
                    case 4:
                        this.content3CurrentPage = 5;

                        this.ButArea3.RowDefinitions[0].Height = new GridLength(30, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[1].Height = new GridLength(2, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[2].Height = new GridLength(23, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[0].Width = new GridLength(24, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[1].Width = new GridLength(119, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[2].Width = new GridLength(24, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[3].Width = new GridLength(23, GridUnitType.Star);

                        this.ButArea3Previous.Visibility = Visibility.Visible;
                        this.ButArea3Next.Visibility = Visibility.Hidden;

                        break;

                    case 6:
                        this.content3CurrentPage = 7;

                        this.ButArea3.RowDefinitions[0].Height = new GridLength(37, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[2].Height = new GridLength(228, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[0].Width = new GridLength(146, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[1].Width = new GridLength(11, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[2].Width = new GridLength(24, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[3].Width = new GridLength(9, GridUnitType.Star);

                        this.ButArea3Previous.Visibility = Visibility.Visible;
                        this.ButArea3Next.Visibility = Visibility.Visible;

                        break;

                    case 7:
                        this.content3CurrentPage = 8;

                        this.ButArea3.RowDefinitions[0].Height = new GridLength(45, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[1].Height = new GridLength(13, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[2].Height = new GridLength(217, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[0].Width = new GridLength(146, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[1].Width = new GridLength(21, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[2].Width = new GridLength(14, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[3].Width = new GridLength(9, GridUnitType.Star);

                        this.ButArea3Next.Visibility = Visibility.Visible;
                        this.ButArea3Previous.Visibility = Visibility.Visible;

                        break;

                    case 8:
                        this.content3CurrentPage = 9;

                        this.ButArea3.RowDefinitions[0].Height = new GridLength(45, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[1].Height = new GridLength(13, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[2].Height = new GridLength(217, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[0].Width = new GridLength(70, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[1].Width = new GridLength(5, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[2].Width = new GridLength(6, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[3].Width = new GridLength(14, GridUnitType.Star);

                        this.ButArea3Next.Visibility = Visibility.Visible;
                        this.ButArea3Previous.Visibility = Visibility.Visible;

                        break;

                    case 9:
                        this.content3CurrentPage = 10;

                        this.ButArea3.RowDefinitions[0].Height = new GridLength(32, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[1].Height = new GridLength(7, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[2].Height = new GridLength(236, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[0].Width = new GridLength(151, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[1].Width = new GridLength(12, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[2].Width = new GridLength(16, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[3].Width = new GridLength(11, GridUnitType.Star);

                        this.ButArea3Next.Visibility = Visibility.Visible;
                        this.ButArea3Previous.Visibility = Visibility.Visible;

                        break;

                    case 10:
                        this.content3CurrentPage = 11;

                        this.ButArea3.RowDefinitions[0].Height = new GridLength(178, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[1].Height = new GridLength(12, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[2].Height = new GridLength(85, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[0].Width = new GridLength(148, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[1].Width = new GridLength(21, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[2].Width = new GridLength(19, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[3].Width = new GridLength(11, GridUnitType.Star);

                        this.ButArea3Next.Visibility = Visibility.Visible;
                        this.ButArea3Previous.Visibility = Visibility.Visible;

                        break;

                    case 11:
                        this.content3CurrentPage = 12;

                        this.ButArea3.RowDefinitions[0].Height = new GridLength(103, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[1].Height = new GridLength(8, GridUnitType.Star);
                        this.ButArea3.RowDefinitions[2].Height = new GridLength(164, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[0].Width = new GridLength(148, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[1].Width = new GridLength(7, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[2].Width = new GridLength(24, GridUnitType.Star);
                        this.ButArea3.ColumnDefinitions[3].Width = new GridLength(11, GridUnitType.Star);

                        this.ButArea3Next.Visibility = Visibility.Hidden;
                        this.ButArea3Previous.Visibility = Visibility.Visible;

                        break;
                }
            }
        }

        private void ButArea3Previous_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentMainPic == MainPic.first)
            {
                switch (this.content3CurrentPage)
                {
                    case 3:
                        this.Nav31AffarButton_Click(sender, e);

                        break;

                    case 4:
                        this.Nav31ProductButton_Click(sender, e);

                        break;

                    case 5:
                        this.Nav31ProductButton_Click(sender, e);

                        break;

                    case 6:
                        this.content3CurrentPage = 4;
                        this.ButArea3Next_Click(sender, e);

                        break;

                    case 9:
                        this.Nav31GoodnessButton_Click(sender, e);

                        break;

                    case 10:
                        this.content3CurrentPage = 8;
                        this.ButArea3Next_Click(sender, e);

                        break;

                    case 11:
                        this.content3CurrentPage = 9;
                        this.ButArea3Next_Click(sender, e);

                        break;
                    case 12:
                        this.content3CurrentPage = 10;
                        this.ButArea3Next_Click(sender, e);

                        break;
                }
            }
            else
            {
                switch (this.content3CurrentPage)
                {
                    case 5:
                        this.Nav31ProductButton_Click(sender, e);

                        break;

                    case 7:
                        this.Nav32CharacterButton_Click(sender, e);

                        break;

                    case 8:
                        this.content3CurrentPage = 6;
                        this.ButArea3Next_Click(sender, e);

                        break;

                    case 9:
                        this.content3CurrentPage = 7;
                        this.ButArea3Next_Click(sender, e);

                        break;

                    case 10:
                        this.content3CurrentPage = 8;
                        this.ButArea3Next_Click(sender, e);

                        break;
                    case 11:
                        this.content3CurrentPage = 9;
                        this.ButArea3Next_Click(sender, e);

                        break;
                    case 12:
                        this.content3CurrentPage = 10;
                        this.ButArea3Next_Click(sender, e);

                        break;
                }

            }

        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            if (HaveUsedTo())
            {
                if (--checkCount == 0)
                {
                    checkCount = defaultCheckCount;

                    timer.Stop();

                    // Some actions here
                    this.GoBackToFirstPic(sender, new RoutedEventArgs());


                    timer.Start();
                }
            }
            else
            {
                checkCount = defaultCheckCount;
            }
        }

        private bool HaveUsedTo()
        {
            long noActionTime = GetNoActionTime() ;
            return noActionTime / 1000 > 1;
        }

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref PLASTINPUTINFO pLASTINPUTINFO);

        private static long GetNoActionTime()
        {
            PLASTINPUTINFO pLASTINPUTINFO = new PLASTINPUTINFO();
            pLASTINPUTINFO.cbSize = Marshal.SizeOf(pLASTINPUTINFO);
            if (!GetLastInputInfo(ref pLASTINPUTINFO))
            {
                return 0;
            }
            return Environment.TickCount - pLASTINPUTINFO.dwTime;
        }
    }
}
