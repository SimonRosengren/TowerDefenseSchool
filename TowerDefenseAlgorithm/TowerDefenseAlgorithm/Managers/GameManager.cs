using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenseAlgorithm
{
    class GameManager
    {
        List<Monster> monsters = new List<Monster>();
        List<Bullet> bullets = new List<Bullet>();
        List<Tower> towers = new List<Tower>();

        public void AddMonster(Vector2 pos)
        {
            monsters.Add(new Monster(pos));
        }

        public void Update(GameTime time)
        {
            foreach (Monster m in monsters)
            {
                m.Update(time);
            }
            foreach (Bullet b in bullets)
            {
                b.Update(time);
            }
        }
        public void Draw(SpriteBatch sb)
        {
            foreach (Monster m in monsters)
            {
                m.Draw(sb);
            }
            foreach (Bullet b in bullets)
            {
                b.Draw(sb);
            }
        }
    }
}
