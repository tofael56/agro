using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbadiAgroApi.Model.UserInfo
{
    public class Password
    {
        [Key]
        public string loginID { get; set; }
        //public Boolean isForgotPassword { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
        public string Salt { get; set; }
    }
}
