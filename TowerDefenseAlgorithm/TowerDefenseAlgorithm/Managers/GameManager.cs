﻿using System;
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
        int cash = 100;
        int health = 100;
        int nrOfMonsters = 10;
        bool prevMState;
        bool pause = false;
        float betweenWaveTimer = 0;
        float timeBetweenWaves = 10f;
        float waveTimer;
        float timeBetweenMonsters = 1f;
        public void AddMonster(Vector2 pos)
        {
            monsters.Add(new Monster(pos));
        }
        public void AddMainTower(Vector2 pos)
        {
            towers.Add(new MainTower(pos, 3));
            cash -= 100;
            Board.board[(int)(pos.X / 50), (int)(pos.Y / 50)].SetPassable(false);
            ResetColorPath();
            PathFinder.CreateMap(); //Gör om kartan för pathfinder efter nytt torn
            PathFinder.CalculateClosestPath(); //Räknar om pathen
        }
        public void AddPurpleTower(Vector2 pos)
        {
            towers.Add(new PurpleTower(pos, 5));
            cash -= 150;
        }
        public void Update(GameTime time)
        {
            ParticleEmitter.Update(time);
            foreach (Monster m in monsters)
            {
                m.Update(time);
            }
            foreach (Tower t in towers)
            {
                t.Update(time);
            }
            CheckForTowerTargets();
            CheckForHits();
            RemoveDeadMonsters();
            ColorPath();    //Borde kankse inte hända i Update?
            AdTowerOnClick();
            WaveSpawner(time);
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
            HiglightTile(sb);
            ParticleEmitter.Draw(sb);
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
        void CheckForHits()
        {
            foreach (Monster m in monsters)
            {
                foreach (Tower t in towers)
                {
                    for (int i = 0; i < t.bullets.Count; i++)
                    {
                        if (Vector2.Distance(t.bullets[i].pos, m.getCenterPos()) < 25)
                        {
                            ParticleEmitter.Explosion(t.bullets[i].pos);
                            m.TakeDamage(t.bullets[i].damage);
                            t.bullets.RemoveAt(i);
                        }   
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
                    cash += 100;
                } 
            }
        }
        void ResetColorPath()
        {
            foreach (Vector2 p in PathFinder.path)
            {
                Board.board[(int)p.X, (int)p.Y].path = false;
            }
        }
        void ColorPath()
        {
            foreach (Vector2 p in PathFinder.path)
            {
                Board.board[(int)p.X, (int)p.Y].path = true;
            }
        }
        private void PreviousMouseState()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                prevMState = true;
            }
            else
            {
                prevMState = false;
            }
        }

        void WaveSpawner(GameTime time)
        {
            waveTimer += (float)time.ElapsedGameTime.TotalSeconds;
            if (nrOfMonsters == 10)
            {
                betweenWaveTimer += (float)time.ElapsedGameTime.TotalSeconds;
                pause = true;
            }            
            if (waveTimer >= timeBetweenMonsters && nrOfMonsters < 10)
            {
                AddMonster(new Vector2(50, 100));
                waveTimer = 0;
                nrOfMonsters++;
            }
            while (betweenWaveTimer >= timeBetweenWaves)
            {
                betweenWaveTimer = 0;
                nrOfMonsters = 0;
                pause = false;
            }           
        }

        private void AdTowerOnClick()
        {
            Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && pause == true)
            {
                mousePos.X = mousePos.X / 50;
                mousePos.Y = mousePos.Y / 50;
                mousePos.X = (int)mousePos.X * 50;
                mousePos.Y = (int)mousePos.Y * 50;
                if (!prevMState && Board.board[(int)mousePos.X/50, (int)mousePos.Y/50].isPassable())
                {                   
                    AddMainTower(new Vector2(mousePos.X, mousePos.Y));
                }

            }
            PreviousMouseState();
        }

        private void HiglightTile(SpriteBatch sb)
        {
            Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            mousePos.X = mousePos.X / 50;
            mousePos.Y = mousePos.Y / 50;
            mousePos.X = (int)mousePos.X * 50;
            mousePos.Y = (int)mousePos.Y * 50;
            //Förlåt för lång if-sats
            if (!pause || (int)mousePos.X / 50 == 0 || (int)mousePos.X / 50 == 14 || (int)mousePos.Y / 50 == 0 || (int)mousePos.Y / 50 == 14 || !Board.board[(int)mousePos.X / 50, (int)mousePos.Y / 50].isPassable())
            {
                sb.Draw(Globals.redHighlight, mousePos);
            }
            else
            {
                sb.Draw(Globals.highlight, mousePos);
            }
            
        }
    }
}
