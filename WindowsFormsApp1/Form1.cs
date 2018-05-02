﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Boolean m_IsFullScreen = false;//标记是否全屏
        Assembly _assembly;
        Stream _imageStream;
        Bitmap pic ;

        public Form1()
        {
            InitializeComponent();

#if DEBUG
#endif

#if REALSE
            this.GoFullScreenMode();
#endif
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this._assembly = Assembly.GetExecutingAssembly();
            this._imageStream = this._assembly.GetManifestResourceStream("WindowsFormsApp1.pic0.png");


            this.Form1_Resize(sender, e);

        }


        private void PictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this._assembly = Assembly.GetExecutingAssembly();
            this._imageStream = this._assembly.GetManifestResourceStream("WindowsFormsApp1.pic0.png");


            this.pictureBox1.Width = this.Width / 4;
            this.pictureBox1.Height = this.Height;
            this.pictureBox2.Width = this.Width / 4;
            this.pictureBox2.Height = this.Height;
            this.pictureBox3.Width = this.Width / 4;
            this.pictureBox3.Height = this.Height;
            this.pictureBox4.Width = this.Width / 4;
            this.pictureBox4.Height = this.Height;

            this.pictureBox1.Left = 0;
            this.pictureBox1.Top = 0;
            this.pictureBox2.Left = this.pictureBox1.Width;
            this.pictureBox2.Top = 0;
            this.pictureBox3.Left = this.pictureBox2.Left + this.pictureBox2.Width;
            this.pictureBox3.Top = 0;
            this.pictureBox4.Left = this.pictureBox3.Left + this.pictureBox3.Width;
            this.pictureBox4.Top = 0;
        }



        private void GoFullScreenMode()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
        }
        private void LeaveFullScreenMode()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;
            this.TopMost = false;
        }

        private void FlipFullScreenByInterMethod()
        {
            if (this.m_IsFullScreen)
            {
                this.GoFullScreenMode();
            }
            else
            {
                this.LeaveFullScreenMode();
            }
            this.m_IsFullScreen = !this.m_IsFullScreen;
        }
    }
}

