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
        protected Vector2 pos;
        protected Vector2 target;
        public Bullet(Vector2 pos, Vector2 target)
        {
            this.pos = pos;
            this.target = target;
        }
        public abstract void Update(GameTime time);
        public abstract void Draw(SpriteBatch sb);
    }
}
