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
        protected Vector2 pos;

        public Tower(Vector2 pos)
        {
            this.pos = pos;
     
        }

        public abstract void Draw(SpriteBatch sb);
    }
}
