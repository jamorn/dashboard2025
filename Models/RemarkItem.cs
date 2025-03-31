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
        
        [Required]
        public int MachineId { get; set; }
        
        [Required]
        [Column(TypeName = "datetime2(7)")]
        public DateTime RecordDate { get; set; }
        
        [Column(TypeName = "nvarchar(max)")]
        public string? ItemText { get; set; }

        // Navigation properties
        [ForeignKey("MachineId")]
        public required virtual Machine Machine { get; set; }
        public required virtual Dashboard Dashboard { get; set; }
    }
}