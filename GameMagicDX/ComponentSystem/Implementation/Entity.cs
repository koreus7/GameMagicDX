using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameMagic.ComponentSystem.Implementation
{
    public class Entity : IEntity
    {
        private readonly World _world;

        private readonly List<int> _componentTypeIDs = new List<int>();

        public List<int> GetComponentTypeIDs()
        {
            return _componentTypeIDs;
        }

        public Vector2 Position { get; set; }
        public void SetPosition(int x, int y)
        {
           Position = new Vector2(x, y);
        }

        public void SetPosition(Vector2 pos)
        {
            Position = pos;
        }

        public World World => _world;

        public Entity(World world, Vector2 pos)
        {
            _world = world;
            Position = pos;
        }

        public T GetComponent<T>() where T : IComponent
        {
            return _world.GetComponent<T>(this.ID);
        }

        public T AddNewComponent<T>() where T : IComponent, new()
        {
            var c = _world.NewComponent<T>(ID);
            c.Entity = this;
            c.Init();
            _componentTypeIDs.Add(ComponentStore.ComponentTypeLookup[c.GetType()]);
            return c;
        }

        public virtual void Init()
        {

        }
        

        public int ID { get; set; }    
    }
}
