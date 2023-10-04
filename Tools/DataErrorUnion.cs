namespace Tools;

public sealed record class DataErrorUnion<T> (T? Object, string ErrorMessage = "")
{
}