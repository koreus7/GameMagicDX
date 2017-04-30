using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using GameMagic.Components;
using GameMagic.Entities;
using GameMagic.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using QuakeConsole;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector4 = Microsoft.Xna.Framework.Vector4;

namespace GameMagic.ComponentSystem.Implementation
{
    public class World
    {
        private readonly ComponentStore components;
        private Dictionary<int, Entity> entities;
        private List<Entity> toBeAdded;
        private MouseState lastMouse;
        private int entityIDCounter = 1;
        public int Width => Game.Width;
        public int Height => Game.Height;
        private bool updating = false;

        private static bool gameOver = false;

        public CollisionSystem CollisionSystem { get; set; }
        public GMGame Game { get; private set; }

        public World(GMGame game)
        {
            Game = game;
            components = new ComponentStore();
            CollisionSystem = new CollisionSystem();
            entities = new Dictionary<int, Entity>();
            toBeAdded = new List<Entity>();
        }

        public T GetComponent<T>(int entityId) where T : IComponent
        {
            return components.FindComponent<T>(entityId);
        }

        public T NewComponent<T>(int entityId) where T : IComponent, new()
        {
            return components.AddComponent<T>(entityId);
        }

        public T AddEntity<T>(T e) where T : Entity
        {
            e.ID = entityIDCounter;
            entityIDCounter++;
            if (!updating)
            {
                InitAndAdd(e);
            }
            else
            {
                toBeAdded.Add(e);
            }
            return e;
        }

        private void InitAndAdd(Entity e)
        {
            e.Init();
            entities[e.ID] = e;
        }

        private int elapsed = 0;

        public void Draw(GameTime time, SpriteBatch batch)
        {
            elapsed += time.ElapsedGameTime.Milliseconds;

            batch.Begin(0, null, null, null, null, GMGame.boostEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 20))
            {
                c.Draw(time, batch);
            }
            batch.End();

            GMGame.gooEffect.Parameters["res"].SetValue(new Vector2(1, 1));
            Console.WriteLine(time.TotalGameTime.Milliseconds);
            GMGame.gooEffect.Parameters["time"].SetValue(elapsed / 2.0f);
            GMGame.gooEffect.Parameters["col"].SetValue(new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
            int seed = 2914;
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 666))
            {
                batch.Begin(0, null, null, null, null, GMGame.gooEffect);
                c.Draw(time, batch);
                GMGame.gooEffect.Parameters["seed"].SetValue(seed/2.0f);
                batch.End();
                seed += 1;
            }

            GMGame.lightEffect.Parameters["time"].SetValue(elapsed/100.0f);
            GMGame.lightEffect.Parameters["col"].SetValue(new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
            batch.Begin(0, null, null, null, null, GMGame.lightEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 1))
            {
                c.Draw(time, batch);
            }
            batch.End();

            batch.Begin(0, null, null, null, null, GMGame.colliderEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 32))
            {
                c.Draw(time, batch);
            }
            batch.End();


            GMGame.mouseEffect.Parameters["res"].SetValue(new Vector2(1, 1));
            batch.Begin(0, null, null, null, null, GMGame.mouseEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 2))
            {
                c.Draw(time, batch);
            }
            batch.End();

            GMGame.lightEffect.Parameters["time"].SetValue(time.TotalGameTime.Milliseconds/1000.0f);
            GMGame.lightEffect.Parameters["col"].SetValue(new Vector4(1.0f, 0.0f, 1.0f, 1.0f));
            batch.Begin(0, null, null, null, null, GMGame.lightEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 3))
            {
                c.Draw(time, batch);
            }
            batch.End();

            GMGame.lightEffect.Parameters["time"].SetValue(time.TotalGameTime.Milliseconds/1000.0f);
            GMGame.lightEffect.Parameters["col"].SetValue(new Vector4(0.0f, 1.0f, 1.0f, 1.0f));
            batch.Begin(0, null, null, null, null, GMGame.lightEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 333))
            {
                c.Draw(time, batch);
            }
            batch.End();


            GMGame.repelatronEffect.Parameters["res"].SetValue(new Vector2(1, 1));
            batch.Begin(0, null, null, null, null, GMGame.repelatronEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 5))
            {
                c.Draw(time, batch);
            }
            batch.End();

            GMGame.hubEffect.Parameters["res"].SetValue(new Vector2(1, 1));
            batch.Begin(0, null, null, null, null, GMGame.hubEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 6))
            {
                c.Draw(time, batch);
            }
            batch.End();


            GMGame.sinkEffect.Parameters["res"].SetValue(new Vector2(1, 1));
            batch.Begin(0, null, null, null, null, GMGame.sinkEffect);
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 7))
            {
                c.Draw(time, batch);
            }
            batch.End();

            batch.Begin();
            foreach (IComponent c in components.GetComponents().Where(x => x.BatchNo == 99))
            {
                c.Draw(time, batch);
            }
            batch.End();


            if (gameOver)
            {
                batch.Begin();
                batch.DrawString(GMGame.mainFont, "Game Over", new Vector2(Width / 2f, Height / 2f), Color.White);
                batch.End();
            }

            if (GMGame.LevelCounter == 0)
            {
                batch.Begin();
                batch.DrawString(GMGame.mainFont, "Right Click to drag the goo to the sink.", new Vector2(20, 20), Color.White);
                batch.DrawString(GMGame.mainFont, "R to restart.", new Vector2(20, 40), Color.White);
                batch.End();
            }
            else if (GMGame.LevelCounter == 1)
            {
                batch.Begin();
                batch.DrawString(GMGame.mainFont, "R to restart.", new Vector2(20, 20), Color.White);
                batch.End();
            }
            else if (GMGame.LevelCounter == 2)
            {
                batch.Begin();
                batch.DrawString(GMGame.mainFont, "Hold control to see available modifier orbs.", new Vector2(20, 20),
                    Color.White);
                batch.DrawString(GMGame.mainFont, "Scroll wheel to select.", new Vector2(20, 40), Color.White);
                batch.DrawString(GMGame.mainFont, "Left Click to place.", new Vector2(20, 60), Color.White);
                batch.DrawString(GMGame.mainFont, "T to reset goo", new Vector2(20, 80), Color.White);
                batch.End();
            }
        }


        public void AddEnt(Vector2 pos, int type)
        {
            switch (type)
            {
                case 0:
                    this.AddEntity(new Hub(this, pos));
                    break;
                case 1:
                    this.AddEntity(new Planet(this, pos));
                    break;
                case 2:
                    this.AddEntity(new Repelatron(this, pos));
                    break;
                case 3:
                    this.AddEntity(new Sink(this, pos));
                    break;
                case 4:
                    this.AddEntity(new Source(this, pos));
                    break;
                case 5:
                    this.AddEntity(new SpeedBoost(this, pos));
                    break;
                case 6:
                    this.AddEntity(new ReversePlanet(this, pos));
                    break;
                case 7:
                    this.AddEntity(new Goo(this, pos));
                    break;
            }
        }

        public int GetEntType(Entity e)
        {
            if (e is Hub)
            {
                return 0;
            }
            if (e is Planet)
            {
                return 1;
            }
            if (e is Repelatron)
            {
                return 2;
            }
            if (e is Sink)
            {
                return 3;
            }
            if (e is Source)
            {
                return 4;
            }
            if (e is SpeedBoost)
            {
                return 5;
            }
            if (e is ReversePlanet)
            {
                return 6;
            }
            if (e is Goo)
            {
                return 7;
            }

            return -1;

        }

        public void AddEntAtMouse(Entity e)
        {
            MouseState ms = Mouse.GetState();
            e.Position = new Vector2(ms.X, ms.Y);
            this.AddEntity(e);
        }

        private class LevelData
        {
            public List<Vector2> Positions { get; set; } = new List<Vector2>();
            public List<int> EntityTypes { get; set; } = new List<int>();
            public List<int> Supplies { get; set; } = new List<int>();
        }

        public void Save(string name="")
        {
            LevelData data = new LevelData();
            foreach (KeyValuePair<int, Entity> keyValuePair in entities)
            {
                int type = GetEntType(keyValuePair.Value);

                if (type != -1)
                {
                    data.Positions.Add(keyValuePair.Value.Position);
                    data.EntityTypes.Add(type);
                }
            }

            string serialised = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            if (String.IsNullOrEmpty(name))
            {
                System.IO.File.WriteAllText(
                    $@"C:\Users\Public\Level{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-tt")}.json", serialised);
                
            }
            else
            {
                System.IO.File.WriteAllText(
                    $@"{name}.json", serialised);

            }
        }




        private int orbCount = 0;

        public void SignalOrbGet()
        {
            orbCount += 1;

            if (orbCount > 45)
            {
                StaticSound.win.Play();
                orbCount = 0;
                GMGame.LevelCounter += 1;

                if (GMGame.LevelCounter > 4)
                {
                    Game.Clear();
                    World.gameOver = true;
                    //GMGame.LevelCounter = 0;
                }
                else
                {
                    Game.Load();
                }
            }
        }

        public void Load(string name)
        {

            ClearLevel();
            string text = System.IO.File.ReadAllText($"Levels/{name}.json");

            LevelData data = Newtonsoft.Json.JsonConvert.DeserializeObject<LevelData>(text);

            for (int i = 0; i < data.EntityTypes.Count; i++)
            {
                AddEnt(data.Positions[i], data.EntityTypes[i]);
            }

            this.AddEntity(new MouseEntity(this, Vector2.One));

            SelectionMenu.Inst.UpdateCounts(data.Supplies);
        }

        private void ClearLevel()
        {
            entities.Clear();
            components.Clear();
        }

        public void ReturnOrbs()
        {
            List<Entity> toRemove = (from entity in entities where entity.Value is Orb select entity.Value).ToList();

            foreach (Entity entity in toRemove)
            {
                RemoveEntity(entity);
            }

            List<Entity> sauce = new List<Entity>();

            foreach (KeyValuePair<int, Entity> keyValuePair in entities)
            {
                if (keyValuePair.Value is Source)
                {
                    sauce.Add(keyValuePair.Value);
                }
            }

            sauce.ForEach(x => x.GetComponent<OrbEmiter>().EmitMany());
        }

        public void Update(GameTime time)
        {
            updating = true;
            MouseState ms = Mouse.GetState();
            
            CollisionSystem.Clear();
            CollisionSystem.Populate();
            foreach (IComponent c in components.GetComponents())
            {
                c.Update(time);
            }



            if (ms.MiddleButton == ButtonState.Released && lastMouse.MiddleButton == ButtonState.Pressed && GMGame.devMode)
            {
                List<RectColider> cols = CollisionSystem.SearchPoint(ms.X, ms.Y);

                foreach (RectColider rectColider in cols)
                {
                    var e = rectColider.Entity;
                    RemoveEntity(e);
                }
            }


            lastMouse = ms;
            updating = false;

            foreach (Entity entity in toBeAdded)
            {
                InitAndAdd(entity);
            }

            toBeAdded.Clear();
        }


        private void RemoveEntity(IEntity e)
        {
            foreach (int iD in e.GetComponentTypeIDs())
            {
                components.RemoveComponent(e.ID, iD);
            }

            this.entities.Remove(e.ID);
        }

        public virtual void Init()
        {

        }
    }
}
