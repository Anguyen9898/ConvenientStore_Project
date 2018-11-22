using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;

namespace ConvenientStore_Pro
{
    public partial class Customer_cs : Form
    {
        
        public Customer_cs()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Customer_cs_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            CustomerBUS _Customer = new CustomerBUS();
            dataGridView1.DataSource = _Customer.GetCustomers();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Customer cus = new Customer();
                cus.MaKH = txtMa.Text.Trim();
                cus.TenKH = txtTen.Text.Trim();
                cus.DiaChi = txtDiaChi.Text.Trim();
                cus.Sdt = txtSDT.Text.Trim();
                string _MaKH = txtMa.Text.Trim();
                CustomerBUS cusBus = new CustomerBUS();
                if (!(cusBus.CheckID(_MaKH)))
                {
                    cusBus.Insert(cus);
                    LoadData();
                }else
                {
                    MessageBox.Show("ma KH:"+ _MaKH+" da ton tai","thong bao",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
