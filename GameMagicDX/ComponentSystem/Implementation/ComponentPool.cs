using System;
using System.Collections.Generic;
using GameMagic.Logging;

namespace GameMagic.ComponentSystem.Implementation
{
    //class ComponentPool<TComponent> : IComponentPool<TComponent> where TComponent : IPoolableComponent<TComponent>, new()
    //{
    //    private int _size;
    //    private readonly Dictionary<int, TComponent> _components = new Dictionary<int, TComponent>();
    //    private readonly Queue<TComponent> _recycleQueue = new Queue<TComponent>();
    //    private ISimpleLogger _logger;
    //    private IComponentStore _overflow;

    //    public ComponentPool(int size, IComponentStore overflowStore, ISimpleLogger logger)
    //    {
    //        _size = size;
    //        _overflow = overflowStore;
    //        _logger = logger;
    //        for (int i = 0; i < _size; i++)
    //        {
    //            var c = new TComponent();
    //            c.RegisterWithPool(this);
    //            _components.Add(c.ID, c);
    //            _recycleQueue.Enqueue(c);
    //        }
    //    }

    //    public TComponent FindComponent(int id)
    //    {
    //        if (_components.ContainsKey(id))
    //        {
    //            return _components[id];
    //        }

    //        return _overflow.FindComponent<TComponent>(id);
    //    }

    //    public TComponent GetBlankComponent()
    //    { 
    //        if (_recycleQueue.Count > 0)
    //        {
    //            var c = _recycleQueue.Dequeue();
    //            c.Reset();
    //            return c;
    //        }
    //        else
    //        {
    //            _logger.LogError("Component Pool Overflow!");
    //            var c = new TComponent();
    //            c.RegisterWithPool(this);
    //            _overflow.AddComponent(c);
    //            return c;
    //        } 
    //    }
    //}
}
