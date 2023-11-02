using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JioMobileRechargeApplication.Models
{
    public class PlanModel
    {
        public int Id { get; set;}

        public int Price { get; set; }

        public string Validity { get; set; }

        public string Data { get; set; }

        public string Voice { get; set; }
        [Display(Name = "SMS")]
        public string Sms { get; set; }
    }
}