namespace ModelsLibrary.Infrastructure;

static class UtilsMethods
{

    public static int RandomValue(int valueMin, int valueMax) => Random.Shared.Next(valueMin, valueMax + 1);
    public static double RandomValue(double valueMin, double valueMax) => Random.Shared.NextDouble() * (valueMin + (valueMax - valueMin));
}
