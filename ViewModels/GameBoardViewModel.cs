using OthelloDesktop.Helpers;
using OthelloDesktop.Interfaces;
using OthelloDesktop.Models;
using OthelloDesktop.Models.Enums; 
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace OthelloDesktop.ViewModels
{
    public class GameBoardViewModel : BaseViewModel
    {
        private readonly GameController _gameController;
        private readonly List<IPlayer> _players;
        private int _currentPlayerIndex = 0;
        private bool _isVsComputer;
        private Position? lastPosition;

        public IPlayer BlackPlayer => _players.Where(p => p.PlayerPiece == Piece.Black).FirstOrDefault();
        public IPlayer WhitePlayer => _players.Where(p => p.PlayerPiece == Piece.White).FirstOrDefault();

        public int BlackScore => _gameController.CountPieces(Piece.Black);
        public int WhiteScore => _gameController.CountPieces(Piece.White);

        public ObservableCollection<SquareViewModel> Squares { get; }

        public ICommand SquareClickCommand { get; }

        public GameBoardViewModel(List<IPlayer> players, bool isVersusComputer)
        {
            _players = players;
            _isVsComputer = isVersusComputer;
            _gameController = new GameController(players);
            _gameController.InitializeBoard();

            Squares = new ObservableCollection<SquareViewModel>();
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var piece = _gameController.GetSquarePiece(new Position { Column = col, Row = row });
                    var validMoves = _gameController.GetValidMoves(Piece.Black);
                    SquareViewModel squareVM = new SquareViewModel(row, col, piece);
                    if (validMoves.Contains(new Position { Column = col, Row = row }))
                    {
                        squareVM.BackgroundColor = Brushes.YellowGreen; // Highlight valid move
                    }
                    Squares.Add(squareVM);
                }
            }
            if (_isVsComputer && _players.Where(p=> p.Name == "CPU").FirstOrDefault().PlayerPiece == Piece.Black)
            {
                ComputerTurn();
            }

            SquareClickCommand = new RelayCommand<SquareViewModel>(OnSquareClicked);

            //UpdateBoard();
        }

        private void OnSquareClicked(SquareViewModel square)
        {
            if (square == null) return;

            var currentPlayer = _players[_currentPlayerIndex];
            var move = new Position { Row = square.Row, Column = square.Column };

            var validMoves = _gameController.GetValidMoves(currentPlayer.PlayerPiece);
            if (validMoves.Exists(m => m.Row == move.Row && m.Column == move.Column))
            {
                _gameController.MakeMove(move, currentPlayer.PlayerPiece);
                lastPosition = move;
                UpdateBoard();
                OnPropertyChanged(nameof(BlackScore));
                OnPropertyChanged(nameof(WhiteScore));
                if (_gameController.GetValidMoves(_players[(_currentPlayerIndex + 1) % 2].PlayerPiece).Count() != 0)
                {
                    _currentPlayerIndex = (_currentPlayerIndex + 1) % 2;
                    if (_isVsComputer)
                    {
                        ComputerTurn();
                    }
                }
                else
                {
                    Debug.WriteLine("No Valid Moves for Opponent. Play Again");
                }
            }

        }

        private void ComputerTurn()
        {
            var computerPlayer = _players[_currentPlayerIndex];
            var validMoves = _gameController.GetValidMoves(computerPlayer.PlayerPiece);
            Debug.WriteLine($"Computer {computerPlayer.PlayerPiece} valid moves: {string.Join(", ", validMoves)}");
            if (validMoves.Count > 0)
            {
                var move = validMoves[new Random().Next(validMoves.Count)];
                Debug.WriteLine($"Computer chooses move: {move}");
                _gameController.MakeMove(move, computerPlayer.PlayerPiece);
                lastPosition = move;
                //Thread.Sleep(1000); 
                UpdateBoard();
                OnPropertyChanged(nameof(BlackScore));
                OnPropertyChanged(nameof(WhiteScore));
                _currentPlayerIndex = (_currentPlayerIndex + 1) % 2;
            }
        }

        private void InitializeBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var state = _gameController.GetSquarePiece(new Position
                    {
                        Column = col,
                        Row = row
                    }); 

                    var squareVm = new SquareViewModel(row, col, state);

                    var validMoves = _gameController.GetValidMoves(_players[(_currentPlayerIndex + 1) % 2].PlayerPiece);
                    if (validMoves.Contains(new Position
                    {
                        Column = col,
                        Row = row
                    }))
                    {
                        squareVm.BackgroundColor = Brushes.YellowGreen; // Highlight valid move
                    }


                    if (state == Piece.Black)
                    {
                        squareVm.PieceColor = Brushes.Black;
                        squareVm.HasPiece = true;
                    }
                    else if (state == Piece.White)
                    {
                        squareVm.PieceColor = Brushes.White;
                        squareVm.HasPiece = true;
                    }
                    else
                    {
                        squareVm.HasPiece = false;
                    }
                    Squares.Add(squareVm);
                }
            }
        }
        private void UpdateBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var piece = _gameController.GetSquarePiece(new Position { Column = col, Row = row });
                    Squares[row * 8 + col].PieceColor = (piece == Piece.Black) ? Brushes.Black :
                                                      (piece == Piece.White) ? Brushes.White :
                                                      Brushes.Transparent;
                    Squares[row * 8 + col].HasPiece = (piece != Piece.Empty);
                    var validMoves = _gameController.GetValidMoves(_players[(_currentPlayerIndex + 1)%2].PlayerPiece);
                    if (validMoves.Count() == 0)
                    {
                        validMoves = _gameController.GetValidMoves(_players[_currentPlayerIndex].PlayerPiece);
                    }

                    if (validMoves.Contains(new Position
                    {
                        Column = col,
                        Row = row
                    }))
                    {
                        Squares[row * 8 + col].BackgroundColor = Brushes.YellowGreen; // Highlight valid move
                    }
                    else
                    {
                        Squares[row * 8 + col].BackgroundColor = (row + col) % 2 == 0
                            ? Brushes.ForestGreen
                            : Brushes.DarkGreen;
                    }

                    if (lastPosition.HasValue && lastPosition.Value.Row == row && lastPosition.Value.Column == col)
                    {
                        Squares[row * 8 + col].BackgroundColor = Brushes.Blue; // Highlight last move
                    }
                }
            }
        }
    }
}
