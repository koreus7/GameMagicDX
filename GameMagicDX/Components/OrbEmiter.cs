using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.ComponentSystem;
using GameMagic.ComponentSystem.Implementation;
using GameMagic.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMagic.Components
{
    class OrbEmiter : IComponent
    {
        public int ID { get; set; }
        public IEntity Entity { get; set; }


        public void Init()
        {
            
        }

        public void Emit()
        {
            Entity.World.AddEntity(new Orb(Entity.World, new Vector2(Entity.Position.X + Rand.Inst.Int(100) - 50, 
                Entity.Position.Y + Rand.Inst.Int(100) - 50)));
        }

        public void EmitMany()
        {
            for (int i = 0; i < 50; i++)
            {
                Emit();
            }
        }

        public void Update(GameTime gameTime)
        {
            //if (Entity.World.Game.KeyPressed(Keys.Space))
            //{
            //    Emit();
            //}
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {   
        }

        public int BatchNo { get; }
    }
}
