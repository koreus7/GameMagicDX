using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GameMagic.ComponentSystem.Implementation;
using Microsoft.Xna.Framework;

namespace GameMagic.ComponentSystem
{
    public interface IEntity : IIdentifiable
    {
        T GetComponent<T>() where T : IComponent;
        T AddNewComponent<T>() where T : IComponent, new();
        Vector2 Position { get; }
        void SetPosition(int x, int y);
        void SetPosition(Vector2 pos);
        World World { get; }
        List<int> GetComponentTypeIDs();
    }
}
