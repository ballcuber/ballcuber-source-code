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
            
        }

        private void cubeNets_MoveClick(object sender, MoveClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Runner.BlockingMove(e.MotorMove);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public void NavigueTo() { }

        public void LeaveFrom() { }
    }
}
