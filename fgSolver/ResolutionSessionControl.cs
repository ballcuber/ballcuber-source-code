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
using System.Diagnostics;

namespace fgSolver
{
    public partial class ResolutionSessionControl : UserControl, INavigableForm
    {
        public static ResolutionSessionControl Instance;

        public ResolutionSessionControl()
        {
            InitializeComponent();
            Instance = this;
        }

        public string FormName
        {
            get
            {
                return "Session de résolution";
            }
        }

        public Image Image
        {
            get
            {
                return Properties.Resources.ResolutionSession;
            }
        }

        public void PeriodicUpdate(GlobalState formerState, GlobalState currentState)
        {
            if (currentState.Solution?.MachineMoves?.MotorMoves != null)
            {
                if (formerState.Solution != currentState.Solution)
                {

                    var sb = new StringBuilder();

                    for (int i = 0; i < currentState.Solution.MachineMoves.MotorMoves.Count; i++)
                    {
                        if (currentState.Solution.LastExecutedMotorMove < i)
                        {
                            sb.Append("> ");
                        }
                        else if (currentState.Solution.LastExecutedMotorMove == i)
                        {
                            sb.Append("----> ");
                        }
                        else
                        {
                            sb.Append("- ");
                        }
                        sb.AppendLine(currentState.Solution.MachineMoves.MotorMoves[i].ToString());
                    }

                    txtMoves.SuspendLayout();

                    txtMoves.Text = sb.ToString();

                    if (currentState.Solution.LastExecutedMotorMove > currentState.Solution.MachineMoves.MotorMoves.Count / 2)
                    {
                        txtMoves.SelectionStart = sb.Length-1;
                        txtMoves.ScrollToCaret();
                    }

                    txtMoves.ResumeLayout();
                }

                lblStatus.Text = (currentState.Solution.LastExecutedMotorMove + 1) + "/" + currentState.Solution.MachineMoves.MotorMoves.Count;

                progressBar.Maximum = currentState.Solution.MachineMoves.MotorMoves.Count;
                progressBar.Value = (currentState.Solution.LastExecutedMotorMove + 1);
            }
            else
            {
                txtMoves.Text = "";
                lblStatus.Text = "";
                progressBar.Value = 0;
            }

            btnRun.Enabled = currentState.Solution != null && !currentState.Solution.IsRunning;
        }
        


        private void btnRun_Click(object sender, EventArgs e)
        {
            Runner.AsyncRun();
        }


        public void NavigueTo() { }


        private Stopwatch _stopWatch = new Stopwatch();
        private void tmrChrono_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = _stopWatch.Elapsed.ToString(@"mm\:ss\.fff");
        }

        public void StartTimer()
        {
            _stopWatch.Restart();
        }

        public void StopTImer()
        {
            _stopWatch.Stop();
        }

        public void LeaveFrom() { }

    }
}
