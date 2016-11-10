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
        public NormalBullet(Vector2 pos, Vector2 target, float damage) : base(pos, target, damage)
        {

        }
        public override void Update(GameTime time)
        {
            this.pos += Vector2.Normalize(target - pos) * (float)time.ElapsedGameTime.TotalSeconds * speed;
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Globals.bullet, pos, Color.White);
        }
        
    }
}
