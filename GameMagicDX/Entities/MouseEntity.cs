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
    class MouseEntity : Entity
    {
        public MouseEntity(World world, Vector2 pos) : base(world, pos)
        {
        }

        public override void Init()
        {
            this.AddNewComponent<MouseSprite>();
            this.AddNewComponent<SelectionMenu>();
        }
    }
}
