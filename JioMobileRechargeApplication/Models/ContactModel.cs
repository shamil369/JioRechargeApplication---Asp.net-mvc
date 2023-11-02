using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JioMobileRechargeApplication.Models
{
    public class ContactModel
    {
        public int Id { get; set; }
        [Display(Name = "First name")]
        public string Firstname { get; set; }
        [Display(Name = "Last name")]
        public string Lastname { get; set; }
        [Display(Name = "Phone number")]
        public string Phonenumber { get; set; }
        [Display(Name = "Email address")]
        public string Email { get; set; }
        public string Description { get; set; }
    }
}