using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenseAlgorithm
{
    class Monster
    {
        int hp = 10;
        Vector2 pos;
        public Vector2 nextTile = new Vector2(4, 1);
        public Vector2 currentTile;
        public Monster(Vector2 pos)
        {
            this.pos = pos;
            currentTile = new Vector2((int)pos.Y / 50, (int)pos.Y / 50);
        }
        public void Update(GameTime time)
        {

            //nextTile.X++;
            TryMove(time);
        }
        public void Move(GameTime time)
        {
            //this.pos = Vector2.Lerp(pos, Board.board[(int)nextTile.X, (int)nextTile.Y].pos, 2f);
            this.pos.X += Vector2.Distance(pos, nextTile * 50) / 50;
        }
        public void TryMove(GameTime time)
        {
            if (nextTile.X < Globals.X_SIZE && nextTile.Y < Globals.Y_SIZE)
            {
                if (Board.board[(int)nextTile.X, (int)nextTile.Y].isPassable())
                {
                    Move(time);
                }
            }

            else
                return;
        }
        public void FindNextTile()
        {
            //Pathfinder goes here
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Globals.enemyTex, pos, Color.White);
        }
    }
}
