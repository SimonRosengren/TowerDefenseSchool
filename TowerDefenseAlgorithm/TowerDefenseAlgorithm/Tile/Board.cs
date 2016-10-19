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
        const int SIZE = 15;
        public static Tile [,] board = new Tile[SIZE, SIZE];
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
            board[6, 5].SetPassable(false);
            board[7, 5].SetPassable(false);
            board[8, 5].SetPassable(false);
            board[3, 5].SetPassable(false);
            board[4, 5].SetPassable(false);
            board[5, 5].SetPassable(false);
            board[12, 8].SetPassable(false);
            board[11, 8].SetPassable(false);
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
