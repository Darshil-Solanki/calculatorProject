using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace Calculator
{
    public partial class help : Form
    {
        FileInfo file;
        public help()
        {
            InitializeComponent();
        }

        private void help_load(object sender, EventArgs e)
        {
            file = new FileInfo(ConfigurationSettings.AppSettings["Pathpdf"]);
            axAcroPDF1.src = file.FullName + "#toolbar=0";
        }

        private void help_keypress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
                this.Close();
        }
         
    }
}
