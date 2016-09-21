using System.Runtime.InteropServices;

namespace ColorPicker.Utilities
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        private int xPos;
        private int yPos;

        public POINT(int x, int y)
        {
            this.xPos = x;
            this.yPos = y;
        }

        public int X
        {
            get { return xPos; }
            set { xPos = value; }
        }

        public int Y
        {
            get { return yPos; }
            set { yPos = value; }
        }
    }
}
