using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ManualDrawing
{
    abstract class Fabryka
    {
        private delegate Shape shaper(string text);
        private static Dictionary<string, shaper> shapes = new Dictionary<string, shaper>()
        {
            {"Circle",(text)=>new Circle(text)},
            {"Triangle",(text)=> new Triangle(text)},
            {"Ractangle",(text)=> new Ractangle(text)},
            
        };
        public static readonly string[] kinds = shapes.Keys.OrderBy(s => s).ToArray();
        public static Shape Make(string kind, string text)
        {
            return shapes[kind](text);
        }
    }
}
