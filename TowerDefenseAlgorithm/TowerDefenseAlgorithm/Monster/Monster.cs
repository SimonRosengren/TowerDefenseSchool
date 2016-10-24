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
        public int hp { get; private set; }
        Vector2 pos;
        float distanceMoved = 0;
        public Vector2 nextTile = new Vector2(0, 0);
        public Vector2 currentTile;

        public Monster(Vector2 pos)
        {
            this.pos = pos;
            this.hp = 10;
            currentTile = new Vector2(((int)pos.X + 25) / 50, ((int)pos.Y + 25) / 50);
            nextTile = PathFinder.path.ElementAt(0);
        }
        public void Update(GameTime time)
        {
            TryMove(time);
        }
        public void Move(GameTime time)
        {
            this.pos += Vector2.Normalize(nextTile * 50 - pos) * (float)time.ElapsedGameTime.TotalSeconds * 100;          
            distanceMoved = Vector2.Distance(currentTile * 50, pos);
            if (distanceMoved >= Globals.TILE_SIZE)
            {
                currentTile = nextTile;
                for (int i = 0; i < PathFinder.path.Count; i++)
                {
                    if (i == PathFinder.path.Count - 1)     
                    {
                        //Monster arrived at goal
                        return;
                    }
                    if (PathFinder.path[i].X == currentTile.X && PathFinder.path[i].Y == currentTile.Y )    //Byter till nästa i listan här
                    {
                        nextTile = PathFinder.path[i + 1];
                    }
                }
                distanceMoved = 0;             
            }
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
        public Vector2 getCenterPos()
        {
            return new Vector2((int)pos.X + 25, (int)pos.Y + 25);
        }
        public void TakeDamage(int damage)
        {
            this.hp -= damage;
        }
    }
}
