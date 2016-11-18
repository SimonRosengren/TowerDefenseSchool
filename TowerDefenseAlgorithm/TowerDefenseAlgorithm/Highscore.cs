using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TowerDefenseAlgorithm
{
    class Highscore
    {
        List<int> highScores;
        Hashtable highScoreTable;

        public List<int> HighScores
        {
            get { return highScores; }
            set { }
        }

        public Highscore()
        {
            highScoreTable = new Hashtable(20);
            //LoadContent();
        }

        private void LoadContent()
        {
            highScores = new List<int>();
        }

        /*public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureLibrary.HudText, highScores[0].ToString(), new Vector2(100, 100), Color.Black);
            spriteBatch.DrawString(TextureLibrary.HudText, highScores[1].ToString(), new Vector2(100, 200), Color.Black);
            spriteBatch.DrawString(TextureLibrary.HudText, highScores[2].ToString(), new Vector2(100, 300), Color.Black);
            spriteBatch.DrawString(TextureLibrary.HudText, highScores[3].ToString(), new Vector2(100, 400), Color.Black);
            spriteBatch.DrawString(TextureLibrary.HudText, highScores[4].ToString(), new Vector2(100, 500), Color.Black);
        }*/

        public void WriteScore(string name, int score)
        {
            
            string textScore = score.ToString();
            StreamWriter file = new StreamWriter("Highscore.txt",true);
            file.WriteLine(name);
            file.WriteLine(textScore);
            highScoreTable.Put(name, score);
            file.Close();
            //SortList();
        }

        public void ReadScore()
        {
            StreamReader file = new StreamReader("Highscore.txt");
            while (!file.EndOfStream)
            {
                string name = file.ReadLine();
                string score = file.ReadLine();
                int testInt = Int32.Parse(score);
                //highScores.Add(testInt);
                highScoreTable.Put(name, score);
            }
            //SortList();
            file.Close();
        }
        public void GetScoreWithName(string name)
        {
            Console.WriteLine(highScoreTable.Get(name));
        }

        private void SortList()
        {
            highScores.Sort((a, b) => b.CompareTo(a));
        }
    }
}
