﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.Components;
using GameMagic.ComponentSystem.Implementation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMagic.Entities
{
    class Orb : Entity
    {

        public Orb(World world, Vector2 pos) : base(world, pos)
        {
        }

        public override void Init()
        { 
            var sr = this.AddNewComponent<SpriteRenderer>();
            sr.tex = StaticImg.asprite;
            sr.Center = true;
            var r = this.AddNewComponent<RectColider>();
            r.WatchCollisions = true;
            r.WatchEntry = true;
            r.rect = new Rectangle(0,0,50,50);
            this.AddNewComponent<Wander>();
        }
    }
}