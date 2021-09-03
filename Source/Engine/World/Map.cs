using acamar.Source.Engine.World.Entities;
using acamar.Source.Engine.World.Script;
using acamar.Source.Engine.World.Script.EventActions;
using acamar.Source.Engine.World.Script.EventConditions;
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
        private int[,] mapArray;
        private Rectangle[,] tileDest;
        private int[] tileArray;
        private Texture2D tileTexture;
        private Rectangle[] tileSource;
        private List<Entity> entities = new List<Entity>();


        private Message message;

        public Map(int id, int tile, int w, int h, Level self)
        {
            selfLevel = self;
            mapID = id;
            tileID = tile;
            width = w;
            height = h;
            mapArray = new int[h / Constants.TILESIZE, w / Constants.TILESIZE];
            tileDest = new Rectangle[h / Constants.TILESIZE, w / Constants.TILESIZE];
            //load map data from file
            LoadBackground(mapID);
            TileIDArray(5);
            LoadEntities(mapID);



            
            entities.Add(Globals.player);

            Event evn = new Event();
            evn.AddCondition(new PositionCondition(Globals.player, 158, 258, 17, PositionCondition.POSTYPE.POSNEAR));
            evn.AddCondition(new ButtonCondition(Keys.Z, ButtonCondition.KEYSTATE.ISPRESSED));

            List<int> flgs = new List<int>();
            flgs.Add(12);
            evn.AddCondition(new FlagCondition(flgs, false));
            evn.AddAction(new MessageAction("ciao$testo{di{prova$testo#paragrafo{di{prova$^", entities[0]));
            evn.AddAction(new FlagAction(flgs, 1));

            Event evn2 = new Event();
            evn2.AddCondition(new FlagCondition(flgs, true));
            evn2.AddCondition(new PositionCondition(Globals.player, 158, 258, 17, PositionCondition.POSTYPE.POSNEAR));
            //evn2.AddAction(new MessageAction("evento{attivato$flag{impostata$^", entities[0]));
            //evn2.AddAction(new TeleportAction(10, 20, player));
            evn2.AddAction(new MessageAction("movimento$^", entities[0]));
            evn2.AddAction(new MoveAction(150, 300, (Character)entities[0]));
            evn2.AddAction(new MoveAction(300, 300, (Character)entities[0]));
            evn2.AddAction(new MoveAction(300, 150, (Character)entities[0]));
            evn2.AddCondition(new ButtonCondition(Keys.Z, ButtonCondition.KEYSTATE.ISPRESSED));

            entities[0].AddEvent(evn2);
            entities[0].AddEvent(evn);



            Event evn3 = new Event();
            evn3.AddCondition(new PositionCondition(Globals.player, 108, 108, 17, PositionCondition.POSTYPE.POSNEAR));
            evn3.AddCondition(new ButtonCondition(Keys.Z, ButtonCondition.KEYSTATE.ISPRESSED));
            evn3.AddAction(new MessageAction("teletrasporto$^", entities[0]));
            evn3.AddAction(new TeleportAction(selfLevel, 1, 10, 10, Globals.player));

            entities[1].AddEvent(evn3);


            //message = new Message("testo{di{prova{testo{di{prova$testo{tsto{estoset$testo{di{prova{testo{di{prova$aaaaaaaaaaaaaaaaa#paragrafo#paragrafo^");
        }

        public void SetPlayer(Player player)
        {
            entities.Add(player);
        }
        
        public void Update()
        {
            foreach(Entity ent in entities)
            {
                ent.Update();
            }

            for (int i = 0; i < entities.Count; i++)
            {
                for(int j = i+1; j < entities.Count; j++)
                {
                    if(entities[i].Collide(entities[j]))
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
            //DrawBackground();
            foreach (Entity ent in entities)
            {
                ent.Draw(batch);
            }

            //message.Draw();
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
                            new Rectangle(j * Constants.TILESIZE, i * Constants.TILESIZE, Constants.TILESIZE, Constants.TILESIZE);
                    }
                }
            }
        }

        private void DrawBackground()
        {
            for(int i = 0; i < height/Constants.TILESIZE; i++)
            {
                for(int j = 0; j < width/Constants.TILESIZE; j++)
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
            string filename = "Content\\" + id + ".bg";

            using (TextReader reader = File.OpenText(filename))
            {
                //int x = int.Parse(bits[0]);

                for (int i = 0; i < height / Constants.TILESIZE; i++)
                {
                    string text = reader.ReadLine();
                    string[] bits = text.Split(' ');
                    for (int j = 0; j < width / Constants.TILESIZE; j++)
                    {
                        mapArray[i, j] = int.Parse(bits[j]);
                        tileDest[i, j] = new Rectangle(j * Constants.TILESIZE, i * Constants.TILESIZE, Constants.TILESIZE, Constants.TILESIZE);
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
    }
}
