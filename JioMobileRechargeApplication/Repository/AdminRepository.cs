using JioMobileRechargeApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JioMobileRechargeApplication.Repository
{
    public class AdminRepository
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
        /// Get method to add admin detail to database using stored procedure
        /// </summary>
        /// <param name="obj">model with admin details</param>
        /// <returns>return true if details are added to database</returns>
        public bool AddAdminDetails(AdminModel obj)
        {
            try { 
            Connection();
            SqlCommand command = new SqlCommand("SPI_AddAdmin", sqlconnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", obj.Username);
            command.Parameters.AddWithValue("@password",Encode(obj.Password));
          
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
        /// Get method to get all admin detail to database using stored procedure
        /// </summary>
        /// <returns>return list if details are found in database</returns>
        public List<AdminModel> GetAllAdmin()
        {
            try { 
            Connection();
            List<AdminModel> adminlist = new List<AdminModel>();
            SqlCommand command = new SqlCommand("SPS_SelectAdmin", sqlconnection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlconnection.Open();
            sda.Fill(dt);
  
            foreach (DataRow dr in dt.Rows)
            {
                adminlist.Add(new AdminModel
                {
                    Id = Convert.ToInt32(dr["id"]),
                    Username = Convert.ToString(dr["username"]),
                    Password = Convert.ToString(dr["password"]),
                });
            }
            return adminlist;
            }
            finally
            {
                sqlconnection.Close();
            }
        }
        /// <summary>
        /// Get method to update admin detail to database using stored procedure
        /// </summary>
        /// <returns>return true if details are updated in database</returns>
        public bool UpdateAdmin(AdminModel obj)
        {
            try { 
            Connection();
            SqlCommand command = new SqlCommand("SPU_UpdateAdmin", sqlconnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", obj.Id);
            command.Parameters.AddWithValue("@username", obj.Username);
            command.Parameters.AddWithValue("@password", Encode(obj.Password));
           
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
        /// Get method to delete admin detail to database using stored procedure
        /// </summary>
        /// <returns>return true if details are found in database are deleted</returns>
        public bool DeleteAdmin(int Id)
        {
            try { 
            Connection();
            SqlCommand command = new SqlCommand("SPD_DeleteAdmin", sqlconnection);
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

        /// <summary>
        /// method to encode password
        /// </summary>
        /// <param name="password">valu of password in string</param>
        /// <returns>return ecrpted password base64convertion</returns>
        /// <exception cref="Exception"></exception>
        public string Encode(string password)
        {
            try
            {
                byte[] encryptbyte = new byte[password.Length];
                encryptbyte = System.Text.Encoding.UTF8.GetBytes(password);
                string encrypteddata = Convert.ToBase64String(encryptbyte);
                return encrypteddata;
            }
            catch (Exception ex)
            {
                throw new Exception("Error code" + ex.Message);
            }
        }
        /// <summary>
        /// decodede password
        /// </summary>
        /// <param name="encryptedata"></param>
        /// <returns>return string after decoded encrypted password</returns>
        /// <exception cref="Exception"></exception>
        public string Decode(string encryptedata)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8decode = encoder.GetDecoder();
                byte[] decodebyte = Convert.FromBase64String(encryptedata);
                int charcount = utf8decode.GetCharCount(decodebyte, 0, decodebyte.Length);
                char[] decodechar = new char[charcount];
                utf8decode.GetChars(decodebyte, 0, decodebyte.Length, decodechar, 0);
                string decrypteddata = new string(decodechar);
                return decrypteddata;
            }
            catch (Exception ex)
            {
                throw new Exception("Error code" + ex.Message);
            }
        }


    }
}