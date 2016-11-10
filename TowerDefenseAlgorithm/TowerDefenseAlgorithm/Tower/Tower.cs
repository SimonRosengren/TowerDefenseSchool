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
        public List<Bullet> bullets;
        protected Vector2 pos;
        public float damage { get; private set; }

        public Tower(Vector2 pos, float damage)
        {
            this.pos = pos;
            this.damage = damage;
            bullets = new List<Bullet>();
     
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
