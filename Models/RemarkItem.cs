using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAPI.Models
{
    [Table("RemarkItems")]
    public class RemarkItem
    {
        [Key]
        public int Id { get; set; }
        public int MachineId { get; set; }
        public DateTime RecordDate { get; set; }
        public string? ItemText { get; set; }

        [ForeignKey("MachineId")]
        public required virtual Machine Machine { get; set; }
    }
}