using JioMobileRechargeApplication.Models;
using JioMobileRechargeApplication.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JioMobileRechargeApplication.Controllers
{
    public class RegisterController : Controller
    {
        /// <summary>
        /// list of state and their corresponding city that need to be send to to signup page AddDetals method
        /// </summary>
        private List<string> states = new List<string> { "Kerala", "Tamilnadu", "Bangalore" };
        private Dictionary<string, List<string>> cityinstate = new Dictionary<string, List<string>> {
            {"Kerala", new List<string>{"Ernakulam","Thrissur","Idukki"} },
                {"Tamilnadu", new List<string>{"Vadavalli","Coimbatore","Chennai" } },
            {"Bangalore", new List<string>{"Nandi","Majestic","Electronic"} }
        };

        /// <summary>
        /// Method for Signup Page for user registration
        /// </summary>
        /// <returns>retirns signup page to signup new user</returns>
        public ActionResult AddDetails()
        {
            ViewBag.States = states;
            ViewBag.Citiesinstate = cityinstate;
            return View();
        }
        /// <summary>
        /// post method of signup data for storing user details
        /// </summary>
        /// <param name="register">model file containing user details</param>
        /// <param name="file"> containing profile picture of user</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddDetails(RegisterModel register, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RegisterRepository repository = new RegisterRepository();
                     

                    register.Profilephoto = new byte[file.ContentLength];
                    file.InputStream.Read(register.Profilephoto,0,file.ContentLength);
                    string firstname = register.Firstname.ToString();

                    string email = register.Emailaddress.ToString();
                    RegisterModel existsignup = new RegisterModel();
                    existsignup = repository.GetAllSignup().Find(reg => reg.Emailaddress == email);
                    if (existsignup != null) 
                    {
                        ViewBag.Userexist = "user already exist";
                        ViewBag.States = states;
                        ViewBag.Citiesinstate = cityinstate;
                        return View(register);
                    }
                    if (file.ContentLength < 1000000)
                    {
                        if (repository.AddSignup(register))
                         {                    
                        RegisterModel signup = new RegisterModel();
                        signup = repository.GetAllSignup().Find(reg => reg.Firstname == firstname);
                        AccountsRepository accountsrepository = new AccountsRepository();
                        AccountsModel accounts = new AccountsModel();
                        accounts.Rechargedplan = 0;
                        accounts.Data = "nil";
                        accounts.Voice = "nil";
                        accounts.Validity = "nil";
                        accounts.Sms = "nil";
                        accounts.Balance = 0;
                        accounts.Extradata = "nil";
                        DateTime enddate = DateTime.Now;
                        int Addday = 28;
                        DateTime expdate = enddate.AddDays(Addday);
                        accounts.Expiry = Convert.ToString(enddate);
                        accounts.Userid =Convert.ToInt32(signup.Id);
                        accountsrepository.AddAccountsDetail(accounts);
                        
                        ViewBag.Message = "details added successfully";
                        return RedirectToAction("Login","Login",new { area = "" });
                        }
                    }
                    else
                    {
                        ViewBag.Msg = "file shoud not exceed 1mb ";
                        return View(register);
                    }
                }
                ViewBag.States = states;
                ViewBag.Citiesinstate = cityinstate;
                return View(register);
            }
            catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
        /// <summary>
        /// Method for getting details of every user signup in application
        /// </summary>
        /// <returns>returns user details of every user</returns>
        public ActionResult GetDetails()
        {
            try
            {
                RegisterRepository repository = new RegisterRepository();
                ModelState.Clear();
                return View(repository.GetAllSignup());
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
        /// <summary>
        /// method for edit page of user 
        /// </summary>
        /// <param name="id">id of user</param>
        /// <returns>returns edit form to edit registered user details</returns>
        public ActionResult EditDetails(int id)
        {
            try { 
            RegisterRepository repository = new RegisterRepository();
            RegisterModel register = repository.GetAllSignup().Find(reg => reg.Id == id);
            string decryptedpassword = repository.Decode(register.Password);
            register.Password = decryptedpassword;
            Session["imgPath"] = register.Profilephoto;
            Session["date"] = register.Dateofbirth;
            return View(register);
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
        /// <summary>
        /// post method that contains edited information of user
        /// </summary>
        /// <param name="register">user details</param>
        /// <param name="file">profile picture of user</param>
        /// <returns>store user details and returns every user details</returns>
        [HttpPost]
        public ActionResult EditDetails(RegisterModel register, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RegisterRepository repository = new RegisterRepository();
                    if (register.Dateofbirth == null)
                    {
                        string date = Session["date"].ToString();
                        register.Dateofbirth = date;
                    }
                    if (file != null)
                    {
                        register.Profilephoto = new byte[file.ContentLength];
                        file.InputStream.Read(register.Profilephoto, 0, file.ContentLength);

                        if (file.ContentLength < 1000000)
                        {
                            if (repository.UpdateSignup(register))
                            {

                            }
                            return RedirectToAction("GetDetails");
                        }
                        else
                        {
                            ViewBag.Msg = "file shoud not exceed 1mb ";
                            return View();
                        }
                    }
                    else
                    {
                        register.Profilephoto = (byte[])Session["imgPath"];
                        repository.UpdateSignup(register);
                        if (TempData["user"] != null)
                        {
                            return RedirectToAction("IndexUser", "User", new { area = "" });
                        }
                        return RedirectToAction("GetDetails");
                    }
                }
                return View(register);
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
        /// <summary>
        /// method for delete user
        /// </summary>
        /// <param name="id">id of user</param>
        /// <returns>delete user data from database</returns>
        public ActionResult Deletedetail(int id)
        {
            try
            {
                RegisterRepository repository = new RegisterRepository();
                if (repository.DeleteSignup(id))
                {
                    ViewBag.Alertmsg = "detail deleted";
                }
                return RedirectToAction("GetDetails");
            }
            catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }

    }
}
