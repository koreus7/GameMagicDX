using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMagic.ComponentSystem
{
    interface IComponentLocator
    {
        T FindComponent<T>(Guid componentID) where T : IComponent;
    }
}
