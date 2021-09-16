using acamar.Source.Engine.World.Script;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Entities
{
    class Item : Entity
    {
        public Item()
        {

        }

        public void Use()
        {
            foreach (Event evn in events)
            {
                evn.Trigger();
                if (evn.IsActive() && !activeEvents.Contains(evn))
                {
                    activeEvents.Add(evn);
                }
            }
        }

        public override void Update()
        {
            foreach (Event evn in activeEvents)
            {
                evn.Continue();
                if (!evn.IsActive())
                {
                    activeEvents.Remove(evn);
                    evn.Reset();
                    break;
                }
            }
        }
    }
}
