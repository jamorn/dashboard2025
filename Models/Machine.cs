using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAPI.Models
{
    [Table("Machines")]
    public class Machine
    {
        [Key]
        public int MachineId { get; set; }
        public required string MachineName { get; set; }
        public required string MachineClass { get; set; }
        public bool MachineActive { get; set; }
        public int UnitId { get; set; }
        public required string MachineLine { get; set; }

        // Navigation properties
        [ForeignKey("UnitId")]
        public required virtual UnitPLBG Unit { get; set; }
        public required virtual ICollection<Dashboard> Dashboards { get; set; }
    }
}
