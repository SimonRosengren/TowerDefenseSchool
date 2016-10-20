using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace TowerDefenseAlgorithm
{
    public static class Globals
    {
        public static int TILE_SIZE = 50;
        public static int X_SIZE = 15;
        public static int Y_SIZE = 15;
        public static Texture2D wallTex;
        public static Texture2D floorText;
        public static Texture2D enemyTex;
        public static Texture2D mainTower;
        public static Texture2D redTower;
        public static Texture2D purpleTower;
        public static Texture2D bullet;
        public static Texture2D particleTex;
        public static Texture2D pathTileTex;
        public static Texture2D highlight;

        public static void LoadTextures(ContentManager content)
        {
            wallTex = content.Load<Texture2D>(@"notPassable");
            floorText = content.Load<Texture2D>(@"Middle");
            enemyTex = content.Load<Texture2D>(@"Ball");
            mainTower = content.Load<Texture2D>(@"Tower1");
            redTower = content.Load<Texture2D>(@"Tower2");
            purpleTower = content.Load<Texture2D>(@"Tower3");
            bullet = content.Load<Texture2D>(@"bullet");
            particleTex = content.Load<Texture2D>(@"Particle");
            pathTileTex = content.Load<Texture2D>(@"PathTile");
            highlight = content.Load<Texture2D>(@"Highlight");
        }
    }
}
