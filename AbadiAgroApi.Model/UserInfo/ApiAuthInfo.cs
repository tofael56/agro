using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbadiAgroApi.Model.UserInfo
{
    public class ApiAuthInfo
    {
        [Key()]
        public string loginID { get; set; }
        public bool otpToMobile { get; set; }
        public bool otpToEmail { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string mobileOtp { get; set; }
        public string emailOtp { get; set; }
        public string token { get; set; }
        public DateTime? validateUpToMobileOtp { get; set; }
        public DateTime? validateUpToEmailOtp { get; set; }
        public DateTime? validateUpToToken { get; set; }
    }
}
