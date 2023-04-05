using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualDrawing
{
    class Ractangle : Shape
    {
        private static Brush bg = Brushes.Blue;
        private float ax, ay,bx,by,cx,cy,dx,dy;
        public Ractangle(string text)
        {
            float[] tb = ParseParams(text, 8);
            int i = 0;
            ax = tb[i++];
            ay = tb[i++];
            bx = tb[i++];
            by = tb[i++];
            cx = tb[i++];
            cy = tb[i++];
            dx = tb[i++];
            dy = tb[i++];
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
                     new PointF(Scale(dx, shift.X, scale), Scale(dy, shift.Y, scale)),
                     new PointF(Scale(ax, shift.X, scale), Scale(ay, shift.Y, scale)),
                }
                );
        }
    }
}
