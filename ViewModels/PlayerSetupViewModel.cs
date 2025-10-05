using OthelloDesktop.Helpers;
using OthelloDesktop.Interfaces;
using OthelloDesktop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace OthelloDesktop.ViewModels
{
    public class PlayerSetupViewModel : BaseViewModel
    {
        private bool _isVsCpu;
        private bool _isVsOtherPlayer;
        private string _playerName;
        private string _player1Name;
        private string _player2Name;
        private string _selectedPieceColor;

        public bool IsVsCpu
        {
            get => _isVsCpu;
            set => SetProperty(ref _isVsCpu, value);
        }

        public bool IsVsOtherPlayer
        {
            get => _isVsOtherPlayer;
            set => SetProperty(ref _isVsOtherPlayer, value);
        }

        public string PlayerName
        {
            get => _playerName;
            set => SetProperty(ref _playerName, value);
        }

        public string Player1Name
        {
            get => _player1Name;
            set => SetProperty(ref _player1Name, value);
        }

        public string Player2Name
        {
            get => _player2Name;
            set => SetProperty(ref _player2Name, value);
        }

        public string SelectedPieceColor
        {
            get => _selectedPieceColor;
            set => SetProperty(ref _selectedPieceColor, value);
        }

        // Commands
        public ICommand BackCommand { get; }
        public ICommand StartGameCommand { get; }

        public PlayerSetupViewModel(MainWindowViewModel mainVm, bool vsCpu)
        {
            if (vsCpu)
            {
                IsVsCpu = true;
                IsVsOtherPlayer = false;
            }
            else
            {
                IsVsCpu = false;
                IsVsOtherPlayer = true;
            }

            BackCommand = new RelayCommand(_ =>
            {
                // navigate back to menu
                mainVm.CurrentViewModel = new MenuViewModel(mainVm);
            });

            StartGameCommand = new RelayCommand(_ =>
            {
                List<IPlayer> players = new List<IPlayer>();
                if (IsVsCpu)
                {
                    string parsedColor = SelectedPieceColor?.Split(" ", StringSplitOptions.RemoveEmptyEntries)[^1];
                    Piece selectedColor = (parsedColor == "Black") ? Piece.Black : Piece.White;
                    Piece cpuColor = (selectedColor == Piece.Black) ? Piece.White : Piece.Black;
                    //Debug.WriteLine($"SelectedPieceColor: {SelectedPieceColor}, selectedColor: {selectedColor}, cpuColor: {cpuColor}");

                    IPlayer human = new Models.Player(PlayerName, selectedColor);
                    IPlayer cpu = new Models.Player("CPU", cpuColor);

                    if (human.PlayerPiece == Piece.Black)
                    {
                        players.Add(human);
                        players.Add(cpu);
                    }
                    else
                    {
                        players.Add(cpu);
                        players.Add(human);
                    }


                }
                else if (IsVsOtherPlayer)
                {
                    players.Add(new Models.Player(Player1Name, Piece.Black));
                    players.Add(new Models.Player(Player2Name, Piece.White));
                }
                    mainVm.CurrentViewModel = new GameBoardViewModel(players, IsVsCpu);
            });
        }
    }
}

