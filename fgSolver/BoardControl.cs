using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using fgSolver.Hardware;

namespace fgSolver
{
    public partial class BoardControl : UserControl
    {
        public BoardControl()
        {
            InitializeComponent();
        }

        private Board Board
        {
            get
            {
                return Runner.GetBoard(Index);
            }
        }

        public int Index { get; set; }

        public void PeriodicUpdate(GlobalState formerState, GlobalState currentState)
        {
            var config = currentState.HardwareConfig(Index);

            lblTitle.Text = "Carte " + Index;


            if (Board.Connected)
            {
                ledConnected.Color = Color.Lime;
                btnConnect.Text = "Déconnexion";
                lblConnect.Text = "Connecté (" + config.COMPort + ", " + config.BaudRate + " bauds)";
            }
            else
            {
                ledConnected.Color = Color.Red;
                btnConnect.Text = "Connexion";
                lblConnect.Text = "Déconnecté";
            }


        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (Board.Connected)
            {
                Board.Disconnect();
            }
            else
            {
                Board.Connect();
            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            Board.EnableOutputs(0xFF);
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            Board.DisableOutputs(0xFF);
        }
    }
}
