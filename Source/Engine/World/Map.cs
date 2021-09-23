using acamar.Source.Engine.Constants;
using acamar.Source.Engine.World.Entities;
using acamar.Source.Engine.World.Script;
using acamar.Source.Engine.World.Script.EventActions;
using acamar.Source.Engine.World.Script.EventConditions;
using acamar.Source.Engine.World.Script.Prompts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace acamar.Source.Engine.World
{
    class Map
    {
        private Level selfLevel;
        private int mapID = 0;
        private int tileID;
        private int width;
        private int height;
        private int tileNO;
        private int[,] mapArray;
        private Rectangle[,] tileDest;
        private int[] tileArray;
        private Texture2D tileTexture;
        private Rectangle[] tileSource;
        private List<Entity> entities = new List<Entity>();
        private Dictionary<string, Entity> entDict = new Dictionary<string, Entity>();

        private OverlayText timer = new OverlayText(Globals.runningTime.ToString(), 10, 10, FontConstants.FONT1);

        private const int LOCFLAGNO = 32;
        private int[] localFlags = new int[LOCFLAGNO];
        //private bool locked = false;

        private Message message;

        public Map(int id, Level self)
        {
            selfLevel = self;
            mapID = id;

            LoadBackground(mapID);
            TileIDArray(tileNO);



            entDict.Add("PLAYER", Globals.player);

            LoadMap(mapID);
        }

        public Map(int id, int tile, int w, int h, Level self)
        {
            selfLevel = self;
            mapID = id;
            tileID = tile;
            width = w;
            height = h;
            //mapArray = new int[h / GlobalConstants.TILESIZE, w / GlobalConstants.TILESIZE];
            //tileDest = new Rectangle[h / GlobalConstants.TILESIZE, w / GlobalConstants.TILESIZE];
            //load map data from file

            

            
            LoadBackground(mapID);
            TileIDArray(10);

            

            entDict.Add("PLAYER", Globals.player);

            LoadMap(mapID);

            /*
            LoadEntities(mapID);



            
            entities.Add(Globals.player);

            Event evn = new Event();
            //evn.AddCondition(new PositionCondition(Globals.player, 158, 258, 17, PositionCondition.POSTYPE.POSNEAR));
            
            evn.AddCondition(new PositionCondition(Globals.player, (Character)entities[0], PositionCondition.POSTYPE.POSTOUCHFACING));
            evn.AddCondition(new ButtonCondition(Keys.Z, ButtonCondition.KEYSTATE.ISPRESSED));

            List<int> flgs = new List<int>();
            flgs.Add(12);
            evn.AddCondition(new FlagCondition(flgs, false));
            evn.AddAction(new MessageAction("ciao$testo{di{prova$testo#paragrafo{di{prova$^", entities[0]));
            evn.AddAction(new FlagAction(flgs, 1));

            Event evn2 = new Event();
            evn2.AddCondition(new FlagCondition(flgs, true));
            List<int> flgs2 = new List<int>();
            flgs2.Add(13);
            evn2.AddCondition(new FlagCondition(flgs2, false));
            //evn2.AddCondition(new PositionCondition(Globals.player, 158, 258, 17, PositionCondition.POSTYPE.POSNEAR));
            evn2.AddCondition(new PositionCondition(Globals.player, (Character)entities[0], PositionCondition.POSTYPE.POSTOUCHFACING));


            //evn2.AddAction(new MessageAction("evento{attivato$flag{impostata$^", entities[0]));
            //evn2.AddAction(new TeleportAction(10, 20, player));
            evn2.AddAction(new FlagAction(flgs2, 1));
            evn2.AddAction(new MessageAction("movimento$^", entities[0]));
            evn2.AddAction(new BlockAction(Globals.player, BlockAction.BLOCKTYPE.LOCK));
            evn2.AddAction(new MoveAction(150, 300, (Character)entities[0]));
            evn2.AddAction(new SleepAction(240));
            evn2.AddAction(new MoveAction(300, 300, (Character)entities[0]));
            evn2.AddAction(new MoveAction(300, 150, (Character)entities[0]));
            evn2.AddAction(new BlockAction(Globals.player, BlockAction.BLOCKTYPE.UNLOCK));
            evn2.AddCondition(new ButtonCondition(Keys.Z, ButtonCondition.KEYSTATE.ISPRESSED));


            //Event evn4 = new Event();
            //evn4.AddCondition(new NoCondition());
            //evn4.AddAction(new FaceAction((Character)entities[0], Globals.player));
            //entities[0].AddEvent(evn4);


            entities[0].AddEvent(evn2);
            entities[0].AddEvent(evn);
            



            Event evn3 = new Event();
            evn3.AddAction(new FaceAction((Character)entities[1], Globals.player));
            evn3.AddCondition(new PositionCondition(Globals.player, 108, 108, 17, PositionCondition.POSTYPE.POSNEAR));
            evn3.AddCondition(new ButtonCondition(Keys.Z, ButtonCondition.KEYSTATE.ISPRESSED));
            evn3.AddAction(new MessageAction("teletrasporto$^", entities[0]));
            evn3.AddAction(new TeleportAction(selfLevel, 1, 10, 10, Globals.player));

            entities[1].AddEvent(evn3);

            entities.Add(new OverlayText("Level " + selfLevel.GetId() + " Map " + mapID, 10, 10, FontConstants.FONT0));
            //message = new Message("testo{di{prova{testo{di{prova$testo{tsto{estoset$testo{di{prova{testo{di{prova$aaaaaaaaaaaaaaaaa#paragrafo#paragrafo^");
        
        
            */

        }

        public void SetPlayer(Player player)
        {
            entities.Add(player);
        }
        
        public void Update()
        {
            //DEBUG
            timer = new OverlayText(Globals.runningTime.ToString(), 10, 10, FontConstants.FONT1);


            foreach (Entity ent in entities)
            {
                ent.Update();
            }

            for (int i = 0; i < entities.Count; i++)
            {
                for (int j = i + 1; j < entities.Count; j++)
                {
                    if (entities[i].Collide(entities[j]) && entities[i].IsEnabled() && entities[j].IsEnabled())
                    {
                        entities[i].HandleCollision(entities[j]);

                        //MessageHandler.currentMessage = new Message("collision$^");
                        //MessageHandler.Activate();

                        ////TransitionHandler.preTransition = TransitionHandler.LTORPRE;
                        ////TransitionHandler.postTransition = TransitionHandler.LTORPOST;
                        ////TransitionHandler.Activate();
                        ////TransitionHandler.prevMap = this;
                        ////TransitionHandler.nextMap = this;

                        //break;
                    }
                }
            }
            //...

            //message.Update();
        }

        public void Draw(SpriteBatch batch)
        {
            DrawBackground();
            foreach (Entity ent in entities)
            {
                ent.Draw(batch);
            }

            timer.Draw(Globals._overBatch);

            OverlayText position = new OverlayText(Globals.player.GetPosX() + " " + Globals.player.GetPosY(),10,20, FontConstants.FONT1);
            position.Draw(Globals._overBatch);
            //message.Draw();
            //DEBUG
        }

        private void TileIDArray(int tileNumber)
        {
            int size = (int)(Math.Sqrt(tileNumber)) + 1;
            tileArray = new int[size*size];
            tileSource = new Rectangle[tileNumber];
            for(int i = 0; i<size; i++)
            {
                for(int j = 0; j<size; j++)
                {
                    tileArray[i * size + j] = Math.Max(i, j) * Math.Max(i, j) + i + 1 + ((i > j) ? (i - j) : 0) - 1;
                    if (tileArray[i * size + j] < tileNumber)
                    {
                        tileSource[tileArray[i * size + j]] = 
                            new Rectangle(
                                j * GlobalConstants.TILESIZE,
                                i * GlobalConstants.TILESIZE,
                                GlobalConstants.TILESIZE,
                                GlobalConstants.TILESIZE);
                    }
                }
            }
        }

        public int GetId()
        {
            return mapID;
        }

        private void DrawBackground()
        {
            for(int i = 0; i < height/ GlobalConstants.TILESIZE; i++)
            {
                for(int j = 0; j < width/ GlobalConstants.TILESIZE; j++)
                {
                    Globals._spriteBatch.Draw(tileTexture,  tileDest[i, j], tileSource[mapArray[i, j]], Color.White);
                }
            }
        }

        private void LoadEntities(int id)
        {
            string filename = "Content\\" + id + ".ent.xml";
            // file structure (es 1.ent)
            /*
            *<entity>
            *    <entid = 0> (entity id in level)
            *    <type = 12> (entity type, eg. character, interactable objects, ...)
            *    <sprid = 23> (entity sprite id, to open correct sprite and/or animation file and/or collision file)
            *    <posx = 113> (starting xpos in map)
            *    <posy = 200> (starting ypos in map)
            *    <dir = 0> (starting dir of entity from 0,1,2,3)
            *</entity>
            */

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);

            XmlNodeList entid = xDoc.GetElementsByTagName("entid");
            XmlNodeList type = xDoc.GetElementsByTagName("type");
            XmlNodeList sprid = xDoc.GetElementsByTagName("sprid");
            XmlNodeList posx = xDoc.GetElementsByTagName("posx");
            XmlNodeList posy = xDoc.GetElementsByTagName("posy");
            XmlNodeList dir  = xDoc.GetElementsByTagName("dir");

            for (int i=0; i<type.Count; i++)
            {
                int eid = Int32.Parse(entid[i].InnerText);
                int x   = Int32.Parse(posx[i].InnerText);
                int y   = Int32.Parse(posy[i].InnerText);
                int d   = Int32.Parse(dir[i].InnerText);
                int spr = Int32.Parse(sprid[i].InnerText);
                switch (Int32.Parse(type[i].InnerText))
                {
                    //load different types of entities
                    case 12:
                        entities.Add(new Character(eid, spr, x, y, d));
                        break;
                    default:
                        entities.Add(new Entity(eid, spr, x, y, d));
                        break;
                }
                
            }
        }


        private void LoadBackground(int id)
        {
            string filename = "Content\\map" + id + ".tm";

            using (TextReader reader = File.OpenText(filename))
            {
                //int x = int.Parse(bits[0]);
                tileID = int.Parse(reader.ReadLine());
                tileNO = int.Parse(reader.ReadLine());
                width  = int.Parse(reader.ReadLine()) * GlobalConstants.TILESIZE;
                height = int.Parse(reader.ReadLine()) * GlobalConstants.TILESIZE;

                mapArray = new int[height / GlobalConstants.TILESIZE, width / GlobalConstants.TILESIZE];
                tileDest = new Rectangle[height / GlobalConstants.TILESIZE, width / GlobalConstants.TILESIZE];

                for (int i = 0; i < height / GlobalConstants.TILESIZE; i++)
                {
                    string text = reader.ReadLine();
                    string[] bits = text.Split(' ');
                    for (int j = 0; j < width / GlobalConstants.TILESIZE; j++)
                    {
                        mapArray[i, j] = int.Parse(bits[j]);
                        tileDest[i, j] = new Rectangle(
                            j * GlobalConstants.TILESIZE,
                            i * GlobalConstants.TILESIZE,
                            GlobalConstants.TILESIZE,
                            GlobalConstants.TILESIZE);
                    }
                }
            }


            //mapArray[i, j] = value;
            //tileDest[i, j] = new Rectangle(j*Constants.TILESIZE, i*Constants.TILESIZE, Constants.TILESIZE, Constants.TILESIZE);

            string tilePATH = "2D\\" + tileID + ".tl";
            tileTexture = Globals.Content.Load<Texture2D>(tilePATH);
        }

        public void ChangeMap(int id)
        {
            selfLevel.SetMap(id);
        }

        public void ChangeLevel(int id)
        {
            selfLevel.ChangeLevel(id);
        }

        private void LoadMap(int id)
        {
            string filename = "Content\\map" + id + ".mp";
            string[] lines = File.ReadAllLines(filename);

            tileID = int.Parse(lines[1].Split(' ')[1]);

            bool loadingEnt = false;
            bool loadingEvn = false;

            string entName = "";
            int entPosx = 0;
            int entPosy = 0;
            int entSprID = 0;
            int entHeight = 0;
            int entWidth = 0;

            int entCPosx = 0;
            int entCPosy = 0;
            int entCWidth = 0;
            int entCHeight = 0;

            int entSPosx = 0;
            int entSPosy = 0;
            int entSWidth = 0;
            int entSHeight = 0;
            int curOption = 0;

            bool preEvn = false;
            bool promptAcn = false;

            int entLayer = 0;

            Event evn = null;
            RunningPrompt currentPrompt = null;
            EntityConstants.ENTTYPE entType;
            Entity curEnt = null;

            foreach (string line in lines)
            {
                

                if (line == "TNE")
                {
                    loadingEnt = false;
                    
                    entName = "";
                    entPosx = 0;
                    entPosy = 0;
                    entSprID = 0;
                    entHeight = 0;
                    entWidth = 0;

                    entCPosx = 0;
                    entCPosy = 0;
                    entCWidth = 0;
                    entCHeight = 0;
                    entLayer = 0;
                }

                if (loadingEnt)
                {
                    if (!loadingEvn)
                    {
                        switch (line.Split('\t')[1]) //Entity initialization
                        {
                            case "NAME":
                                entName = line.Split('\t')[2];
                                break;
                            case "POSX":
                                entPosx = int.Parse(line.Split('\t')[2]);
                                break;
                            case "POSY":
                                entPosy = int.Parse(line.Split('\t')[2]);
                                break;
                            case "CPOSX":
                                entCPosx = int.Parse(line.Split('\t')[2]);
                                break;
                            case "CPOSY":
                                entCPosy = int.Parse(line.Split('\t')[2]);
                                break;
                            case "SPOSX":
                                entSPosx = int.Parse(line.Split('\t')[2]);
                                break;
                            case "SPOSY":
                                entSPosy = int.Parse(line.Split('\t')[2]);
                                break;
                            case "SPRD":
                                entSprID = int.Parse(line.Split('\t')[2]);
                                break;
                            case "HEIGHT":
                                entHeight = int.Parse(line.Split('\t')[2]);
                                break;
                            case "WIDTH":
                                entWidth = int.Parse(line.Split('\t')[2]);
                                break;
                            case "CHEIGHT":
                                entCHeight = int.Parse(line.Split('\t')[2]);
                                break;
                            case "CWIDTH":
                                entCWidth = int.Parse(line.Split('\t')[2]);
                                break;
                            case "SHEIGHT":
                                entSHeight = int.Parse(line.Split('\t')[2]);
                                break;
                            case "SWIDTH":
                                entSWidth = int.Parse(line.Split('\t')[2]);
                                break;
                            case "LAYER":
                                entLayer = int.Parse(line.Split('\t')[2]);
                                break;
                            case "INAC":
                                curEnt.Deactivate();
                                break;
                            case "TYPE":
                                switch (line.Split('\t')[2].Split(' ')[0])
                                {
                                    case "ITEM":
                                        entType = EntityConstants.ENTTYPE.ITEM;
                                        curEnt = new Item();
                                        curEnt.SetPosition(entPosx, entPosy);
                                        curEnt.SetCollisionRectangle(entCPosx, entCPosy, entCWidth, entCHeight);
                                        curEnt.SetLayer(entLayer);
                                        curEnt.SetSprite(entSprID);
                                        curEnt.SetSourceRectangle(new Rectangle(entSPosx, entSPosy, entSWidth, entSHeight));
                                        entities.Add(curEnt);
                                        entDict.Add(entName, curEnt);
                                        curEnt.Activate();
                                        break;
                                    case "SIMPLEENT":
                                        entType = EntityConstants.ENTTYPE.CHARACTER;
                                        curEnt = new Entity();
                                        //curEnt.Deactivate();
                                        curEnt.SetPosition(entPosx, entPosy);
                                        curEnt.SetCollisionRectangle(entCPosx, entCPosy, entCWidth, entCHeight);
                                        entities.Add(curEnt);
                                        entDict.Add(entName, curEnt);
                                        break;
                                    case "OVERTEXT":
                                        entType = EntityConstants.ENTTYPE.OVERTEXT;
                                        curEnt = new OverlayText(
                                            line.Split('<')[1],
                                            entPosx,
                                            entPosy,
                                            FontConstants.FontDictionary.GetValueOrDefault(line.Split(' ')[1]));
                                        entities.Add(curEnt);
                                        entDict.Add(entName, curEnt);
                                        curEnt.Activate();
                                        break;
                                    case "PLAYER":
                                        Globals.player.SetPosition(entPosx, entPosy);
                                        curEnt = Globals.player;
                                        curEnt.SetCollisionRectangle(entCPosx, entCPosy, entCWidth, entCHeight);
                                        entities.Add(curEnt);

                                        curEnt.Activate();
                                        break;
                                    case "EVENT":
                                        entType = EntityConstants.ENTTYPE.EVENT;
                                        curEnt = new Entity();
                                        entities.Add(curEnt);
                                        curEnt.Activate();
                                        break;
                                }
                                
                                break;
                            case "EVN":
                                evn = new Event();
                                preEvn = false;
                                loadingEvn = true;
                                break;
                        }
                    }

                    else
                    {
                        if (line.Split('\t')[1] == "NVE")
                        {
                            loadingEvn = false;
                            curEnt.AddEvent(evn);
                            if(preEvn)
                                evn.Trigger();
                        }
                        if (loadingEvn)
                        {
                            switch (line.Split('\t')[2])
                            {
                                case "PREEVN":
                                    preEvn = true;
                                    break;
                                case "COND":
                                    string lin = line.Split('\t')[3];
                                    switch (lin.Split(' ')[0])
                                    {
                                        case "NOCOND":
                                            evn.AddCondition(new NoCondition());
                                            break;
                                        case "LOCFLGISSET":
                                            evn.AddCondition(new LocalFlagCondition(int.Parse(lin.Split(' ')[1]), true, this));
                                            break;
                                        case "LOCFLGNOTSET":
                                            evn.AddCondition(new LocalFlagCondition(int.Parse(lin.Split(' ')[1]), false, this));
                                            break;
                                        case "FLGISSET":
                                            evn.AddCondition(new FlagCondition(int.Parse(lin.Split(' ')[1]), true));
                                            break;
                                        case "FLGNOTSET":
                                            evn.AddCondition(new FlagCondition(int.Parse(lin.Split(' ')[1]), false));
                                            break;
                                        case "BUTPRES":
                                            Keys k = Keys.Space;
                                            if (lin.Split(' ')[1] == "A") k = Keys.Z;
                                            evn.AddCondition(new ButtonCondition(
                                                k,
                                                int.Parse(lin.Split(' ')[2]) == 1 ? ButtonCondition.KEYSTATE.ISPRESSED : ButtonCondition.KEYSTATE.ISRELEASED));
                                            break;
                                        case "HASITEM":
                                            evn.AddCondition(new ItemCondition(Globals.player, lin.Split(' ')[1], ItemCondition.TYPE.HASITEM));
                                            break;
                                        case "HASNOTITEM":
                                            evn.AddCondition(new ItemCondition(Globals.player, lin.Split(' ')[1], ItemCondition.TYPE.HASNOTITEM));
                                            break;
                                        case "POSTOUCH":
                                            string targetT = lin.Split(' ')[1];
                                            string sourceT = lin.Split(' ')[2];
                                            if (sourceT == "SELF")
                                                evn.AddCondition(new PositionCondition(
                                                    (Character)entDict.GetValueOrDefault(targetT),
                                                    curEnt,
                                                    PositionCondition.POSTYPE.POSTOUCH));
                                            else
                                                evn.AddCondition(new PositionCondition(
                                                    (Character)entDict.GetValueOrDefault(targetT),
                                                    entDict.GetValueOrDefault(sourceT),
                                                    PositionCondition.POSTYPE.POSTOUCH));
                                            break;
                                        case "POSTOUCHFACING":
                                            string target = lin.Split(' ')[1];
                                            string source = lin.Split(' ')[2];
                                            if (source == "SELF")
                                                evn.AddCondition(new PositionCondition(
                                                    (Character)entDict.GetValueOrDefault(target),
                                                    curEnt,
                                                    PositionCondition.POSTYPE.POSTOUCHFACING));
                                            else
                                                evn.AddCondition(new PositionCondition(
                                                    (Character)entDict.GetValueOrDefault(target),
                                                    entDict.GetValueOrDefault(source),
                                                    PositionCondition.POSTYPE.POSTOUCHFACING));
                                            break;
                                    }
                                    break;
                                case "PRMT":
                                    string lin3 = line.Split('\t')[3];
                                    curOption = int.Parse(lin3.Split(' ')[0]);
                                    break;
                                case "ACTN":
                                case "PRAC":
                                    if(line.Split('\t')[2] == "ACTN")
                                    {
                                        promptAcn = false;
                                    }
                                    if(line.Split('\t')[2] == "PRAC")
                                    {
                                        promptAcn = true;
                                    }
                                    string lin2 = line.Split('\t')[3];
                                    EventAction curAction = null;
                                    switch (lin2.Split(' ')[0])
                                    {
                                        case "PROMPT":
                                            string promptMessage = lin2.Split('<')[1];
                                            List<string> promptOptions = new List<string>();
                                            for(int i = 2; i < lin2.Split('<').Length; i++)
                                            {
                                                promptOptions.Add(lin2.Split('<')[i]);
                                            }
                                            currentPrompt = new RunningPrompt(promptMessage, promptOptions);
                                            //promptAcn = true;
                                            curAction = new PromptAction(currentPrompt);
                                            //evn.AddAction(new PromptAction(currentPrompt));
                                            break;
                                        case "NOACN":
                                            curAction = new NoAction();
                                            break;

                                        case "SAVE":
                                            curAction = new SaveAction(1);
                                            //evn.AddAction(new SaveAction(1));
                                            break;

                                        case "LOCFLGSET":
                                            curAction = new LocalFlagAction(int.Parse(lin2.Split(' ')[1]), 1, this);
                                            //evn.AddAction(new LocalFlagAction(int.Parse(lin2.Split(' ')[1]), 1, this));
                                            break;
                                        case "FLGSET":
                                            curAction = new FlagAction(int.Parse(lin2.Split(' ')[1]), 1);
                                            //evn.AddAction(new FlagAction(int.Parse(lin2.Split(' ')[1]), 1));
                                            break;
                                        case "ACTIVATE":
                                            if (lin2.Split(' ')[1] == "SELF")
                                            {
                                                curAction = new ActivateAction(curEnt, ActivateAction.ACTIVTYPE.ACTIVATE);
                                                //evn.AddAction(new ActivateAction(curEnt, ActivateAction.ACTIVTYPE.ACTIVATE));
                                            }
                                            else
                                            {
                                                curAction = new ActivateAction(entDict.GetValueOrDefault(lin2.Split(' ')[1]), ActivateAction.ACTIVTYPE.ACTIVATE);
                                                //evn.AddAction(new ActivateAction(entDict.GetValueOrDefault(lin2.Split(' ')[1]), ActivateAction.ACTIVTYPE.ACTIVATE));
                                            }
                                            break;
                                        case "DEACTIVATE":
                                            if (lin2.Split(' ')[1] == "SELF")
                                            {
                                                curAction = new ActivateAction(curEnt, ActivateAction.ACTIVTYPE.DEACTIVATE);
                                                //evn.AddAction(new ActivateAction(curEnt, ActivateAction.ACTIVTYPE.DEACTIVATE));
                                            }
                                            else
                                            {
                                                curAction = new ActivateAction(entDict.GetValueOrDefault(lin2.Split(' ')[1]), ActivateAction.ACTIVTYPE.DEACTIVATE);
                                                //evn.AddAction(new ActivateAction(entDict.GetValueOrDefault(lin2.Split(' ')[1]), ActivateAction.ACTIVTYPE.DEACTIVATE));
                                            }
                                            break;
                                        case "SLEEP":
                                            curAction = new SleepAction(int.Parse(lin2.Split(' ')[1]));
                                            //evn.AddAction(new SleepAction(int.Parse(lin2.Split(' ')[1])));
                                            break;
                                        case "MESSAGE":
                                            curAction = new MessageAction(lin2.Split('<')[1], curEnt);
                                            //evn.AddAction(new MessageAction(lin2.Split('<')[1], curEnt));
                                            break;
                                        case "FADEIN":
                                            if (lin2.Split(' ')[1] == "SELF")
                                            {
                                                curAction = new FadeAction(curEnt, FadeAction.FADETYPE.FADEIN);
                                                //evn.AddAction(new FadeAction(curEnt, FadeAction.FADETYPE.FADEIN));
                                            }
                                            else
                                            {
                                                curAction = new FadeAction(entDict.GetValueOrDefault(lin2.Split(' ')[1]), FadeAction.FADETYPE.FADEIN);
                                                //evn.AddAction(new FadeAction(entDict.GetValueOrDefault(lin2.Split(' ')[1]), FadeAction.FADETYPE.FADEIN));
                                            }
                                            break;
                                        case "FADEOUT":
                                            if (lin2.Split(' ')[1] == "SELF")
                                            {
                                                curAction = new FadeAction(curEnt, FadeAction.FADETYPE.FADEOUT);
                                                //evn.AddAction(new FadeAction(curEnt, FadeAction.FADETYPE.FADEOUT));
                                            }
                                            else
                                            {
                                                curAction = new FadeAction(entDict.GetValueOrDefault(lin2.Split(' ')[1]), FadeAction.FADETYPE.FADEOUT);
                                                //evn.AddAction(new FadeAction(entDict.GetValueOrDefault(lin2.Split(' ')[1]), FadeAction.FADETYPE.FADEOUT));
                                            }
                                            break;
                                        case "LOCK":
                                            if (lin2.Split(' ')[1] == "SELF")
                                            {
                                                curAction = new BlockAction(curEnt, BlockAction.BLOCKTYPE.LOCK);
                                                //evn.AddAction(new BlockAction(curEnt, BlockAction.BLOCKTYPE.LOCK));
                                            }
                                            else
                                            {
                                                curAction = new BlockAction(entDict.GetValueOrDefault(lin2.Split(' ')[1]), BlockAction.BLOCKTYPE.LOCK);
                                                //evn.AddAction(new BlockAction(entDict.GetValueOrDefault(lin2.Split(' ')[1]), BlockAction.BLOCKTYPE.LOCK));
                                            }
                                            break;
                                        case "UNLOCK":
                                            if (lin2.Split(' ')[1] == "SELF")
                                            {
                                                curAction = new BlockAction(curEnt, BlockAction.BLOCKTYPE.UNLOCK);
                                                //evn.AddAction(new BlockAction(curEnt, BlockAction.BLOCKTYPE.UNLOCK));
                                            }
                                            else
                                            {
                                                curAction = new BlockAction(entDict.GetValueOrDefault(lin2.Split(' ')[1]), BlockAction.BLOCKTYPE.UNLOCK);
                                                //evn.AddAction(new BlockAction(entDict.GetValueOrDefault(lin2.Split(' ')[1]), BlockAction.BLOCKTYPE.UNLOCK));
                                            }
                                            break;
                                        case "GIVEITEM":
                                            if (lin2.Split(' ')[1] == "SELF")
                                            {
                                                curAction = new GiveItemAction(Globals.player, entName);
                                                //evn.AddAction(new GiveItemAction(Globals.player, entName));
                                            }
                                            else
                                            {
                                                curAction = new GiveItemAction(Globals.player, lin2.Split(' ')[1]);
                                                //evn.AddAction(new GiveItemAction(Globals.player, lin2.Split(' ')[1]));
                                            }
                                            break;
                                        case "DISABLE":
                                            if (lin2.Split(' ')[1] == "SELF")
                                            {
                                                curAction = new DisableAction(curEnt, DisableAction.TYPE.DISABLE);
                                                //evn.AddAction(new DisableAction(curEnt, DisableAction.TYPE.DISABLE));
                                            }
                                            else
                                            {
                                                curAction = new DisableAction(entDict.GetValueOrDefault(lin2.Split(' ')[1]), DisableAction.TYPE.DISABLE);
                                                //evn.AddAction(new DisableAction(entDict.GetValueOrDefault(lin2.Split(' ')[1]), DisableAction.TYPE.DISABLE));
                                            }
                                            break;
                                        case "ENABLE":
                                            if (lin2.Split(' ')[1] == "SELF")
                                            {
                                                curAction = new DisableAction(curEnt, DisableAction.TYPE.ENABLE);
                                                //evn.AddAction(new DisableAction(curEnt, DisableAction.TYPE.ENABLE));
                                            }
                                            else
                                            {
                                                curAction = new DisableAction(entDict.GetValueOrDefault(lin2.Split(' ')[1]), DisableAction.TYPE.ENABLE);
                                                //evn.AddAction(new DisableAction(entDict.GetValueOrDefault(lin2.Split(' ')[1]), DisableAction.TYPE.ENABLE));
                                            }
                                            break;
                                        case "TELEPORT":
                                            int level = int.Parse(lin2.Split(' ')[1]);
                                            int map = int.Parse(lin2.Split(' ')[2]);
                                            int posx = int.Parse(lin2.Split(' ')[3]);
                                            int posy = int.Parse(lin2.Split(' ')[4]);
                                            string target = lin2.Split(' ')[5];

                                            if (level == selfLevel.GetId() && map == mapID)
                                                curAction = new TeleportAction(posx, posy, (Character)entDict.GetValueOrDefault(target));
                                            //evn.AddAction(new TeleportAction(posx, posy, (Character)entDict.GetValueOrDefault(target)));
                                            else if (level == selfLevel.GetId() && map != mapID)
                                                curAction = new TeleportAction(
                                                    selfLevel,
                                                    map,
                                                    posx,
                                                    posy,
                                                    (Character)entDict.GetValueOrDefault(target));
                                            /*evn.AddAction(new TeleportAction(
                                                selfLevel,
                                                map,
                                                posx,
                                                posy,
                                                (Character)entDict.GetValueOrDefault(target)));*/
                                            else
                                                curAction = new TeleportAction(
                                                    Globals.world,
                                                    level,
                                                    map,
                                                    posx,
                                                    posy,
                                                    (Character)entDict.GetValueOrDefault(target));
                                                /*evn.AddAction(new TeleportAction(
                                                    Globals.world,
                                                    level,
                                                    map,
                                                    posx,
                                                    posy,
                                                    (Character)entDict.GetValueOrDefault(target)));*/
                                            break;
                                    }
                                    if (promptAcn)
                                        currentPrompt.AddAction(curAction, curOption);
                                    else
                                        evn.AddAction(curAction);
                                    break;
                            }
                        }
                    }
                }

                if (line.Split(' ')[0] == "ENT")
                {
                    loadingEnt = true;
                }

                
            }

            //SetLevel(int.Parse(lines[0]));
            //currentLevel.SetMap(int.Parse(lines[1]));
            //Flag.SetFlags(lines[2]);
            //Globals.player.SetPosition(int.Parse(lines[3].Split(' ')[0]), int.Parse(lines[3].Split(' ')[1]));
            //Globals.player.SetDir(int.Parse(lines[3].Split(' ')[2]));
        }

        public void SetFlag(int id)
        {
            localFlags[id] = 1;
        }

        public void UnsetFlag(int id)
        {
            localFlags[id] = 0;
        }

        public bool CheckFlag(int flagId)
        {
            return localFlags[flagId] == 0 ? false : true;
        }

        public void ResetFlags()
        {
            for (int i = 0; i < LOCFLAGNO; i++)
            {
                UnsetFlag(i);
            }

            //DEBUG
            timer = new OverlayText(Globals.runningTime.ToString(), 10, 10, FontConstants.FONT1);
        }

        //private EventAction ParseAction(string line)
        //{

        //}

    }
}
