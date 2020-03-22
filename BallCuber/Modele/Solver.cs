using RevengeCube;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballcuber.Modele
{
    public class Solver
    {
        public static void BackgroundSolve()
        {
            Task.Factory.StartNew(() =>
            {
                MainForm.Instance.Viewer.RefreshCube();

                try
                {
                    ColorCube cube;
                    using (var state = GlobalState.GetState())
                    {
                        cube = state.InitialCube.CloneCube();
                        state.SolutionInCalculation = true;
                        state.Solution = null;
                    }

                    Solution solution=null;

                    using (var task = Task.Factory.StartNew(() =>
                     {
                         solution = Modele.Solver.Solve(cube);
                     }))
                    {

                        if (!task.Wait(TimeSpan.FromSeconds(5))) throw new Exception("Timeout à la résolution");
                    }
                    using (var state = GlobalState.GetState())
                    {
                        state.Solution = solution;
                    }
                    ResolutionSession.FromSolution();
                }
                catch(Exception ex)
                {
                    Logger.Log(ex);
                    using (var state = GlobalState.GetState())
                    {
                        state.Solution = null;
                    }
                }
                finally
                {
                    using (var state = GlobalState.GetState())
                    {
                        state.SolutionInCalculation = false;
                    }
                }
            });
        }

        public static Solution Solve(ColorCube cube)
        {
            var c1 = string.Join("", cube.FaceColors.Select(x => x.ToString()));

            var c1Split = Enumerable.Range(0, c1.Length / 16).Select(i => c1.Substring(i * 16, 16)).ToArray();

            // le solver java attend les couleurs des faces dans l'ordre URFDLB alors que le modèle renvoi ULFRBD donc on split la chaine en 6 et on réordonne
            var colorsForSolver = string.Join("", c1Split[0], c1Split[3], c1Split[2], c1Split[5], c1Split[1], c1Split[4]);

            var startInfo = new ProcessStartInfo("java", "solver " + colorsForSolver);
            startInfo.WorkingDirectory = Path.Combine(Path.GetDirectoryName(cube.GetType().Assembly.Location), "solver");
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            var proc = Process.Start(startInfo);

            proc.WaitForExit(8000);

            var output = proc.StandardOutput.ReadToEnd();

            var algStr = output.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Last().Replace("    ", " ").Replace("   ", " ").Replace("  ", " ");

            var moves = MoveParser.Parse(algStr);

            if (moves.Count == 0 && !cube.IsSolved)
            {
                throw new Exception("Cube insoluble");
            }

            var calculator = new MoveCalculator();

            foreach (Move mv in moves)
            {
                calculator.AddCubeMove(mv);
            }

            var sol = new Solution();

            sol.MachineMoves = calculator.MachineMoves;
            sol.OriginalCube = cube.CloneCube();
            sol.SolverOutput = output;
            sol.Moves = moves;
            sol.MovesString = algStr;
            sol.Date = DateTime.Now;

            return sol;
        }
    }
}
