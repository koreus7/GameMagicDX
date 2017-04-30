using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.ComponentSystem;
using GameMagic.ComponentSystem.Implementation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMagic.Components
{
    class Nanny : IComponent
    {
        public int ID { get; set; }
        public IEntity Entity { get; set; }

        private List<Entity> children = new List<Entity>();

        public void Init()
        {        
        }

        public void Update(GameTime gameTime)
        {
            foreach (Entity child in children)
            {
                child.Position = Entity.Position;
            }
        }

        public void AddChild(Entity e)
        {
            children.Add(e);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public int BatchNo { get; }
    }
}
