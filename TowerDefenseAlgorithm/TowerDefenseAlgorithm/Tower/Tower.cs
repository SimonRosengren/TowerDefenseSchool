using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefenseAlgorithm
{
    class Tower
    {
        Vector2 pos;
        Texture2D texture;
        public Tower(Texture2D texture, Vector2 pos)
        {
            this.pos = pos;
            this.texture = texture;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, pos);
        }
    }
}
