using OthelloDesktop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloDesktop.Models
{
    public class GameBoard : IGameBoard
    {
        public int Size { get; set; }
        public ISquare[,] Squares { get; set; }

        public GameBoard(int size)
        {
            Squares = new ISquare[size, size];
            Size = size;
        }
    }
}
