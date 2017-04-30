using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.ComponentSystem;
using GameMagic.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMagic.Components
{
    class SelectionMenu : IComponent
    {
        public int ID { get; set; }
        public IEntity Entity { get; set; }

        List<SelectionMenuItem> items = new List<SelectionMenuItem>();

        private MouseState prevMouse;

        private int selectedIndex = 0;

        private bool visible;

        public void Init()
        {
            items.Add(new SelectionMenuItem
            {
                Image = StaticImg.expandIcon,
                Count =  999,
                Action = () =>
                {
                    Entity.World.AddEntAtMouse(new Repelatron(Entity.World, Vector2.One));
                }
            });

            items.Add(new SelectionMenuItem
            {
                Image = StaticImg.counterClockwise,
                Count = 999,
                Action = () =>
                {
                      Entity.World.AddEntAtMouse(new Planet(Entity.World, Vector2.One));
                }
            });

            items.Add(new SelectionMenuItem
            {
                Image = StaticImg.clockwise,
                Count = 999,
                Action = () =>
                {
                    Entity.World.AddEntAtMouse(new ReversePlanet(Entity.World, Vector2.One));
                }
            });


            items.Add(new SelectionMenuItem
            {
                Image = StaticImg.speed,
                Count = 999,
                Action = () =>
                {
                    Entity.World.AddEntAtMouse(new SpeedBoost(Entity.World, Vector2.One));
                }
            });

            Inst = this;
        }

        public static SelectionMenu Inst;


        public void UpdateCounts(List<int> counts)
        {
            for (int i = 0; i < counts.Count; i++)
            {
                items[i].Count = counts[i];
            }
        }

        public void Update(GameTime gameTime)
        {
            visible = Keyboard.GetState().IsKeyDown(Keys.LeftControl);

            

            MouseState ms = Mouse.GetState();

            if (visible)
            {
                if (ms.ScrollWheelValue < prevMouse.ScrollWheelValue)
                {
                    StaticSound.click.Play();
                    if (selectedIndex + 1 > items.Count - 1)
                    {
                        selectedIndex = 0;
                    }
                    else
                    {
                        selectedIndex += 1;
                    }

                }
                else if (ms.ScrollWheelValue > prevMouse.ScrollWheelValue)
                {
                    StaticSound.click.Play();
                    if (selectedIndex - 1 < 0)
                    {
                        selectedIndex = items.Count - 1;
                    }
                    else
                    {
                        selectedIndex -= 1;
                    }
                }

                if (ms.LeftButton == ButtonState.Released && prevMouse.LeftButton == ButtonState.Pressed)
                {

                    SelectionMenuItem item = items[selectedIndex];
                    if (item.Count > 0)
                    {
                        item.Action();
                        item.Count -= 1;
                    }
                }
            }
   

            prevMouse = ms;
        }


        const int incr = 60;

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (visible)
            {
                Entity.World.Game.DrawRectangle(new Rectangle(
                (int)Math.Floor(Entity.Position.X),
                (int)Math.Floor(Entity.Position.Y),
                100, items.Count * incr + 10), Color.Orange.MultiplyAlpha(0.4f));

                for (int i = 0; i < items.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Entity.World.Game.DrawRectangle(new Rectangle(
                        (int)Math.Floor(Entity.Position.X),
                        (int)Math.Floor(Entity.Position.Y) + i * incr,
                        100, incr), Color.Aqua);
                    }
                    spriteBatch.Draw(items[i].Image, Entity.Position + new Vector2(50, i * incr + 10));
                    spriteBatch.DrawString(GMGame.mainFont, $"{items[i].Count}x", Entity.Position + new Vector2(20, i * incr + 20), Color.White);
                }
            }
        }

        public int BatchNo => 99;
    }
}
