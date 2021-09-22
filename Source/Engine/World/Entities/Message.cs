using acamar.Source.Engine.Constants;
using acamar.Source.Engine.Graphics;
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
        private bool newLine = false;
        private char actualChar;
        private int count;
        private const int TICKMAX = 3;
        private int tick = TICKMAX;
        private bool prompt = true;
        private bool prompted = false;
        private bool ended = false;
        private Font currentFont = FontConstants.FONT0;

        public Message(string text)
        {
            this.text = text;
            count = text.Length;
            textBox = new Entity(1, 9, 0, 300, 0);
            textBox.SetSourceRectangle(new Rectangle(0, 0, 400, 100));

        }

        public void SetFont(Font font)
        {
            this.currentFont = font;
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
                        //if (Keyboard.GetState().IsKeyDown(Keys.Z))
                        if (MyKeyboard.IsPressedNotCont(Keys.Z))
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

        public void Draw(SpriteBatch batch)
        {
            if (!ended)
            {
                textBox.Draw(batch);
                if (newLine)
                {

                    currentFont.Draw(preLine, firstChar, firstLine, batch, 1);
                    currentFont.Draw(curLine, firstChar, secondLine, batch, 1);
                    //draw preLine on line 1
                    //draw curLine on line 2
                }
                else
                {
                    currentFont.Draw(curLine, firstChar, firstLine, batch, 1);
                    //draw curLine on line 1
                }
            }
        }
    }
}
