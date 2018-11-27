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
    public partial class frm_ReprintBill : Form
    {
        public frm_ReprintBill()
        {
            InitializeComponent();
        }

        private void frm_ReprintBill_Load(object sender, EventArgs e)
        {
            ScannerBUS scannerBUS = new ScannerBUS();
            try
            {
                dgvBill.DataSource = scannerBUS.GetBill();
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu\n" + ex.Message, "Connection Failed");
            }
        }

        private void btn_App_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
