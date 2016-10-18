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
        List<Tower> towers = new List<Tower>();

        public void AddMonster(Vector2 pos)
        {
            monsters.Add(new Monster(pos));
        }
        public void AddTower(Vector2 pos)
        {
            towers.Add(new MainTower(pos));
        }
        public void Update(GameTime time)
        {
            foreach (Monster m in monsters)
            {
                m.Update(time);
            }
            foreach (Tower t in towers)
            {
                t.Update(time);
            }
            CheckForTowerTargets();
        }
        public void Draw(SpriteBatch sb)
        {
            foreach (Monster m in monsters)
            {
                m.Draw(sb);
            }
            foreach (Tower t in towers)
            {
                t.Draw(sb);
            }
        }
        void CheckForTowerTargets()
        {
            foreach (Tower t in towers)
            {
                foreach (Monster m in monsters)
                {
                    if (t.IsMonsterInRange(m))
                    {
                        t.Shoot(m.getCenterPos());
                    }
                }
            }
        }
        void RemoveDeadMonsters()
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].hp <= 0)
                {
                    monsters.RemoveAt(i);
                } 
            }
        }
    }
}
