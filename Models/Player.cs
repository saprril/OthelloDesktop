using OthelloDesktop.Interfaces;
using OthelloDesktop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloDesktop.Models
{
    public class Player : IPlayer
    {
        public string Name { get; set; }
        public Piece PlayerPiece { get; set; }

        public Player(string name, Piece piece)
        {
            Name = name;
            PlayerPiece = piece;
        }
    }
}
