using OthelloDesktop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloDesktop.Interfaces
{
    public interface IPlayer
    {
        string Name { get; }
        Piece PlayerPiece { get; }
    }
}
