using System;

namespace BackendAPI.DTOs;

public class MachineOEEData
    {
        public List<OEEDataDTO> OEEDatalListPIZA { get; set; } = new List<OEEDataDTO>();
        public List<OEEDataDTO> OEEDatalListPIZC { get; set; } = new List<OEEDataDTO>();
        public List<OEEDataDTO> OEEDatalListP2A { get; set; } = new List<OEEDataDTO>();
        public List<OEEDataDTO> OEEDatalListP3B { get; set; } = new List<OEEDataDTO>();
        public List<OEEDataDTO> OEEDatalListP4B { get; set; } = new List<OEEDataDTO>();
        public List<OEEDataDTO> OEEDatalListP5C { get; set; } = new List<OEEDataDTO>();
        public List<OEEDataDTO> OEEDatalListHDPEA { get; set; } = new List<OEEDataDTO>();
    }
