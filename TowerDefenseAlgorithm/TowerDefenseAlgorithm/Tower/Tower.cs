using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefenseAlgorithm
{
    abstract class Tower
    {
        float range = 200;
        protected List<Bullet> bullets = new List<Bullet>();
        protected Vector2 pos;

        public Tower(Vector2 pos)
        {
            this.pos = pos;
     
        }
        public abstract void Shoot(Vector2 target);
        public abstract void Update(GameTime time);
        public abstract void Draw(SpriteBatch sb);
        public bool IsMonsterInRange(Monster other)
        {
            Vector2 center = new Vector2((int)pos.X + 25, (int)pos.Y + 25);
            if (Vector2.Distance(center, other.getCenterPos()) <= range)
            {
                return true;
            }
            return false;
        }
    }
}
