using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BackendAPI.Models
{
    [Table("Dashboards")]
    public class Dashboard
    {
        [Key]
        public int Id { get; set; }
        public int MachineId { get; set; }

        [Column(TypeName = "datetime2(7)")]
        public DateTime RecordDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Availability { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Performance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quality { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OEE { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Giveaway { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string ResponsiblePerson { get; set; } = "default auto-generate";

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        [Required]
        public int Status { get; set; } = 1;

        // Navigation property
        [ForeignKey("MachineId")]
        public required virtual Machine Machine { get; set; }

        // Use ICollection for the one-to-many relationship
        public virtual ICollection<RemarkItem> RemarkItems { get; set; } = new List<RemarkItem>();
    }
}
