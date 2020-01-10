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

namespace fgSolver
{
    public partial class CubePartControl : UserControl
    {
        private int _index;
        public CubePartControl(int index)
        {
            _index = index;
            InitializeComponent();
            Clickable = true;
        }

        public bool Clickable { get; set; }

        public void PeriodicUpdate(ColorCube cube)
        {

            Color color = Color.Transparent;

            if (cube != null) color = cube.colors[_index];

            if (Clickable && ClientRectangle.Contains(PointToClient(Cursor.Position))){
                Cursor = Cursors.Hand;
                BackColor = ControlPaint.Dark(color, 0.02f);
            }
            else {
                Cursor = Cursors.Default;
                BackColor = color;
            }
        }


        private void CubePartControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Clickable) return;

            using (var state = GlobalState.GetState())
            {

            Color nextColor = Color.White;
            for (var i = 0; i < ColorCube.colorDictionary.Count-1; i++)
            {
                if (state.InitialCube.colors[_index] == ColorCube.colorDictionary.Values.ElementAt(i))
                {
                    var nextI = i + (e.Button == MouseButtons.Left ? 1 : -1);
                    if (nextI == ColorCube.colorDictionary.Count-1) nextI = 0;
                    else if (nextI == -1) nextI = ColorCube.colorDictionary.Count - 2;

                    nextColor = ColorCube.colorDictionary.Values.ElementAt(nextI);
                    break;
                }
            }

                var newCube = state.InitialCube.CloneCube();
                newCube.colors[_index] = nextColor;
                state.InitialCube = newCube;
                PeriodicUpdate(newCube);
            }

        }


    }
}
