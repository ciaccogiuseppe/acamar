using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Entities
{
    class Message
    {
        private const int MAXLEN = 20;
        Texture2D font;
        Entity textBox;
        private string text;
        private string preLine = "";
        private string curLine = "";
        private int firstLine = 325;
        private int secondLine = 360;
        private int firstChar = 20;
        private int charHeight = 16;
        private int charWidth = 12;
        private Rectangle[] charToRect;
        private bool newLine = false;
        private char actualChar;
        private int count;
        private const int TICKMAX = 3;
        private int tick = TICKMAX;
        private bool prompt = true;
        private bool prompted = false;
        private bool ended = false;

        public Message(string text)
        {
            this.text = text;
            count = text.Length;
            textBox = new Entity(1, 9, 0, 300, 0);
            textBox.SetSourceRectangle(new Rectangle(0, 0, 400, 100));

            font = Globals.Content.Load<Texture2D>("2D\\font3.spr");

            charToRect = new Rectangle[27];
            charToRect[0] = new Rectangle(0, 0, charWidth, charHeight);
            charToRect[1] = new Rectangle(charWidth, 0, charWidth, charHeight);
            charToRect[2] = new Rectangle(2* charWidth, 0, charWidth, charHeight);
            charToRect[3] = new Rectangle(3* charWidth, 0, charWidth, charHeight);
            charToRect[4] = new Rectangle(4* charWidth, 0, charWidth, charHeight);
            charToRect[5] = new Rectangle(5 * charWidth, 0, charWidth, charHeight);
            charToRect[6] = new Rectangle(6 * charWidth, 0, charWidth, charHeight);
            charToRect[7] = new Rectangle(7 * charWidth, 0, charWidth, charHeight);
            charToRect[8] = new Rectangle(8 * charWidth, 0, charWidth, charHeight);
            charToRect[9] = new Rectangle(9 * charWidth, 0, charWidth, charHeight);
            charToRect[10] = new Rectangle(10 * charWidth, 0, charWidth, charHeight);
            charToRect[11] = new Rectangle(11 * charWidth, 0, charWidth, charHeight);
            charToRect[12] = new Rectangle(12 * charWidth, 0, charWidth, charHeight);
            charToRect[13] = new Rectangle(13 * charWidth, 0, charWidth, charHeight);
            charToRect[14] = new Rectangle(14 * charWidth, 0, charWidth, charHeight);
            charToRect[15] = new Rectangle(15 * charWidth, 0, charWidth, charHeight);

            charToRect[16] = new Rectangle(0, charHeight, charWidth, charHeight);
            charToRect[17] = new Rectangle(charWidth, charHeight, charWidth, charHeight);
            charToRect[18] = new Rectangle(2* charWidth, charHeight, charWidth, charHeight);
            charToRect[19] = new Rectangle(3* charWidth, charHeight, charWidth, charHeight);
            charToRect[20] = new Rectangle(4* charWidth, charHeight, charWidth, charHeight);
            charToRect[21] = new Rectangle(5* charWidth, charHeight, charWidth, charHeight);
            charToRect[22] = new Rectangle(6* charWidth, charHeight, charWidth, charHeight);
            charToRect[23] = new Rectangle(7* charWidth, charHeight, charWidth, charHeight);
            charToRect[24] = new Rectangle(8* charWidth, charHeight, charWidth, charHeight);
            charToRect[25] = new Rectangle(9* charWidth, charHeight, charWidth, charHeight);

            charToRect[26] = new Rectangle(10 * charWidth, charHeight, charWidth, charHeight);
        }

        public bool IsEnded()
        {
            return count == 0;
        }
        
        public void Reset()
        {
            preLine = "";
            curLine = "";
            newLine = false;
            count = text.Length;
            ended = false;
            prompt = true;
            prompted = false;
            tick = TICKMAX;
            textBox.SetSourceRectangle(new Rectangle(0, 0, 400, 100));
        }

        public void Update()
        {
            if (!ended)
            {
                if (tick == TICKMAX)
                {
                    tick--;
                    if (prompt)
                    {
                        actualChar = text[text.Length - count];

                        if (actualChar == '$' || actualChar == '#')
                        {
                            prompt = false;
                            prompted = false;
                        }

                        else if (actualChar == '^') //fine messaggio
                        {
                            prompt = false;
                            prompted = false;
                        }

                        else
                        {
                            curLine += actualChar;
                        }
                        count--;
                    }

                    else
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            prompted = true;
                            prompt = true;
                        }

                        if (prompted)
                        {
                            if (actualChar == '$') //a capo
                            {
                                newLine = true;
                                preLine = curLine.ToString();
                                curLine = "";
                            }
                            else if (actualChar == '#') //paragrafo
                            {
                                newLine = false;
                                preLine = "";
                                curLine = "";
                            }
                            else if (actualChar == '^') //fine testo
                            {
                                ended = true;
                            }
                        }

                    }
                }
                else
                {
                    tick--;
                }
            }
            if (tick == 0 || actualChar == ' ') tick = TICKMAX;

            
        }

        public void DrawLine(int line, string s)
        {
            int pos = firstChar;
            foreach(char c in s.ToCharArray())
            {
                Globals._spriteBatch.Draw(font, new Rectangle(pos, line == 1 ? firstLine : secondLine, charWidth, charHeight), charToRect[c-'a'], Color.White); //change new Rectangle with predefined rectangles foreach char pos
                pos += charWidth;
            }
        }

        public void Draw()
        {
            if (!ended)
            {
                textBox.Draw();
                if (newLine)
                {
                    DrawLine(1, preLine);
                    DrawLine(2, curLine);
                    //draw preLine on line 1
                    //draw curLine on line 2
                }
                else
                {
                    DrawLine(1, curLine);
                    //draw curLine on line 1
                }
            }
        }
    }
}
