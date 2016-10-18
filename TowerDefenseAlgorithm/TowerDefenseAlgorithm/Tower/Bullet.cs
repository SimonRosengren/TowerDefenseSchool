using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenseAlgorithm
{
    abstract class Bullet
    {
        public Vector2 pos {get; protected set;}
        protected Vector2 target;
        public int damage { get; protected set; }
        public Bullet(Vector2 pos, Vector2 target, int damage)
        {
            this.pos = pos;
            this.target = target;
            this.damage = damage;
        }
        public abstract void Update(GameTime time);
        public abstract void Draw(SpriteBatch sb);
    }
}
