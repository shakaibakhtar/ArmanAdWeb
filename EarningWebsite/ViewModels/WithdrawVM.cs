using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EarningWebsite.ViewModels
{
    public class WithdrawVM
    {
        public int id { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int UserScore { get; set; }
        public int PointsToWithdraw { get; set; }
    }
}