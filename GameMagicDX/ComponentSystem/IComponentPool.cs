using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMagic.ComponentSystem
{
    interface IComponentPool<TComponent> where TComponent : IComponent
    {
        TComponent FindComponent(Guid id);
        TComponent GetBlankComponent();
    }
}
