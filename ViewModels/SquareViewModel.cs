using OthelloDesktop.Models;
using OthelloDesktop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OthelloDesktop.ViewModels
{
    public class SquareViewModel : BaseViewModel
    {
        private Brush _backgroundColor;
        public Brush BackgroundColor
        {
            get => _backgroundColor;
            set => SetProperty(ref _backgroundColor, value);
        }

        private Brush _pieceColor;
        public Brush PieceColor
        {
            get => _pieceColor;
            set => SetProperty(ref _pieceColor, value);
        }

        private bool _hasPiece;
        public bool HasPiece
        {
            get => _hasPiece;
            set => SetProperty(ref _hasPiece, value);
        }

        // To identify board position
        public int Row { get; set; }
        public int Column { get; set; }

        public SquareViewModel(int row, int column, Piece color)
        {
            Row = row;
            Column = column;

            // Alternate background colors for readability
            BackgroundColor = (row + column) % 2 == 0
                ? Brushes.ForestGreen
                : Brushes.DarkGreen;

            // Default no piece
            HasPiece = (color != Piece.Empty);
            PieceColor = (color == Piece.Black) ? Brushes.Black :
                         (color == Piece.White) ? Brushes.White :
                         Brushes.Transparent;
        }
    }
}
