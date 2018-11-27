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
using System.Collections;
using BUS;
using DTO;

namespace ConvenientStore_Pro
{
    public partial class frm_Main : Form
    {
        frm_SignOn SignOn;
        frm_SignOff signOff;
        frm_Sum Sum;
        ScannerBUS Scanner;
        SignOnBUS SignBus;
        Tender_CusBUS Tender_CusBUS;
        String barcode="";
        TreeNode treeNode;
        TreeNode childNode;
        TreeNode childNode2;
        String cusCode="";
        String cusName = "";
        Sum sum;
        bool Test = false;
        //double Funds = 0;
        double Tendered = 0;
       
        int i = 0;
        //int billCode = 1;
        //string i= "1";
        public int Quan { get; set; }
        public string EmployeeName { get; set; }
        public frm_Main()
        {
            InitializeComponent();
            Scanner = new ScannerBUS();
            SignBus = new SignOnBUS();
            SignOn = new frm_SignOn();
            signOff = new frm_SignOff();
            Sum = new frm_Sum();
            Tender_CusBUS = new Tender_CusBUS();
            sum = new Sum();
            UC_SignOn.Instance.btn_SignOn.Click += Btn_SignOn_Click;
            UC_OK.Instance.btn_OK.Click += Btn_OK_Tool_Click;
            UC_Tool.Instance.btn_Off.Click += Btn_Off_Click;
            UC_Tool.Instance.btn_Tender.Click += Btn_Tender_Click;
            UC_TenderCus.Instance.btn_Can.Click += Btn_Can_Click;
            UC_TenderCus.Instance.btn_Skip.Click += Btn_Skip_Click;
            UC_TenderMethod.Instance.btn_Can.Click += Btn_Can_Click1;
            UC_TenderMethod.Instance.btn_Cre.Click += Btn_Cre_Click;
            UC_TenderMethod.Instance.btn_Cash.Click += Btn_Cash_Click;
            UC_Tool.Instance.btn_Quan.Click += Btn_Quan_Click;
            UC_Tool.Instance.btn_Void.Click += Btn_Void_Click;
            UC_Tool.Instance.btn_Sup.Click += Btn_Sup_Click;
            UC_Tool.Instance.btn_Reprint.Click += Btn_Reprint_Click;
        }

        private void Btn_Reprint_Click(object sender, EventArgs e)
        {
            frm_ReprintBill frm = new frm_ReprintBill();
            frm.ShowDialog();
        }

        private void Btn_Sup_Click(object sender, EventArgs e)
        {
            List<TreeNodeCollection> list = new List<TreeNodeCollection>();
            list.Add(this.treeView1.Nodes);
            this.treeView1.Nodes.Clear();
            pictureBox1.Visible = true;
        }

        private void Btn_Void_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode==null)
            {
                return; 
            }
            else
            {
                if (treeView1.SelectedNode == childNode)
                    treeView1.SelectedNode = treeNode;
                treeView1.SelectedNode.Remove();
            }
             if (this.treeView1.Nodes.Count == 0)
                pictureBox1.Visible = true;
        }

        private void Btn_Quan_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            else
            {
                lb_Note.Text = "How many items do you sell?";
                sC_Tool2.Panel2.Controls.Remove(UC_Tool.Instance);
                sC_Tool2.Panel2.Controls.Add(UC_Em.Instance);
                UC_OK.Instance.Dock = DockStyle.Fill;
                Barcode_textBox.Text = "1";
                Barcode_textBox.Focus();
            }
        }
        public TreeNode TreePro(int Quan)
        {
            treeNode = new TreeNode("--   " + Scanner.GetProducts(barcode).proName + "                         " +
                        "                " + Scanner.GetProducts(barcode).netW + "                   " + (Quan*Scanner.GetProducts(barcode).sellingP).ToString("###,###"));
            treeNode.Expand();
            childNode = new TreeNode("       " + barcode);
            childNode.Expand();
            childNode2 = new TreeNode("       Quantity: " + Quan);
            treeNode.Nodes.Add(childNode);
            //this.treeView1.Nodes.Add(treeNode);
            return treeNode;
        }
        private void Btn_Cash_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string billCode = "#000" + i++;
            Bill bill = new Bill(billCode, dt, Tendered);
            Sum.Cash = 0;
            try
            {
                int numOfRows = Scanner.setBill(bill);
                if (numOfRows > 0)
                {
                    
                    Sum.Cash += Tendered;
                    lb_Note.Text = "Enter Customer's Cash";
                    Barcode_textBox.Focus();
                    sC_Tool2.Panel2.Controls.Remove(UC_TenderMethod.Instance);
                    sC_Tool2.Panel2.Controls.Add(UC_Em.Instance);
                    UC_Em.Instance.Dock = DockStyle.Fill;
                }
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu\n" + ex.Message, "Connection Failed");
            }
            /*DetailBill Dbill = new DetailBill("1", Scanner.GetProducts(barcode).sellingP, "", Tendered);
            if (Scanner.setdetailBill(Dbill) < 0)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu\n", "Connection Failed");
            }*/
        }

        private void Btn_Cre_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            Bill bill = new Bill("#000" + i++, dt, Tendered);
           Sum.Cre = 0;
            try
            {
                int numOfRows = Scanner.setBill(bill);
                if (numOfRows > 0)
                {
                    Sum.Cre+= Tendered;
                    Barcode_textBox.Text = null;
                    this.treeView1.Nodes.Clear();
                    treeNode = null;
                    sC_Tool2.Panel2.Controls.Remove(UC_TenderMethod.Instance);
                    sC_Tool2.Panel2.Controls.Add(UC_OK.Instance);
                    UC_OK.Instance.Dock = DockStyle.Fill;
                    lb_Note.Text = "Done";
                    Tendered = 0;
                    lbTendered.Text = "Tender:   ";
                }
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu\n" + ex.Message, "Connection Failed");
            }
        }
        private void Btn_Can_Click1(object sender, EventArgs e)
        {
            sC_Tool2.Panel2.Controls.Remove(UC_TenderMethod.Instance);
            sC_Tool2.Panel2.Controls.Add(UC_TenderCus.Instance);
            UC_TenderMethod.Instance.Dock = DockStyle.Fill;
            Barcode_textBox.Text = null;
            lb_Note.Text = "Enter Customer Code or Click Skip";
            Barcode_textBox.Focus();
        }

        private void Btn_Skip_Click(object sender, EventArgs e)
        {
            sC_Tool2.Panel2.Controls.Remove(UC_TenderCus.Instance);
            sC_Tool2.Panel2.Controls.Add(UC_TenderMethod.Instance);
            UC_TenderMethod.Instance.Dock = DockStyle.Fill;
            lb_Note.Text = "Please choose one tender-method";
            Barcode_textBox.Text = Tendered.ToString("###,###");
            Barcode_textBox.Focus();
        }
        private void Btn_Can_Click(object sender, EventArgs e)
        {
            sC_Tool2.Panel2.Controls.Remove(UC_TenderCus.Instance);
            sC_Tool2.Panel2.Controls.Add(UC_Tool.Instance); 
            UC_TenderCus.Instance.Dock = DockStyle.Fill;
            lb_Note.Text = "Please scan barcode or input from keyboard";
            Barcode_textBox.Focus();
        }

        private void Btn_Tender_Click(object sender, EventArgs e)
        {
            if (treeNode==null || Barcode_textBox.Text !="")
            {
                sC_Tool2.Panel2.Controls.Remove(UC_Tool.Instance);
                Unknow();
            }
            else //if(treeNode!=null)
            {
                lb_Note.Text = "Enter Customer Code or Click Skip";
                Barcode_textBox.Focus();
                sC_Tool2.Panel2.Controls.Remove(UC_Tool.Instance);
                sC_Tool2.Panel2.Controls.Add(UC_TenderCus.Instance);
                UC_TenderCus.Instance.Dock = DockStyle.Fill;
            }
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
            else if (lb_Note.Text == "Enter Customer Code or Click Skip")
                Enter_btn_TenderCus();
            else if (lb_Note.Text == "Enter Customer's Cash")
                Enter_ChangeDue();
            else if (lb_Note.Text == "How many items do you sell?")
                Quan = Enter_Qan();
        }
        private void Enter_btn_ID()
        {
            barcode = Barcode_textBox.Text;
            try
            {
                EmployeeName = SignBus.getNameID(barcode);
                Test = SignBus.SignOn_ID(barcode);
                SignOn.lb_Employee.Text = "#Employee: " + EmployeeName;
                if (Test == true)
                {
                    Barcode_textBox.Text = null;
                    lb_Note.Text = "Plese input password";
                    Barcode_textBox.PasswordChar = '*';
                }
                else
                {
                    DialogResult result = MessageBox.Show("Your ID is wrong!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (result == DialogResult.OK)
                        Barcode_textBox.Text = null;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu\n" + ex.Message, "Connection Failed");
            }
        }
        private void Enter_btn_Pass()
        {
            barcode = Barcode_textBox.Text;
            try
            {
                Test = SignBus.SignOn_Pass(barcode);
                if (Test == true)
                {
                    Barcode_textBox.Text = null;
                    this.Enabled = false;
                    SignOn.ShowDialog();
                    if (SignOn.DialogResult == DialogResult.OK)
                    {
                        this.Enabled = true;
                        sC_Tool2.Panel2.Controls.Remove(UC_Em.Instance);
                        sC_Tool2.Panel2.Controls.Add(UC_Tool.Instance);
                        UC_Tool.Instance.Dock = DockStyle.Fill;
                        lb_Note.Text = "Please scan barcode or input from keyboard";
                        Barcode_textBox.PasswordChar = '\0';
                        //Funds = SignOn.Funds;
                    }
                    else if (SignOn.DialogResult == DialogResult.Cancel)
                    {
                        //frm_Sign.Close();
                        this.Enabled = true;
                        lb_Note.Text = "Choose Sign ON button to enter the system or Exit button to exit";
                        sC_Tool2.Panel2.Controls.Remove(UC_Em.Instance);
                        sC_Tool2.Panel2.Controls.Add(UC_SignOn.Instance);
                        UC_SignOn.Instance.Dock = DockStyle.Fill;
                        Barcode_textBox.PasswordChar = '\0';
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Your password is wrong!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (result == DialogResult.OK)
                        Barcode_textBox.Text = null;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu\n" + ex.Message, "Connection Failed");
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
                    this.treeView1.Nodes.Add(TreePro(1));
                    //treeView1.SelectedNode = treeNode;
                    Tendered = Tendered+ Scanner.GetProducts(barcode).sellingP;
                    lbTendered.Text = "Tendered: " + Tendered.ToString("###,###");
                }
                else //if(Test==false || Barcode_textBox.Text=="")
                {
                    sC_Tool2.Panel2.Controls.Remove(UC_Tool.Instance);
                    Unknow();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu\n" + ex.Message, "Connection Failed");
            }
            //b = Scanner.GetBarcode(barcode);
        }
        public void Unknow()
        {
            Barcode_textBox.Text = null;
            lb_Note.Text = "Unknow Code\n" +
                "Plese enter different one";
            sC_Tool2.Panel2.Controls.Add(UC_OK.Instance);
            UC_OK.Instance.Dock = DockStyle.Fill;
        }
        private void Enter_btn_TenderCus()
        {
            cusCode = Barcode_textBox.Text;
            try
            {
                cusName = Tender_CusBUS.GetCustomer(cusCode).cusName;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu\n" + ex.Message, "Connection Failed");
            }
            if (cusName != "")
            {
                lbTendered.TextAlign = ContentAlignment.TopRight;
                lbTendered.Text = "Tendered: " +Tendered.ToString("###,###") + "\nKhách hàng: " + cusName + "\n MÃ KH: " + cusCode;
                sC_Tool2.Panel2.Controls.Remove(UC_TenderCus.Instance);
                sC_Tool2.Panel2.Controls.Add(UC_TenderMethod.Instance);
                UC_TenderMethod.Instance.Dock = DockStyle.Fill;
                lb_Note.Text = "Please choose one tender-method";
                Barcode_textBox.Text = Tendered.ToString("###,###");
                Barcode_textBox.Focus();
            }
            else
            {
                sC_Tool2.Panel2.Controls.Remove(UC_TenderCus.Instance);
                Unknow();
            }
        }
        private int Enter_Qan()
        {
            int i = int.Parse(Barcode_textBox.Text);
            sC_Tool2.Panel2.Controls.Remove(UC_OK.Instance);
            sC_Tool2.Panel2.Controls.Add(UC_Tool.Instance);
            UC_OK.Instance.Dock = DockStyle.Fill;
            lb_Note.Text = "Please scan barcode or input from keyboard";
            Barcode_textBox.Text = "";
            this.treeView1.Nodes.Remove(treeView1.SelectedNode);

            return i;
        }
        private void Btn_OK_Tool_Click(object sender, EventArgs e)
        {
            Barcode_textBox.Text = "";
            sC_Tool2.Panel2.Controls.Remove(UC_OK.Instance);
            sC_Tool2.Panel2.Controls.Add(UC_Tool.Instance);
            UC_Tool.Instance.Dock = DockStyle.Fill;
            lb_Note.Text = "Please scan barcode or input from keyboard";
            Barcode_textBox.Focus();
            pictureBox1.Visible = true;
        }
        private void Enter_ChangeDue()
        {
            double ChangeDue = 0;
            double Cash = double.Parse(Barcode_textBox.Text);
            if (Cash < Tendered)
            {
                MessageBox.Show("Số tiền không được nhỏ hơn giá tiền", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Barcode_textBox.Focus();
            }
            else
            {
                if (Cash == Tendered)
                    Barcode_textBox.Text = "0";
                else
                {
                    ChangeDue = Cash - Tendered;
                    lb_Note.Text = "Change Due";
                    Barcode_textBox.Text = ChangeDue.ToString("###,###");
                }
                sC_Tool2.Panel2.Controls.Remove(UC_Em.Instance);
                sC_Tool2.Panel2.Controls.Add(UC_OK.Instance);
                UC_OK.Instance.Dock = DockStyle.Fill;
                treeView1.Nodes.Clear();
                treeNode = null;
                Tendered = 0;
                lbTendered.Text = "Tender:   "; 
            }
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
            if (SignOn.DialogResult==DialogResult.OK)
                infor_label.Text = dt.ToString("HH:mm:ss -- dd/MM/yyyy\n" + EmployeeName);
            else
                infor_label.Text = dt.ToString("HH:mm:ss -- dd/MM/yyyy"); 
        }

        private void frm_Main_Shown(object sender, EventArgs e)
        {
            Barcode_textBox.Focus();
            timer1.Start();
        }

        private void Btn_Off_Click(object sender, EventArgs e)
        {
            signOff.ShowDialog();
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