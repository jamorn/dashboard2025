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
        public required string UnitName { get; set; }
        public required string costcenter { get; set; }

        // Navigation property
        public required virtual ICollection<Machine> Machines { get; set; }
    }
}