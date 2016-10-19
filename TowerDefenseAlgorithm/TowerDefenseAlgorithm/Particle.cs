using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenseAlgorithm
{
    class Particle
    {
        public Vector2 pos;
        public Vector2 velocity;
        float speed;
        public float ttl;
        public Particle(Vector2 startPos, Vector2 velocity, float ttl, float speed)
        {
            this.ttl = ttl;
            this.velocity = velocity;
            this.pos = startPos;
            this.speed = speed;
        }
        public void Update(GameTime time)
        {
            this.pos += velocity * (float)time.ElapsedGameTime.TotalSeconds * speed;
            ttl -= (float)time.ElapsedGameTime.TotalSeconds;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Globals.particleTex, pos, Color.White * ttl);
        }

    }
}
