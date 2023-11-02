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
    public class ContactRepository
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
        /// Get method to add enquiry detail to database using stored procedure
        /// </summary>
        /// <param name="contact">model with enquiry details</param>
        /// <returns>return true if details are added to database</returns>
        public bool AddContactDetail(ContactModel contact)
        {
            try
            {
                Connection();
                SqlCommand command = new SqlCommand("SPI_AddContacts", sqlconnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@firstname", contact.Firstname);
                command.Parameters.AddWithValue("@lastname", contact.Lastname);
                command.Parameters.AddWithValue("@phonenumber", contact.Phonenumber);
                command.Parameters.AddWithValue("@email", contact.Email);
                command.Parameters.AddWithValue("@description", contact.Description);


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
        /// Get method to get all enquiry detail to database using stored procedure
        /// </summary>
        /// <returns>return list if details are found in database</returns>
        public List<ContactModel> GetAllContact()
        {
            try { 
            Connection();
            List<ContactModel> contactlist = new List<ContactModel>();
            SqlCommand command = new SqlCommand("SPS_SelectContacts", sqlconnection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlconnection.Open();
            sda.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                contactlist.Add(new ContactModel
                {
                    Id = Convert.ToInt32(dr["id"]),
                    Firstname = Convert.ToString(dr["firstname"]),
                    Lastname = Convert.ToString(dr["lastname"]),
                    Phonenumber = Convert.ToString(dr["phonenumber"]),
                    Email = Convert.ToString(dr["email"]),
                    Description = Convert.ToString(dr["description"]),
                });
            }
            return contactlist;
            }
            finally
            {
                sqlconnection.Close();
            }
        }
        /// <summary>
        /// Get method to update enquiry detail to database using stored procedure
        /// </summary>
        /// <returns>return true if details are updated in database</returns>
        public bool UpdateContact(ContactModel contact)
        {
            try { 
            Connection();
            SqlCommand command = new SqlCommand("SPU_UpdateContacts", sqlconnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", contact.Id);
            command.Parameters.AddWithValue("@firstname", contact.Firstname);
            command.Parameters.AddWithValue("@lastname", contact.Lastname);
            command.Parameters.AddWithValue("@phonenumber", contact.Phonenumber);
            command.Parameters.AddWithValue("@email", contact.Email);
            command.Parameters.AddWithValue("@description", contact.Description);

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
        /// Get method to delete enquiry detail to database using stored procedure
        /// </summary>
        /// <returns>return true if details are found in database are deleted</returns>
        public bool DeleteContact(int Id)
        {
            try { 
            Connection();
            SqlCommand command = new SqlCommand("SPD_DeleteContacts", sqlconnection);
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

    }
}