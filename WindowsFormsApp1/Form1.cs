using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using Microsoft.VisualBasic;
using System.Resources;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Boolean m_IsFullScreen = false;//标记是否全屏
        Assembly _assembly;
        Stream _imageStream;
        StreamReader _textStreamReader;
        StreamWriter _textStreamWriter;
        Bitmap pic ;

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_IsFullScreen = !m_IsFullScreen;//点一次全屏，再点还原。  
            this.SuspendLayout();
            if (m_IsFullScreen)//全屏 ,按特定的顺序执行
            {
                SetFormFullScreen(m_IsFullScreen);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.Activate();//
            }
            else//还原，按特定的顺序执行——窗体状态，窗体边框，设置任务栏和工作区域
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                SetFormFullScreen(m_IsFullScreen);
                this.Activate();
            }
            this.ResumeLayout(false);
        }
        /// <summary>  
        /// 设置全屏或者取消全屏  
        /// </summary>  
        /// <param name="fullscreen">true:全屏 false:恢复</param>  
        /// <param name="rectOld">设置的时候，此参数返回原始尺寸，恢复时用此参数设置恢复</param>  
        /// <returns>设置结果</returns>  
        public Boolean SetFormFullScreen(Boolean fullscreen)//, ref Rectangle rectOld
        {
            Rectangle rectOld = Rectangle.Empty;
            Int32 hwnd = 0;
            hwnd = FindWindow("Shell_TrayWnd", null);//获取任务栏的句柄

            if (hwnd == 0) return false;

            if (fullscreen)//全屏
            {
                ShowWindow(hwnd, SW_HIDE);//隐藏任务栏

                SystemParametersInfo(SPI_GETWORKAREA, 0, ref rectOld, SPIF_UPDATEINIFILE);//get  屏幕范围
                Rectangle rectFull = Screen.PrimaryScreen.Bounds;//全屏范围
                SystemParametersInfo(SPI_SETWORKAREA, 0, ref rectFull, SPIF_UPDATEINIFILE);//窗体全屏幕显示
            }
            else//还原 
            {
                ShowWindow(hwnd, SW_SHOW);//显示任务栏

                SystemParametersInfo(SPI_SETWORKAREA, 0, ref rectOld, SPIF_UPDATEINIFILE);//窗体还原
            }
            return true;
        }

        #region user32.dll

        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        public static extern Int32 ShowWindow(Int32 hwnd, Int32 nCmdShow);
        public const Int32 SW_SHOW = 5; public const Int32 SW_HIDE = 0;

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        private static extern Int32 SystemParametersInfo(Int32 uAction, Int32 uParam, ref Rectangle lpvParam, Int32 fuWinIni);
        public const Int32 SPIF_UPDATEINIFILE = 0x1;
        public const Int32 SPI_SETWORKAREA = 47;
        public const Int32 SPI_GETWORKAREA = 48;

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern Int32 FindWindow(string lpClassName, string lpWindowName);

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            this._textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("WindowsFormsApp1.text.txt"));
            MessageBox.Show(this._textStreamReader.ReadToEnd());
            this._textStreamReader.Close();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this._assembly = Assembly.GetExecutingAssembly();
            this._imageStream = this._assembly.GetManifestResourceStream("WindowsFormsApp1.pic0.png");
            this.pic = new Bitmap(this._imageStream);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Properties.Resources.testString);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Height = this.Height;
            this.pictureBox1.Top = 0;
            this.pictureBox1.Image = pic;
        }
    }
}

