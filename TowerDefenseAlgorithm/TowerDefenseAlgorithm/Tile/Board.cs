using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenseAlgorithm
{
    class Board
    {
        const int SIZE = 10;
        public static Tile [,] board = new Tile[10, 10];
        public Board()
        {
            CreateEmptyBoard();
        }
        void CreateEmptyBoard()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (i == 0 || i == SIZE - 1 || j == 0 || j == SIZE - 1)    //Top and lower row and right and left
                    {
                        board[i, j] = new Tile(false, Globals.TILE_SIZE * i, Globals.TILE_SIZE * j);
                    }
                    else
                        board[i, j] = new Tile(true, Globals.TILE_SIZE * i, Globals.TILE_SIZE * j);
                }
            }
        }
        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    board[i, j].Draw(sb);
                }
            }
        }
    }
}
