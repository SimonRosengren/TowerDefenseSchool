using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenseAlgorithm.Tile
{
    class Tile
    {
        Texture2D text;
        bool passable;

        Tile(bool passable)
        {
            this.passable = passable;
        }
        public void SetPassable(bool b)
        {
            this.passable = b;
        }
        public bool isPassable()
        {
            return this.passable;
        }
    }
}
