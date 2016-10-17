using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefenseAlgorithm.Tile
{
    class Board
    {
        const int SIZE = 10;
        Tile [,] board = new Tile[10, 10];
        Board()
        {

        }
        void CreateEmptyBoard()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (i == 0)
                    {
                        board[i][j] = new Tile(false);
                    }
                }
            }
        }
    }
}
