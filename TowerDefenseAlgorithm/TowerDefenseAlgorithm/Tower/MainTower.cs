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
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Globals.mainTower, pos);
        }


    }
}
