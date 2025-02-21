namespace Extensions
{
    public static class FloatExtensions
    {
        public static float Remap(this float value, float oldMin, float oldMax, float newMin, float newMax)
        {
            return newMin + (value - oldMin) * (newMax - newMin) / (oldMax - oldMin);
        }
    }
}