using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using System.IO;
using CefSharp;
using fgSolver.Modele;
using RevengeCube;

namespace fgSolver
{
    public partial class CubeViewer : UserControl
    {
        private ChromiumWebBrowser viewer;


        public CubeViewer()
        {
            InitializeComponent();

            var url = Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "twisty.js", "fgViewer.html");

            viewer = new ChromiumWebBrowser(url);
            viewer.Dock = DockStyle.Fill;
            Controls.Add(this.viewer);

            cbAxe.Text = "X";
        }

        public bool ShowDebugPanel
        {
            get
            {
                return pnlDebug.Visible;
            }
            set
            {
                pnlDebug.Visible = value;
            }
        }

        public void PeriodicUpdate(GlobalState formerState, GlobalState currentState)
        {
           if(formerState.InitialCube != currentState.InitialCube)
            {
                RefreshCube(currentState.InitialCube);
            }

            btnSolve.Enabled = currentState.Solution?.MachineMoves?.MotorMoves != null;
        }

        private void ExecuteJS(string js)
        {
            viewer.GetBrowser().MainFrame.ExecuteJavaScriptAsync(js);
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            viewer.ShowDevTools();
        }

        private void btnTestMoves_Click(object sender, EventArgs e)
        {
            ExecuteJS("move(\"" + txtMove.Text + "\", " + udStartLayer.Value + ", " + udEndLayer.Value + ", " + udAmount.Value + ")");
        }

        public void RefreshCube()
        {
            using(var state = GlobalState.GetState())
            {
                RefreshCube(state.InitialCube);
            }
        }

        public void RefreshCube(ColorCube cube)
        {
            var faces = cube?.FaceColors;

            var sb = new StringBuilder();

            sb.Append("reloadWithColors([");

            for (int i = 0; i < 6; i++)
            {
                sb.Append("[");
                for (int j = 0; j < 16; j++)
                {
                    sb.Append(faces == null ? 7 : (int)faces[i * 16 + j] + 1);
                    sb.Append(",");
                }
                sb.Append("],");
            }
            sb.Append("])");

            ExecuteJS(sb.ToString());
        }

        public void setColorsAndInclination(IEnumerable<Color> colors, double inclination)
        {
            if (colors == null)
            {
                RefreshCube(null);
                return;
            }

            var sb = new StringBuilder();

            sb.Append("setColors([");

            sb.Append(string.Join(",", colors.Select((color) => string.Format("0x{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B))));

            sb.Append("],");

            sb.Append(inclination.ToString().Replace(',', '.'));

            sb.Append(')');

            ExecuteJS(sb.ToString());
        }



        public void ExecuteMachineMove(MotorMove mv)
        {


            int[] moves = { 0, mv.MaxMovesCount, mv.MidMaxMovesCount, mv.MidMinMovesCount };

            int[,] indexes = { { 1, 3 }, { 1, 2 }, { 1, 1 }, { 2, 3 }, { 2, 2 }, { 3, 3 } };

            while (moves[1] != 0 || moves[2] != 0 || moves[3] != 0)
            {

                for (int i = 0; i < indexes.Length / 2; i++)
                {
                    int startLayer = indexes[i, 0];
                    int endLayer = indexes[i, 1];
                    int amount = moves[startLayer];

                    for (int j = startLayer+1; j <= endLayer; j++)
                    {
                        if(Math.Sign(moves[j]) != Math.Sign(amount))
                        {
                            amount = 0; ;
                            break;
                        }
                        else if(amount > 0 && moves[j] < amount || amount < 0 && moves[j] > amount)
                        {
                            amount = moves[j];
                        }
                    }

                    if (amount!=0) {
                        for (int j = startLayer; j <= endLayer; j++)
                        {
                            moves[j] -= amount;
                        }

                        Move(mv.Axe,startLayer, endLayer,amount);

                        break;
                    }
                }
                
            }
        }

        public void Move(Axe axe, int startLayer, int endLayer, int amount)
        {
            string axeName;

            switch (axe)
            {
                case Axe.X:
                    axeName = "b";
                    break;
                case Axe.Y:
                    axeName = "l";
                    break;
                case Axe.Z:
                    axeName = "u";
                    break;
                default:
                    return;
            }
            ExecuteJS("move(\"" + axeName + "\", " + startLayer + ", " + endLayer + ", " + -amount + ")");
        }


        public void RotateCube(Axe axe, int amount = 1)
        {
            SetSpeed(1);
            Move(axe, 1, 4, amount);
        }

        public void SetCameraInclination(double radian)
        {
            ExecuteJS("setCameraInclination(" + radian.ToString().Replace(',','.') + ")");
        }

        private void btnTestMotorMoves_Click(object sender, EventArgs e)
        {
            Axe axe;
            if(Enum.TryParse<Axe>(cbAxe.Text, out axe))
            {
                var mv = new MotorMove(axe);
                mv.MaxMovesCount = (int)udCourronneMax.Value;
                mv.MidMaxMovesCount = (int)udCourronneMidMax.Value;
                mv.MidMinMovesCount = (int)udCourronneMidMin.Value;

                ExecuteMachineMove(mv);
            }
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            using(var state = GlobalState.GetState())
            {
                var moves = state.Solution?.MachineMoves?.MotorMoves;

                if(moves != null)
                {
                    foreach(var mv in moves)
                    {
                        ExecuteMachineMove(mv);
                    }
                }
            }
        }

        public void SetSpeed(int speed)
        {
            ExecuteJS("twistyScene.setSpeed(" + speed + ")");
        }

        private void udSpeed_ValueChanged(object sender, EventArgs e)
        {
            SetSpeed((int)udSpeed.Value);
        }
    }
}
