using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMagic.ComponentSystem
{
    interface IComponentStore : IComponentLocator
    {
        T AddComponent<T>(int entityID) where T : IComponent, new();
        T FindComponent<T>(int entityID) where T : IComponent;
    }
}
