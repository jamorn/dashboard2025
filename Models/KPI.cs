using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAPI.Models
{
    [Table("KPI")]
    public class KPI
    {
        [Key]
        public int Item { get; set; }
        public int Year { get; set; }
        public int UnitId { get; set; }
        [Column(TypeName = "decimal(10,3)")]
        public decimal? Waste_Pellet_Target { get; set; }
        [Column(TypeName = "decimal(10,3)")]
        public decimal? Waste_Film_Target { get; set; }
        [Column(TypeName = "decimal(10,3)")]
        public decimal? GiveAway_Target { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal? Oee_Target { get; set; }
        [Column(TypeName = "decimal(10,3)")]
        public decimal? GiveAwayMin { get; set; }
        [Column(TypeName = "decimal(10,3)")]
        public decimal? GiveAwayMax { get; set; }

        [ForeignKey("UnitId")]
        public required virtual UnitPLBG Unit { get; set; }
    }
}