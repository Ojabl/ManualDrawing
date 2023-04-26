using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualDrawing
{
    class Circle : Shape
    {
        private static Brush bg = Brushes.Red;
        private float x, y, r;
        public Circle(string text)
        {

            float[] tb = ParseParams(text, 3);
            int i = 0;
            x = tb[i++];
            y = tb[i++];
            r = tb[i++];
        }

        public override void Draw(Graphics g, PointF shift, float scale)
        {
            g.FillEllipse
            (
                bg,
                Scale(x - r, shift.X, scale),
                Scale(y - r, shift.Y, scale),
                Scale(2 * r, 0, scale),
                Scale(2 * r, 0, scale)
            );
        }

        public override float MinX() { return Math.Min(x - r, x + r); }
        public override float MaxX() { return Math.Max(x - r, x + r); }
        public override float MinY() { return Math.Min(y - r, y + r); }
        public override float MaxY() { return Math.Max(y - r, y + r); }
    }
}
