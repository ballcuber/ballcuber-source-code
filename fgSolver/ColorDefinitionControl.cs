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
using fgSolver.Modele;

namespace fgSolver
{
    public partial class ColorDefinitionControl : UserControl, INavigableForm
    {

        public ColorDefinitionControl()
        {
            InitializeComponent();

        }

        public string FormName
        {
            get
            {
                return "Définition des couleurs";
            }
        }

        public void PeriodicUpdate(GlobalState formerState, GlobalState currentState)
        {
            cubeNets.PeriodicUpdate(currentState.InitialCube);
            btnSolve.Enabled = !currentState.SolutionInCalculation;

            if(currentState.InitialCube != formerState.InitialCube)
            {
                InitializeCornerCube(currentState.InitialCube);
            }
        }

        private void InitializeCornerCube(ColorCube cube)
        {
            Color topColor = Color.Gray;
            Color leftColor = Color.Gray;
            Color rightColor = Color.Gray;

            if ((object)cube != null)
            {
                 topColor = cube.colors[47];
                 leftColor = cube.colors[83];
                 rightColor = cube.colors[60];
            }

            var img = (Bitmap)Properties.Resources.FixedCornerColor.Clone();

            for (int x = 0; x < img.Size.Width; x++)
            {
                for (int y = 0; y < img.Size.Height; y++)
                {
                    var pixelColor = img.GetPixel(x, y);

                    if (pixelColor.R == 0 && pixelColor.G == 0 && pixelColor.B == 255)
                    {
                        img.SetPixel(x, y, rightColor);
                    }
                    if (pixelColor.R == 0 && pixelColor.G == 255 && pixelColor.B == 0)
                    {
                        img.SetPixel(x, y, topColor);
                    }
                    if (pixelColor.R == 255 && pixelColor.G == 0 && pixelColor.B == 0)
                    { 
                        img.SetPixel(x, y, leftColor);
                    }


                }
            }

            cornerPicture.Image = img;

        }

        public Image Image
        {
            get
            {
                return Properties.Resources.FaceCube;
            }
        }


        public int Index
        {
            get
            {
                return 1;
            }
        }

        private void btnRand_Click(object sender, EventArgs e)
        {
            var cube = new Cube();

            var rand = new Random();

            var twists = new List<Twist>();

            for (int i = 0; i < 100; i++)
            {
                var t = Twist.Twists[rand.Next(0, Twist.Twists.Length - 1)];

                cube.twist(t);

                twists.Add(t);

                // ce modèle ne respecte pas la norme où les movement u,l,d,r,b bougent 2 couronnes. Ici la fonction twist n'en bouge qu'une. Le 2eme est bougé artificiellement
                if (char.IsLower(t.Name[0]))
                {
                    var t2 = Twist.Twists.First(x => x.Name == t.Name.ToUpper());

                    cube.twist(t2);
                }
            }

            var colorCube = new ColorCube();

            colorCube.setColors(cube);

            using(var state = GlobalState.GetState())
            {
                state.InitialCube = colorCube;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            using (var state = GlobalState.GetState())
            {
                state.InitialCube = new ColorCube();
            }
        }

        public void NavigueTo()
        {
            using (var state = GlobalState.GetState())
            {
                InitializeCornerCube(state.InitialCube);
            }
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            Solver.BackgroundSolve();
            FormManager.Navigate<SolverControl>();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            byte[] buffer;

            using (var state = GlobalState.GetState())
            {
                buffer = state.InitialCube?.FaceColors?.Select((x) => (byte)x.ToString()[0]).ToArray();
            }

            if (buffer != null)
            {
                if (dlgSave.ShowDialog() == DialogResult.OK)
                {
                    using (var stream = dlgSave.OpenFile())
                    {
                        stream.Flush();
                        
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if(dlgOpen.ShowDialog() == DialogResult.OK)
            {
                var cube = new ColorCube();

                using(var stream = dlgOpen.OpenFile())
                {

                    for(int i = 0; i < 4 * 4 * 6;i++)
                    {
                        var b = stream.ReadByte();

                        if (b < 0) break;

                        Faces face;
                        if (Enum.TryParse<Faces>(((char)b).ToString(), out face))
                        {
                            cube.colors[i] = ColorCube.colorDictionary[face];
                        }
                    }
                }

                using (var state = GlobalState.GetState())
                {
                    state.InitialCube = cube;
                }
            }
        }

        public void LeaveFrom() { }

    }
}
