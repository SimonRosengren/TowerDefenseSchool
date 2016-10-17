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
        protected List<Bullet> bullets = new List<Bullet>();
        protected Vector2 pos;

        public Tower(Vector2 pos)
        {
            this.pos = pos;
     
        }
        public abstract void Shoot(Vector2 target);
        public abstract void Update(GameTime time);
        public abstract void Draw(SpriteBatch sb);
    }
}
