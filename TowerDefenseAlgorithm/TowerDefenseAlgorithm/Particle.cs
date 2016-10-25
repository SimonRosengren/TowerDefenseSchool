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
        Texture2D tex;
        public Vector2 pos;
        public Vector2 velocity;
        float speed;
        public float ttl;
        Color color;
        Random rnd = new Random();
        public Particle(Vector2 startPos, Vector2 velocity, float ttl, float speed, Color color1, Texture2D tex)
        {
            this.ttl = ttl;
            this.velocity = velocity;
            this.pos = startPos;
            this.speed = speed;
            int i = rnd.Next(0, 255);
            this.color = color1;
            this.tex = tex;
         
        }
        public void Update(GameTime time)
        {
            this.pos += velocity * (float)time.ElapsedGameTime.TotalSeconds * speed;
            ttl -= (float)time.ElapsedGameTime.TotalSeconds;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, pos, color * ttl);
        }

    }
}
