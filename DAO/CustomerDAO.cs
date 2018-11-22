using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DAO;
using DTO;

namespace DAO
{
    public class CustomerDAO: DataProvider
    {
   
        public DataTable GetCustomers()
        {
            DataTable dtTable = new DataTable();
            string sql = "SELECT * From Customer";
            try
            {
                dtTable = GetDataTable(sql);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return dtTable;

        }
        public bool CheckID(string maKh)
        {
            bool result = true;
            int number;
            string strQuery = "select COUNT (*) from where customerCode '" + maKh + "'";
            number = myExcuteScalar(strQuery);
            if (number > 0)
                return false;
            return true;
        }
        public int Insert(Customer cus)
        {
            int result = 0;
            string strQuery = "insert into Customer(customerCode, customerName, Address, SDT)";
            result = myExcuteNonQuery(strQuery);
            return result;
        }
        public int update(Customer cus)
        {
            int result = 0;
            string strQuery = "update Customer set customerName = '"+cus.TenKH+ "', Address = '" + cus.DiaChi + "' , SDT = '" + cus.Sdt + "' where customerCode '" + cus.MaKH+"'";
            result = myExcuteNonQuery(strQuery);
            return result;
        }
        public int delete(string maKh)
        {
            int result = 0;
            string strQuery = "delete from Customer where customerCode '" + maKh + "'";
            result = myExcuteNonQuery(strQuery);
            return result;
        }
    }
}