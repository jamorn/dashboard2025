using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAPI.Models
{
    [Table("UnitPLBG")]
    public class UnitPLBG
    {
        [Key]
        public int UnitId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string UnitName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string CostCenter { get; set; } = string.Empty;

        // Navigation property
        public virtual ICollection<Machine> Machines { get; set; } = new List<Machine>();
    }
}