using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameMagic
{
    public static class Vector2Extensions
    {
        public static Vector2 Normalized(this Vector2 v)
        {
            Vector2 v1 = new Vector2(v.X, v.Y);
            v1.Normalize();
            return v1;
        }
    }
}
