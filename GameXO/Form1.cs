using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameXO
{
    public partial class GameXO : Form
    {
        public GameXO()
        {
            InitializeComponent();

            drawChessBoard();
        }

        void drawChessBoard()
        {
            Button oldButton = new Button();
            for (int i = 0; i < 10; i++)
            {
                Button btn = new Button() {
                    Width = Cons.CHESS_WIDTH,
                    Height = Cons.CHESS_HEIGHT,
                    Location = new Point(oldButton.Location.X + Cons.CHESS_WIDTH, oldButton.Location.Y)
                };

                pnlChessBoard.Controls.Add(btn);

                oldButton = btn;
            }
        }
    }
}
