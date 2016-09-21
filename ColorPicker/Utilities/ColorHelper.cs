
namespace ColorPicker.Utilities
{
    public class ColorHelper
    {
        public static byte GetRValue(uint rgb)
        {
            return (byte)rgb;
        }

        public static byte GetGValue(uint rgb)
        {
            return (byte)(((ushort)(rgb)) >> 8);
        }

        public static byte GetBValue(uint rgb)
        {
            return (byte)(rgb >> 16);
        }
    }
}
