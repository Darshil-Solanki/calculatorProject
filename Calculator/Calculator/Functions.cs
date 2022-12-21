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
    public class Functions
    {
        private static Expression e;
        private static char[] op = { '+', '-', '⨉', '÷', '^' };// for conditional purpose
        private static char[] op1 = { '+', '-', '⨉', '÷', '^', '%', '!', '(', ')', '√' };//for dot operation flag

        public static void gotohelp(Form o) {
            o.Hide();
            help h = new help();
            h.ShowDialog();
            h.BringToFront();
            h.Dispose();
            o.Show();
        
        }
        public static void allclear(Guna2TextBox txtshow,Guna2TextBox txtres,Guna2CircleButton btnac) {
            if (btnac.Text == "AC")
            {
                txtres.Text = "";
                txtshow.Text = "0";
            }
            else
            {
                if (!txtshow.Text.Contains("="))
                    txtshow.Text = "0";
            }
        }
        public static void clear(Guna2TextBox txtshow)
        {
            if (!txtshow.Text.Contains("="))
            {
                if (txtshow.Text != "0")
                {
                    int l = txtshow.TextLength;
                    string last = "", last1 = "", last2 = "";
                    if (l >= 3)
                    {
                        last = txtshow.Text.Substring(l - 3, 3);
                        if (l >= 4)
                        {
                            last1 = txtshow.Text.Substring(l - 4, 4);
                            if (l >= 5)
                            {
                                last2 = txtshow.Text.Substring(l - 5, 5);
                            }
                        }
                    }


                    if (last2 == "asin(" || last == "acos(" || last == "atan(")
                    {
                        txtshow.Text = txtshow.Text.Remove(l - 5);
                    }
                    else if (last1 == "sin(" || last == "cos(" || last == "tan(")
                    {
                        txtshow.Text = txtshow.Text.Remove(l - 4);
                    }
                    else if (last == "lg(" || last == "ln(")
                    {
                        txtshow.Text = txtshow.Text.Remove(l - 3);
                    }
                    else
                    {
                        txtshow.Text = txtshow.Text.Remove(l - 1);
                        if (txtshow.Text == "")
                        {
                            txtshow.Text = "0";
                        }
                    }
                }
            }
        }
        public static  void result(Guna2TextBox txtshow, Guna2TextBox txtres)
        {
            try
            {
                if (!txtshow.Text.Contains("=") && txtshow.Text != "0")
                {
                    txtres.AppendText(txtshow.Text);
                    e = new Expression(txtshow.Text);
                    if (e.checkSyntax())
                        txtshow.Text = "=" + e.calculate();
                    else
                    {
                        txtshow.Text = "=Syntax Error";
                        MessageBox.Show("Syntax Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                txtshow.Text = "=Error";
            }
        }
        private static bool isIn(char[] op, int i,Guna2TextBox txtshow)
        {
            bool f = false;
            for (int j = 0; j < op.Length; j++)
            {
                if (txtshow.Text[i] == op[j])
                {
                    f = true;
                    break;
                }
            }
            return f;
        }
        public static void settext(string c, Guna2TextBox txtshow, Guna2TextBox txtres)
        {
            if (c == "0")
            {
                if (txtshow.Text != "0" && !txtshow.Text.Contains("="))
                    txtshow.Text += c;
            }
            else if (c == "." && !txtshow.Text.Contains("="))
            {
                if (!txtshow.Text.Contains("."))
                    txtshow.Text += ".";
                else
                {
                    int i = txtshow.TextLength - 1;
                    while (!isIn(op1, i,txtshow))
                    {
                        i--;
                    }
                    int j;
                    string last = txtshow.Text.Substring(i + 1, (txtshow.TextLength - i - 1));
                    bool n = int.TryParse(last, out j);
                    if (n && !last.Contains("."))
                    {
                        txtshow.Text += ".";
                    }
                }
            }
            else if (txtshow.Text.Contains("="))
            {
                if (c != ".")
                {
                    txtres.AppendText(txtshow.Text + Environment.NewLine);
                    txtshow.Text = c;
                }
            }
            else
            {
                if (txtshow.Text != "0")
                    txtshow.Text += c;
                else
                    txtshow.Text = c;
            }
        }
        public static void setarop(string c, Guna2TextBox txtshow, Guna2TextBox txtres)
        {
            if (txtshow.Text.Contains("="))
            {
                txtres.AppendText(txtshow.Text + Environment.NewLine);
                txtshow.Text = txtshow.Text.Remove(0, 1) + c;
            }
            else if (c == "√" && txtshow.Text == "0")
            {
                txtshow.Text = c;
            }
            else
            {
                bool f = isIn(op, txtshow.TextLength - 1,txtshow);
                if (f)
                {
                    txtshow.Text = txtshow.Text.Remove(txtshow.TextLength - 1);
                    txtshow.Text += c;
                }
                else
                {
                    txtshow.Text += c;
                }
            }
        }
        
        public static void txtchanged(Guna2TextBox txtshow,Guna2CircleButton btnac) {
            if (txtshow.Text == "")
            {
                txtshow.Text = "0";
            }
            if (txtshow.Text == "0")
            {
                btnac.Text = "AC";
            }
            else
            {

                btnac.Text = "C";
            }
        }
    }
}
