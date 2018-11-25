using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.UI;
using Emgu.CV;
using static fgSolver.VideoParameters;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

namespace fgSolver.Video
{
    public partial class VideoScannerControl : UserControl, INavigableForm
    {

        private Capture _capture;
        private ImageBox _processViewer;

        private ImageBox[] _faceViewers = new ImageBox[3];

        private const int size = 100;

        public VideoScannerControl()
        {
            InitializeComponent();



            for (int i = 0; i < 3; i++)
            {
                _faceViewers[i] = new ImageBox();
                _faceViewers[i].Dock = DockStyle.Fill;
                _faceViewers[i].Width = size;
            }
            pnl1.Controls.Add(_faceViewers[0]);
            pnl2.Controls.Add(_faceViewers[1]);
            pnl3.Controls.Add(_faceViewers[2]);

            _processViewer = new ImageBox();
            pnlMain.Controls.Add(_processViewer);
            pnlMain.Controls.SetChildIndex(_processViewer, 0);
            _processViewer.Dock = DockStyle.Fill;
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

        public void LeaveFrom()
        {
            if (_capture != null)
            {
                _capture.Stop();
                _capture.Dispose();
                _capture = null;
            }
            using (var state = GlobalState.GetState())
            {
                MainForm.Instance.Viewer.RefreshCube(state.InitialCube);
            }

        }

        public void NavigueTo()
        {
            using (var state = GlobalState.GetState())
            {
                _capture = new Capture(state.VideoParameters.CaptureID); //create a camera captue
                grid.SelectedObject = state.VideoParameters;
            }
            DebugImages = new Mat[(int)SelectedImage.Count];
            _lastResults = new MCvScalar[2, 3, 4, 4];
            _currentSommet = -1;
            btnSommet2.Enabled = false;
            btnInit.Enabled = false;
           validateAsked = false;
            this.btnValidateCalib.BackgroundImage = global::fgSolver.Properties.Resources.OK;
            grid.Visible = false;

            MainForm.Instance.Viewer.RefreshCube(null);
            _lastScannedSommet = -1;
        }

        public Mat[] DebugImages = new Mat[(int)SelectedImage.Count];

        private MCvScalar[,,,] _lastResults = new MCvScalar[2, 3, 4, 4];

        // 0 ou 1, -1 pour désactiver
        private int _currentSommet = -1;

        public void PeriodicUpdate(GlobalState formerState, GlobalState currentState)
        {
            if (_capture == null) return;


            var frame = _capture.QueryFrame();
            var inImage = frame;
            DebugImages[(int)SelectedImage.InImage] = inImage;

            var medianBlurImageIn = new Image<Bgr, byte>(inImage.Size);
            CvInvoke.MedianBlur(inImage, medianBlurImageIn, currentState.VideoParameters.MedianBlurSize);
            //DebugImages[(int)SelectedImage.MedianBlurIn] = medianBlurImageIn.Mat;


            double rectSize = (size - 4 * currentState.VideoParameters.SquareDistance) / 4;

            for (int i = 0; i < 3; i++)
            {
                var points = new PointF[] { currentState.VideoParameters.Points[2 * i], currentState.VideoParameters.Points[2 * i + 1], currentState.VideoParameters.Points[(2 * i + 2) % 6], currentState.VideoParameters.Center };

                var destPoint = new PointF[] { new Point(size - 1, size - 1), new Point(0, size - 1), new Point(0, 0), new Point(size - 1, 0) };
                var transformMat = CvInvoke.GetPerspectiveTransform(points, destPoint);

                var dest = new Image<Bgr, byte>(new Size(size, size));

                CvInvoke.WarpPerspective(medianBlurImageIn, dest, transformMat, new Size(size, size));

                for (int y = 0; y < 4; y++)
                {
                    double yTop = (0.5 + y) * currentState.VideoParameters.SquareDistance + y * rectSize;

                    for (int x = 0; x < 4; x++)
                    {
                        double xLeft = (0.25 + x) * currentState.VideoParameters.SquareDistance + x * rectSize;
                        var rect = new Rectangle(new Point((int)xLeft, (int)yTop), new Size((int)rectSize, (int)rectSize));

                        if (_currentSommet >= 0)
                        {
                            MCvScalar mean = new MCvScalar();
                            MCvScalar std = new MCvScalar();

                            CvInvoke.MeanStdDev(dest.GetSubRect(rect), ref mean, ref std);

                            _lastResults[_currentSommet, i, x, y] = mean;
                            btnSommet2.Enabled = true;

                            if (_currentSommet == 1)
                            {
                                btnInit.Enabled = true;
                            }

                        }
                        dest.Draw(rect, new Bgr(Color.Blue));


                    }
                }

                _faceViewers[i].Image = dest;

            }


            Image<Bgr, byte> displayedImage = null;

            displayedImage = (grid.Visible ? DoCalibration(medianBlurImageIn, currentState) : inImage).ToImage<Bgr, byte>();

            // afficher l'hexagone
            displayedImage.DrawPolyline(currentState.VideoParameters.Points, true, new Bgr(Color.Red), 3);


            // afficher les n°
            for (int i = 0; i < currentState.VideoParameters.Points.Length; i++)
            {
                displayedImage.Draw(i.ToString(), currentState.VideoParameters.Points[i], FontFace.HersheySimplex, 1, new Bgr(Color.Orange));
            }

            // Croix centrale
            displayedImage.Draw(new Cross2DF(currentState.VideoParameters.Center, 10, 10), new Bgr(Color.Red), 3);





            if (_currentSommet > -1)
            {
                _currentSommet = -1;

                MainForm.Instance.Viewer.setColors(GetColorList());
            }
            _processViewer.Image = displayedImage;
        }

        private List<Color> GetColorList()
        {
            var colors = new List<Color>();

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    var color = _lastResults[1, 1, 3 - y, x];
                    colors.Add(Color.FromArgb((int)color.V2, (int)color.V1, (int)color.V0));
                }

            }

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    var color = _lastResults[1, 2, 3 - y, x];
                    colors.Add(Color.FromArgb((int)color.V2, (int)color.V1, (int)color.V0));
                }

            }

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    var color = _lastResults[0, 1, y, 3 - x];
                    colors.Add(Color.FromArgb((int)color.V2, (int)color.V1, (int)color.V0));
                }

            }

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    var color = _lastResults[0, 2, 3 - x, 3 - y];
                    colors.Add(Color.FromArgb((int)color.V2, (int)color.V1, (int)color.V0));
                }

            }


            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    var color = _lastResults[1, 0, x, y];
                    colors.Add(Color.FromArgb((int)color.V2, (int)color.V1, (int)color.V0));
                }

            }

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    var color = _lastResults[0, 0, x, y];
                    colors.Add(Color.FromArgb((int)color.V2, (int)color.V1, (int)color.V0));
                }

            }
            return colors;
        }


        private int _lastScannedSommet = -1;
        private void btnSommet1_Click(object sender, EventArgs e)
        {
            _currentSommet = 0;
            MainForm.Instance.Viewer.setCameraPosition(Math.PI / 4, -2);
            if (_lastScannedSommet == 1)
            {
                MainForm.Instance.Viewer.Move(Modele.Axe.Z, 1, 4, 1);
                MainForm.Instance.Viewer.Move(Modele.Axe.Y, 1, 4, -1);
                MainForm.Instance.Viewer.Move(Modele.Axe.X, 1, 4, -1);
            }
            _lastScannedSommet = 0;
        }

        private void btnSommet2_Click(object sender, EventArgs e)
        {
            _currentSommet = 1;
            if (_lastScannedSommet != 1)
            {
                MainForm.Instance.Viewer.Move(Modele.Axe.X, 1, 4, 1);
                MainForm.Instance.Viewer.Move(Modele.Axe.Y, 1, 4, 1);
                MainForm.Instance.Viewer.Move(Modele.Axe.Z, 1, 4, -1);
            }

            _lastScannedSommet = 1;
        }

        private void btnParam_Click(object sender, EventArgs e)
        {
            grid.Visible = !grid.Visible;

        }

        private void grid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            using (var state = GlobalState.GetState())
            {
                state.SaveConfiguration();
            }
        }

        private Mat DoCalibration(Image<Bgr, byte> medianBlurImageIn, GlobalState state)
        {

            DebugImages[(int)SelectedImage.InImageB] = medianBlurImageIn[0].Mat;
            DebugImages[(int)SelectedImage.InImageG] = medianBlurImageIn[1].Mat;
            DebugImages[(int)SelectedImage.InImageR] = medianBlurImageIn[2].Mat;

            var InImageSum = medianBlurImageIn[0] + medianBlurImageIn[1] + medianBlurImageIn[2];
            DebugImages[(int)SelectedImage.InImageSum] = InImageSum.Mat;

            Mat threshold = new Mat();
            CvInvoke.Threshold(InImageSum, threshold, state.VideoParameters.Threshold, 255, ThresholdType.Binary);
            DebugImages[(int)SelectedImage.threshold] = threshold;

            Mat CannyImage = new Mat();
            CvInvoke.Canny(threshold, CannyImage, state.VideoParameters.CannyThreshold1, state.VideoParameters.CannyThreshold2, 3, true);
            DebugImages[(int)SelectedImage.Canny] = CannyImage;


            var contoursImage = medianBlurImageIn.Clone();
            DebugImages[(int)SelectedImage.approxContour] = contoursImage.Mat;

            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(CannyImage, contours, null, RetrType.External, ChainApproxMethod.ChainApproxNone);

                VectorOfPoint maxContour = null;
                double arcSize = -1;

                for (int i = 0; i < contours.Size; i++)
                {
                    var arc = CvInvoke.ArcLength(contours[i], true);
                    if (arc > arcSize)
                    {
                        arcSize = arc;
                        maxContour = contours[i];
                    }

                }
                if (maxContour != null)
                {

                    using (VectorOfPoint approxContour = new VectorOfPoint())
                    {

                        CvInvoke.ApproxPolyDP(maxContour, approxContour, state.VideoParameters.ContourEpsilon, true);
                        var convexContour = CvInvoke.ConvexHull(approxContour.ToArray().Select((x) => new PointF(x.X, x.Y)).ToArray());
                        var pointConvexContour = convexContour.Select((x) => new Point((int)x.X, (int)x.Y)).ToArray();

                        var circle = CvInvoke.MinEnclosingCircle(convexContour);

                        if (convexContour.Length == 6 && validateAsked)
                        {
                            validateAsked = false;

                            if (MessageBox.Show("Un hexagonne a été trouvé, mettre à jour ?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                using (var globState = GlobalState.GetState())
                                {
                                    globState.VideoParameters.Center = new Point((int)circle.Center.X, (int)circle.Center.Y);

                                    var maxY = convexContour.Max((x) => x.Y);

                                    var indexSommetHaut = convexContour.ToList().FindIndex((x) => x.Y >= maxY - 0.1);

                                    for (int i = indexSommetHaut; i < convexContour.Length; i++)
                                    {
                                        globState.VideoParameters.Points[i] = pointConvexContour[i];
                                    }

                                    for (int i = 0; i < indexSommetHaut; i++)
                                    {
                                        globState.VideoParameters.Points[i] = pointConvexContour[i];
                                    }


                                    grid.Refresh();

                                    globState.SaveConfiguration();
                                    this.btnValidateCalib.BackgroundImage = global::fgSolver.Properties.Resources.OK;

                                }
                            }
                        }

                        contoursImage.DrawPolyline(pointConvexContour, true, new Bgr(Color.Green), 3);

                        contoursImage.Draw(circle, new Bgr(Color.DarkGreen), 3);

                        contoursImage.Draw(new Cross2DF(circle.Center, 10, 10), new Bgr(Color.DarkGreen), 3);

                    }
                }

                return DebugImages[(int)state.VideoParameters.DebugImage];
            }
        }

        private void grid_VisibleChanged(object sender, EventArgs e)
        {
            btnValidateCalib.Visible = grid.Visible;
        }

        private bool validateAsked = false;

        private void btnValidateCalib_Click(object sender, EventArgs e)
        {
           if( MessageBox.Show("Voulez-vous lancer une détection de l'héxagone ?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                validateAsked = true;
                this.btnValidateCalib.BackgroundImage = global::fgSolver.Properties.Resources.AnimatedCube;

            }
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
           var colors =  GetColorList();

            using (var state = GlobalState.GetState())
            {
                state.InitialCube = ColorClassification.GetCube(colors);
            }
        }
    }
}