using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.ComponentSystem.Implementation;
using Microsoft.Xna.Framework.Graphics;

namespace GameMagic.Components
{
    class SelectionMenuItem
    {
        public Texture2D Image { get; set; }
        public int Count { get; set; }
        public Action Action { get; set; }
    }
}
