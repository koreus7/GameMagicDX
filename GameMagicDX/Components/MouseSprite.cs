using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.ComponentSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMagic.Components
{
    public class MouseSprite : IComponent
    {
        public int ID { get; set; }
        public IEntity Entity { get; set; }

        public void Init()
        {
         
        }

        public void Update(GameTime gameTime)
        {
            MouseState m = Mouse.GetState();
            Entity.SetPosition(m.X, m.Y);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            MouseState m = Mouse.GetState();
            spriteBatch.Draw(StaticImg.asprite, new Vector2(m.X,m.Y) -  new Vector2(StaticImg.asprite.Width, StaticImg.asprite.Height) *0.5f);
        }

        public int BatchNo => 2;
    }
}
