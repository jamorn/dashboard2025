using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAPI.Models
{
    [Table("Machines")]
    public class Machine
    {
        [Key]
        public int MachineId { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string MachineName { get; set; } = null!;
        
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string MachineClass { get; set; } = null!;
        
        [Required]
        [Column(TypeName = "bit")]
        public bool MachineActive { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(10)")]
        public string CostCenter { get; set; } = null!;
        
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string MachineLine { get; set; } = null!;

        // Navigation properties
        [ForeignKey("CostCenter")]
        public virtual UnitPLBG? Unit { get; set; }
        public virtual ICollection<Dashboard> Dashboards { get; set; } = new List<Dashboard>();
    }
}
