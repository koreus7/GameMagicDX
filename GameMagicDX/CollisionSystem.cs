using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMagic.Components;

namespace GameMagic
{
    public class CollisionSystem
    {
        private QuadTree<RectColider> quadTree;
        private Dictionary<int, RectColider> registeredColliders = new Dictionary<int, RectColider>(); 

        public CollisionSystem()
        {
            quadTree = new QuadTree<RectColider>(4,0,0,5000,5000);
        }

        public void Register(RectColider collider)
        {
            registeredColliders[collider.ID] = collider;
        }

        public void UnRegister(RectColider colider)
        {
            registeredColliders.Remove(colider.ID);
        }

        public void AddRect(RectColider collider)
        {
            quadTree.Insert(collider, collider.WorldRect.X, collider.WorldRect.Y, collider.WorldRect.Width,
                collider.WorldRect.Height);
        }

        public List<RectColider> SearchPoint(float x, float y)
        {
            List<RectColider> result = new List<RectColider>();
            quadTree.SearchPoint(x, y, ref result);
            return result;
        }

        public bool GetCollisions(RectColider collider, ref List<RectColider> results)
        {
            return quadTree.FindCollisions(collider, ref results);
        }

        public void Populate()
        {
            foreach (KeyValuePair<int, RectColider> kvp in registeredColliders )
            {
                AddRect(kvp.Value);
            }

            foreach (KeyValuePair<int, RectColider> kvp in registeredColliders)
            {
                kvp.Value.UpdateCollisions();
            }
        }

        public void Clear()
        {
            quadTree.Clear();
        }
    }
}
