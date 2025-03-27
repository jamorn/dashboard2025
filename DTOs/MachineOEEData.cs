using System;

namespace BackendAPI.DTOs;

public class MachineOEEData
    {
        public List<OEEDataDTO> OEEDataListPP12A { get; set; } = new List<OEEDataDTO>(); //  machineOEEData.OEEDataListPP12A
        public List<OEEDataDTO> OEEDataListPP12C { get; set; } = new List<OEEDataDTO>();
        public List<OEEDataDTO> OEEDataListPP3A { get; set; } = new List<OEEDataDTO>();
        public List<OEEDataDTO> OEEDataListPP3B { get; set; } = new List<OEEDataDTO>();
        public List<OEEDataDTO> OEEDataListPPCA { get; set; } = new List<OEEDataDTO>();
        public List<OEEDataDTO> OEEDataListPPCB { get; set; } = new List<OEEDataDTO>();
        public List<OEEDataDTO> OEEDataListPPEC { get; set; } = new List<OEEDataDTO>();
        public List<OEEDataDTO> OEEDataListPPED { get; set; } = new List<OEEDataDTO>();
        public List<OEEDataDTO> OEEDataListHDPEA { get; set; } = new List<OEEDataDTO>();
    }
