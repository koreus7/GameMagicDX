using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.ComponentSystem.Implementation;
using Microsoft.Xna.Framework;

namespace GameMagic.Entities
{
    class SinkCollider : HubCollider
    {
        public SinkCollider(World world, Vector2 pos) : base(world, pos)
        {
        }
    }
}
