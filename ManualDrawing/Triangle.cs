using System.Drawing;

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
    }
}
