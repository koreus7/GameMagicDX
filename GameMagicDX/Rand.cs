using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameMagic
{
    class Rand
    {

        private static Rand _instance;

        public static Rand Inst
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Rand();
                }

                return _instance;
            }
        }

        private Random sysRan;

        public Rand()
        {
            sysRan = new Random();
        }

        public int Int(int max)
        {
            return sysRan.Next(max);
        }

        public Vector2 Vec2(int xMax, int yMax)
        {
            return new Vector2(Int(xMax), Int(yMax));
        }

        public Vector2 Vec2(int max)
        {
            return new Vector2(Int(max), Int(max));
        }
    }
}
