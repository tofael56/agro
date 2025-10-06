using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbadiAgroApi.Model.UserInfo
{
    public class ResetPasswordRequest
    {
        public string EmployeeCode { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
