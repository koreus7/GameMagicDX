using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.ComponentSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMagic.Components
{
    class SpriteRenderer : IComponent
    {
        public int ID { get; set; }

        public IEntity Entity { get; set; }

        public Texture2D tex { get; set; }

        public bool Center { get; set; } = false;

        public bool Visible { get; set; } = true;


        public void Init()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Visible) return;
            if (Center)
            {
                spriteBatch.Draw(tex, Entity.Position - new Vector2(tex.Width, tex.Height) * 0.5f);
            }
            else
            {
                spriteBatch.Draw(tex, Entity.Position);
            }
        }

        public int BatchNo { get; set; } = 1;
    }
}
