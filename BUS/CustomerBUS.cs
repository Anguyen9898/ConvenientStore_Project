using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAO;
using DTO;

namespace BUS
{
    public class CustomerBUS
    {

        public DataTable GetCustomers()
        {
            return new CustomerDAO().GetCustomers();
        }
        public void Insert(Customer cus)
        {
            CustomerDAO customer = new CustomerDAO();
            customer.Insert(cus);
        }
        public bool CheckID(string maKh)
        {
            bool chek;
            CustomerDAO customer = new CustomerDAO();
            chek = customer.CheckID(maKh);
            return chek;
        }
    }
}
