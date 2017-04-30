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
    class Goo : Entity
    {
        public Goo(World world, Vector2 pos) : base(world, pos)
        {
        }


        public override void Init()
        {
            var sr = this.AddNewComponent<SpriteRenderer>();
            sr.Center = true;
            sr.BatchNo = 666;
            sr.tex = StaticImg.sprite512;
            var r = this.AddNewComponent<RectColider>();
            r.WatchCollisions = true;
            r.rect = new Rectangle(0, 0, 350, 350);
        }

    }
}
