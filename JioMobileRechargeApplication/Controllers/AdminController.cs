using JioMobileRechargeApplication.Models;
using JioMobileRechargeApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Security;

namespace JioMobileRechargeApplication.Controllers
{
    public class AdminController : Controller
    {
        /// <summary>
        /// get method for admin panel after login
        /// </summary>
        /// <returns>returns admin panel</returns>
        public ActionResult IndexAdmin()
        {  
            try
            {
                if (TempData["admin"] != null)
                {
                    Session["Adminname"] = TempData["admin"].ToString();
                }
                if (Session["Adminname"] != null)
                { 
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
                return RedirectToAction("Login", "Login", new { area = "" });
            }    
        }
        /// <summary>
        /// get method for add admin details
        /// </summary>
        /// <returns> returns admin form add admin</returns>
        public ActionResult AddAdminDetails()
        {           
            return View();
        }
        /// <summary>
        /// post method for add admin
        /// </summary>
        /// <param name="admin">model of admin details</param>
        /// <returns> admin details are added to database and return admin list</returns>
        [HttpPost]
        public ActionResult AddAdminDetails(AdminModel admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AdminRepository adminrepository = new AdminRepository();
                    if (adminrepository.AddAdminDetails(admin))
                    {
                        ViewBag.Message = "details added successfully";
                        return RedirectToAction("GetAdminDetails");
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return View();
            }
        }
        /// <summary>
        /// get method for getting all admin details
        /// </summary>
        /// <returns> returns table with all admin details</returns>
        public ActionResult GetAdminDetails()
        {
            try { 
            if (Session["Adminname"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            AdminRepository adminrepository = new AdminRepository();
            ModelState.Clear();
            return View(adminrepository.GetAllAdmin());
            }
            catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("Login", "Login", new { area = "" });
            }
        }
        /// <summary>
        /// get method for edit admin details
        /// </summary>
        /// <param name="id">id of admin</param>
        /// <returns>return edit form to edit admin details</returns>
        public ActionResult EditAdminDetails(int id)
        {
            try { 
            if (Session["Adminname"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
                AdminRepository adminrepository = new AdminRepository();
                AdminModel admin = adminrepository.GetAllAdmin().Find(reg => reg.Id == id);
                string decryptedpassword = adminrepository.Decode(admin.Password);
                admin.Password = decryptedpassword;
                return View(admin);
            }
            catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutAdmin", "Admin", new { area = "" });
            }
        }
        /// <summary>
        /// post method for admin after edit 
        /// </summary>
        /// <param name="admin">details of admin in model</param>
        /// <returns>store admin edited details in database and return to all admin details</returns>
        [HttpPost]
        public ActionResult EditAdminDetails(AdminModel admin)
        {
            try { 
            if (ModelState.IsValid)
            {
                AdminRepository adminrepository = new AdminRepository();
                if (adminrepository.UpdateAdmin(admin))
                {
                  return RedirectToAction("GetAdminDetails");
                }     
            }
            return View(admin);
            }
            catch (Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutAdmin", "Admin", new { area = "" });
            }
        }
        /// <summary>
        /// delete admin based on id
        /// </summary>
        /// <param name="id">id of admin</param>
        /// <returns>delete admin and go back all admin details</returns>
        public ActionResult DeleteAdmin(int id)
        {
            try
            {
                AdminRepository adminrepository = new AdminRepository();
                if (adminrepository.DeleteAdmin(id))
                {
                    ViewBag.Alertmsg = "detail deleted";
                }
                return RedirectToAction("GetAdminDetails");
            }
            catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutAdmin", "Admin", new { area = "" });
            }
        }

        /// <summary>
        /// Plan Controller starts here, get method for add new plan details
        /// </summary>
        /// <param name="plancontroller">plan controller</param>
        /// <returns> returns form to add new plan details</returns>

        public ActionResult AddPlanDetails()
        {
            try { 
            if (Session["Adminname"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            return View();
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutAdmin", "Admin", new { area = "" });
            }
        }
        /// <summary>
        /// post method for add plan
        /// </summary>
        /// <param name="plan">model of plan details</param>
        /// <returns> plan details are added to database and return plan list</returns>
        [HttpPost]
        public ActionResult AddPlanDetails(PlanModel plan)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    PlanRepository planrepository = new PlanRepository();
                    if (planrepository.AddPlanDetail(plan))
                    {
                        ViewBag.Message = "details added successfully";
                        return RedirectToAction("GetPlanDetails");
                    }

                }
                return View();
            }
            catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutAdmin", "Admin", new { area = "" });
            }
        }
        /// <summary>
        /// get method for getting all plan details
        /// </summary>
        /// <returns> returns table with all plan details</returns>
        public ActionResult GetPlanDetails()
        {
            try { 
            if (Session["Adminname"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            PlanRepository planrepository = new PlanRepository();
            ModelState.Clear();
            return View(planrepository.GetAllPlan());
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutAdmin", "Admin", new { area = "" });
            }
        }
        /// <summary>
        /// get method for edit plan details
        /// </summary>
        /// <param name="id">id of plan</param>
        /// <returns>return edit form to edit plan details</returns>
        public ActionResult EditPlanDetails(int id)
        {
            try { 
            if (Session["Adminname"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            PlanRepository planrepository = new PlanRepository();
            PlanModel plan = planrepository.GetAllPlan().Find(reg => reg.Id == id);
            return View(plan);
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutAdmin", "Admin", new { area = "" });
            }
        }
        /// <summary>
        /// post method for plan after edit 
        /// </summary>
        /// <param name="plan">details of plan in model</param>
        /// <returns>store plan edited details in database and return to all plan details</returns>
        [HttpPost]
        public ActionResult EditPlanDetails(PlanModel plan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PlanRepository planrepository = new PlanRepository();

                    if (planrepository.UpdatePlan(plan))
                    {
                        return RedirectToAction("GetPlanDetails");
                    }
                }
                return View(plan);
            }
            catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutAdmin", "Admin", new { area = "" });
            }
        }
        /// <summary>
        /// delete plan based on id
        /// </summary>
        /// <param name="id">id of plan</param>
        /// <returns>delete plan and go back all plan details</returns>
        public ActionResult DeletePlan(int id)
        {
            try
            {
                PlanRepository planrepository = new PlanRepository();
                if (planrepository.DeletePlan(id))
                {
                    ViewBag.Alertmsg = "detail deleted";
                }
                return RedirectToAction("GetPlanDetails");
            }
            catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutAdmin", "Admin", new { area = "" });
            }
        }

        /// <summary>
        /// Contact controller starte here, Get method for all contact enquiry messages
        /// </summary>
        /// <param name="contactcontroller">Contact controller start here</param>
        /// <returns> return all contact enquiry details</returns>

        public ActionResult GetContactDetails()
        {
            try { 
            if (Session["Adminname"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            ContactRepository contactrepository = new ContactRepository();
            ModelState.Clear();
            return View(contactrepository.GetAllContact());
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutAdmin", "Admin", new { area = "" });
            }
        }
        /// <summary>
        /// get method for edit contact enquiry details
        /// </summary>
        /// <param name="id">id of contact enquiry</param>
        /// <returns>return edit form to edit enquiry details</returns>
        public ActionResult EditContactDetails(int id)
        {
            ContactRepository contactrepository = new ContactRepository();
            ContactModel contact = contactrepository.GetAllContact().Find(reg => reg.Id == id);
            return View(contact);
        }
        /// <summary>
        /// post method for conatct enquiry after edit 
        /// </summary>
        /// <param name="contact">details of enquiry in model</param>
        /// <returns>store enquiry edited details in database and return to all enquiry details</returns>
        [HttpPost]
        public ActionResult EditContactDetails(ContactModel contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactRepository contactrepository = new ContactRepository();

                    if (contactrepository.UpdateContact(contact))
                    {
                        return RedirectToAction("GetContactDetails");
                    }
                }
                return View(contact);
            }
            catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutAdmin", "Admin", new { area = "" });
            }
        }
        /// <summary>
        /// delete enquiry based on id
        /// </summary>
        /// <param name="id">id of enquiry</param>
        /// <returns>delete enquiry and go back all enquiry details</returns>
        public ActionResult DeleteContact(int id)
        {
            try
            {
                ContactRepository contactrepository = new ContactRepository();
                if (contactrepository.DeleteContact(id))
                {
                    ViewBag.Alertmsg = "detail deleted";
                }
                return RedirectToAction("GetContactDetails");
            }
            catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutAdmin", "Admin", new { area = "" });
            }
        }
        /// <summary>
        /// Transaction controller,Get method for all Transaction details
        /// </summary>
        /// <param name="transactioncontroller">transaction controller start here</param>
        /// <returns>return all transaction details of all users</returns>

        public ActionResult GetTransactionDetail()
        {
            try { 
            if (Session["Adminname"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            TransactionRepository transactionrepository = new TransactionRepository();
            return View(transactionrepository.GetAllTransaction());
            }catch(Exception ex)
            {
                ErrorRepository errorrepository = new ErrorRepository();
                errorrepository.ErrorLog(ex);
                return RedirectToAction("LogoutAdmin", "Admin", new { area = "" });
            }
        }
        /// <summary>
        /// method for looging out from admin panel
        /// </summary>
        /// <returns>logout from admin panel and goto home page</returns>
        public ActionResult LogoutAdmin()
        {
            TempData["admin"] = null;
            Session["Adminname"] = null;
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

    }
}
