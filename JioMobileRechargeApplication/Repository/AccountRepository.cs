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
    public class AccountRepository
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
        /// Get method to add account detail to database using stored procedure
        /// </summary>
        /// <param name="account">model with account details</param>
        /// <returns>return true if details are added to database</returns>
        public bool AddAccountDetail(AccountModel account)
        {
            try
            {
                Connection();
                SqlCommand command = new SqlCommand("SPI_AddAccount", sqlconnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@rechargedPlan", account.Rechargedplan);
                command.Parameters.AddWithValue("@data", account.Data);
                command.Parameters.AddWithValue("@voice", account.Voice);
                command.Parameters.AddWithValue("@validity", account.Validity);
                command.Parameters.AddWithValue("@sms", account.Sms);
                command.Parameters.AddWithValue("@balance", account.Balance);
                command.Parameters.AddWithValue("@userid", account.Userid);

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
        public List<AccountModel> GetAllAccount()
        {
            try { 
            Connection();
            List<AccountModel> accountlist = new List<AccountModel>();
            SqlCommand command = new SqlCommand("SPS_SelectAccount", sqlconnection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlconnection.Open();
            sda.Fill(dt);
     
            foreach (DataRow dr in dt.Rows)
            {
                accountlist.Add(new AccountModel
                {
                    Id = Convert.ToInt32(dr["id"]),
                    Rechargedplan = Convert.ToInt32(dr["rechargedPlan"]),
                    Data = Convert.ToString(dr["data"]),
                    Voice = Convert.ToString(dr["voice"]),
                    Validity = Convert.ToString(dr["validity"]),
                    Sms = Convert.ToString(dr["sms"]),
                    Balance = Convert.ToInt32(dr["balance"]),
                    Userid = Convert.ToInt32(dr["userid"]),

                });
            }
            return accountlist;
            }
            finally
            {
                sqlconnection.Close();
            }
        }
        public bool UpdateAccount(AccountModel account)
        {
            try { 
            Connection();
            SqlCommand command = new SqlCommand("SPU_UpdateAccount", sqlconnection);
            command.CommandType = CommandType.StoredProcedure;
           
            command.Parameters.AddWithValue("@rechargedPlan", account.Rechargedplan);
            command.Parameters.AddWithValue("@data", account.Data);
            command.Parameters.AddWithValue("@voice", account.Voice);
            command.Parameters.AddWithValue("@validity", account.Validity);
            command.Parameters.AddWithValue("@sms", account.Sms);
            command.Parameters.AddWithValue("@balance", account.Balance);
            command.Parameters.AddWithValue("@userid", account.Userid);

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
        public bool DeleteAccount(int Id)
        {
            try { 
            Connection();
            SqlCommand command = new SqlCommand("SPD_DeleteAccount", sqlconnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", Id);
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


        public bool UpdateAccountBalance(BalanceModel balance)
        {
            try { 
            Connection();
            SqlCommand command = new SqlCommand("SPU_UpdateAccountBalance", sqlconnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@balance", balance.Balance);
            command.Parameters.AddWithValue("@userid", balance.Userid);

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

    }
}