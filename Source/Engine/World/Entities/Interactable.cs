using acamar.Source.Engine.World.Script;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Entities
{
    class Interactable : Entity
    {
        public Interactable(int entid, int sprid, int posx, int posy, int dir) :
            base(entid, sprid, posx, posy, dir)
        {

        }

        protected List<Event> events;

        public virtual void OnInteract()
        {
            ActionMachine();
            throw new NotImplementedException();
        }

        public virtual void ActionMachine()
        {

        }
    }
}
