using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    public class User
    {
        public string Name { get; set; }
        public int ID { get; set; }

        public int gamesPlayed;
        public int gamesWon;
        public string lastGame;
        public int lastLevel;
        public int highestLevel;

        public User(string Name, int ID, int gamesPlayed, int gamesWon, string lastGame, int lastLevel, int highestLevel)
        {
            this.Name = Name;
            this.ID = ID;
            this.gamesPlayed = gamesPlayed;
            this.gamesWon = gamesWon;
            this.lastGame = lastGame;
            this.lastLevel = lastLevel;
            this.highestLevel = highestLevel;
        }

        public void updateData()
        {
            string filePath = "C:\\Users\\rbr72\\OneDrive\\Desktop\\Semestrul 2\\Retele Calculatoare\\TEMA1_CPP\\MemoryGame\\MemoryGame\\users_info.txt";
            try
            {
                string output = "";

                List<string> lines = new List<string>();
                lines = File.ReadAllLines(filePath).ToList();

                foreach (string line in lines)
                {
                    string[] split_data = line.Split(' ');
                    if (int.Parse(split_data[1]) == this.ID)
                    {
                        output += Name + " " + ID.ToString() + " " + gamesPlayed.ToString() + " " +
                            gamesWon.ToString() + " " +  lastGame.ToString() + " " + lastLevel.ToString() + " " + highestLevel.ToString() + '\n';
                    }
                    else
                    {
                        output += line + "\n";
                    }
                }

                File.WriteAllText(filePath, output);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
