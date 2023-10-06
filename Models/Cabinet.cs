using Models.IDs;
using Tools.Flags;

namespace Models;

public class Cabinet : IEntityModel
{
    public CabinetNumber Number { get; set; }
}