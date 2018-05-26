﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameXO
{
    public class ChessBoardManager
    {
        #region Properties
        private Panel chessBoard;
        public Panel ChessBoard { get => chessBoard; set => chessBoard = value; }

        private List<Player> players;
        public List<Player> Players { get => players; set => players = value; }
        
        private int currentPlayer;
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }

        private TextBox playerName;
        public TextBox PlayerName { get => playerName; set => playerName = value; }

        private PictureBox mark;
        public PictureBox Mark { get => mark; set => mark = value; }
        
        private List<List<Button>> matrix;

        public List<List<Button>> Matrix { get => matrix; set => matrix = value; }
        #endregion

        #region Initialize
        public ChessBoardManager(Panel chessBoard, TextBox playerName, PictureBox mark)
        {
            this.chessBoard = chessBoard;
            this.playerName = playerName;
            this.mark = mark;

            this.players = new List<Player>() {
                new Player("Player1", Image.FromFile(Application.StartupPath + "\\Resources\\P1.png")),
                new Player("Player2", Image.FromFile(Application.StartupPath + "\\Resources\\P2.png"))
            };

            currentPlayer = 0;

            ChangePlayer();
        }
        #endregion

        #region Methods
        public void drawChessBoard()
        {
            Matrix = new List<List<Button>>();


            Button oldButton = new Button() { Width = 0, Location = new Point(0, 0) };
            for (int i = 0; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                Matrix.Add(new List<Button>());


                for (int j = 0; j < Cons.CHESS_BOARD_WIDTH; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Cons.CHESS_WIDTH,
                        Height = Cons.CHESS_HEIGHT,
                        Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()
                    };

                    btn.Click += btn_Click;

                    chessBoard.Controls.Add(btn);

                    Matrix[i].Add(btn);

                    oldButton = btn;
                }
                oldButton.Location = new Point(0, oldButton.Location.Y + Cons.CHESS_HEIGHT);
                oldButton.Width = 0;
                oldButton.Height = 0;
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            if (btn.BackgroundImage != null) {
                return;
            }


            ChangeMark(btn);

            ChangePlayer();

            if (isEndGame(btn))
            {
                EndGame();
            }


        }


        private void EndGame()
        {
            MessageBox.Show("End game");
        }

        private bool isEndGame(Button btn)
        {
            return isEndGamehorizontal(btn) || isEndGameVertical(btn) || isEndGamePrimary(btn) || isEndGameSub(btn);
        }

        private Point GetChessPoint(Button btn)
        {

            int vertical = Convert.ToInt32(btn.Tag);
            int horizontal = Matrix[vertical].IndexOf(btn);

            Point point = new Point(horizontal, vertical);

            return point;

        }

        private bool isEndGamehorizontal(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countLeft = 0;
            for (int i = point.X; i >= 0; i--)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;
                } else
                {
                    break;
                }
            }

            int countRight = 0;
            for (int i = point.X + 1; i < Cons.CHESS_BOARD_WIDTH; i++)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else
                {
                    break;
                }
            }




            return countLeft + countRight == 5;
        }

        private bool isEndGameVertical(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                {
                    break;
                }
            }

            int countBottom = 0;
            for (int i = point.X + 1; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                {
                    break;
                }
            }




            return countTop + countBottom == 5;
        }

        private bool isEndGamePrimary(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X - i < 0 || point.Y - i < 0)
                    break;
                if (Matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                {
                    break;
                }
            }

            int countBottom = 0;
            for (int i = 1; i <= Cons.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X + i >= Cons.CHESS_BOARD_WIDTH)
                    break;
                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                {
                    break;
                }
            }




            return countTop + countBottom == 5;
        }

        private bool isEndGameSub(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X + i > Cons.CHESS_BOARD_WIDTH || point.Y - i < 0)
                    break;
                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                {
                    break;
                }
            }

            int countBottom = 0;
            for (int i = 1; i <= Cons.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X + i < 0)
                    break;
                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                {
                    break;
                }
            }




            return countTop + countBottom == 5;
        }



        private void ChangePlayer() {
            playerName.Text = Players[currentPlayer].Name;

            mark.Image = Players[currentPlayer].Mark;
        }


        private void ChangeMark(Button btn)
        {
            btn.BackgroundImage = players[currentPlayer].Mark;

            currentPlayer = currentPlayer == 1 ? 0 : 1;
        }
        #endregion


    }
}
