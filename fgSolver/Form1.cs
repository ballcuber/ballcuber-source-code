using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RevengeCube;
using System.IO;
using CefSharp.WinForms;
using System.Diagnostics;

namespace fgSolver
{
    public partial class Form1 : Form
    {
        private ChromiumWebBrowser viewer;

        public Form1()
        {
            InitializeComponent();

            viewer = new ChromiumWebBrowser(GetUriViewer());
            this.viewer.Dock = DockStyle.Fill;
            this.pnlViewer.Controls.Add(this.viewer);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var cube = new Cube();

            var rand = new Random();

            var twists = new List<Twist>();

            for(int i=0; i < udMoves.Value; i++)
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

            var c1 = string.Join("", colorCube.FaceColors.Select(x => x.ToString()));

            var c1Split= Enumerable.Range(0, c1.Length / 16).Select(i => c1.Substring(i * 16, 16)).ToArray();

            // le solver java attend les couleurs des faces dans l'ordre URFDLB alors que le modèle renvoi ULFRBD donc on split la chaine en 6 et on réordonne
            txtColors.Text = string.Join("", c1Split[0], c1Split[3], c1Split[2], c1Split[5], c1Split[1], c1Split[4]);

            txtMoves.Text = string.Join(" ", twists.Select(x => x.Name));

            var startInfo = new ProcessStartInfo("java", "solver " + txtColors.Text);
            startInfo.WorkingDirectory = Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "solver");
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            var proc = Process.Start(startInfo);

            

            proc.WaitForExit(8000);


            var output=  proc.StandardOutput.ReadToEnd();

            var algStr = output.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Last().Replace("    ", " ").Replace("   ", " ").Replace("  ", " ");

            lblMoveCount.Text = algStr.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length.ToString();

            var dir = GetUriViewer(algStr, txtMoves.Text);

            viewer.Load(dir);

        }


        private string GetUriViewer(string alg = null, string setup = null)
        {
            var url = Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "alg.cubing.net", "index.html") + "?type=reconstruction&title=fgSolver&puzzle=4x4x4&view=playback";

            if (!string.IsNullOrEmpty(setup))
            {
                url += "&setup=" + setup.Replace(" ", "_").Replace("'", "-");
            }
            if (!string.IsNullOrEmpty(alg))
            {
                url += "&alg=" + alg.Replace(" ", "_").Replace("'", "-");
            }

            return url;
        }
    }
}
