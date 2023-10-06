namespace Models.IDs;

public interface IStronglyTypedID<T> where T : struct
{
    T Value { get; }

    abstract static T New ();
}