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
        public Bullet(Vector2 pos)
        {
            this.pos = pos;
        }
        public abstract void Update(GameTime time);
        public abstract void Draw(SpriteBatch sb);
    }
}
