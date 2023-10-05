using Tools;
using Tools.Flags;

namespace Models;

public readonly record struct CabinetNumber : IStronglyTypedID<int>
{
    public CabinetNumber (int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static implicit operator CabinetNumber (int id) => new (id);

    public static explicit operator int (CabinetNumber id) => id.Value;

    public static bool operator == (int num, CabinetNumber id) =>
        num == id.Value;

    public static bool operator != (int num, CabinetNumber id) =>
        num != id.Value;

    public static int New ()
    {
        return default;
    }
}

public class Cabinet : IEntityModel
{
    public CabinetNumber Number { get; set; }
}