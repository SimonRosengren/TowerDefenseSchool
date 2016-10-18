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
        private static Random rnd;
        private static List<Particle> particles = new List<Particle>();
        public static void Explosion(Vector2 pos)
        {
            for (int i = 0; i < 50; i++)
            {
                particles.Add(new Particle(pos, new Vector2((float)rnd.NextDouble(), (float)rnd.NextDouble()), 1f, 100));
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

    }
}
