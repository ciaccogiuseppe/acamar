using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Entities
{
    class Picture
    {
        private Texture2D texture;
        private Rectangle sourceRec;
        private Rectangle destRec;

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, destRec, sourceRec, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}
