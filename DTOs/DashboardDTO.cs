using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAPI.DTOs
{
    public class DashboardDTO
    {
        public string? RecordDate { get; set; }
        public string? MachineId { get; set; }
        public string? Availability { get; set; }
        public string? Performance { get; set; }
        public string? Quality { get; set; }
        public string? OEE { get; set; }
        public string? Giveaway { get; set; }
        public string? RemarkItems { get; set; }
    }
}