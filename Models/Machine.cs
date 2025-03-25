using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models
{
    public class Machine
    {
        public int MachineId { get; set; }
        [Required]
        [StringLength(50)]
        public required string MachineName { get; set; }
        [Required]
        [StringLength(10)]
        public required string MachineClass { get; set; }
        public bool MachineActive { get; set; }
         public virtual ICollection<RemarkItem> RemarkItems { get; set; } = new List<RemarkItem>();
    }
}
