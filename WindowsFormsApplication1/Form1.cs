using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    using NetDimension.NanUI;

    public partial class Form1 : Formium
    {
        public Form1()
            :base("http://res.app.local/index.html")
        {
            InitializeComponent();
        }
    }
}
