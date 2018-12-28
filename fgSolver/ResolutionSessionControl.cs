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


        private System.Windows.Shell.TaskbarItemInfo _Taskbar = new System.Windows.Shell.TaskbarItemInfo();

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


        public int Index
        {
            get
            {
                return 3;
            }
        }

        public void PeriodicUpdate(GlobalState formerState, GlobalState currentState)
        {
            resolutionSessionSupervisionControl.PeriodicUpdate(currentState);
        }
        


        private void btnRun_Click(object sender, EventArgs e)
        {
            Modele.ResolutionSession.Run();
        }


        public void NavigueTo() {
            using (var state = GlobalState.GetState())
            {
                resolutionSessionSupervisionControl.PeriodicUpdate(state);
            }
        }


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
