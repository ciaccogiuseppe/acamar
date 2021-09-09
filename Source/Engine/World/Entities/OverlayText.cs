using acamar.Source.Engine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Entities
{
    class OverlayText : Entity
    {
        private string text;
        private Font currentFont;
        //private int posx;
        //private int posy;
        public OverlayText(string text, int posx, int posy, Font font)
        {
            this.text = text;
            this.posx = posx;
            this.posy = posy;
            currentFont = font;
        }

        public override void Draw(SpriteBatch batch)
        {
            if(active)
                currentFont.Draw(text, posx, posy, batch, opacity);
        }
    }
}
