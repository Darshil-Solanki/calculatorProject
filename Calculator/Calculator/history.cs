using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Calculator
{
    public partial class history : Form
    {
        public history()
        {
            InitializeComponent();
        }


        private void history_load(object sender, EventArgs e)
        {
            txtres.AppendText(Home.data);
        }

        private void history_keypress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==27)
                this.Close();
        }
        
       

        
    }
}
