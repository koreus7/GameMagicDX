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
    class Sink : Entity
    {
        public Sink(World world, Vector2 pos) : base(world, pos)
        {
        }


        public override void Init()
        {
            var sr = this.AddNewComponent<SpriteRenderer>();
            sr.Center = true;
            sr.BatchNo = 7;
            sr.tex = StaticImg.sprite1024;
            var r = this.AddNewComponent<RectColider>();
            r.WatchCollisions = true;
            r.rect = new Rectangle(0, 0, 350, 350);

            var nanny = this.AddNewComponent<Nanny>();

            var child = new SinkCollider(this.World, this.Position);
            World.AddEntity(child);
            nanny.AddChild(child);
        }

    }
}
