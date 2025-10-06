using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbadiAgroApi.Model.Security
{
    public class LoginInfo
    {

        [Key()]
        [Required]
        public string loginID { get; set; }
        [Required]
        public string password { get; set; }

    }
}
