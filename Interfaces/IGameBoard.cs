using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloDesktop.Interfaces
{
    public interface IGameBoard
    {
        public int Size { get; set; }
        public ISquare[,] Squares { get; set; }
    }
}
