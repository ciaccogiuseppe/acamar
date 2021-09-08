using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.Graphics
{
    public class Font
    {
        //private Rectangle[] charToRect;
        private Dictionary<char, Rectangle> charToRect;
        private int charWidth;
        private int charHeight;

        private Texture2D font;

        public Font(int fontID, int charWidth, int charHeight)
        {
            font = Globals.Content.Load<Texture2D>("Font\\font" + fontID + ".spr");
            this.charWidth = charWidth;
            this.charHeight = charHeight;
            GenerateDictionary();
        }

        public void Draw(string text, int posx, int posy, SpriteBatch batch)
        {
            int pos = posx;
            foreach (char c in text.ToCharArray())
            {
                Globals._overBatch.Draw(font, new Rectangle(pos, posy, charWidth, charHeight), charToRect.GetValueOrDefault(c), Color.White);
                pos += charWidth;
            }
        }

        private void GenerateDictionary()
        {
            charToRect = new Dictionary<char, Rectangle>();
            for (char c = 'A'; c < 'Z' + 1; c++)
                charToRect.Add(c, new Rectangle((c-'A')*charWidth, 0, charWidth, charHeight));
            for (char c = 'a'; c < 'z' + 1; c++)
                charToRect.Add(c, new Rectangle((c - 'a') * charWidth, charHeight, charWidth, charHeight));
            for (char c = '0'; c < '9' + 1; c++)
                charToRect.Add(c, new Rectangle((c - '0') * charWidth, 2 * charHeight, charWidth, charHeight));
            charToRect.Add(' ', new Rectangle(('Z'-'A'+1)*charWidth, 0, charWidth, charHeight));
        }
    }
}
