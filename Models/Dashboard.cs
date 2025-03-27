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
        public DateTime RecordDate { get; set; }
        public decimal Availability { get; set; }
        public decimal Performance { get; set; }
        public decimal Quality { get; set; }
        public decimal OEE { get; set; }
        public decimal Giveaway { get; set; }

        // Navigation property
        [ForeignKey("MachineId")]
        public required virtual Machine Machine { get; set; }

        // เพิ่ม navigation property สำหรับ RemarkItems
        public required virtual ICollection<RemarkItem> RemarkItems { get; set; }
    }
}
