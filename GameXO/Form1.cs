﻿using System;
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
        #region Properties
        ChessBoardManager ChessBoard;
        #endregion
        public GameXO()
        {
            InitializeComponent();

            ChessBoard = new ChessBoardManager(pnlChessBoard, txtPlayerName, pbMark);

            ChessBoard.drawChessBoard();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
