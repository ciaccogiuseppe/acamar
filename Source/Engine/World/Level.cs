using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace acamar.Source.Engine.World
{
    class Level
    {
        private World selfWorld;
        private int firstMap;
        private int lastMap;
        private List<Map> maps = new List<Map>();
        private Map currentMap;
        private int levelID;

        int curmap;
        int count;

        public Level(int id, World self)
        {
            this.levelID = id;
            this.selfWorld = self;
            LoadLevel(id);
            LoadMaps(levelID);
        }

        private void LoadLevel(int id)
        {
            string filename = "Content\\" + id + ".liv";

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);

            XmlNodeList firstMap = xDoc.GetElementsByTagName("firstMap");
            XmlNodeList lastMap = xDoc.GetElementsByTagName("lastMap");

            this.lastMap = Int32.Parse(lastMap[0].InnerText);
            this.firstMap = Int32.Parse(firstMap[0].InnerText);
        }

        internal void Draw(SpriteBatch batch)
        {
            currentMap.Draw(batch);
        }

        private void LoadMaps(int id)
        {
            string filename = "Content\\" + id + ".map";

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);

            XmlNodeList mapid = xDoc.GetElementsByTagName("mapid");
            XmlNodeList tileid = xDoc.GetElementsByTagName("tileid");
            XmlNodeList width = xDoc.GetElementsByTagName("width");
            XmlNodeList height = xDoc.GetElementsByTagName("height");

            for (int i = 0; i < mapid.Count; i++)
            {
                int mid = Int32.Parse(mapid[i].InnerText);
                int tile = Int32.Parse(tileid[i].InnerText);
                int w = Int32.Parse(width[i].InnerText);
                int h = Int32.Parse(height[i].InnerText);
                maps.Add(new Map(mid, tile, w, h, this));
            }
            currentMap = maps[0];
        }

        public void SetMap(int id)
        {
            currentMap = maps[id];
            currentMap.ResetFlags();
        }

        public Map GetCurrentMap()
        {
            return currentMap;
        }


        //DEBUG
        internal void Reset()
        {
            currentMap = new Map(currentMap.GetId(), 0, 0, 0, this);
        }

        public void ChangeLevel(int id)
        {
            selfWorld.SetLevel(id);
        }

        public void Update()
        {
            currentMap.Update();
        }

        public int GetId()
        {
            return levelID;
        }
    }
}
