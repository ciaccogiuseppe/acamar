using acamar.Source.Engine.Constants;
using acamar.Source.Engine.World.Inventory;
using acamar.Source.Engine.World.Script;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace acamar.Source.Engine
{
    public class SaveSlot
    {
        private bool empty = false;
        private int slot;

        private TimeSpan saveTime = TimeSpan.Zero;
        private int saveLevel;
        private int saveMap;
        private int[] saveFlags = new int[Flag.FLAGNO];
        private int savePosx;
        private int savePosy;
        private int saveDir;
        private List<InventoryItem> saveInventory;


        public SaveSlot(int slot)
        {
            if(slot == 0)
            {
                empty = true;
            }
            else
            {
                empty = false;
            }
            this.slot = slot;
        }

        public void Load()
        {
            string filename = slot + ".sav";
            if(File.Exists(filename))
            {
                //check integrity (checksum)
                string[] lines = File.ReadAllLines(filename);
                saveTime = TimeSpan.Parse(lines[0]);
                saveLevel = (int.Parse(lines[1]));
                saveMap = (int.Parse(lines[2]));

                string[] flgs = lines[3].Split();

                for (int i = 0; i < Flag.FLAGNO; i++)
                {
                    saveFlags[i] = int.Parse(flgs[i]);
                }

                savePosx = int.Parse(lines[4].Split(' ')[0]);
                savePosy = int.Parse(lines[4].Split(' ')[1]);

                saveDir = int.Parse(lines[4].Split(' ')[2]);

                string itmString = lines[5];
                string name;
                int amt;
                saveInventory = new List<InventoryItem>();
                if (itmString != "")
                {
                    foreach (string item in itmString.Split(' '))
                    {
                        name = item.Split(':')[0];
                        amt = int.Parse(item.Split(':')[1]);
                        saveInventory.Add(new InventoryItem(
                            ItemConstants.itemDict.GetValueOrDefault(name),
                            ItemConstants.itemNames.GetValueOrDefault(name),
                            amt));
                    }
                }
                empty = false;
            }
            else
            {
                empty = true;
            }
        }

        public void Save()
        {
            string filename = slot + ".sav";
            saveTime = Globals.runningTime;
            saveLevel = Globals.world.GetCurrentLevel();
            saveMap = Globals.world.GetCurrentMap();
            savePosx = Globals.player.GetPosX();
            savePosy = Globals.player.GetPosY();
            saveDir = Globals.player.GetDir();
            saveInventory = Globals.player.GetInventoryItems();

            for (int i = 0; i < Flag.FLAGNO; i++)
            {
                saveFlags[i] = Flag.flags[i];
            }

            string flgString = "";
            foreach (int i in saveFlags)
            {
                flgString += i + " ";
            }

            string itmString = "";
            foreach (InventoryItem item in saveInventory)
            {
                itmString += item.GetItemName() + ":" + item.GetCount();
            }

            string[] lines = {
                saveTime.ToString(),
                saveLevel.ToString(),
                saveMap.ToString(),
                flgString,
                savePosx + " " + savePosy + " " + saveDir,
                itmString
            };

            StreamWriter file = new StreamWriter(filename);

            foreach (string s in lines)
            {
                file.WriteLine(s);
            }
            file.Flush();
            file.Close();
        }

        public void Apply()
        {
            if (!empty)
            {
                string flgString = "";
                foreach (int i in saveFlags)
                {
                    flgString += i + " ";
                }
                Globals.runningTime = saveTime;
                Flag.ResetFlags();
                Flag.SetFlags(flgString);
                Globals.world.SetLevel(saveLevel);
                Globals.world.SetMap(saveMap);
                Globals.world.Reset();
                Globals.player.SetPosition(savePosx, savePosy);
                Globals.player.SetDir(saveDir);
                Globals.player.SetInventory(saveInventory);
            }
        }

        public void Delete()
        {
            string filename = slot + ".sav";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }

        public override string ToString()
        {
            if (empty) return "Empty";
            else return (int)saveTime.TotalHours + "h:" +
                    (int)saveTime.Minutes + "m:" +
                    (int)saveTime.Seconds + "s - " + saveLevel + ":" + saveMap;
        }

        public bool IsEmpty()
        {
            return empty;
        }


    }
}
