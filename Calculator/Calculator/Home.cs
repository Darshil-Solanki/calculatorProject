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
using org.mariuszgromada.math.mxparser;
using System.Configuration;
using Guna.UI2.WinForms;
namespace Calculator
{
    public partial class Home : Form
    {
        //Declaration
        private static StreamReader sr;
        private static StreamWriter sw;
        private static FileInfo file;
        public static string data;
        private int f;   //flag for change button
        private int fInv = 1; //flag for inverse and radian
        
        
        public Home()
        {
            InitializeComponent();
        }

        private void fileread()
        {
            try
            {
                file = new FileInfo(ConfigurationSettings.AppSettings["Path"]);
                sr = file.OpenText();
                if (file.Length != 0)
                {
                    txtres.AppendText(sr.ReadToEnd());
                }
                sr.Dispose();
            }
            catch (FileNotFoundException e)
            {

            }
            catch (IOException e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void filewrite()
        {
            try
            {
                if (txtres.Text != null)
                {
                    if (txtshow.Text.Contains("="))
                    {
                        txtres.Text += txtshow.Text + Environment.NewLine;
                        txtshow.Text = "0";
                    }
                    sw = file.CreateText();
                    string[] data = txtres.Text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    string hist = "";
                    int l = data.Length;
                    if (l > 5)
                    {
                        for (int i = l - 5; i < l; i++)
                        {
                            hist += data[i] + "\r\n";
                        }
                    }
                    else
                    {
                        for (int i = 0; i < l; i++)
                        {
                            hist += data[i] + "\r\n";
                        }
                    }
                    sw.Write(hist);
                }
                else if (txtres.Text == null && file.Length != 0)
                {
                    sw.Write("");
                }
                sw.Dispose();
            }
            catch (IOException)
            {

            }
        }
        private void Home_Load(object sender, EventArgs e)
        {
            fileread();
            tblpanel.ColumnStyles[4].SizeType = SizeType.Absolute;
            tblpanel.ColumnStyles[4].Width = 0;
            tblpanel.RowStyles[3].SizeType = SizeType.Absolute;
            tblpanel.RowStyles[3].Height = 0;
            tblpanel.RowStyles[4].SizeType = SizeType.Absolute;
            tblpanel.RowStyles[4].Height = 0;
            f = 1;
        }
        private  void btnchange_Click(object sender, EventArgs e)
        {
            if (f == 0)
            {
                tblpanel.RowStyles[3].SizeType = SizeType.Absolute;
                tblpanel.RowStyles[3].Height = 0;
                tblpanel.RowStyles[4].SizeType = SizeType.Absolute;
                tblpanel.RowStyles[4].Height = 0;
                uptblpanel.Visible = false;
                tblpanel.ColumnStyles[4].SizeType = SizeType.Absolute;
                tblpanel.ColumnStyles[4].Width = 0;
                sidetblpanel.Visible = false;
                f = 1;
            }
            else
            {
                tblpanel.ColumnStyles[4].SizeType = SizeType.Percent;
                tblpanel.ColumnStyles[4].Width = 20;
                sidetblpanel.Visible = true;
                tblpanel.RowStyles[4].SizeType = SizeType.Percent;
                tblpanel.RowStyles[4].Height = 10.0f;
                tblpanel.RowStyles[5].SizeType = SizeType.Percent;
                tblpanel.RowStyles[5].Height = 10.0f;
                uptblpanel.Visible = true;
                for (int i = 0; i < 10; i++)
                {
                    tblpanel.RowStyles[i].SizeType = SizeType.Percent;
                    tblpanel.RowStyles[i].Height = 10.00f;
                }
                f = 0;
            }
        }
        private void number_click(object sender, EventArgs e)
        {
            
            Guna2CircleButton b = (Guna2CircleButton)sender;
            Functions.settext(b.Text,this.txtshow,this.txtres);
        }
        private void arop_click(object sender, EventArgs e)
        {
            Guna2CircleButton b = (Guna2CircleButton)sender;
            string c = b.Text;
            if (c == "X!")
                Functions.setarop("!",this.txtshow,this.txtres);
            else if (c == "Xʸ")
                Functions.setarop("^", this.txtshow, this.txtres);
            else if (c == "√X")
            {
                if (txtshow.Text == "0")
                    txtshow.Text = "";
                Functions.setarop("√", this.txtshow, this.txtres);
            }
            else if (c == "1/x")
                Functions.setarop("^(-1)", this.txtshow, this.txtres);
            else if (c == "%")
            {
                if (txtshow.Text != "0")
                    Functions.setarop(c,this.txtshow,this.txtres);
            }
            else
                Functions.setarop(c, this.txtshow, this.txtres);
        }
        private void txtshow_TextChanged(object sender, EventArgs e)
        {
            Functions.txtchanged(this.txtshow,this.btnac);
        }
        private void btnc_Click(object sender, EventArgs e)
        {
            Functions.clear(this.txtshow);
        }
        private void btnac_Click(object sender, EventArgs e)
        {
            Functions.allclear(this.txtshow, this.txtres, this.btnac);
        } 
        private void btnequal_Click(object sender, EventArgs e)
        {
            Functions.result(this.txtshow,this.txtres);
        }
        private void trigno_click(object sender, EventArgs e)
        {
            Guna2CircleButton b = (Guna2CircleButton)sender;

            if (txtshow.Text == "0")
            {
                txtshow.Text = b.Text + "(";
            }
            else if (txtshow.Text.Contains("="))
            {
                txtres.Text += txtshow.Text + "\r\n";
                txtshow.Text = b.Text + "(";
            }
            else
            {
                txtshow.Text += b.Text + "(";
            }
        }
        private void brackate_click(object sender, EventArgs e)
        {
            Guna2CircleButton b = (Guna2CircleButton)sender;
            if (txtshow.Text == "0")
            {
                txtshow.Text = b.Text;
            }
            else if (!txtshow.Text.Contains("="))
            {
                txtshow.Text += b.Text;
            }
        }
        private void const_click(object sender, EventArgs e)
        {
            Guna2CircleButton b = (Guna2CircleButton)sender;
            if (txtshow.Text == "0")
            {
                txtres.Text += b.Text;
                if (b.Text == "π")
                    txtshow.Text = "=" + Math.PI.ToString();
                else
                    txtshow.Text = "=" + Math.E.ToString();
            }
            else if (txtshow.Text.Contains("="))
            {
                txtres.Text += txtshow.Text + Environment.NewLine;
                txtshow.Text = b.Text;
            }
            else
            {
                txtshow.Text += b.Text;
            }
        }
        private void btnraddeg_Click(object sender, EventArgs e)
        {
            Guna2CircleButton b = (Guna2CircleButton)sender;
            if (b.Text == "rad")
            {
                btnraddeg.Text = "deg";
                mXparser.setDegreesMode();
                btninv.Enabled = false;
            }
            else
            {
                btnraddeg.Text = "rad";
                btninv.Enabled = true;
            }
        }
        private void btninv_Click(object sender, EventArgs e)
        {
            if (fInv == 1)
            {
                btnraddeg.Enabled = false;
                mXparser.setRadiansMode();
                btnsin.Text = "asin";
                btncos.Text = "acos";
                btntan.Text = "atan";
                fInv = 0;
            }
            else
            {
                btnraddeg.Enabled = true;
                btnsin.Text = "sin";
                btncos.Text = "cos";
                btntan.Text = "tan";
                fInv = 1;
            }
        }
        private void form_keypress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Close();
            }
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46)
            {
                Functions.settext(e.KeyChar.ToString(),this.txtshow,this.txtres);
            }
            if (e.KeyChar == 42 || e.KeyChar == 43 || e.KeyChar == 45 || e.KeyChar == 47)
            {
                if (e.KeyChar == 42)
                {
                    Functions.setarop("⨉", this.txtshow, this.txtres);
                }
                else if (e.KeyChar == 47)
                {
                    Functions.setarop("÷", this.txtshow, this.txtres);
                }
                else
                {
                    Functions.setarop(e.KeyChar.ToString(), this.txtshow, this.txtres);
                }
            }
            if (e.KeyChar == 40 || e.KeyChar == 41)
            {
                if (txtshow.Text == "0")
                {
                    txtshow.Text = e.ToString();
                }
                else if (!txtshow.Text.Contains("="))
                {
                    txtshow.Text += e.ToString();
                }
            }
            if (e.KeyChar == 61)
            {
                Functions.result(this.txtshow,this.txtres);
            }
            if (e.KeyChar == 8)
            {
                Functions.clear(this.txtshow);
            }
            
        }
        private void form_close(object sender, FormClosingEventArgs e)
        {
            filewrite();
        }

        private void btnpop_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (txtshow.Text.Contains("="))
            {
                txtres.AppendText(txtshow.Text + Environment.NewLine);
                txtshow.Text = "0";
            }
            data = txtres.Text;
            Popup d = new Popup();
            d.ShowDialog();
            d.Dispose();
            txtres.Text=data;
            txtres.Select(txtres.TextLength-1, 0);
            txtres.ScrollToCaret();
            this.Show();
        }

        private void tab_click(object sender, EventArgs e)
        {

            topgradientpanel.Size = new Size(this.Width, 95);
            toptoppanel.Size = new Size(this.Width, 29);
            topbottompanel.Size = new Size(this.Width, 65);
            topbottompanel.Dock = DockStyle.None;
            topbottomtblpanel.RowStyles[0].Height = 50.0f;
            topbottomtblpanel.RowStyles.Add(new RowStyle(SizeType.Percent,50.00f));
            topbottomtblpanel.SetRowSpan(guna2TabControl1, 2);
            guna2TabControl1.Size = new Size(47, 68);
            
        }

        private void tab_leave(object sender, EventArgs e)
        {

            topbottomtblpanel.SetRowSpan(guna2TabControl1, 1);
            guna2TabControl1.Size = new Size(47, 20);
            topbottomtblpanel.RowStyles[0].Height = 100.0f;
            topbottompanel.Size = new Size(this.Width, 40);
            topbottompanel.Dock = DockStyle.Bottom;
            topgradientpanel.Size = new Size(this.Width, 70);

        }
      
        private void help_click(object sender, EventArgs e)
        {
            if (txtshow.Text.Contains("="))
            {
                txtres.AppendText(txtshow.Text + Environment.NewLine);
                txtshow.Text = "0";
            }
            Functions.gotohelp(this);
            
        }

        private void hist_click(object sender, EventArgs e)
        {
            this.Hide();
            if (txtshow.Text.Contains("="))
            {
                txtres.AppendText(txtshow.Text + Environment.NewLine);
                txtshow.Text = "0";
            }
            data = txtres.Text;
            history h = new history();
            h.ShowDialog();
            h.Dispose();
            this.Show();
        }

        private void frm_keydown(object sender, KeyEventArgs e)
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
