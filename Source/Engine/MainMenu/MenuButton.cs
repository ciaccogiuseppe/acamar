using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.MainMenu
{
    class MenuButton : MenuEntity
    {
        public enum TYPE
        {
            ZERO,
            NEWGAME,
            LOADGAME,
            OPTIONS
        }
        int cnt = 0;
        TYPE type;

        private bool active;
        private int nextPage;
        public MenuButton() :
        base()
        {

        }

        public void SetType(TYPE type)
        {
            this.type = type;
        }

        public override void Update()
        {
            //if(Keyboard.GetState().IsKeyDown(Keys.Enter) && type == 1 && selected)
            if (selected)
            {
                if(MyKeyboard.IsPressedNotCont(Keys.Z))
                    switch(type)
                    {
                        case TYPE.NEWGAME:
                            Globals.CURRENTSTATE = Globals.STATE.RUNNING;
                            break;
                        case TYPE.LOADGAME:
                            Globals.mainMenu.GoToPage(nextPage);
                            break;
                        case TYPE.OPTIONS:
                            Globals.mainMenu.GoToPage(nextPage);
                            break;
                    }
            }
        }

        public override void Select()
        {
            if (selected == false)
            {
                base.Select();
                Animation();
            }
            
        }

        public override void Deselect()
        {
            if (selected == true)
            {
                base.Deselect();
                Animation();
            }
            
        }

        public void OnPress()
        {

        }

        public void SetNextPage(int nextPage)
        {
            this.nextPage = nextPage;
        }
    }
}
