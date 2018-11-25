using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;
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
using System.Xml.Serialization;

namespace TestImageProcessing
{
    public partial class Form3 : Form
    {
        public enum SelectedImage
        {
            InImage,
            MedianBlurIn,
            InImageB,
            InImageG,
            InImageR,
            InImageSum,
            threshold,
            Canny,
            approxContour,

            [Browsable(false)]
            Count,
            [Browsable(false)]
            Last = Count - 1,
        }

        public Mat[] DebugImages = new Mat[(int)SelectedImage.Count];

        public class VideoParameters
        {
            public int CaptureID { get; set; } = 0;

            public Point[] Points { get; set; } = new Point[6];

            public Point Center { get; set; }

            public int PerpectiveSize { get; set; } = 100;

            public int SquareDistance { get; set; } = 5;

            public SelectedImage SelectedImage { get; set; } = SelectedImage.Last;

            public int CannyThreshold1 { get; set; } = 20;
            public int CannyThreshold2 { get; set; } = 20;

            public int MedianBlurSize { get; set; } = 11;
            public double ContourEpsilon { get; set; } = 50;
            public double Threshold { get; set; } = 120;
    }

        public VideoParameters Parameters = new VideoParameters();


        private Capture _capture;
        private ImageBox _processViewer;

        private ImageBox[] _faceViewers = new ImageBox[3];

        private int size
        {
            get
            {
                return Parameters.PerpectiveSize;
            }
        }

        private MCvScalar[,,] _lastResults = new MCvScalar[3, 4, 4];

        public Form3()
        {


            InitializeComponent();

            _capture = new Capture(Parameters.CaptureID); //create a camera captue

            for (int i = 2; i >= 0; i--)
            {
                _faceViewers[i] = new ImageBox(); //create an image viewer
                perspectivePnl.Controls.Add(_faceViewers[i]);
                _faceViewers[i].Dock = DockStyle.Left;
                _faceViewers[i].Width = size;
            }

            _processViewer = new ImageBox(); //create an image viewer
            pnl.Panel2.Controls.Add(_processViewer);
            pnl.Panel2.Controls.SetChildIndex(_processViewer,0);
            _processViewer.Dock = DockStyle.Fill;
            _processViewer.Height = _capture.Height;


            if (File.Exists("parameters.xml"))
            {
                var serializer = new XmlSerializer(typeof(VideoParameters));

                using (var reader = new StreamReader("parameters.xml"))
                {
                    this.Parameters = (VideoParameters)serializer.Deserialize(reader);
                }
            }

            grid.SelectedObject = Parameters;
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
                if (_capture == null) return;
                if (_processViewer == null) return;


                var frame = _capture.QueryFrame();
                var inImage = frame;
            DebugImages[(int)SelectedImage.InImage] = inImage;

            var medianBlurImageIn = new Image<Bgr, byte>(inImage.Size);
            CvInvoke.MedianBlur(inImage, medianBlurImageIn, Parameters.MedianBlurSize);
            DebugImages[(int)SelectedImage.MedianBlurIn] = medianBlurImageIn.Mat;


            double rectSize = (size - 4 * Parameters.SquareDistance) / 4;

            for (int i = 0; i < 3; i++)
            {
                var points = new PointF[] { Parameters.Points[2*i], Parameters.Points[2*i+1], Parameters.Points[(2*i+2)%6], Parameters.Center };

                var destPoint = new PointF[] { new Point(size - 1, size - 1), new Point(0, size - 1), new Point(0, 0), new Point(size - 1, 0) };
                var transformMat = CvInvoke.GetPerspectiveTransform(points, destPoint);

                var dest = new Image<Bgr, byte>(new Size(size, size));

                CvInvoke.WarpPerspective(medianBlurImageIn, dest, transformMat, new Size(size, size));

                for (int y = 0; y < 4; y++)
                {
                    double yTop = (0.5 + y) * Parameters.SquareDistance + y * rectSize;

                    for (int x = 0; x < 4; x++)
                    {
                        double xLeft = (0.25 + x) * Parameters.SquareDistance + x * rectSize;
                        var rect = new Rectangle(new Point((int)xLeft, (int)yTop), new Size((int)rectSize, (int)rectSize));

                        MCvScalar mean = new MCvScalar();
                        MCvScalar std = new MCvScalar();

                        CvInvoke.MeanStdDev(dest.GetSubRect(rect), ref mean, ref std);

                        _lastResults[i, x, y] = mean;

                        dest.Draw(rect, new Bgr(Color.Blue));


                    }
                }

                _faceViewers[i].Image = dest;

            }

                Image<Bgr,byte> displayedImage = null;

            displayedImage =( chkAutoCalibration.Checked ? DoCalibration(medianBlurImageIn) : inImage).ToImage<Bgr, byte>();

            // afficher l'hexagone
            displayedImage.DrawPolyline(Parameters.Points, true, new Bgr(Color.Red), 3);


            // afficher les n°
            for (int i=0;i< Parameters.Points.Length; i++)
            {
                displayedImage.Draw(i.ToString(), Parameters.Points[i], FontFace.HersheySimplex, 1, new Bgr(Color.Orange));
            }

            // Croix centrale
            displayedImage.Draw(new Cross2DF(Parameters.Center, 10, 10), new Bgr(Color.Red), 3);


            _processViewer.Image = displayedImage;

        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmr.Stop();
        }

        private Mat DoCalibration(Image<Bgr, byte> medianBlurImageIn) {

                DebugImages[(int)SelectedImage.InImageB] = medianBlurImageIn[0].Mat;
                DebugImages[(int)SelectedImage.InImageG] = medianBlurImageIn[1].Mat;
                DebugImages[(int)SelectedImage.InImageR] = medianBlurImageIn[2].Mat;

                var InImageSum = medianBlurImageIn[0] + medianBlurImageIn[1] + medianBlurImageIn[2];
                DebugImages[(int)SelectedImage.InImageSum] = InImageSum.Mat;

                Mat threshold = new Mat();
                CvInvoke.Threshold(InImageSum, threshold, Parameters.Threshold, 255, ThresholdType.Binary);
                DebugImages[(int)SelectedImage.threshold] = threshold;

                Mat CannyImage = new Mat();
                CvInvoke.Canny(threshold, CannyImage, Parameters.CannyThreshold1, Parameters.CannyThreshold2, 3, true);
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

                        CvInvoke.ApproxPolyDP(maxContour, approxContour, Parameters.ContourEpsilon, true);
                        var convexContour = CvInvoke.ConvexHull(approxContour.ToArray().Select((x) => new PointF(x.X, x.Y)).ToArray());
                        var pointConvexContour = convexContour.Select((x) => new Point((int)x.X, (int)x.Y)).ToArray();

                        var circle = CvInvoke.MinEnclosingCircle(convexContour);

                        if (convexContour.Length == 6  && validateAsked)
                        {
                            validateAsked = false;

                            Parameters.Center = new Point( (int)circle.Center.X,(int) circle.Center.Y) ;

                           var maxY= convexContour.Max((x) => x.Y);

                          var indexSommetHaut=  convexContour.ToList().FindIndex((x) => x.Y >= maxY - 0.1);

                            for (int i = indexSommetHaut; i < convexContour.Length; i++)
                            {
                                Parameters.Points[i] = pointConvexContour[i];
                            }

                            for (int i = 0; i < indexSommetHaut; i++)
                            {
                                Parameters.Points[i] = pointConvexContour[i];
                            }

                            grid.Refresh();
                            Save();
                            chkAutoCalibration.Checked = false;

                        }

                        contoursImage.DrawPolyline(pointConvexContour, true, new Bgr(Color.Green), 3);

                            contoursImage.Draw(circle, new Bgr(Color.DarkGreen), 3);

                            contoursImage.Draw(new Cross2DF(circle.Center, 10, 10), new Bgr(Color.DarkGreen), 3);

                    }
                }

                    return DebugImages[(int)Parameters.SelectedImage];
                }
            }
        

        private void grid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Save();

        }

        private void Save()
        {
            var serializer = new XmlSerializer(typeof(VideoParameters));

            using (var writer = new StreamWriter("parameters.xml"))
            {
                serializer.Serialize(writer, this.Parameters);
            }
        }

        private void chkAutoCalibration_CheckedChanged(object sender, EventArgs e)
        {

            btnUse.Visible = chkAutoCalibration.Checked;
        }

        private Boolean validateAsked = false;
        private void btnUse_Click(object sender, EventArgs e)
        {
            validateAsked = true;
        }
    }
}
