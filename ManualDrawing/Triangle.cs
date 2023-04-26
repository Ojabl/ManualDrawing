using System.Drawing;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace ManualDrawing
{
    class Triangle : Shape
    {
        private static Brush bg = Brushes.Yellow;
        private float ax, ay, bx, by, cx, cy;
        public Triangle(string text)
        {
            float[] tb = ParseParams(text, 6);
            int i = 0;
            ax = tb[i++];
            ay = tb[i++];
            bx = tb[i++];
            by = tb[i++];
            cx = tb[i++];
            cy = tb[i++];

        }
        public override void Draw(Graphics g, PointF shift, float scale)
        {
            g.FillPolygon
              (
                  bg,
                  new PointF[]
                  {
                     new PointF(Scale(ax, shift.X, scale), Scale(ay, shift.Y, scale)),
                     new PointF(Scale(bx, shift.X, scale), Scale(by, shift.Y, scale)),
                     new PointF(Scale(cx, shift.X, scale), Scale(cy, shift.Y, scale)),
                     new PointF(Scale(ax, shift.X, scale), Scale(ay, shift.Y, scale)),
                  }
              );
        }

        public override float MinX() { return Math.Min(ax, Math.Min(bx,cx)); }
        public override float MaxX() { return Math.Max(ax, Math.Max(bx,cx)); }
        public override float MinY() { return Math.Min(ay, Math.Min(by,cy)); }
        public override float MaxY() { return Math.Max(ay, Math.Max(by,cy)); }
    }
}
