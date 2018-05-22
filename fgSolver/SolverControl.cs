using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RevengeCube;
using fgSolver.Hardware;

namespace fgSolver
{
    public partial class SolverControl : UserControl, INavigableForm
    {
        public SolverControl()
        {
            InitializeComponent();
        }

        public string FormName
        {
            get
            {
                return "Solveur";
            }
        }

        public Image Image
        {
            get
            {
                return Properties.Resources.Solver;
            }
        }

        public void PeriodicUpdate(GlobalState formerState, GlobalState currentState)
        {
            cubeNets.PeriodicUpdate(currentState.Solution?.OriginalCube);

            if(currentState.Solution?.MachineMoves?.MotorMoves == null)
            {
                txtMoves.Text = "";
            }
            else if (formerState.Solution?.MachineMoves != currentState.Solution?.MachineMoves)
            {
                string txt = string.Join("\r\n",currentState.Solution.MachineMoves.MotorMoves.Select(x => x.ToString()).ToArray());

                txtMoves.Text = txt;
            }

            btnSolve.Enabled = !currentState.SolutionInCalculation && currentState.Solution == null;

            btnRun.Enabled = currentState.Solution != null;

            imgCube1.Visible = currentState.SolutionInCalculation;
            imgCube2.Visible = currentState.SolutionInCalculation;

            lblMovement.Text = currentState.Solution?.MachineMoves?.MotorMoves?.Count.ToString(); ;
            lblQuarter.Text = currentState.Solution?.MachineMoves?.MotorMoves?.Sum((x) => x.QuarterNumber).ToString(); ;
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            Modele.Solver.BackgroundSolve();
             
        }


        public void NavigueTo() { }

        private void btnRun_Click(object sender, EventArgs e)
        {
            FormManager.Navigate<ResolutionSessionControl>();
            Runner.AsyncRun();
        }

        public void LeaveFrom() { }

    }
}
