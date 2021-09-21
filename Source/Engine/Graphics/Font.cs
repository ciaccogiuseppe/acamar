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

        public void Draw(string text, int posx, int posy, SpriteBatch batch, float opacity)
        {
            int pos = posx;
            int lin = posy;
            foreach (char c in text.ToCharArray())
            {
                if (c == '_')
                {
                    lin += charHeight;
                    pos = posx;
                }
                else
                {
                    Globals._overBatch.Draw(font, new Rectangle(pos, lin, charWidth, charHeight), charToRect.GetValueOrDefault(c), Color.White*opacity);
                    pos += charWidth;
                }
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
            charToRect.Add('è', new Rectangle(10 * charWidth, 2 * charHeight, charWidth, charHeight));
            charToRect.Add('é', new Rectangle(11 * charWidth, 2 * charHeight, charWidth, charHeight));
            charToRect.Add('à', new Rectangle(12 * charWidth, 2 * charHeight, charWidth, charHeight));
            charToRect.Add('ù', new Rectangle(13 * charWidth, 2 * charHeight, charWidth, charHeight));
            charToRect.Add('ì', new Rectangle(14 * charWidth, 2 * charHeight, charWidth, charHeight));
            charToRect.Add('ò', new Rectangle(15 * charWidth, 2 * charHeight, charWidth, charHeight));
            charToRect.Add('.', new Rectangle(16 * charWidth, 2 * charHeight, charWidth, charHeight));
            charToRect.Add(',', new Rectangle(17 * charWidth, 2 * charHeight, charWidth, charHeight));
            charToRect.Add('!', new Rectangle(18 * charWidth, 2 * charHeight, charWidth, charHeight));
            charToRect.Add('?', new Rectangle(19 * charWidth, 2 * charHeight, charWidth, charHeight));
            charToRect.Add('\'', new Rectangle(20 * charWidth, 2 * charHeight, charWidth, charHeight));
            charToRect.Add(':', new Rectangle(21 * charWidth, 2 * charHeight, charWidth, charHeight));
        }
    }
}
