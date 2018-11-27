using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvenientStore_Pro
{
    public partial class frm_Sum : Form
    {
        public double Doanhthu { get; set; }
        public double Funds { get; set; }
        public double Cash { get; set; }
        public double Cre { get; set; }
        public frm_Sum()
        {
            InitializeComponent();
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btn_Can_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Sum_Load(object sender, EventArgs e)
        {
            frm_SignOff signOff = new frm_SignOff();
            frm_Main main = new frm_Main();
            frm_SignOn signOn = new frm_SignOn();
            //double dt= signOff.Doanhthu;
            //double tt = (main.Cash + signOn.Funds);
            lb_Cash.Text = Doanhthu.ToString();
            lbTTCash.Text = (Cash +Funds).ToString();
           // lbCLCash.Text = ().ToString();
        }

        private void lbTTCash_Click(object sender, EventArgs e)
        {

        }
    }
}
