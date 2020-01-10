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

        private MotorMove _mvToAdd;

        public void PeriodicUpdate(GlobalState formerState, GlobalState currentState)
        {
            lblMv.Text = _mvToAdd == null ? "..." : _mvToAdd.ToString();
            resolutionSessionSupervisionControl.PeriodicUpdate(currentState);
            
        }

        private void cubeNets_MoveClick(object sender, MoveClickEventArgs e)
        {
            if (_mvToAdd == null || _mvToAdd.Axe != e.Axe) _mvToAdd = e.MotorMove;
            else _mvToAdd.Add(e.Couronne, e.Sens);

            if (_mvToAdd.QuarterNumber == 0) _mvToAdd = null;
        }

       
        private void btnAddMv_Click(object sender, EventArgs e)
        {
            if (_mvToAdd == null) return;
            ResolutionSession.Add(_mvToAdd);
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
