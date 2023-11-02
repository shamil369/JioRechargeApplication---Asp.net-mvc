using JioMobileRechargeApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace JioMobileRechargeApplication.Repository
{
    public class TransactionRepository
    {
        /// <summary>
        /// method to open connection to sql in Databse
        /// </summary>
        private SqlConnection sqlconnection;
        public void Connection()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["getconnection"].ConnectionString;
            sqlconnection = new SqlConnection(connectionstring);
        }
        /// <summary>
        /// Get method to add detail to database using stored procedure
        /// </summary>
        /// <param name="transaction">model with plan details</param>
        /// <returns>return true if details are added to database</returns>
        public bool AddTransactionDetail(TransactionModel transaction)
        {
            try
            {
                Connection();
                SqlCommand command = new SqlCommand("SPI_AddTransactionTable", sqlconnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", transaction.Username);
                command.Parameters.AddWithValue("@phonenumber", transaction.Phonenumber);
                command.Parameters.AddWithValue("@date", DateTime.Parse(transaction.Date));
                command.Parameters.AddWithValue("@rechargePlan", transaction.Rechargeplan);

                sqlconnection.Open();
                int i = command.ExecuteNonQuery();

                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                sqlconnection.Close();
            }
        }
        /// <summary>
        /// Get method to get all detail to database using stored procedure
        /// </summary>
        /// <returns>return list if details are found in database</returns>
        public List<TransactionModel> GetAllTransaction()
        {
            try { 
            Connection();
            List<TransactionModel> transactionlist = new List<TransactionModel>();
            SqlCommand command = new SqlCommand("SPS_SelectTransactionTable", sqlconnection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlconnection.Open();
            sda.Fill(dt);
  
            foreach (DataRow dr in dt.Rows)
            {
                transactionlist.Add(new TransactionModel
                {
                    Id = Convert.ToInt32(dr["id"]),
                    Username = Convert.ToString(dr["username"]),
                    Phonenumber = Convert.ToString(dr["phonenumber"]),
                    Date = Convert.ToString(dr["date"]).Substring(0, 10),
                    Rechargeplan = Convert.ToInt32(dr["rechargePlan"]),
                });
            }
            return transactionlist;
            }
            finally
            {
                sqlconnection.Close();
            }
        }
        /// <summary>
        /// Get method to get user transaction detail to database using stored procedure
        /// </summary>
        /// <returns>return list if details are in database</returns>
        public List<TransactionModel> GetUserTransaction(string username)
        {
            try { 
            Connection();
            List<TransactionModel> transactionlist = new List<TransactionModel>();
            SqlCommand command = new SqlCommand("SPS_UserTransaction", sqlconnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", username);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlconnection.Open();
            sda.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                transactionlist.Add(new TransactionModel
                {
                    Id = Convert.ToInt32(dr["id"]),
                    Username = Convert.ToString(dr["username"]),
                    Phonenumber = Convert.ToString(dr["phonenumber"]),
                    Date = Convert.ToString(dr["date"]).Substring(0, 10),
                    Rechargeplan = Convert.ToInt32(dr["rechargePlan"]),
                });
            }
            return transactionlist;
            }
            finally
            {
                sqlconnection.Close();
            }
        }
    }
}