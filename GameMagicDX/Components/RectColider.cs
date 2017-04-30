using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using GameMagic.ComponentSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMagic.Components
{
    public class RectColider : IComponent, IDisposable
    {
        public int ID { get; set; }

        public IEntity Entity { get; set; }


        private Rectangle _rect = new Rectangle(0,0,0,0);

        public Rectangle rect {
            get { return _rect; }
            set { _rect = value; }
        }

        public Rectangle WorldRect
        {
            get {
                if (Center)
                {
                return new Rectangle(
                _rect.X + (int)Math.Floor(Entity.Position.X) - _rect.Width/2,
                _rect.Y + (int)Math.Floor(Entity.Position.Y) - _rect.Height/2,
                _rect.Width,
                _rect.Height);
                }
                else
                {
                    return new Rectangle(
                _rect.X + (int)Math.Floor(Entity.Position.X),
                _rect.Y + (int)Math.Floor(Entity.Position.Y),
                _rect.Width,
                _rect.Height);
                }
            }
        }

        public bool Center { get; set; } = true;

        public bool WatchCollisions { get; set; } = false;
        public bool WatchEntry { get; set; } = false;

        public void Init()
        {
            Entity.World.CollisionSystem.Register(this);
        }


        public class CollisionResult
        {
            public RectColider Collider { get; set; }
            public bool JustEntered { get; set; }
        }

        private  List<RectColider> _collisions = new List<RectColider>();
        private  HashSet<int> _lastCollisions = new HashSet<int>();

        public List<CollisionResult> Collisions => _collisions.Select(x => new CollisionResult
        {
            Collider = x,
            JustEntered = !_lastCollisions.Contains(x.ID),
        }).ToList();

        public void UpdateCollisions()
        {
            if (WatchCollisions)
            {
                if (WatchEntry)
                {
                    _lastCollisions.Clear();
                    foreach (RectColider rectColider in _collisions)
                    {
                        _lastCollisions.Add(rectColider.ID);
                    }
                }
                Entity.World.CollisionSystem.GetCollisions(this, ref _collisions);
               // Logger.Log($"Rect Collider ID:{ID} Collisions: {_collisions.Count}");
            }
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (GMGame.DebugOverlay)
            {
                Entity.World.Game.DrawRectangle(WorldRect, Color.Red.MultiplyAlpha(0.1f));
            }
        }

        public int BatchNo => 32;

        public void Dispose()
        {
            Entity.World.CollisionSystem.UnRegister(this);
        }
    }
}
