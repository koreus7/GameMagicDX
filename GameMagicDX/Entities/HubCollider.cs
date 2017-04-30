using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.Components;
using GameMagic.ComponentSystem.Implementation;
using Microsoft.Xna.Framework;

namespace GameMagic.Entities
{
    class HubCollider : Entity
    {
        public HubCollider(World world, Vector2 pos) : base(world, pos)
        {
        }

        public override void Init()
        {
            var r = this.AddNewComponent<RectColider>();
            r.rect = new Rectangle(0,0,100,100);
        }
    }
}
