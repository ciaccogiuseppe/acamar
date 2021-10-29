using acamar.Source.Engine.World;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine
{
    static class OverlayEntitiesHandler
    {
        public static List<Entity> entities = new List<Entity>();

        public static void Draw(SpriteBatch batch)
        {
            foreach (Entity e in entities)
            {
                e.Draw(batch);
            }
        }

        public static void AddEntity(Entity e)
        {
            entities.Add(e);
        }

        public static void RemoveEntity(Entity e)
        {
            entities.Remove(e);
        }

        public static void Reset()
        {
            entities.Clear();
        }
    }

}
