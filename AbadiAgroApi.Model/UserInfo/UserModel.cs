using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbadiAgroApi.Model.UserInfo
{
    public class UserModel
    {
        public int Userid { get; set; }
        [Key()]
        public string LoginId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string Pwd { get; set; }
        public string Salt { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool Locked { get; set; }
        public string Email_OTP { get; set; }
        public string Mobile_OTP { get; set; }
    }
}
