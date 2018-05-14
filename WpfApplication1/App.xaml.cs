using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Threading;

namespace 孵化
{



    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private DispatcherTimer timer;
        private readonly static int defaultCheckCount = 300;
        private static int checkCount = defaultCheckCount;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
          
        }


    }
}
