using acamar.Source.Engine.Constants;
using acamar.Source.Engine.Graphics;
using acamar.Source.Engine.World.Entities;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script
{
    class Prompt
    {
        protected Message promptMessage;
        protected List<string> promptOptions;
        protected bool active = false;
        protected PromptPage promptPage;
        protected bool ended = false;
        
        internal class PromptPage
        {
            private List<string> promptOptions;
            private int selectedOption = 0;
            private Font selFont;
            private Font unselFont;
            private int posx = 340;
            private int posy = 340;
            private int step = 20;
            private bool ended = false;

            internal PromptPage(List<string> promptOptions, Font selFont, Font unselFont)
            {
                this.promptOptions = promptOptions;
                this.selFont = selFont;
                this.unselFont = unselFont;
            }

            internal void Draw(SpriteBatch batch)
            {
                int posyy = posy;
                for(int i = 0; i < promptOptions.Count; i++)
                {
                    if (i == selectedOption)
                        unselFont.Draw("@"+promptOptions[i], posx, posyy, batch, 1);
                    else
                        unselFont.Draw(" "+promptOptions[i], posx, posyy, batch, 1);

                    posyy += step;
                }
            }

            internal void Act()
            {
                Globals.PROMPTRESULT = selectedOption;
                ended = true;
            }

            internal void Update()
            {
                if (MyKeyboard.IsPressedNotCont(Keys.Down))
                {
                    selectedOption = (selectedOption + 1) % promptOptions.Count;
                }

                else if (MyKeyboard.IsPressedNotCont(Keys.Up))
                {
                    if (selectedOption > 0)
                        selectedOption--;
                    else
                        selectedOption = promptOptions.Count - 1;
                }
                else if (MyKeyboard.IsPressedNotCont(Keys.Z))
                {
                    Act();
                }
            }

            internal bool IsEnded()
            {
                return ended;
            }

            internal void Reset()
            {
                ended = false;
                selectedOption = 0;
            }
        }

        public Prompt(string promptMessage, List<string> promptOptions)
        {
            this.promptMessage = new Message(promptMessage);
            this.promptOptions = promptOptions;
            promptPage = new PromptPage(promptOptions, FontConstants.FONT4, FontConstants.FONT3);
        }

        public virtual void Activate()
        {
        }

        public bool IsEnded()
        {
            //return promptPage.IsEnded();
            return ended;
        }

        public virtual void Update()
        {

        }

        public void Draw(SpriteBatch batch)
        {
            if(!promptPage.IsEnded())
            {
                if (!promptMessage.IsEnded())
                {
                    promptMessage.Draw(batch);
                }
                else
                {
                    promptMessage.Draw(batch);
                    promptPage.Draw(batch);
                }
            }
            
        }

        public virtual void Reset()
        {
            active = false;
            promptMessage.Reset();
            promptPage.Reset();
            ended = false;
        }
    }
}
