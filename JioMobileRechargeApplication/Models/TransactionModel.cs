using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JioMobileRechargeApplication.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [Display(Name = "Phone number")]
        public string Phonenumber { get; set; }
        [DataType(DataType.Date)]
        public String Date { get; set; }
        [Display(Name = "Recharge Plan")]
        public int Rechargeplan { get; set; }
    }
}