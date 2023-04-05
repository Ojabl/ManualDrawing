using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualDrawing
{
    abstract class Shape
    {
        public delegate void SetFloat(float value);
        public static float[] ParseParams(string text, int size)
        {
            string[] cells = text.Split(new char[] { ' ', '\t', ';' },StringSplitOptions.RemoveEmptyEntries);
            if (size != cells.Length) throw new Exception("Invalid parameters count");
            float[] param = new float[size];
            for (int i = 0; i < param.Length; ++i) param[i] = float.Parse(cells[i]);
            return param;
        }
        public void ParseLambdas(string text, params SetFloat[] tb)
        {
            int size = tb.Length;
            string[] cells = text.Split("\t ;".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            if (size != cells.Length) throw new Exception("Invalid parameters count");
            for (int i = 0; i < size; ++i) tb[i](float.Parse(cells[i]));
        }
        public static float Scale(float value,float shift,float scale)
        {
            return value * scale + shift;
        }
        public abstract void Draw(Graphics g, PointF shift, float scale);
    }
}
