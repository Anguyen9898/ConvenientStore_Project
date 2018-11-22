using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace DAO
{
    public class DataProvider
    {
        // Đối tượng dùng để kết nối. 
        SqlConnection cn;
        // Đối tượng thực hiện các câu lệnh thêm, sửa, xóa. 
        private SqlCommand sqlCommand;
        // Đối tượng dùng để lấy dữ liệu table từ các câu lệnh truy vấn. 
        private SqlDataAdapter sqlAdapter;
        // Đối tượng table dùng để chứa dữ liệu. 
        private DataTable dtTable;

        public DataProvider()
        {
            string cnStr = @"Data Source= DESKTOP-9NDS0EG;Initial Catalog=Store;Integrated Security=True";
            cn = new SqlConnection(cnStr);
        }
        public void Connect()
        {
            try
            {
                if (cn != null && cn.State == System.Data.ConnectionState.Closed)
                {
                    cn.Open();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void DisConnect()
        {
            if (cn != null && cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
        }
        public int myExcuteScalar(string sql)
        {
            SqlCommand CMD = new SqlCommand();
            CMD.Connection = cn;
            CMD.CommandText = sql;
            CMD.CommandType = System.Data.CommandType.Text;
            Connect();
            try
            {
                int number = (int)CMD.ExecuteScalar();
                return number;
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
        public SqlDataReader myExcuteReader(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.CommandType = System.Data.CommandType.Text;
            try
            {
                return (cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }
        public DataTable GetDataTable(string strCode)
        {
            dtTable = new DataTable();
            sqlAdapter = new SqlDataAdapter(strCode, cn);
            sqlAdapter.Fill(dtTable);
            return dtTable;
        }
        public int myExcuteNonQuery(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.CommandType = System.Data.CommandType.Text;
            Connect();
            try
            {
                int numOfRows = cmd.ExecuteNonQuery();
                return numOfRows;
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

    }
}