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
using fgSolver.Modele;

namespace fgSolver
{
    public partial class ManualDriving : UserControl, INavigableForm
    {
        public ManualDriving()
        {
            InitializeComponent();

            cubeNets.PeriodicUpdate(new RevengeCube.ColorCube());
        }

        public string FormName
        {
            get
            {
                return "Pilotage manuel";
            }
        }

        public Image Image
        {
            get
            {
                return Properties.Resources.ManualDriving;
            }
        }

        public int Index
        {
            get
            {
                return 102;
            }
        }

        public void PeriodicUpdate(GlobalState formerState, GlobalState currentState)
        {
            resolutionSessionSupervisionControl.PeriodicUpdate(currentState);
            
        }

        private void cubeNets_MoveClick(object sender, MoveClickEventArgs e)
        {
            //    Runner.BlockingMove(e.MotorMove);

            ResolutionSession.Add(e.MotorMove);
        }

        public void NavigueTo() { }

        public void LeaveFrom() { }

        private void btnInit_Click(object sender, EventArgs e)
        {
            ResolutionSession.AddInitBlock();
        }

        private void btnAlign_Click(object sender, EventArgs e)
        {
            ResolutionSession.AddAlignment();
        }
    }
}
