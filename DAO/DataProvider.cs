using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace DAO
{
    public class DataProvider
    {
        SqlConnection cn;
        public DataProvider()
        {
            string cnStr = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=Store;Integrated Security=True";
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