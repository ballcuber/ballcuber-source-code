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
using Emgu.CV.UI;
using Emgu.CV;

namespace fgSolver
{
    public partial class VideoScannerControl : UserControl, INavigableForm
    {
        private Capture _capture;

        private ImageBox _viewer;

        private object _lockVideoAccess = new object();
        public VideoScannerControl()
        {
            InitializeComponent();

            _viewer = new ImageBox();
            _viewer.FunctionalMode = ImageBox.FunctionalModeOption.RightClickMenu;
            _viewer.VerticalScrollBar.Visible = false;
            pnlVideo.Controls.Add(_viewer);
            _viewer.Dock = DockStyle.Fill;

            Application.Idle += Application_Idle;
        }


        public string FormName
        {
            get
            {
                return "Caméra";
            }
        }

        public Image Image
        {
            get
            {
                return Properties.Resources.Camera;
            }
        }

        public void PeriodicUpdate(GlobalState formerState, GlobalState currentState)
        {

        }

        public void NavigueTo()
        {
            lock (_lockVideoAccess)
            {
                _capture = new Capture();

            }
        }


        public void LeaveFrom()
        {
            lock (_lockVideoAccess)
            {
                if (_capture != null)
                {
                    _capture.Stop();
                    _capture.Dispose();
                    _capture = null;
                }
            }
        }



        private void Application_Idle(object sender, EventArgs e)
        {
            lock (_lockVideoAccess)
            {
                if (_capture == null) return;

                _viewer.Image = _capture.QueryFrame();
            }
        }
    }
}
