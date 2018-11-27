using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DAO;
using DTO;

namespace DAO
{
    public class ScannerDAO : DataProvider
    {
        public bool GetBarcode(String barcode)
        {
            string sql = "SELECT COUNT (*) FROM Product WHERE Barcode= '" + barcode + "'";
            int number;
            try
            {
                number = myExcuteScalar(sql);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            if (number > 0)
            {
                return true;
            }
            else
                return false;
        }
        public Product GetProducts(string barcode)
        {
            string sql = "SELECT* FROM Product WHERE Barcode = '" + barcode + "'";
            string proN = "";
            float sellingP = 0;
            string netW = "";
            Product pro;
            try
            {
                Connect();
                SqlDataReader dr = myExcuteReader(sql);
                while (dr.Read())
                {
                    proN = dr[1].ToString();
                    sellingP = float.Parse(dr[5].ToString());
                    netW = dr[7].ToString();
                }
                pro = new Product(proN, netW, sellingP);
                dr.Close();
                return pro;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                DisConnect();
            }
        }
        public int setBill(Bill bill)
        {
            String sql = "INSERT INTO Bill (billcode, dayofSale, totalMoney) VALUES('" + bill.billCode + "', '"   + bill.day + "', " + bill.total + ")" ;
            try
            {
                return myExcuteNonQuery(sql);
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }
        public List<Bill> GetBill()
        {
            string sql = "SELECT* FROM Bill";
            string billCode = "";
            DateTime day = new DateTime();
            double total = 0;
            //Bill bill;
            List<Bill> list = new List<Bill>();
            try
            {
                Connect();
                SqlDataReader dr = myExcuteReader(sql);
                while (dr.Read())
                {
                    billCode = dr[0].ToString();
                    day = DateTime.Parse(dr[3].ToString());
                    total = double.Parse(dr[4].ToString());
                    Bill bill = new Bill(billCode, day, total);
                    list.Add(bill);
                }
                dr.Close();
                return list;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                DisConnect();
            }
        }
        public int setDetailBill(DetailBill detailBill)
        {
            String sql = "INSERT INTO detailBill (Quantity, Price, Discount, toMoney) VALUES('"+ detailBill.Quan + "' , " + detailBill.price + ", '" + detailBill.Discount + "', " + detailBill.total+ ")";
            try
            {
                return myExcuteNonQuery(sql);
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }
    }
}