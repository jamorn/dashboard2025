using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAPI.Models;

public class MachineConfiguration : IEntityTypeConfiguration<Machine>
{
    public void Configure(EntityTypeBuilder<Machine> builder)
    {
        builder.HasData(
            new Machine { MachineId = 1, MachineName = "PP12/A", MachineClass = "g1", MachineActive = true },
            new Machine { MachineId = 2, MachineName = "PP12/C", MachineClass = "g1", MachineActive = true },
            new Machine { MachineId = 3, MachineName = "PP3/A", MachineClass = "g1", MachineActive = true },
            new Machine { MachineId = 4, MachineName = "PP3/B", MachineClass = "g1", MachineActive = true },
            new Machine { MachineId = 5, MachineName = "PPE/C", MachineClass = "g1", MachineActive = true },
            new Machine { MachineId = 6, MachineName = "PPE/D", MachineClass = "g1", MachineActive = true },
            new Machine { MachineId = 7, MachineName = "PPC/A", MachineClass = "g1", MachineActive = true },
            new Machine { MachineId = 8, MachineName = "PPC/B", MachineClass = "g1", MachineActive = true },
            new Machine { MachineId = 9, MachineName = "HDPE/A", MachineClass = "g1", MachineActive = true }
        );
    }
}
