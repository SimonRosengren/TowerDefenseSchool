using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenseAlgorithm
{
    class Tile
    {
        public Vector2 pos = new Vector2();
        public bool path = false;
        bool passable;

        public Tile(bool passable, int xPos, int yPos)
        {
            this.passable = passable;
            this.pos.X = xPos;
            this.pos.Y = yPos;
        }
        public void SetPassable(bool b)
        {
            this.passable = b;
        }
        public bool isPassable()
        {
            return this.passable;
        }
        public void Draw(SpriteBatch sb)
        {
            if (!passable)
            {
                sb.Draw(Globals.wallTex, pos, Color.White);
            }
            else if (passable && path)
                sb.Draw(Globals.pathTileTex, pos, Color.White * 0.5f);
            else
                sb.Draw(Globals.floorText, pos, Color.White);            
        }
    }
}
