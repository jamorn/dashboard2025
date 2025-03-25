namespace BackendAPI.Models
{
    public class Machine
    {
        public int MachineId { get; set; }
        public string? MachineName { get; set; }
        public string? MachineClass { get; set; }
        public bool MachineActive { get; set; }
    }
}
