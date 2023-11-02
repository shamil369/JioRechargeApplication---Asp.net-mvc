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
    public class PlanRepository
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
        /// Get method to add plan detail to database using stored procedure
        /// </summary>
        /// <param name="plan">model with plan details</param>
        /// <returns>return true if details are added to database</returns>
        public bool AddPlanDetail(PlanModel plan)
        {
            try
            {
                Connection();
                SqlCommand command = new SqlCommand("SPI_AddPlans", sqlconnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@price", plan.Price);
                command.Parameters.AddWithValue("@validity", plan.Validity);
                command.Parameters.AddWithValue("@data", plan.Data);
                command.Parameters.AddWithValue("@voice", plan.Voice);
                command.Parameters.AddWithValue("@sms", plan.Sms);

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
        /// Get method to get all plan detail to database using stored procedure
        /// </summary>
        /// <returns>return list if details are found in database</returns>
        public List<PlanModel> GetAllPlan()
        {
            try
            {
                Connection();
                List<PlanModel> planlist = new List<PlanModel>();
                SqlCommand command = new SqlCommand("SPS_SelectPlans", sqlconnection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sqlconnection.Open();
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    planlist.Add(new PlanModel
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Price = Convert.ToInt32(dr["price"]),
                        Validity = Convert.ToString(dr["validity"]),
                        Data = Convert.ToString(dr["data"]),
                        Voice = Convert.ToString(dr["voice"]),
                        Sms = Convert.ToString(dr["sms"]),
                    });
                }
                return planlist;
            }
            finally
            {
                sqlconnection.Close();
            }
        }
        /// <summary>
        /// Get method to update plan detail to database using stored procedure
        /// </summary>
        /// <returns>return true if details are updated in database</returns>
        public bool UpdatePlan(PlanModel plan)
        {
            try { 
            Connection();
            SqlCommand command = new SqlCommand("SPU_UpdatePlans", sqlconnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", plan.Id);
            command.Parameters.AddWithValue("@price", plan.Price);
            command.Parameters.AddWithValue("@validity", plan.Validity);
            command.Parameters.AddWithValue("@data", plan.Data);
            command.Parameters.AddWithValue("@voice", plan.Voice);
            command.Parameters.AddWithValue("@sms", plan.Sms);

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
        /// Get method to delete plan detail to database using stored procedure
        /// </summary>
        /// <returns>return true if details are found in database are deleted</returns>
        public bool DeletePlan(int Id)
        {
            try { 
            Connection();
            SqlCommand command = new SqlCommand("SPD_DeletePlans", sqlconnection);
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