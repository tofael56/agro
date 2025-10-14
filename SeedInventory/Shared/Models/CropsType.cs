using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedInventory.Shared.Models
{
    public class CropsType
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        public string Variety { get; set; }
    }
}
