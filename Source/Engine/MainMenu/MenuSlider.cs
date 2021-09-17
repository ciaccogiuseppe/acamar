using acamar.Source.Engine.World;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.MainMenu
{
    class MenuSlider : MenuEntity
    {
        public MenuSlider() :
            base()
        {

        }
        private bool active;
        private Entity sliderBar;
        private Entity sliderTag; //name of slider:   SliderTag       |------<>-----------|
        private int step;
        private int min;
        private int max;
        private int current;

        public override void Draw(SpriteBatch batch)
        {
            sliderTag.Draw(batch);
            sliderBar.Draw(batch);
            base.Draw(batch);
        }

        public void MoveLeft()
        {
            if (current > min)
            {
                current -= step;
            }
        }

        public void MoveRight()
        {
            if (current < max)
            {
                current += step;
            }
        }
    }
}
