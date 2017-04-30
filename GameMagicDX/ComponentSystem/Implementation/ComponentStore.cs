using System;
using System.Collections.Generic;
using System.Diagnostics;
using GameMagic.Components;

namespace GameMagic.ComponentSystem.Implementation
{
    class ComponentStore
    {
        private readonly Dictionary<int, IComponent> _components = new Dictionary<int, IComponent>();

        public static readonly Dictionary<Type, int> ComponentTypeLookup = new Dictionary<Type, int>
        {
            {typeof(SpriteRenderer), 10},
            {typeof(RectColider), 5},
            {typeof(Wander), 80085 },
            {typeof(MouseSprite), 666},
            {typeof(Nanny), 777 },
            {typeof(OrbEmiter), 888 },
            {typeof(SelectionMenu), 9090 }
        };
            
        private int componentIdCounter = 1;

        public T AddComponent<T>(int entityID) where T : IComponent, new()
        {
            var component = new T();
            component.ID = componentIdCounter;
            componentIdCounter++;
            _components.Add(Hash(entityID, ComponentTypeLookup[typeof(T)]), component);
            return component;
        }

        public T FindComponent<T>(int entityID) where T : IComponent
        {
            return (T) _components[Hash(entityID, ComponentTypeLookup[typeof(T)])];
        }

        public void RemoveComponent(int entityID, int componentTypeId)
        {
            _components.Remove(Hash(entityID, componentTypeId));
        }

        private int Hash(int a, int b)
        {
            return (a + b)*(a + b + 1)/2 + b;
        }

        public void Clear()
        {
            _components.Clear();
            componentIdCounter = 1;
        }

        public IEnumerable<IComponent> GetComponents()
        {
            foreach (KeyValuePair<int, IComponent> entry in _components)
            {
                yield return entry.Value;
            }
        }
    }
}
