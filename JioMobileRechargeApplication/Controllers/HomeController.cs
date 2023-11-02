using JioMobileRechargeApplication.Models;
using JioMobileRechargeApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JioMobileRechargeApplication.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Method for getting HomePage
        /// </summary>
        /// <returns>returns HomePage of application </returns>
        public ActionResult Index()
        {
            try { 
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
        /// Get Method for getting Abot page
        /// </summary>
        /// <returns>returns About of application </returns>
        public ActionResult About()
        {
            return View();
        }
        /// <summary>
        /// Get Method for getting Contact page
        /// </summary>
        /// <returns>returns Contact page of application </returns>
        public ActionResult AddContactDetails()
        {
            return View();
        }

        /// <summary>
        /// post method with enquiry details form in contact page
        /// </summary>
        /// <param name="contact">model enquiry details</param>
        /// <returns>add enquiry details to database and returns contact page</returns>
        [HttpPost]
        public ActionResult AddContactDetails(ContactModel contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactRepository contactrepository = new ContactRepository();
                    if (contactrepository.AddContactDetail(contact))
                    {
                        ViewBag.Message = "details added successfully";
                        return View();
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

    }
}