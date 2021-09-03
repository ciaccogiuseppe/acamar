using acamar.Source.Engine.World.Entities;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace acamar.Source.Engine.World
{
    public class World
    {
        private Level currentLevel;
        //private Player player = new Player(1, 7, 200, 200, 0);

        public void Load(int saveID)
        {
            if(saveID < 0)
            {

            }
            string filename = saveID + "sav";
            //get player stats and flags from savefile
            //get current level and map from savefile
            
        }

        public void Save(int saveID)
        {
            string filename = saveID + "sav";
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

    }
}
