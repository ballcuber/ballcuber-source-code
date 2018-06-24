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
using System.Diagnostics;

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

            grpServos.Visible = config.HasServo;

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

            grpMain.Enabled = Board.Connected;

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


        private Label _getFunctionLabel(string text, Color color)
        {
            var ctrl = new Label();
            ctrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            ctrl.AutoSize = true;
            ctrl.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            ctrl.Text = text;
            ctrl.ForeColor = color;
            return ctrl;
        }

        private TextBox _getFunctionTextbox()
        {
            var ctrl = new TextBox();
            ctrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            ctrl.AutoSize = true;
            ctrl.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            return ctrl;
        }

        public void NavigueTo()
        {
            refreshFunctionList();
        }

        private void refreshFunctionList()
        {
            try
            {
                if (Board.Functions != null)
                {
                    try
                    {
                        this.pnlFunctions.SuspendLayout();
                        this.pnlFunctions.Controls.Clear();

                        foreach (var func in Board.Functions)
                        {
                            var pnl = new System.Windows.Forms.FlowLayoutPanel();


                            pnl.Controls.Add(_getFunctionLabel(func.ReturnType.ToString(), Color.Purple));
                            pnl.Controls.Add(_getFunctionLabel(func.Name + "(", Color.Black));

                            // call button
                            var btn = new Button();
                            btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
                            btn.Margin = new System.Windows.Forms.Padding(0);
                            btn.Text = "Call";
                            btn.AutoSize = true;

                            var args = new List<Control>();
                            for (int i = 0; i < func.Arguments.Count; i++)
                            {
                                var arg = func.Arguments[i];
                                pnl.Controls.Add(_getFunctionLabel(arg.Type.ToString(), Color.Purple));
                                pnl.Controls.Add(_getFunctionLabel(arg.Name + " = ", Color.Black));
                                var box = _getFunctionTextbox();

                                // Send command with enter button
                                box.KeyDown += (object sender, KeyEventArgs e) => { if (e.KeyCode == Keys.Enter) btn.PerformClick(); };

                                pnl.Controls.Add(box);
                                args.Add(box);
                                if (i != func.Arguments.Count - 1)
                                {
                                    pnl.Controls.Add(_getFunctionLabel(" , ", Color.Black));
                                }
                            }

                            pnl.Controls.Add(_getFunctionLabel(")", Color.Black));


                            pnl.Controls.Add(btn);

                            pnl.Controls.Add(_getFunctionLabel(" = ", Color.Black));

                            var lblResult = _getFunctionLabel("?", Color.SlateGray);
                            pnl.Controls.Add(lblResult);

                            btn.Click += (object sender, EventArgs e) =>
                            {
                                if (Board.Connected)
                                {
                                    Cursor = Cursors.WaitCursor;
                                    try
                                    {
                                        var sw = Stopwatch.StartNew();
                                        var ret = Board.Connection.Call(func.Name, TimeSpan.FromSeconds(10), args.Select((x) => x.Text).ToArray());

                                        var t = sw.Elapsed;

                                        if (ret.Status == Sharer.Command.SharerCallFunctionStatus.OK)
                                        {
                                            lblResult.ForeColor = Color.Black;
                                        }
                                        else
                                        {
                                            lblResult.ForeColor = Color.Red;
                                        }

                                        lblResult.Text = ret.ToString() + " (" + t.ToString(@"ss\:fff") + ")";
                                    }
                                    catch (Exception ex)
                                    {
                                        lblResult.ForeColor = Color.Red;
                                        lblResult.Text = ex.Message;
                                        if (ex.InnerException != null)
                                        {
                                            lblResult.Text += " (" + ex.InnerException.Message + ")";
                                        }
                                    }
                                    finally
                                    {
                                        Cursor = Cursors.Default;
                                    }
                                }
                            };

                            pnl.AutoSize = true;
                            this.pnlFunctions.Controls.Add(pnl);
                        }

                    }
                    finally
                    {
                        this.pnlFunctions.ResumeLayout(true);

                    }
                }
            }
            catch { }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshFunctionList();
        }

        private void btnLockServo_Click(object sender, EventArgs e)
        {
            Board.LockServos();
        }

        private void btnOpenServo_Click(object sender, EventArgs e)
        {
            Board.OpenServos();
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            Board.InitSteppers();
        }
    }
}
