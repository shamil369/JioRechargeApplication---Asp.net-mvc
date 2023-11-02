using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JioMobileRechargeApplication.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        [Display(Name="First name")]
        public string Firstname { get; set; }
        [Display(Name = "Last name")]
        public string Lastname { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public string Dateofbirth { get; set; }

        public string Gender { get; set; }
        [Display(Name = "Phone number")]
        public string Phonenumber { get; set; }
        [Display(Name = "Email address")]
        public string Emailaddress { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [NotMapped]
        public string Confirmpassword { get; set; }
        [Display(Name = "Profile photo")]
        public byte[] Profilephoto { get; set; }

    }
}