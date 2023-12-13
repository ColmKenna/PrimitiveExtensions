namespace PrimativeExtensions;

public static class DoubleExtensions
{
    public static int ToInt(this double value) => Convert.ToInt32(value);
    public static decimal ToDecimal(this double value) => Convert.ToDecimal(value);
}