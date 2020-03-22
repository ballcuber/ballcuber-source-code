using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ballcuber
{
    public partial class MainForm : Form
    {
        public CubeViewer Viewer
        {
            get
            {
                return _viewer;
            }
        }

        private CubeViewer _viewer;
        
        public static MainForm Instance;

        public MainForm()
        {
            Instance = this;

            InitializeComponent();

            _viewer = new CubeViewer();
            splitter.Panel2.Controls.Add(_viewer);
            _viewer.Dock = DockStyle.Fill;

            using (var state = GlobalState.GetState())
            {
                state.ImportConfiguration();
                _formerState = state.CloneState();
            }

            FormManager.Init();

            foreach(var form in FormManager.Forms.OrderBy((x) => x.Value.Index))
            {
                var tooltip = new ToolStripButton();
                tooltip.Image = form.Value.Image;
                tooltip.Name = form.Key.Name + "tooltip";
                tooltip.Text = form.Value.FormName;
                toolStrip2.Items.Add(tooltip);
                tooltip.Click += (object o, EventArgs e) => FormManager.Navigate(form.Value);
                tooltip.DragOver += (object o, DragEventArgs e) => { };
                tooltip.DragLeave += (object o, EventArgs e) => { };
            }

            FormManager.Navigate<ColorDefinitionControl>();

            bwConnect.RunWorkerAsync();
        }


        public void Navigate(INavigableForm form)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => this.Navigate(form)));
                return;
            }

            var formCtrl = (Control)form;

            // indiquer à la form précédente qu'elle dégage
            navPnl.Controls.OfType<INavigableForm>().FirstOrDefault()?.LeaveFrom();

            navPnl.Controls.Clear();
            navPnl.Controls.Add(formCtrl);
            formCtrl.Dock = DockStyle.Fill;
            lblTitle.Text = form.FormName;
            lblTitle.Image = form.Image;
            this.Text = "BallCuber Solver - " + form.FormName;

            form.NavigueTo();

            PeriodicUpdate();
        }

        private GlobalState _formerState;
        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            PeriodicUpdate();
        }

        private void PeriodicUpdate()
        {
            // get state
            GlobalState currentState;
            using (var state = GlobalState.GetState())
            {
                currentState = state.CloneState();
            }


            if(currentState.Alarms.Count > 0)
            {
                stripAlarm.Visible = true;
                var last = currentState.Alarms.Last();
                stripAlarm.Text = "(" + currentState.Alarms.Count + ") " + last.Message;
                stripAlarm.ToolTipText = last.AdditionnalInfo;
            }
            else
            {
                stripAlarm.Visible = false;
            }

            strpAcq.Visible = stripAlarm.Visible;

            // Refresh HMI
            _viewer.PeriodicUpdate(_formerState, currentState);

            var right = navPnl.Controls.OfType<INavigableForm>().FirstOrDefault();

            if (right != null)
            {
                right.PeriodicUpdate(_formerState, currentState);
            }


            // update previous state
            _formerState = currentState;
        }

        private void fenêtreDeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new FormTest();
            frm.Show();
        }

        private void debugger3DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _viewer.ShowDebugPanel = debugger3DToolStripMenuItem.Checked;
        }

        private void sauvegarderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(var state = GlobalState.GetState())
            {
                state.SaveConfiguration();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            FormManager.Previous();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            FormManager.Next();
        }

        private void précédentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.Previous();
        }

        private void suivantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.Next();
        }

        private void tmrAlarmeBlink_Tick(object sender, EventArgs e)
        {
            stripAlarm.BackColor= stripAlarm.BackColor == Color.Orange? Color.Yellow : Color.Orange;
        }

        private void stripAlarm_Click(object sender, EventArgs e)
        {
            FormManager.Navigate<AlarmControl>();
        }

        private void strpAcq_Click(object sender, EventArgs e)
        {
            using (var state = GlobalState.GetState())
            {
                state.Alarms.Clear();
            }
        }

        private void bwConnect_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                bool connect1, connect2;
                using (var state = GlobalState.GetState())
                {
                    connect1 = state.HardwareConfig1.ConnectAtStartup;
                    connect2 = state.HardwareConfig2.ConnectAtStartup;
                }

                if (connect1) Hardware.Runner.Board1.Connect();
                if (connect2) Hardware.Runner.Board2.Connect();
            }
            catch(Exception ex)
            {
                Logger.Log(ex);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Hardware.Runner.StopAll();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Space)
            {
                Hardware.Runner.StopAll();
                return true;    // indicate that you handled this keystroke
            }

            // Call the base class
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
