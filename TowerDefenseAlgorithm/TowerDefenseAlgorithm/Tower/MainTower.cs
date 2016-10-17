using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseAlgorithm
{
    class MainTower : Tower 
    {
        public MainTower(Vector2 pos) : base(pos)
        {
            
        }
        public override void Update(GameTime time)
        {
            foreach (Bullet b in bullets)
            {
                b.Update(time);
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Globals.mainTower, pos);
            foreach (Bullet b in bullets)
            {
                b.Draw(sb);
            }
        }
        public override void Shoot(Vector2 target)
        {
            bullets.Add(new NormalBullet(this.pos, target));
        }


    }
}
