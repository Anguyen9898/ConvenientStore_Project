using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BUS;
using DTO;

namespace ConvenientStore_Pro
{
    public partial class frm_Main : Form
    {
        frm_SignOn frm_Sign;
        ScannerBUS Scanner;
        SignOnBUS Sign;
        String barcode="";
        String EmployeeName = "";
        bool Test = false;
        public frm_Main()
        {
            InitializeComponent();
            Scanner = new ScannerBUS();
            Sign = new SignOnBUS();
            frm_Sign = new frm_SignOn();
        }
        private void btn_1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender; //Tao doi tuong button cua chinh no
            Barcode_textBox.Text += btn.Text;
        }

        private void Enter_btn_Click(object sender, EventArgs e)
        {
            if (lb_Note.Text == "Please scan barcode or input from keyboard")
                Enter_btn_Tool();
            else if (lb_Note.Text == "What your ID number?")
                Enter_btn_ID();
            else if (lb_Note.Text == "Plese input password")
                Enter_btn_Pass();
        }
        private void Enter_btn_ID()
        {
            barcode = Barcode_textBox.Text;
            EmployeeName = Sign.getNameID(barcode);
            Test = Sign.SignOn_ID(barcode);
            if (Test == true)
            {
                Barcode_textBox.Text = null;
                lb_Note.Text = "Plese input password";
                Barcode_textBox.PasswordChar = '*';
            }
            else
            {
                DialogResult result= MessageBox.Show("Your ID is wrong!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                    Barcode_textBox.Text = null;
            }
        }
        private void Enter_btn_Pass()
        {
            barcode = Barcode_textBox.Text;
            Test = Sign.SignOn_Pass(barcode);
            if (Test == true)
            {
                Barcode_textBox.Text = null;
                this.Enabled = false;
                frm_Sign.ShowDialog();
                if(frm_Sign.DialogResult== DialogResult.OK)
                {
                    this.Enabled = true;
                    sC_Tool2.Panel2.Controls.Remove(UC_Em.Instance);
                    sC_Tool2.Panel2.Controls.Add(UC_Tool.Instance);
                    UC_Tool.Instance.Dock = DockStyle.Fill;
                    lb_Note.Text = "Please scan barcode or input from keyboard";
                    Barcode_textBox.PasswordChar = '\0';
                }
                if (frm_Sign.DialogResult == DialogResult.Cancel)
                {
                    frm_Sign.Close();
                    this.Enabled = true;
                    lb_Note.Text = "Choose Sign ON button to enter the system or Exit button to exit";
                    sC_Tool2.Panel2.Controls.Remove(UC_Em.Instance);
                    sC_Tool2.Panel2.Controls.Add(UC_SignOn.Instance);
                    UC_SignOn.Instance.Dock = DockStyle.Fill;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Your password is wrong!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                    Barcode_textBox.Text = null;
            }
        }
        private void Enter_btn_Tool()
        {
            try
            {
                barcode = Barcode_textBox.Text;
                Test = Scanner.GetBarcode(barcode);
                if (Test == true)
                {
                    Barcode_textBox.Text = null;
                    pictureBox1.Visible = false;
                    //pictureBox1.Dock = DockStyle.Bottom;
                    listBox1.Items.Add("  --   " + Scanner.GetProducts(barcode).proName + "                         " +
                        "                " + Scanner.GetProducts(barcode).netW + "                   " + Scanner.GetProducts(barcode).sellingP.ToString("###,###"));
                    listBox1.Items.Add("       " + barcode + "@1");
                    listBox1.Items.Add(string.Format(Environment.NewLine));
                }
                else //if(Test==false || Barcode_textBox.Text=="")
                {
                    Barcode_textBox.Text = null;
                    lb_Note.Text = "Unknow barcode\n" +
                        "Plese enter different one";
                    sC_Tool2.Panel2.Controls.Remove(UC_Tool.Instance);
                    sC_Tool2.Panel2.Controls.Add(UC_OK.Instance);
                    UC_OK.Instance.Dock = DockStyle.Fill;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu\n" + ex.Message, "Connection Failed");
            }
            //b = Scanner.GetBarcode(barcode);
        }

        private void Btn_OK_Tool_Click(object sender, EventArgs e)
        {
            sC_Tool2.Panel2.Controls.Remove(UC_OK.Instance);
            sC_Tool2.Panel2.Controls.Add(UC_Tool.Instance);
            UC_Tool.Instance.Dock = DockStyle.Fill;
            lb_Note.Text = "Please scan barcode or input from keyboard";
            Barcode_textBox.Focus();
        }

        private void Bksp_btn_Click(object sender, EventArgs e)
        {
            if (Barcode_textBox.Text != "")//o barcode khong rong
                Barcode_textBox.Text = Barcode_textBox.Text.Substring(0, Barcode_textBox.Text.Length - 1);
            else//o bracode rong
                return;
        }

        private void Clear_btn_Click(object sender, EventArgs e)
        {
            Barcode_textBox.Text = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            if (EmployeeName=="")
                infor_label.Text = dt.ToString("HH:mm:ss -- dd/MM/yyyy"); 
            else
                infor_label.Text = dt.ToString("HH:mm:ss -- dd/MM/yyyy\n" + EmployeeName);
        }

        private void frm_Main_Shown(object sender, EventArgs e)
        {
            Barcode_textBox.Focus();
            timer1.Start();
            UC_SignOn.Instance.btn_SignOn.Click += Btn_SignOn_Click;
            UC_OK.Instance.btn_OK.Click += Btn_OK_Tool_Click;
        }

        private void Btn_SignOn_Click(object sender, EventArgs e)
        {
            Barcode_textBox.Text = null;
            Barcode_textBox.Focus();
            sC_Tool2.Panel2.Controls.Remove(UC_SignOn.Instance);
            sC_Tool2.Panel2.Controls.Add(UC_Em.Instance);
            UC_Em.Instance.Dock = DockStyle.Fill;
            lb_Note.Text = "What your ID number?";
        }
        private void frm_Main_Load(object sender, EventArgs e)
        {
            lb_Note.Text = "Choose Sign ON button to enter the system or Exit button to exit";
            sC_Tool2.Panel2.Controls.Add(UC_SignOn.Instance);
            UC_SignOn.Instance.Dock = DockStyle.Fill;
        }

        private void Barcode_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}