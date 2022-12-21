using Guna.UI2.WinForms;
using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace Calculator
{
    public partial class Popup : Form
    {
        public Popup()
        {
            InitializeComponent();
        }
        
        private void number_click(object sender, EventArgs e)
        {
            Guna2CircleButton b = (Guna2CircleButton)sender;
            Functions.settext(b.Text,this.txtshow,this.txtres);
        }

        private void btnequal_Click(object sender, EventArgs e)
        {
            Functions.result(this.txtshow,this.txtres);
        }

        private void btnc_Click(object sender, EventArgs e)
        {
            Functions.clear(this.txtshow);
        }
       
        private void btnac_Click(object sender, EventArgs e)
        {
            Functions.allclear(this.txtshow,this.txtres,this.btnac);
        }

        private void arop_click(object sender, EventArgs e)
        {
            Guna2CircleButton b = (Guna2CircleButton)sender;
            string c = b.Text;
            
            if (c == "%")
            {
                if (txtshow.Text != "0")
                    Functions.setarop(c,this.txtshow,this.txtres);
            }
            else
                Functions.setarop(c,this.txtshow,this.txtres);
        }

        private void btnpop_Click(object sender, EventArgs e)
        {
            this.Close();           
        }

        private void guna2TrackBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.Opacity=(double)transparencybar.Value/100.0;
        }

        private void txtshow_TextChanged(object sender, EventArgs e)
        {
            Functions.txtchanged(this.txtshow, this.btnac);
        }

        private void Popup_Load(object sender, EventArgs e)
        {
            txtres.AppendText(Home.data);
        }

        private void Popup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Close();
            }
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46)
            {
                Functions.settext(e.KeyChar.ToString(), this.txtshow, this.txtres);
            }
            if (e.KeyChar == 42 || e.KeyChar == 43 || e.KeyChar == 45 || e.KeyChar == 47)
            {
                if (e.KeyChar == 42)
                {
                    Functions.setarop("⨉",this.txtshow,this.txtres);
                }
                else if (e.KeyChar == 47)
                {
                    Functions.setarop("÷",this.txtshow,this.txtres);
                }
                else
                {
                    Functions.setarop(e.KeyChar.ToString(), this.txtshow, this.txtres);
                }
            }
            if (e.KeyChar == 61)
            {
                Functions.result(this.txtshow, this.txtres);
            }
            if (e.KeyChar == 8)
            {
                Functions.clear(this.txtshow);
            }
            
        }

        private void demo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtshow.Text.Contains("="))
            {
                txtres.AppendText(txtshow.Text + Environment.NewLine);
                txtshow.Text = "0";
            }
            Home.data = txtres.Text; 
        }

        private void moved(object sender, EventArgs e)
        {
            this.Opacity = (double)transparencybar.Value / 100.0;
        }

        private void popup_keydown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt16(e.KeyCode) == 112)
            {
                if (txtshow.Text.Contains("="))
                {
                    txtres.AppendText(txtshow.Text + Environment.NewLine);
                    txtshow.Text = "0";
                }
                Functions.gotohelp(this);
            }
        }

       

        
    }
}
