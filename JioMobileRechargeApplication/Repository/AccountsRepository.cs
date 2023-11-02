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
    public class AccountsRepository
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
        public bool AddAccountsDetail(AccountsModel accounts)
        {
            try
            {
                Connection();
                SqlCommand command = new SqlCommand("SPI_AddAccounts", sqlconnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@rechargedPlan", accounts.Rechargedplan);
                command.Parameters.AddWithValue("@data", accounts.Data);
                command.Parameters.AddWithValue("@voice", accounts.Voice);
                command.Parameters.AddWithValue("@validity", accounts.Validity);
                command.Parameters.AddWithValue("@sms", accounts.Sms);
                command.Parameters.AddWithValue("@balance", accounts.Balance);
                command.Parameters.AddWithValue("@extraData", accounts.Extradata);
                command.Parameters.AddWithValue("@expiry",DateTime.Parse(accounts.Expiry));
                command.Parameters.AddWithValue("@userid", accounts.Userid);

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
        /// Get method to get all account detail to database using stored procedure
        /// </summary>
        /// <returns>return list if details are found in database</returns>
        public List<AccountsModel> GetAllAccounts()
        {
            try
            {
                Connection();
                List<AccountsModel> accountlist = new List<AccountsModel>();
                SqlCommand command = new SqlCommand("SPS_SelectAccounts", sqlconnection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sqlconnection.Open();
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    accountlist.Add(new AccountsModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Rechargedplan = Convert.ToInt32(dr["rechargedPlan"]),
                        Data = Convert.ToString(dr["data"]),
                        Voice = Convert.ToString(dr["voice"]),
                        Validity = Convert.ToString(dr["validity"]),
                        Sms = Convert.ToString(dr["sms"]),
                        Balance = Convert.ToInt32(dr["balance"]),
                        Extradata = Convert.ToString(dr["extraData"]),
                        Expiry = Convert.ToString(dr["expiry"]),
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
        /// <summary>
        /// Get method to update account detail to database using stored procedure
        /// </summary>
        /// <returns>return true if details are updated in database</returns>
        public bool UpdateAccounts(AccountsModel accounts)
        {
            try
            {
                Connection();
                SqlCommand command = new SqlCommand("SPU_UpdateAccounts", sqlconnection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@rechargedPlan", accounts.Rechargedplan);
                command.Parameters.AddWithValue("@data", accounts.Data);
                command.Parameters.AddWithValue("@voice", accounts.Voice);
                command.Parameters.AddWithValue("@validity", accounts.Validity);
                command.Parameters.AddWithValue("@sms", accounts.Sms);
                command.Parameters.AddWithValue("@balance", accounts.Balance);
                command.Parameters.AddWithValue("@extraData", accounts.Extradata);
                command.Parameters.AddWithValue("@expiry", DateTime.Parse(accounts.Expiry));
                command.Parameters.AddWithValue("@userid", accounts.Userid);

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
        /// Get method to delete account detail to database using stored procedure
        /// </summary>
        /// <returns>return true if details are found in database are deleted</returns>
        public bool DeleteAccounts(int Id)
        {
            try
            {
                Connection();
                SqlCommand command = new SqlCommand("SPD_DeleteAccounts", sqlconnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userid", Id);
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
        /// Get method to update account balance to database using stored procedure
        /// </summary>
        /// <returns>return true </returns>
        public bool UpdateAccountsBalance(BalanceModel balance)
        {
            try
            {
                Connection();
                SqlCommand command = new SqlCommand("SPU_UpdateAccountsBalance", sqlconnection);
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
        /// <summary>
        /// Get method to update account data to database using stored procedure
        /// </summary>
        /// <returns>return true </returns>
        public bool UpdateAccountsData(DataModel data)
        {
            try
            {
                Connection();
                SqlCommand command = new SqlCommand("SPU_UpdateAccountsData", sqlconnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@extraData", data.Extradata);
                command.Parameters.AddWithValue("@userid", data.Userid);

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