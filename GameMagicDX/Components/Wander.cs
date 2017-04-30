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
    class Wander : IComponent
    {
        public int ID { get; set; }
        public IEntity Entity { get; set; }

        private Vector2 dir;

        public Vector2 Dir {
            get { return dir;  }
        }
        
        private RectColider rect;

        public void Init()
        {
            dir = Rand.Inst.Vec2(2);
            rect = Entity.GetComponent<RectColider>();
        }

        public void Update(GameTime gameTime)
        {
            float x = Entity.Position.X;
            float y = Entity.Position.Y;
            int w = Entity.World.Width;
            int h = Entity.World.Height;

            float theta = Noise.Generate(x/320.0f + 2000.0f, y/320.0f + 2000.0f);
            dir = new Vector2(5.0f*(float)Math.Cos(theta*2*Math.PI),-5.0f*(float)Math.Sin(theta*2*Math.PI)) * 0.06f;

           // dir += new Vector2(x/w, y/h) * 0.1f;
           // dir += new Vector2((w - x)/w, (h - y)/y) * 0.1f;
            MouseState m = Mouse.GetState();

            float mouseDistance = Vector2.Distance(Entity.Position, new Vector2(m.X, m.Y));

            bool mouse = false;

            Vector2 mouseEffect = new Vector2(0.0f, 0.0f);

            if (mouseDistance < 200 && m.RightButton == ButtonState.Pressed)
            {
                mouse = true;
                Vector2 delta = new Vector2(m.X - Entity.Position.X, m.Y - Entity.Position.Y);
                float mod = Math.Min(delta.LengthSquared(), 100.0f) / 100.0f;
                mouseEffect = delta.Normalized() * 24.0f * mod;
            }

            bool boost = false;

            foreach (RectColider.CollisionResult res in rect.Collisions)
            {

                RectColider colider = res.Collider;

                if (colider.Entity is Orb)
                {
                    Vector2 delta = (colider.Entity.Position - Entity.Position);
                    if (delta.LengthSquared() < 10)
                    {
                        // Atract when really close.
                        dir += delta.Normalized()*3.0f * MathHelper.Min(delta.LengthSquared(), 10.0f) / 10.0f;
                        //dir += delta.Normalized()*10.0f;
                    }
                    else if (delta.LengthSquared() < 200)
                    {
                        dir -= delta.Normalized()*1.0f;
                    }
                }
                else if (colider.Entity is Repelatron)
                {
                    Vector2 delta = (colider.Entity.Position - Entity.Position);
                    float nd = MathHelper.Min(300.0f, delta.LengthSquared())/ 300.0f;
                    float mod = 1.4f/MathHelper.Lerp(10f,0.2f,nd);
                    dir -= delta.Normalized()*mod*2.5f;
                }
                else if (colider.Entity is Hub)
                {
                    Vector2 delta = colider.Entity.Position - Entity.Position;

                    if (delta.LengthSquared() < 80000.0f)
                    {
                        float mod = Math.Min(delta.LengthSquared(), 200.0f) / 200.0f;
                        dir += delta.Normalized() * 24.0f * mod;
                    }
                }
                else if (colider.Entity is SinkCollider)
                {
                    if (res.JustEntered)
                    {
                        StaticSound.absorb.Play(0.1f, 1.0f, 1.0f);
                        Entity.World.SignalOrbGet();
                    }
                }
                else if(colider.Entity is HubCollider)
                {
                    if (res.JustEntered)
                    {
                        StaticSound.absorbBad.Play(0.5f, 1.0f, 1.0f);;
                    }

                }
                else if (colider.Entity is Sink)
                {
                    Vector2 delta = colider.Entity.Position - Entity.Position;

                    if (delta.LengthSquared() < 81000.0f)
                    {
                        float mod = Math.Min(delta.LengthSquared(), 200.0f)/200.0f;
                        dir += delta.Normalized()*30.0f*mod;
                    }
                    if (delta.LengthSquared() < 10.0f)
                    {
                        Entity.GetComponent<SpriteRenderer>().Visible = false;
                    }

                }
                else if (colider.Entity is Planet)
                {
                    Vector2 delta = colider.Entity.Position - Entity.Position;

                    if (delta.LengthSquared() < 57600)
                    {
                        Vector2 normal = delta.Normalized();
                        Vector2 tangent = new Vector2(-normal.Y, normal.X);

                        dir += tangent * 30.0f;
                    }
                    else if (delta.LengthSquared() < 59600 && !mouse)
                    {
                        float mod = Math.Min(delta.LengthSquared() - 57600, 200.0f) / 200.0f;
                        dir += delta.Normalized() * 28.0f *mod;
                    }


                }
                else if (colider.Entity is ReversePlanet)
                {
                    Vector2 delta = colider.Entity.Position - Entity.Position;

                    if (delta.LengthSquared() < 57600)
                    {
                        Vector2 normal = delta.Normalized();
                        Vector2 tangent = new Vector2(-normal.Y, normal.X);

                        dir -= tangent * 30.0f;
                    }
                    else if (delta.LengthSquared() < 59600 && !mouse)
                    {
                        float mod = Math.Min(delta.LengthSquared() - 57600, 200.0f) / 200.0f;
                        dir += delta.Normalized() * 28.0f * mod;
                    }
                }
                else if (colider.Entity is Goo)
                {
                    Vector2 delta = colider.Entity.Position - Entity.Position;

                    if (delta.LengthSquared() < 10000)
                    {
                        mouseEffect *= 0.0f;
                    }
                }
                else if (colider.Entity is SpeedBoost)
                {
                    boost = true;
                }
            }

            if (boost)
            {
                dir *= 5.0f;
            }

            dir += mouseEffect;


            Vector2 projected = Project(dir, gameTime);
            if (projected.X - rect.rect.Width/2.0f > Entity.World.Width)
            {
                projected.X = -rect.rect.Width/2.0f;
                dir = new Vector2(-100.0f, 0);
                projected = Project(dir, gameTime);
            }
            else if(projected.X + rect.rect.Width/2.0f < 0)
            {
                projected.X = Entity.World.Width + rect.rect.Width/2.0f;
                dir = new Vector2(100.0f, 0);
                projected = Project(dir, gameTime);
            }

            if (projected.Y - rect.rect.Height/2.0f > Entity.World.Height)
            {
               projected.Y = -rect.rect.Height/2.0f;
         
            }
            else if (projected.Y + rect.rect.Height/2.0f < 0)
            {
                projected.Y = Entity.World.Height + rect.rect.Height/2.0f;
            }

            Entity.SetPosition(projected);
        }

        private Vector2 Project(Vector2 dir, GameTime gameTime)
        {
            return Entity.Position + dir * 0.01f * gameTime.ElapsedGameTime.Milliseconds;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
         
        }

        public int BatchNo => 0;
    }
}
