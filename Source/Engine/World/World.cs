using acamar.Source.Engine.World.Entities;
using acamar.Source.Engine.World.Script;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace acamar.Source.Engine.World
{
    public class World
    {
        private Level currentLevel;
        //private Player player = new Player(1, 7, 200, 200, 0);

        public void Save(int saveID)
        {
            if(saveID < 0)
            {

            }
            string filename = saveID + ".sav";
            string[] lines = {
                currentLevel.GetId().ToString(),
                currentLevel.GetCurrentMap().GetId().ToString(),
                Flag.FlagsToString(),
                Globals.player.ToString()            
            };
            StreamWriter file = new StreamWriter(filename);

            foreach (string s in lines)
            {
                file.WriteLine(s);
            }
            //get player stats and flags from savefile
            //get current level and map from savefile

            file.Close();
        }

        public void Load(int saveID)
        {
            string filename = saveID + ".sav";

            string[] lines = File.ReadAllLines(filename);
            SetLevel(int.Parse(lines[0]));
            currentLevel.SetMap(int.Parse(lines[1]));
            Flag.SetFlags(lines[2]);
            Globals.player.SetPosition(int.Parse(lines[3].Split(' ')[0]), int.Parse(lines[3].Split(' ')[1]));
            Globals.player.SetDir(int.Parse(lines[3].Split(' ')[2]));
            

            //save player stats and flags
            //save current level and map from savefile
        }

        public void SetLevel(int id)
        {
            currentLevel = new Level(id, this);
        }

        public void SetMap(int id)
        {
            currentLevel.SetMap(id);
        }

        public void Draw(SpriteBatch batch)
        {
            currentLevel.Draw(batch);
            //player.Draw();
        }


        internal void Update()
        {
            currentLevel.Update();
            //player.Update();
        }


        //DEBUG
        internal void Reset()
        {
            currentLevel.Reset();
        }

        public int GetCurrentLevel()
        {
            return currentLevel.GetId();
        }

        public int GetCurrentMap()
        {
            return currentLevel.GetCurrentMap().GetId();
        }
    }
}
