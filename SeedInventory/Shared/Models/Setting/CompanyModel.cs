using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedInventory.Shared.Models.Setting
{
    public class CompanyModel
    {
        public int Id { get; set; }


        [Required]
        [StringLength(200)]
        public string Name { get; set; }


        [StringLength(500)]
        public string Address { get; set; }


        [Phone]
        public string Contact { get; set; }


        [EmailAddress]
        public string Email { get; set; }


        [StringLength(1000)]
        public string Description { get; set; }


        // Add other domain fields as needed, e.g., LicenseNo, Products, EstablishedYear
        public string ProductsOffered { get; set; }
    }
}
