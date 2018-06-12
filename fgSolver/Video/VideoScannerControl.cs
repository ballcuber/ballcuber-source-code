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
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using OpenTK;
using Emgu.CV.Util;
using System.Diagnostics;
using RevengeCube;

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

            for(int f = 0; f < 6; f++)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        _scannedColors[f, i, j] = new List<Pastille>();
                    }
                }
            }

            Application.Idle += Application_Idle;

            _refreshCubeSW.Start();

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

        public int Index
        {
            get
            {
                return 0;
            }
        }

        public void PeriodicUpdate(GlobalState formerState, GlobalState currentState)
        {
            switch (currentState.Scanner.CurrentScannedFace)
            {
                case Faces.L:
                    barProgress.Value = currentState.Scanner.WaitingForFace ? 0 : 1;
                    break;
                case Faces.B:
                    barProgress.Value = 2;
                    break;
                case Faces.R:
                    barProgress.Value = 3;
                    break;
                case Faces.F:
                    barProgress.Value = 4;
                    break;
                case Faces.D:
                    barProgress.Value = 5;
                    break;
                case Faces.U:
                    barProgress.Value = 6;
                    break;
                case Faces.UNKNOWN:
                    barProgress.Value = 7;
                    break;
                default:
                    barProgress.Value = 0;
                    break;
            }

            if (currentState.Scanner.CurrentScannedFace == Faces.UNKNOWN)
            {
                lblStatus.Text = "Scan terminé";
            }
            else
            {
                lblStatus.Text = currentState.Scanner.WaitingForFace ? "En attente de le face " + currentState.Scanner.CurrentScannedFace + "..." : "Scan de " + currentState.Scanner.CurrentScannedFace + " en cours";
            }
        }

        public void NavigueTo()
        {
            lock (_lockVideoAccess)
            {
                _capture = new Capture(0);
            }

            ResetAll();
        }

        private void ResetAll()
        {
            using (var state = GlobalState.GetState())
            {
                state.Scanner.Reset();
            }

            MainForm.Instance.Viewer.RefreshCube(null);

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

            using(var state = GlobalState.GetState())
            {
                MainForm.Instance.Viewer.RefreshCube(state.InitialCube);
            }
        }


        private void btnStop_Click(object sender, EventArgs e)
        {
            ResetAll();
        }

        private List<Pastille>[,,] _scannedColors = new List<Pastille>[6,4,4];

        private void Application_Idle(object sender, EventArgs e)
        {

            Image<Bgr, byte> inImage = null;
            lock (_lockVideoAccess)
            {
                if (_capture == null) return;

                Pastille[,] pastilleMatrix = null;
                double angle=0;

                //run this until application closed (close button click on image viewer)
                var frame = _capture.QueryFrame();

                if (frame == null) return;

                inImage = frame.ToImage<Bgr, byte>(); //draw the image obtained from camera

               // var contourImage = inImage.Clone();

                   Mat outImage = new Mat();

                CvInvoke.MedianBlur(inImage, outImage, 5);

                ////var processsImageChannels = new Image<Bgr, byte>(inImage.Size);
                ////CvInvoke.CvtColor(frame, processsImageChannels, ColorConversion.Rgb2Hsv);



                // var processsImage = new Mat();
                // CvInvoke.ExtractChannel(inImage, processsImage, 1);

                //processViewer.Image = processsImageChannels;

                Mat canny = new Mat();

                CvInvoke.Canny(outImage, canny, 58, 130, 3, true);

                //cannyViewer.Image = canny;

                //CvInvoke.DrawContours(outImage, contours,-1, new MCvScalar(0,0,255));

                using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
                {

                    CvInvoke.FindContours(canny, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

                    var lstParallelogram = new List<Quadrilateral>();

                    for (int i = 0; i < contours.Size; i++)
                    {


                        using (VectorOfPoint contour = contours[i])
                        using (VectorOfPoint approxContour = new VectorOfPoint())
                        {
                            var arc = CvInvoke.ArcLength(contour, true);

                            // éliminer les objets les plus petits
                            if (arc / 4 < inImage.Width / 30) continue;

                            CvInvoke.ApproxPolyDP(contour, approxContour, arc * 0.02, true);

                            if (approxContour.Size == 4)
                            {

                                var pts = approxContour.ToArray();

                              //  contourImage.DrawPolyline(pts, true, new Bgr(Color.Green));

                                var quadri = new Quadrilateral(pts, 0.1, 0.1);

                                if (quadri.IsParallelogram)
                                {
                                    lstParallelogram.Add(quadri);
                                }

                            }

                        }
                    }

                    //contourViewer.Image = contourImage;

                    // suppression des doublons
                    var comparer = new QuadrilateralCenterComparer(0.3);
                    lstParallelogram = lstParallelogram.Distinct(comparer).ToList();

                    var lstSquares = lstParallelogram.Where((x) => x.IsSquare).ToList();

                    // nombre minimal de cube
                    if (lstSquares.Count > 5)
                    {
                        var minX = lstSquares.Min((x) => x.Center.X);
                        var maxX = lstSquares.Max((x) => x.Center.X);
                        var minY = lstSquares.Min((x) => x.Center.Y);
                        var maxY = lstSquares.Max((x) => x.Center.Y);

                        lstSquares.Min((x) => x.Center.X);

                        // largeur moyenne d'une pastille
                        var averageLength = lstSquares.Average((x) => x.AverageLength);

                        // distance entre les centres des pastilles. Première approximation
                        var centerDistance = averageLength * 1.4;

                        var bottom = new Vector2(lstSquares.Average((x) => x.Bottom.X), lstSquares.Average((x) => x.Bottom.Y));

                        bottom.Normalize();

                        var right = new Vector2(bottom.Y, -bottom.X);

                        var pastilles = new List<Pastille>();

                        var pastilleOrigine = new Pastille();
                        pastilleOrigine.m = 0;
                        pastilleOrigine.n = 0;
                        pastilleOrigine.Quadrilateral = lstSquares[0];
                        pastilleOrigine.Center = lstSquares[0].Center;
                        pastilles.Add(pastilleOrigine);

                        var origine = lstSquares[0].Center;

                        for (int i = 1; i < lstSquares.Count; i++)
                        {
                            var u = lstSquares[i].Center - origine;

                            var pastille = new Pastille();
                            pastille.Quadrilateral = lstSquares[i];

                            pastille.m = (int)Math.Round(Vector2.Dot(u, bottom) / centerDistance);
                            pastille.n = (int)Math.Round(Vector2.Dot(u, right) / centerDistance);

                            pastille.Center = lstSquares[i].Center;

                            pastilles.Add(pastille);
                        }

                        var xMax = pastilles.Max((x) => x.n);
                        var xMin = pastilles.Min((x) => x.n);
                        var yMax = pastilles.Max((x) => x.m);
                        var yMin = pastilles.Min((x) => x.m);

                        if (xMax - xMin != 3 || yMax - yMin != 3) return; // ce n'est pas un cube 4x4

                        // x et y entre 0 et 3
                        pastilles.ForEach((x) => { x.m -= yMin; x.n -= xMin; });

                        // recalcul de centerDistance
                        centerDistance = 0;
                        for (int i = 0; i < pastilles.Count - 1; i++)
                        {
                            var dm = pastilles[i + 1].m - pastilles[i].m;
                            var dn = pastilles[i + 1].n - pastilles[i].n;
                            centerDistance += (pastilles[i].Center - pastilles[i + 1].Center).LengthFast / Math.Sqrt(dm * dm + dn * dn);
                        }
                        centerDistance /= pastilles.Count - 1;

                       pastilleMatrix = new Pastille[4, 4];

                        for (int m = 0; m < 4; m++)
                        {
                            for (int n = 0; n < 4; n++)
                            {
                                var pastille = pastilles.FirstOrDefault((x) => x.m == m && x.n == n);

                                if (pastille == null)
                                {
                                    pastille = new Pastille();
                                    pastille.m = m;
                                    pastille.n = n;

                                    // calcul de la position des pastilles non détéctées
                                    foreach (var p in pastilles)
                                    {
                                        pastille.Center += p.Center + (float)centerDistance * ((m - p.m) * bottom + (n - p.n) * right);
                                    }
                                    pastille.Center /= pastilles.Count;
                                }

                                pastilleMatrix[m, n] = pastille;

                                var ptRect = pastille.Center - new Vector2(averageLength / 4);

                                if (ptRect.X < 0) ptRect.X = 0;
                                else if (ptRect.X + averageLength / 2 >= inImage.Width) ptRect.X = inImage.Width - 1 - (int)averageLength / 2;

                                if (ptRect.Y < 0) ptRect.Y = 0;
                                else if (ptRect.Y + averageLength / 2 >= inImage.Height) ptRect.Y = inImage.Height - 1 - (int)averageLength / 2;

                                var rectangle = new Rectangle((int)ptRect.X, (int)ptRect.Y, (int)averageLength / 2, (int)averageLength / 2);
                             //   var rectMat = processsImageChannels.GetSubRect(rectangle);

                              //  var color = CvInvoke.Mean(rectMat);

                             //   pastille.MeanColorHSV.MCvScalar = color;

                              var  rectMat = inImage.GetSubRect(rectangle);
                                pastille.MeanColorBGR.MCvScalar = CvInvoke.Mean(rectMat);

                                //var minDist = float.MaxValue;
                                //foreach (var elt in Colors)
                                //{
                                //    var dist = (elt.Value - new Vector2((float)color.V0, (float)color.V1)).Length;

                                //    if (dist < minDist)
                                //    {
                                //        minDist = dist;
                                //        pastille.Color = elt.Key;
                                //    }
                                //}

                            }
                        }


                        foreach (var pastille in pastilleMatrix)
                        {
                            if (pastille.Quadrilateral != null)
                            {
                                inImage.DrawPolyline(pastille.Quadrilateral.Points, true, new Bgr(Color.Blue), 3);
                                inImage.Draw(new LineSegment2D(new Point((int)(pastille.Quadrilateral.Center.X), (int)(pastille.Quadrilateral.Center.Y)), new Point((int)(pastille.Quadrilateral.Center.X + right.X * 20), (int)(pastille.Quadrilateral.Center.Y + right.Y * 20))), new Bgr(Color.SeaGreen), 3);
                                inImage.Draw(new LineSegment2D(new Point((int)(pastille.Quadrilateral.Center.X), (int)(pastille.Quadrilateral.Center.Y)), new Point((int)(pastille.Quadrilateral.Center.X + bottom.X * 20), (int)(pastille.Quadrilateral.Center.Y + bottom.Y * 20))), new Bgr(Color.Red), 3);
                                inImage.Draw(pastille.m + ";" + pastille.n, new Point((int)pastille.Quadrilateral.Center.X - 10, (int)pastille.Quadrilateral.Center.Y + 10), FontFace.HersheyComplex, 0.5, new Bgr(Color.Aquamarine), 1);
                            }
                            else
                            {
                                var circle = new CircleF(new PointF(pastille.Center.X, pastille.Center.Y), averageLength / 4);
                                inImage.Draw(circle, new Bgr(Color.Pink), (int)averageLength / 4);
                                //inImage.Draw(circle, new Bgr(Color.Pink), 3);
                            }


                            //var l = 20;
                            //var rect = new Rectangle(pastille.m * l + l, pastille.n * l + l, l / 2, l / 2);
                            //inImage.Draw(rect, new Bgr(pastille.Color), l / 2, LineType.AntiAlias);

                        }
                        inImage.Draw(new CircleF(new PointF(origine.X, origine.Y), 5), new Bgr(Color.Purple), 10);

                        angle = Math.Atan2(bottom.X, bottom.Y);
                    }

                }

                var allFacesScanned = false;




                // traitement des infos de l'image, mise à jour de l'état
                using (var state = GlobalState.GetState())
                {

                    if (state.Scanner.Starting)
                    {
                        for (int f = 0; f < 6; f++)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                for (int j = 0; j < 4; j++)
                                {
                                    _scannedColors[f, i, j].Clear();
                                    //_scannedCube.colors[f * 16 + i * 4 + j] = Color.Transparent;
                                }
                            }
                        }
                    }

                    // si on est en scan mais qu'aucun cube n'est détécté
                    if (!state.Scanner.WaitingForFace && pastilleMatrix == null)
                    {
                        // lancer un timer
                        if (!_noCubeSW.IsRunning)
                        {
                            _noCubeSW.Restart();
                        }

                        // si le temps imparti est atteint, passer à la face suivant
                        else if (_noCubeSW.ElapsedMilliseconds > 1000)
                        {
                            _noCubeSW.Stop();

                            state.Scanner.WaitingForFace = true;
                            MainForm.Instance.Viewer.SetCameraInclination(0);


                            switch (state.Scanner.CurrentScannedFace)
                            {
                                case Faces.L:
                                    state.Scanner.CurrentScannedFace = Faces.B;
                                    MainForm.Instance.Viewer.RotateCube(Modele.Axe.Z);
                                    break;
                                case Faces.B:
                                    state.Scanner.CurrentScannedFace = Faces.R;
                                    MainForm.Instance.Viewer.RotateCube(Modele.Axe.Z);
                                    break;
                                case Faces.R:
                                    state.Scanner.CurrentScannedFace = Faces.F;
                                    MainForm.Instance.Viewer.RotateCube(Modele.Axe.Z);
                                    break;
                                case Faces.F:
                                    state.Scanner.CurrentScannedFace = Faces.D;
                                    MainForm.Instance.Viewer.RotateCube(Modele.Axe.Y);
                                    break;
                                case Faces.D:
                                    state.Scanner.CurrentScannedFace = Faces.U;
                                    MainForm.Instance.Viewer.RotateCube(Modele.Axe.Y, -2);
                                    break;
                                case Faces.U:
                                    allFacesScanned = true;
                                    state.Scanner.CurrentScannedFace = Faces.UNKNOWN;
                                    MainForm.Instance.Viewer.RotateCube(Modele.Axe.Y);
                                    System.Threading.Thread.Sleep(1000);
                                    break;
                                default:
                                    Logger.Log("Face inconnue lors du scan", TraceEventType.Error);
                                    break;
                            }

                        }
                    }
                    else if (pastilleMatrix != null)
                    {
                        _noCubeSW.Stop();

                        if (state.Scanner.CurrentScannedFace == Faces.UNKNOWN)
                        {
                            state.Scanner.Reset();
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                _scannedColors[(int)state.Scanner.CurrentScannedFace, i, j].Add(pastilleMatrix[i, j]);
                            }
                        }

                        // raz du cube
                        if (state.Scanner.Starting)
                        {
                            MainForm.Instance.Viewer.RefreshCube(null);
                            MainForm.Instance.Viewer.RotateCube(Modele.Axe.Z);
                        }

                        SumAngle += angle;
                        nbAngleSamples++;

                        if (_refreshCubeSW.ElapsedMilliseconds > 300 && nbAngleSamples > 0)
                        {
                            MainForm.Instance.Viewer.setColorsAndInclination(_scannedColors.OfType<List<Pastille>>().Select((lst) => lst.Count == 0 ? Color.FromArgb(0x22, 0x22, 0x22) : Color.FromArgb((int)lst.Average((x) => x.MeanColorBGR.Red), (int)lst.Average((x) => x.MeanColorBGR.Green), (int)lst.Average((x) => x.MeanColorBGR.Blue))), SumAngle / nbAngleSamples);

                            SumAngle = 0;
                            nbAngleSamples = 0;
                            _refreshCubeSW.Restart();
                        }

                        state.Scanner.WaitingForFace = false;
                    }

                    // dessiner un carré autour de l'image si un scan est en cours
                    if (!state.Scanner.WaitingForFace)
                    {
                        inImage.Draw(new Rectangle(0, 0, inImage.Width, inImage.Height), new Bgr(Color.Red), 10);
                    }

                    if (allFacesScanned)
                    {
                     state.InitialCube=   ColorClassification.GetCube(_scannedColors.OfType<List<Pastille>>().Select((lst) => Color.FromArgb((int)lst.Average((x) => x.MeanColorBGR.Red), (int)lst.Average((x) => x.MeanColorBGR.Green), (int)lst.Average((x) => x.MeanColorBGR.Blue))).ToList(), state.ColorsAssociation);
                    }
                }

                if (allFacesScanned)
                {
                    FormManager.Navigate<ColorDefinitionControl>();
                }
           }

            _viewer.Image = inImage;
        }

        // moyenne de l'angle du cube
        private double SumAngle = 0;
        private int nbAngleSamples = 0;

        // temps sans cube présent à la caméra
        private Stopwatch _noCubeSW = new Stopwatch();

        // timer de refresh du viewer 3d
        private Stopwatch _refreshCubeSW = new Stopwatch();


        private void btnDebug_Click(object sender, EventArgs e)
        {
            (new VideoScannerDebugForm(_scannedColors)).Show();
        }
    }
}
