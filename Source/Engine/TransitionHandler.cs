using acamar.Source.Engine.World;
using acamar.Source.Engine.World.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine
{
    class TransitionHandler
    {
        public static Map prevMap;
        public static Map nextMap;

        public static Transition preTransition;
        public static Transition postTransition;

        public static Transition LTORPRE = new Transition(Transition.TRANDIR.LTOR, Transition.TRANTYPE.PRE);
        public static Transition LTORPOST = new Transition(Transition.TRANDIR.LTOR, Transition.TRANTYPE.POST);
        public static Transition RTOLPRE = new Transition(Transition.TRANDIR.RTOL, Transition.TRANTYPE.PRE);
        public static Transition RTOLPOST = new Transition(Transition.TRANDIR.RTOL, Transition.TRANTYPE.POST);
        public static Transition UTODPRE = new Transition(Transition.TRANDIR.UTOD, Transition.TRANTYPE.PRE);
        public static Transition UTODPOST = new Transition(Transition.TRANDIR.UTOD, Transition.TRANTYPE.POST);
        public static Transition DTOUPRE = new Transition(Transition.TRANDIR.DTOU, Transition.TRANTYPE.PRE);
        public static Transition DTOUPOST = new Transition(Transition.TRANDIR.DTOU, Transition.TRANTYPE.POST);

        private static bool active = false;
        private static TRANSTATUS status;
        private enum TRANSTATUS
        {
            PRE,
            POST,
            END
        }

        public static void Activate()
        {
            active = true;
            Globals.CURRENTSTATE = Globals.STATE.PAUSE;
            status = TRANSTATUS.PRE;
            preTransition.ResetArray();
            preTransition.ResetCount();
            postTransition.ResetArray();
            postTransition.ResetCount();
        }

        public static bool IsActive()
        {
            return active;
        }

        public static void Update()
        {
            switch(status)
            {
                case TRANSTATUS.PRE:
                    preTransition.Update();
                    break;
                case TRANSTATUS.POST:
                    postTransition.Update();
                    break;
                case TRANSTATUS.END:
                    break;
            }
            if(preTransition.IsEnded() && status == TRANSTATUS.PRE)
            {
                status = TRANSTATUS.POST;
            }
            if(postTransition.IsEnded() && status == TRANSTATUS.POST)
            {
                status = TRANSTATUS.END;
                Globals.CURRENTSTATE = Globals.STATE.RUNNING;
                active = false;
            }

        }

        public static void Draw()
        {
            switch (status)
            {
                case TRANSTATUS.PRE:
                    prevMap.Draw();
                    preTransition.Draw();
                    break;
                case TRANSTATUS.POST:
                    nextMap.Draw();
                    postTransition.Draw();
                    break;
                case TRANSTATUS.END:
                    break;
            }
        }
    }
}
