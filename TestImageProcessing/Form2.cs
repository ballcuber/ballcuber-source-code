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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestImageProcessing
{
    public partial class Form2 : Form
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
            Last = Count-1,
        }

        public class ImageParameters
        {
            public SelectedImage SelectedImage { get; set; } = SelectedImage.Last;

            public int CannyThreshold1 { get; set; } = 20;
            public int CannyThreshold2 { get; set; } = 20;

            public int MedianBlurSize { get; set; } = 11;
            public double ContourEpsilon { get; set; } = 50;
            public double Threshold { get; set; } = 120;
        }

        public ImageParameters Parameters = new ImageParameters();

        public IImage[] DebugImages = new IImage[(int)SelectedImage.Count];

        public Form2()
        {
            InitializeComponent();



            Capture capture = new Capture(); //create a camera captue

            var processViewer = new ImageBox(); //create an image viewer
            splitContainer1.Panel2.Controls.Add(processViewer);
            processViewer.Dock = DockStyle.Fill;

            grid.SelectedObject = Parameters;



            Application.Idle += new EventHandler(delegate (object sender, EventArgs e)
            {

                //run this until application closed (close button click on image viewer)
                var frame = capture.QueryFrame();
                var inImage = frame.ToImage<Bgr, byte>(); //draw the image obtained from camera

                DebugImages[(int)SelectedImage.InImage] = inImage;

                var medianBlurImageIn = new Image<Bgr, byte>(inImage.Size);
                CvInvoke.MedianBlur(inImage, medianBlurImageIn, Parameters.MedianBlurSize);
                DebugImages[(int)SelectedImage.MedianBlurIn] = medianBlurImageIn;



                DebugImages[(int)SelectedImage.InImageB] = medianBlurImageIn[0];
                DebugImages[(int)SelectedImage.InImageG] = medianBlurImageIn[1];
                DebugImages[(int)SelectedImage.InImageR] = medianBlurImageIn[2];

                var InImageSum = medianBlurImageIn[0]+ medianBlurImageIn[1]+ medianBlurImageIn[2];
                DebugImages[(int)SelectedImage.InImageSum] = InImageSum;
                /*
                Mat medianBlurImage = new Mat();

                CvInvoke.MedianBlur(inImage, medianBlurImage, Parameters.MedianBlurSize);

                DebugImages[(int)SelectedImage.MedianBlur] = medianBlurImage;
                */

                Mat threshold = new Mat();
                CvInvoke.Threshold(InImageSum, threshold, Parameters.Threshold, 255, ThresholdType.Binary);
                DebugImages[(int)SelectedImage.threshold] = threshold;

                /*
                Mat blackWhite = new Mat();
                //   medianBlurImage.ConvertTo(blackWhite, DepthType.)
                CvInvoke.CvtColor(inImage, blackWhite, ColorConversion.Bgra2Gray, 1);
                DebugImages[(int)SelectedImage.BlackWhite] = blackWhite;
                */
                /*
                Mat GaussianBlur = new Mat();
                CvInvoke.GaussianBlur(blackWhite, GaussianBlur, Parameters.GaussianKSize, Parameters.SigmaX);
                DebugImages[(int)SelectedImage.GaussianBlur] = GaussianBlur;
                */
                /*
                Mat medianBlurImage = new Mat();
                CvInvoke.MedianBlur(InImageSum, medianBlurImage, Parameters.MedianBlurSize);
                DebugImages[(int)SelectedImage.MedianBlur] = medianBlurImage;
                */

                Mat CannyImage = new Mat();
                CvInvoke.Canny(threshold, CannyImage, Parameters.CannyThreshold1, Parameters.CannyThreshold2, 3, true);
                DebugImages[(int)SelectedImage.Canny] = CannyImage;


                var contoursImage = inImage.Clone();
                DebugImages[(int)SelectedImage.approxContour] = contoursImage;

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
                    if (maxContour == null) return;

                    using (VectorOfPoint approxContour = new VectorOfPoint())
                    {

                        CvInvoke.ApproxPolyDP(maxContour, approxContour, Parameters.ContourEpsilon, true);
                        var convexContour = CvInvoke.ConvexHull(approxContour.ToArray().Select((x) => new PointF(x.X, x.Y)).ToArray());
                        var pointConvexContour = convexContour.Select((x) => new Point((int)x.X, (int)x.Y)).ToArray();

                        var circle = CvInvoke.MinEnclosingCircle(convexContour);

                        if (convexContour.Length == 6)
                        {
                            contoursImage.DrawPolyline(pointConvexContour, true, new Bgr(Color.Green), 3);
                        }

                            contoursImage.Draw(circle, new Bgr(Color.Orange), 3);

                            contoursImage.Draw(new Cross2DF(circle.Center, 10, 10), new Bgr(Color.Orange), 3);
                        //PointF linePoint;
                        //PointF lineDirection;
                        //CvInvoke.FitLine(convexContour, out lineDirection,out linePoint, DistType.L2, 0, 0.01, 0.01);
                        //CvInvoke.Line(contoursImage, new Point((int)(linePoint.X + 1000 * lineDirection.X), (int)(linePoint.Y + 1000 * lineDirection.Y)), new Point((int)(linePoint.X - 1000 * lineDirection.X), (int)(linePoint.Y - 1000 * lineDirection.Y)), new MCvScalar(10, 150, 255));
                    }

                    processViewer.Image = DebugImages[(int)Parameters.SelectedImage];
                }
            });

            }
    }
}
