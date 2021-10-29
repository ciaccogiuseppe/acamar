using acamar.Source.Engine.Constants;
using acamar.Source.Engine.Graphics;
using acamar.Source.Engine.World.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World.Script.EventActions
{
    class OverMessageAction : EventAction
    {
        private string message;
        private int posx;
        private int posy;
        private Font font;
        private int step;
        private int cnt = 0;
        private int stepCnt = 0;
        private string curText = "";

        private OverlayText text;

        private bool ended = false;
        private bool started = false;
        public OverMessageAction(string message, int posx, int posy, Font font, int step = 7)
        {
            this.message = message;
            this.posx = posx;
            this.posy = posy;
            this.font = font;
            this.step = step;

            text = new OverlayText("", posx, posy, font);
        }


        public override void Trigger()
        {
            
            if (!started)
            {
                started = true;
                curText = "" + message[cnt++];
                text.SetText(curText);
                OverlayEntitiesHandler.AddEntity(text);
            }
        }

        public override bool IsEnded()
        {
            if(cnt == message.Length)
            {
                OverlayEntitiesHandler.RemoveEntity(text);
                ended = true;
            }
            else
            {
                if(stepCnt == step)
                {
                    curText += message[cnt++];
                    text.SetText(curText);
                    stepCnt = 0;
                }
                else
                {
                    stepCnt++;
                }
                //text.Draw(Globals._overBatch);
            }

            return ended;
        }

        public override void Reset()
        {
            started = false;
            ended = false;
            curText = "";
            cnt = 0;
            stepCnt = 0;
        }

        public override bool IsStarted()
        {
            return started;
        }

        public override bool GetEnded()
        {
            return ended;
        }
    }
}
