namespace PrimativeExtensions;


public static class DecimalExtensions
{
    public static int ToInt(this decimal value) => Convert.ToInt32(value);
    public static double ToDouble(this decimal value) => Convert.ToDouble(value);
}