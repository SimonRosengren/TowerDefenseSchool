using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenseAlgorithm
{
    class NormalBullet : Bullet
    {
        public NormalBullet(Vector2 pos) : base(pos)
        {

        }
        public override void Update(GameTime time)
        {
            
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Globals.bullet, pos, Color.White);
        }
    }
}
