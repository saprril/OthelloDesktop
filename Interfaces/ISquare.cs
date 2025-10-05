using OthelloDesktop.Models;
using OthelloDesktop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloDesktop.Interfaces
{
    public interface ISquare
    {
        public Position SquarePosition { get; set; }
        public Piece SquarePiece { get; set; }
        public int Value { get; set; }

    }
}
