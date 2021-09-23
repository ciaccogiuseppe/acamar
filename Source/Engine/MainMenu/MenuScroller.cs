using acamar.Source.Engine.World;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.MainMenu
{
    //TODO: REMOVE IF NOT USED
    class MenuScroller : MenuEntity
    {
        private Entity scrollerTag; //name of scroller:   ScrollerTag      Options↕
        private int optionsNumber;
        private int currentOption;
        private bool active;

        public MenuScroller() :
        base()
        {

        }

        public override void Draw(SpriteBatch batch)
        {
            scrollerTag.Draw(batch);
            base.Draw(batch);
        }

        public void MoveUp()
        {
            int animationLength = 0; //[TODO] see Entity
            currentAnimation = (currentAnimation - 1) % animationLength;
            if (currentOption != 0)
                currentOption = (currentOption - 1);
            else
                currentOption = optionsNumber - 1;
        }

        public void MoveDown()
        {
            int animationLength = 0; //[TODO] see Entity
            currentAnimation = (currentAnimation + 1) % animationLength;
            currentOption = (currentOption + 1) % optionsNumber;
        }
    }
}
