using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JioMobileRechargeApplication.Models
{
    public class AccountsModel
    {
        public int Id { get; set; }

        public int Rechargedplan { get; set; }
        public string Data { get; set; }
        public string Voice { get; set; }
        public string Validity { get; set; }
        public string Sms { get; set; }
        public float Balance { get; set; }
        public string Extradata { get; set; }
        public string Expiry  { get; set; }

        public int Userid { get; set; }
    }
}