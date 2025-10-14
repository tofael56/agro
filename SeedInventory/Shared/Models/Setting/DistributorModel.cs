using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedInventory.Shared.Models.Setting
{
    public class DistributorModel
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string Contact { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Address { get; set; }
    }
}
