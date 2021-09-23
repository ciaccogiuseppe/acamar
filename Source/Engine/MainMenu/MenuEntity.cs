using acamar.Source.Engine.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.MainMenu
{
    //TODO: REMOVE IF NOT USED
    class MenuEntity : Entity
    {
        protected bool selected = false;
        public MenuEntity() :
        base()
        {

        }

        public virtual void Select()
        {
            selected = true;
        }

        public virtual void Deselect()
        {
            selected = false;
        }
    }
}
