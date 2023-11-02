using JioMobileRechargeApplication.Models;
using JioMobileRechargeApplication.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace JioMobileRechargeApplication.Controllers
{
    public class LoginController : Controller
    {
        private SqlConnection sqlconnection;
        /// <summary>
        /// method for open sql connection to database
        /// </summary>
        public void Connection()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["getconnection"].ConnectionString;
            sqlconnection = new SqlConnection(connectionstring);
        }

        /// <summary>
        /// get method for signin page
        /// </summary>
        /// <returns>returns signin page</returns>
        public ActionResult Login()
        {
            if (TempData["Nouser"] != null)
            {
                ViewBag.Loginfo2 = TempData["Nouser"].ToString();
            }
            ModelState.Clear();
            return View();
        }
        /// <summary>
        /// post method with incomig login details
        /// </summary>
        /// <param name="login"> model with login details</param>
        /// <returns>after checking grant permission to user or admin panel</returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            try
            {
                Connection();
                SqlCommand command = new SqlCommand("SPS_AdminAuth", sqlconnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", login.Username);
                sqlconnection.Open();
                SqlDataReader sdr = command.ExecuteReader();

                if (sdr.Read())
                {

                    string dbpassword = sdr["password"].ToString();
                    AdminRepository admin = new AdminRepository();
                    string decryptedpassword = admin.Decode(dbpassword);
                    if (decryptedpassword == login.Password)
                    {
                        TempData["admin"] = login.Username.ToString();
                        sqlconnection.Close();
                        return RedirectToAction("IndexAdmin", "Admin", new { area = "" });
                    }
                    ViewBag.Loginfo = "Invalid login credentials";
                    return View(login);
                }
                else
                {
                    sqlconnection.Close();
                    return RedirectToAction("UserLogin", "Login", login);
                }
            }
            catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
        /// <summary>
        /// to check user details to grant permission to user panel
        /// </summary>
        /// <param name="login">model for login details</param>
        /// <returns>return user panel after checking login details</returns>
        public ActionResult UserLogin(LoginModel login)
        {
            try
            {
                Connection();
                SqlCommand command2 = new SqlCommand("SPS_UserAuth", sqlconnection);
                command2.CommandType = CommandType.StoredProcedure;
                command2.Parameters.AddWithValue("@username", login.Username);
                sqlconnection.Open();
                SqlDataReader sdr2 = command2.ExecuteReader();
                if (sdr2.Read())
                {
                    string dbpassword2 = sdr2["password"].ToString();
                    string inputpassword = login.Password;
                    RegisterRepository register = new RegisterRepository();
                    string decryptedpassword = register.Decode(dbpassword2);

                    if (decryptedpassword == inputpassword)
                    {
                        TempData["user"] = login.Username.ToString();
                        return RedirectToAction("IndexUser", "User", new { area = "" });
                    }
                    else
                    {
                        TempData["Nouser"] = "Invalid login credentials";
                        return RedirectToAction("Login", "Login", new { area = "" });
                    }
                }
                else
                {
                    TempData["Nouser"] = "Invalid login credentials";
                    return RedirectToAction("Login", "Login", new { area = "" });
                }
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }
    }
}

