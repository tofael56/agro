using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedInventory.Shared.Models
{
    public class Seed
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string Variety { get; set; } = string.Empty;
        [StringLength(200)]
        public string BatchNo { get; set; } = string.Empty;

        public int UnitId { get; set; }
        public string? UnitName { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow;

        // Optional supplier for Receive, or recipient for Issue
        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }
    }
}
