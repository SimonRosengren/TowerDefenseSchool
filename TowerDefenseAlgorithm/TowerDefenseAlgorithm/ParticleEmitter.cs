using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenseAlgorithm
{
    public static class ParticleEmitter
    {
        private static Random rnd = new Random();
        private static List<Particle> particles = new List<Particle>();
        public static void Explosion(Vector2 pos)
        {
            for (int i = 0; i < 100; i++)
            {
                particles.Add(new Particle(pos, new Vector2((float)rnd.Next(-10, 10), (float)rnd.Next(-10, 10)), 1f, rnd.Next(5, 150)));
            }
        }
        private static void RemoveFinished()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].ttl <= 0)
                {
                    particles.RemoveAt(i);
                }
            }
        }
        public static void Update(GameTime time)
        {
            foreach (Particle p in particles)
            {
                p.Update(time);
            }
            RemoveFinished();
        }
        public static void Draw(SpriteBatch sb)
        {
            foreach (Particle p in particles)
            {
                p.Draw(sb);
            }
        }

    }
}
