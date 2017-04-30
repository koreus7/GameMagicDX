using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMagic.ComponentSystem
{
    interface IPoolableComponent<TComponent> : IComponent where TComponent : IComponent
    {
        /// <summary>
        /// Reset the component so it can be reused.
        /// </summary>
        void Reset();

        /// <summary>
        /// Register with the pool so it knows when this component is ready to be reset.
        /// </summary>
        /// <param name="pool"></param>
        void RegisterWithPool(IComponentPool<TComponent> pool);
    }
}
