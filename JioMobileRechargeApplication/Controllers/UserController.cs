using JioMobileRechargeApplication.Models;
using JioMobileRechargeApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace JioMobileRechargeApplication.Controllers
{
    public class UserController : Controller
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
        /// get method for user panel after login
        /// </summary>
        /// <returns>returns user panel</returns>
        public ActionResult IndexUser()
        {
            try
            {
                      if (TempData["user"] != null)
                      {
                          Session["Username"] = TempData["user"].ToString();
                      }
                      if (Session["Username"] != null)
                      {
                          RegisterRepository register = new RegisterRepository();
                          RegisterModel signup = register.GetAllSignup().Find(reg=> reg.Username == Session["Username"].ToString());
                          string dates = signup.Dateofbirth;
                          string smalldate = dates.Substring(0, 10);
                          signup.Dateofbirth = smalldate;
                          ViewBag.User = signup;
                          return View();
                      }
                      else
                      {
                          return RedirectToAction("Login", "Login", new { area = "" });
                      } 
            }
            catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutUser", "User", new { area = "" });
            }
        }
        /// <summary>
        /// Get method for recharge from user panel, add transaction details
        /// </summary>
        /// <param name="id">id of recharge plan</param>
        /// <returns> recharge user account with selected plan and show recharge succesful message</returns>
        public ActionResult Recharge(int id)
        {
            try
            {
                TransactionModel transaction = new TransactionModel();
                PlanRepository planrepository = new PlanRepository();
                PlanModel plan = planrepository.GetAllPlan().Find(reg => reg.Id == id);
                RegisterRepository register = new RegisterRepository();
                RegisterModel signup = register.GetAllSignup().Find(reg => reg.Username == Session["Username"].ToString());
                transaction.Username = signup.Username;
                transaction.Phonenumber = signup.Phonenumber;
                transaction.Date = DateTime.Now.ToShortDateString();
                transaction.Rechargeplan = plan.Price;
                TransactionRepository transactionrepository = new TransactionRepository();
      
                AccountsRepository accountsrepository = new AccountsRepository();
                AccountsModel accounts = new AccountsModel();
                accounts.Rechargedplan = plan.Price;
                accounts.Data = plan.Data;
                accounts.Voice = plan.Voice;
                accounts.Validity = plan.Validity;
                accounts.Sms = plan.Sms;
                accounts.Balance = 0;
                accounts.Extradata = "nil";
                DateTime enddate = DateTime.Now;
                string validity = plan.Validity.ToString().Substring(0, 2);
                int Addday = Convert.ToInt32(validity);
                DateTime expdate = enddate.AddDays(Addday);
                accounts.Expiry = Convert.ToString(expdate);
                accounts.Userid = signup.Id;

                AccountsModel orginalaccount = accountsrepository.GetAllAccounts().Find(acc => acc.Userid == signup.Id);
                DateTime checkexpiry = DateTime.Parse(orginalaccount.Expiry);
                DateTime todaydate = DateTime.Now;
                if(todaydate >= checkexpiry)
                {
                    accountsrepository.UpdateAccounts(accounts);
                    transactionrepository.AddTransactionDetail(transaction);
                    return View();
                }
                return RedirectToAction("NotRecharge", "User", new { area = "" });
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutUser", "User", new { area = "" });
            }
        }
        /// <summary>
        /// Get method for not recharge message when recharge the account
        /// </summary>
        /// <returns>show not recharged message</returns>
        public ActionResult NotRecharge()
        {
            return View();
        }
        /// <summary>
        /// Get method for topup from user panel, add transaction details
        /// </summary>
        /// <param name="id">id of recharge topup</param>
        /// <returns> topup user account with selected topup and show recharge succesful message</returns>
        public ActionResult Topup(int id)
        {
            try
            {
                RegisterRepository register = new RegisterRepository();
                RegisterModel signup = register.GetAllSignup().Find(reg => reg.Username == Session["Username"].ToString());
                TransactionModel transaction = new TransactionModel();
                TransactionRepository transactionrepository = new TransactionRepository();
                transaction.Username = signup.Username;
                transaction.Phonenumber = signup.Phonenumber;
                transaction.Date = DateTime.Now.ToShortDateString();
                transaction.Rechargeplan = id;
                transactionrepository.AddTransactionDetail(transaction);
           
                AccountsRepository accountsrepository = new AccountsRepository();
                AccountsModel accounts = new AccountsModel();
                accounts = accountsrepository.GetAllAccounts().Find(acc => acc.Userid == signup.Id);
                BalanceModel balance2 = new BalanceModel();
                balance2.Balance = accounts.Balance + id;
                balance2.Userid = signup.Id;
          
                    if (accountsrepository.UpdateAccountsBalance(balance2))
                    {
                       return View();
                    }
                return RedirectToAction("IndexUser", "User", new { area = "" });
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutUser", "User", new { area = "" });
            }
        }
        /// <summary>
        /// Get method for add extra data from user panel, add transaction details
        /// </summary>
        /// <param name="id">id of extra data </param>
        /// <returns> add Data to  user account with selected Extra data plan and show recharge succesful message</returns>
        public ActionResult ExtraData(int id)
        {
            try
            {
                RegisterRepository register = new RegisterRepository();
                RegisterModel signup = register.GetAllSignup().Find(reg => reg.Username == Session["Username"].ToString());
                TransactionModel transaction = new TransactionModel();
                TransactionRepository transactionrepository = new TransactionRepository();
                transaction.Username = signup.Username;
                transaction.Phonenumber = signup.Phonenumber;
                transaction.Date = DateTime.Now.ToShortDateString();
                transaction.Rechargeplan = id;
                transactionrepository.AddTransactionDetail(transaction);
                int gigabyte = id / 10;

                AccountsRepository accountsrepository = new AccountsRepository();
                AccountsModel accounts = new AccountsModel();
                accounts = accountsrepository.GetAllAccounts().Find(acc => acc.Userid == signup.Id);
                if (accounts.Extradata == "nil")
                {
                    accounts.Extradata = gigabyte + "" + "GB";
                }
                else
                {
                    string subvalue = accounts.Extradata.ToString();
                    string result = Regex.Match(subvalue, @"\d+").Value;
                    int previousGB = Int32.Parse(result);
                    int extravalue = previousGB + gigabyte;
                    accounts.Extradata = extravalue + "" + "GB";
                }
                DataModel datamodel = new DataModel();
                datamodel.Extradata = accounts.Extradata;
                datamodel.Userid = signup.Id;
                if (accountsrepository.UpdateAccountsData(datamodel))
                {
                    return View();
                }
                return RedirectToAction("IndexUser", "User", new { area = "" });
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutUser", "User", new { area = "" });
            }
        }
        /// <summary>
        /// Get method for showing particular signed user transaction detail
        /// </summary>
        /// <param name="name">name of signed user</param>
        /// <returns> return transaction detail of signed user</returns>
        public ActionResult ShowTransactionDetail(string name)
        {
            try { 
            TransactionRepository transactionrepository = new TransactionRepository();
            return View(transactionrepository.GetUserTransaction(name));
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutUser", "User", new { area = "" });
            }
        }
        /// <summary>
        /// Get method for edit user details form
        /// </summary>
        /// <param name="id">id of user</param>
        /// <returns>returns form to edit</returns>
        public ActionResult UserEditDetails(int id)
        {
            try
            {
                RegisterRepository repository = new RegisterRepository();
                RegisterModel register = repository.GetAllSignup().Find(reg => reg.Id == id);
                string decryptedpassword = repository.Decode(register.Password);
                register.Password = decryptedpassword;
                Session["imgPath"] = register.Profilephoto;
                Session["date"] = register.Dateofbirth;
                ViewBag.States = states;
                ViewBag.Citiesinstate = cityinstate;
                return View(register);
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutUser", "User", new { area = "" });
            }
        }
        /// <summary>
        /// post method for user after edit 
        /// </summary>
        /// <param name="register">details of user in model</param>
        /// /// <param name="file">details of user profile photo</param>
        /// <returns>store user edited details in database and return to user panel</returns>
        [HttpPost]
        public ActionResult UserEditDetails(RegisterModel register, HttpPostedFileBase file)
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
                            return RedirectToAction("IndexUser", "User", new { area = "" });
                        }
                        else
                        {
                            ViewBag.msg = "file shoud not exceed 1mb ";
                            return View(register);
                        }
                    }
                    else
                    {
                        register.Profilephoto = (byte[])Session["imgPath"];
                        repository.UpdateSignup(register);
                        return RedirectToAction("IndexUser", "User", new { area = "" });
                    }
                }
                return View(register);
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutUser", "User", new { area = "" });
            }
        }
        /// <summary>
        /// Get method for account view
        /// </summary>
        /// <returns>return page with account details and offers, extra data</returns>
        public ActionResult AccountView()
        {
            try { 
            RegisterRepository register = new RegisterRepository();
            RegisterModel signup = register.GetAllSignup().Find(reg => reg.Username == Session["Username"].ToString());
         //   AccountRepository accountrepository = new AccountRepository();
        //    ViewBag.Account = accountrepository.GetAllAccount().Find(reg => reg.Userid == signup.Id);

            AccountsRepository accountsrepository = new AccountsRepository();
            AccountsModel accounts = new AccountsModel(); 
            accounts = accountsrepository.GetAllAccounts().Find(reg => reg.Userid == signup.Id);
            accounts.Expiry = accounts.Expiry.ToString().Substring(0, 10);
            ViewBag.Accounts = accounts;

            PlanRepository planrepository = new PlanRepository();
            ViewBag.Plans = planrepository.GetAllPlan();

            return View();
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutUser", "User", new { area = "" });
            }
        }
        /// <summary>
        /// Get method for plans view
        /// </summary>
        /// <returns>return page with plan details to recharge and topups</returns>
        public ActionResult PlanView()
        {
            try { 
            PlanRepository planrepository = new PlanRepository();
            ViewBag.Plans = planrepository.GetAllPlan();
            return View();
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutUser", "User", new { area = "" });
            }
        }
        /// <summary>
        /// Get method to logout from user panel
        /// </summary>
        /// <returns>logout user panel and goto home page</returns>
        public ActionResult LogoutUser() 
        {
            TempData["user"] = null;
            Session["Username"] = null;
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
