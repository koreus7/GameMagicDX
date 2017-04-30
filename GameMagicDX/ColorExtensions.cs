using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameMagic
{
    public static class ColorExtensions
    {
        public static Color MultiplyAlpha(this Color c, float mod)
        {
            var c2 = c;
            c2.A = Convert.ToByte(Math.Floor(mod*255));
            return c2;
        }
    }
}
