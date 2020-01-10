using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using System.Diagnostics;
using System.Windows.Media.Media3D;
using OpenTK;

namespace TestImageProcessing
{
    public partial class Form1 : Form
    {

        //public static List<Bgr> Colors = new List<Bgr>(new Bgr[] { new Bgr(85,210,172), new Bgr(93,162,61), new Bgr(95,166,55), new Bgr(155,122,88),    new Bgr(57,105,233),    new Bgr(47,62,146) });

        //public static List<Vector3> VectorColors = Colors.Select((x)=>  new Vector3((float)x.Blue, (float)x.Green, (float)x.Red)).ToList();

        public static Dictionary<Color, Vector2> Colors = new Dictionary<Color, Vector2>() {
    {Color.Yellow, new Vector2(73,130) },
    {Color.Blue, new Vector2(18,180) },
    {Color.White, new Vector2(19,70) },
    {Color.Green, new Vector2(54,170) },
    {Color.Orange, new Vector2(116,150) },
    {Color.Red, new Vector2(115,150) },

        };


        public List<double> h = new List<double>();
        public List<double> s = new List<double>();


        public static float MaxColorDistance = float.MaxValue;

        public Form1()
        {
            InitializeComponent();

            //Mat picture = new Mat(@"C:\Users\flore\Desktop\OK.png", Emgu.CV.CvEnum.LoadImageType.AnyColor); // Pick some path on your disk!
            //CvInvoke.Imshow("Hello World!", picture); // Open window with image


            Capture capture = new Capture(); //create a camera captue

            // var inImage = new Image<Bgr, byte>(@"PhotosCube\cube3.jpg");

            var processViewer = new ImageBox(); //create an image viewer
            this.pnl.Controls.Add(processViewer);
            processViewer.Dock = DockStyle.Top;
            processViewer.Height = capture.Height;

            var cannyViewer = new ImageBox(); //create an image viewer
            this.pnl.Controls.Add(cannyViewer);
            cannyViewer.Dock = DockStyle.Top;
            cannyViewer.Height = capture.Height;


            var contourViewer = new ImageBox(); //create an image viewer
            this.pnl.Controls.Add(contourViewer);
            contourViewer.Dock = DockStyle.Top;
            contourViewer.Height = capture.Height;



            var viewer = new ImageBox(); //create an image viewer
            this.pnl.Controls.Add(viewer);
            viewer.Dock = DockStyle.Top;
            viewer.Height = capture.Height;


            Vector2 vect1;
            Vector2 vect2;

            for (int i = 0; i < Colors.Count-1; i++)
            {
                for (int j = i+1; j < Colors.Count; j++)
                {
                    var dist = (Colors.Values.ElementAt(i) - Colors.Values.ElementAt(j)).LengthFast;

                    if (dist < MaxColorDistance)
                    {
                        MaxColorDistance = dist;
                        vect1 = Colors.Values.ElementAt(i);
                        vect2 = Colors.Values.ElementAt(j);

                    }
                }
            }


            Application.Idle += new EventHandler(delegate (object sender, EventArgs e)
            {

                //run this until application closed (close button click on image viewer)
                var frame = capture.QueryFrame();
                var inImage = frame.ToImage<Bgr, byte>(); //draw the image obtained from camera

                var contourImage = inImage.Clone();

                //   Mat outImage = new Mat();

                //CvInvoke.MedianBlur(frame, outImage, 5);

             var processsImageChannels = new Image<Bgr,byte>(inImage.Size);
                CvInvoke.CvtColor(frame, processsImageChannels, ColorConversion.Rgb2Hsv);



               // var processsImage = new Mat();
               // CvInvoke.ExtractChannel(inImage, processsImage, 1);

                processViewer.Image = processsImageChannels;

                Mat canny = new Mat();

                CvInvoke.Canny(inImage, canny, barCanny1.Value, barCanny2.Value, 3, true);

                cannyViewer.Image = canny;

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

                            CvInvoke.ApproxPolyDP(contour, approxContour, arc * 0.1, true);

                            if (approxContour.Size == 4)
                            {

                                var pts = approxContour.ToArray();

                                contourImage.DrawPolyline(pts, true, new Bgr(Color.Green));

                                var quadri = new Quadrilateral(pts, 0.1, 0.1);

                                if (quadri.IsParallelogram)
                                {
                                    lstParallelogram.Add(quadri);
                                }

                            }

                        }
                    }

                    contourViewer.Image = contourImage;

                    // suppression des doublons
                    var comparer = new QuadrilateralCenterComparer(0.3);
                    lstParallelogram = lstParallelogram.Distinct(comparer).ToList();

                   var lstSquares = lstParallelogram.Where((x) => x.IsSquare).ToList();

                    // nombre minimal de cube
                    if (lstSquares.Count > 5)
                    {
                        //var circle = CvInvoke.MinEnclosingCircle(lstCube.SelectMany((x) => x.Points).Select((x) => new PointF(x.X, x.Y)).ToArray());
                        //inImage.Draw(circle, new Bgr(255, 255, 0));

                       var minX = lstSquares.Min((x) => x.Center.X);
                        var maxX = lstSquares.Max((x) => x.Center.X);
                        var minY = lstSquares.Min((x) => x.Center.Y);
                        var maxY = lstSquares.Max((x) => x.Center.Y);



                        //inImage.Draw(new Rectangle((int)minX, (int)minY, (int)(maxX - minX), (int)(maxY - minY)), new Bgr(Color.Bisque), 1);

                        lstSquares.Min((x) => x.Center.X);

                        var averageLength = lstSquares.Average((x) => x.AverageLength);
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

                            //inImage.DrawPolyline(quadri.Points, true, new Bgr(Color.Blue), 3);
                            pastille.n = (int)Math.Round(Vector2.Dot(u, bottom) / centerDistance);
                            pastille.m = (int)Math.Round(Vector2.Dot(u, right) / centerDistance);

                            pastille.Center = lstSquares[i].Center;

                            pastilles.Add(pastille);
                        }

                        var xMax = pastilles.Max((x) => x.m);
                        var xMin = pastilles.Min((x) => x.m);
                        var yMax = pastilles.Max((x) => x.n);
                        var yMin = pastilles.Min((x) => x.n);

                        if (xMax - xMin != 3 || yMax - yMin != 3) return; // ce n'est pas un cube 4x4

                        // x et y entre 0 et 3
                        pastilles.ForEach((x) => { x.m -= xMin; x.n -= yMin; });

                        // recalcul de centerDistance
                        centerDistance = 0;
                        for (int i = 0; i < pastilles.Count - 1; i++)
                        {
                            var dm = pastilles[i + 1].m - pastilles[i].m;
                            var dn = pastilles[i + 1].n - pastilles[i].n;
                            centerDistance += (pastilles[i].Center - pastilles[i + 1].Center).LengthFast / Math.Sqrt(dm*dm+dn*dn);
                        }
                        centerDistance /= pastilles.Count - 1;
                        
                        Pastille[,] pastilleMatrix = new Pastille[4,4];
                        

                        //http://www.wolframalpha.com/input/?i=x%3Dx1%2Bk1*u1,x%3Dx2%2Bk2*u2,y%3Dy1%2Bk1*v1,y%3Dy2%2Bk2*v2+for+x,y

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
                                    foreach(var p in pastilles)
                                    {
                                        pastille.Center += p.Center + (float)centerDistance * ((m - p.m) * right + (n - p.n) * bottom);
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
                                var rectMat = processsImageChannels.GetSubRect(rectangle);

                                 var color = CvInvoke.Mean(rectMat);

                                pastille.MeanColor.MCvScalar = color;

                                var minDist = float.MaxValue;
                                foreach(var elt in Colors)
                                {
                                    var dist = (elt.Value - new Vector2((float)color.V0, (float)color.V1)).Length;

                                    if (dist < minDist)
                                    {
                                        minDist = dist;
                                        pastille.Color = elt.Key;
                                    }
                                }

                            }
                        }


                        //    inImage.Draw(pastille.X + ";" + pastille.Y, new Point((int)lstSquares[i].Center.X - 10, (int)lstSquares[i].Center.Y + 10), FontFace.HersheyComplex, 0.5, new Bgr(Color.Aquamarine), 1);

                        // int n = 0;

                        h.Add(pastilleMatrix[0, 0].MeanColor.Hue);
                        s.Add(pastilleMatrix[0, 0].MeanColor.Satuation);

                        lblH.Text = h.Average().ToString();
                        lblS.Text = s.Average().ToString();

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


                            var l = 20;
                            var rect = new Rectangle(pastille.m * l + l, pastille.n * l + l, l/2,l/2);
                            inImage.Draw(rect, new Bgr(pastille.Color), l/2, LineType.AntiAlias);

                  

                            // n++;
                            //  inImage.Draw(n.ToString(), new Point(quadri.Points.Sum((x) => x.X) / 4 - 10, quadri.Points.Sum((x) => x.Y) / 4 + 10), FontFace.HersheyComplex, 0.5, new Bgr(Color.Aquamarine), 1);


                        }
                        inImage.Draw(new CircleF(new PointF(origine.X, origine.Y), 5), new Bgr(Color.Purple), 10);


                        //var angles = lstParallelogram.SelectMany((x) => x.Angles).ToList();


                        //angles.Sort();


                        //var derivateAngle = new List<double>(angles.Count-1);

                        //for (int a=0;a<angles.Count-1;a++)
                        //{
                        //    derivateAngle.Add(angles[a + 1] - angles[a]);
                        //}

                        //derivateAngle.Sort();

                        //lblNbFace.Text = derivateAngle.Count((x) => x > 0.5).ToString();
                    }
                    else
                    {
                        h.Clear();
                        s.Clear();
                    }

                }

                // CvInvoke.inra

                // CvInvoke.CvtColor(inImage, outImage, ColorConversion.Rgb2Hsv);

                //var hue = outImage.Split()[0];

                //CvInvoke.Laplacian(inImage, outImage, DepthType.Default, 3);

                // CvInvoke.Erode(inImage, outImage, null, new Point(-1, -1), 3, BorderType.Default, new MCvScalar(1));


                viewer.Image = inImage;
            });




            /*
            Mat outImage = new Mat();

            CvInvoke.MedianBlur(inImage, outImage, 5);

            Mat canny = new Mat();

            CvInvoke.Canny(outImage, canny, barCanny1.Value, barCanny2.Value);


            var contours = new VectorOfVectorOfPoint();
            var hierarchy = new Mat();

            CvInvoke.FindContours(canny, contours, hierarchy, RetrType.List, ChainApproxMethod.ChainApproxNone);


            CvInvoke.DrawContours(outImage, contours, -1, new MCvScalar(0, 0, 255), 1, LineType.FourConnected, hierarchy);


            var area = new List<double>();

            for(int i=0;i<contours.Size;i++)
            {
                var contour = contours[i];
                
                area.Add( CvInvoke.ContourArea(contour));
            }

            area.Sort();

            richTextBox1.Text = String.Join("\r\n", area.Select((x) => x.ToString()).ToArray());



            viewer.Image = outImage;
            */


            return;



            StringBuilder msgBuilder = new StringBuilder("Performance: ");

            //Load the image from file and resize it for display
            Image<Bgr, Byte> img =
               new Image<Bgr, byte>(@"PhotosCube\cube3.jpg");

            //Convert the image to grayscale and filter out the noise
            UMat uimage = new UMat();
            CvInvoke.CvtColor(img, uimage, ColorConversion.Bgr2Gray);

            //use image pyr to remove noise
            //  UMat pyrDown = new UMat();
            //CvInvoke.PyrDown(uimage, pyrDown);
            //CvInvoke.PyrUp(pyrDown, uimage);

            //Image<Gray, Byte> gray = img.Convert<Gray, Byte>().PyrDown().PyrUp();

            //#region circle detection
            Stopwatch watch = Stopwatch.StartNew();
            double cannyThreshold = 30;
            /*
    double circleAccumulatorThreshold = 100;
        CircleF[] circles = CvInvoke.HoughCircles(uimage, HoughType.Gradient, 2.0, 20.0, cannyThreshold, circleAccumulatorThreshold, 5);

        watch.Stop();
        msgBuilder.Append(String.Format("Hough circles - {0} ms; ", watch.ElapsedMilliseconds));
        #endregion
        */
            #region Canny and edge detection
            watch.Reset(); watch.Start();
            double cannyThresholdLinking = 100;
            UMat cannyEdges = new UMat();
            CvInvoke.Canny(uimage, cannyEdges, cannyThreshold, cannyThresholdLinking);

            LineSegment2D[] lines = CvInvoke.HoughLinesP(
               cannyEdges,
               1, //Distance resolution in pixel-related units
               Math.PI / 45.0, //Angle resolution measured in radians.
               20, //threshold
               30, //min Line width
               10); //gap between lines

            watch.Stop();
            msgBuilder.Append(String.Format("Canny & Hough lines - {0} ms; ", watch.ElapsedMilliseconds));
            #endregion

            #region Find triangles and rectangles
            watch.Reset(); watch.Start();
            List<Triangle2DF> triangleList = new List<Triangle2DF>();
            List<RotatedRect> boxList = new List<RotatedRect>(); //a box is a rotated rectangle

            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(cannyEdges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

                CvInvoke.DrawContours(img, contours, -1, new MCvScalar(0, 0, 255));

                int count = contours.Size;
                for (int i = 0; i < count; i++)
                {
                    using (VectorOfPoint contour = contours[i])
                    using (VectorOfPoint approxContour = new VectorOfPoint())
                    {
                        CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);

                        var a = new VectorOfVectorOfPoint();



                        // img.Draw(approxContour, new Bgr(Color.DarkOrange), 2);


                        CvInvoke.DrawContours(img, approxContour, 0, new MCvScalar(0, 255, 0));

                        if (CvInvoke.ContourArea(approxContour, false) > 2) //only consider contours with area greater than 250
                        {
                            if (approxContour.Size == 3) //The contour has 3 vertices, it is a triangle
                            {
                                Point[] pts = approxContour.ToArray();
                                triangleList.Add(new Triangle2DF(
                                   pts[0],
                                   pts[1],
                                   pts[2]
                                   ));
                            }
                            else if (approxContour.Size == 4) //The contour has 4 vertices.
                            {
                                /*
                                #region determine if all the angles in the contour are within [80, 100] degree
                                bool isRectangle = true;
                                Point[] pts = approxContour.ToArray();
                                LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

                                for (int j = 0; j < edges.Length; j++)
                                {
                                    double angle = Math.Abs(
                                       edges[(j + 1) % edges.Length].GetExteriorAngleDegree(edges[j]));
                                    if (angle < 80 || angle > 100)
                                    {
                                        isRectangle = false;
                                        break;
                                    }
                                }
                                #endregion
                               
                                if (isRectangle) */// boxList.Add(CvInvoke.MinAreaRect(approxContour));
                            }
                        }
                    }
                }
            }

            watch.Stop();
            msgBuilder.Append(String.Format("Triangles & Rectangles - {0} ms; ", watch.ElapsedMilliseconds));
            #endregion

            this.Text = msgBuilder.ToString();

            #region draw triangles and rectangles
            //Image<Bgr, Byte> triangleRectangleImage = img.CopyBlank();
            //foreach (Triangle2DF triangle in triangleList)
            //img.Draw(triangle, new Bgr(Color.DarkBlue), 2);
            foreach (RotatedRect box in boxList)
                img.Draw(box, new Bgr(Color.DarkOrange), 2);
            #endregion
            /*
            #region draw circles
           // Image<Bgr, Byte> circleImage = img.CopyBlank();
           // foreach (CircleF circle in circles)
            //    img.Draw(circle, new Bgr(Color.Brown), 2);
            #endregion


            #region draw lines
            //Image<Bgr, Byte> lineImage = img.CopyBlank();
            foreach (LineSegment2D line in lines)
                img.Draw(line, new Bgr(Color.Green), 2);
            #endregion
            */

            viewer.Image = img;

        }


        /*

    private static int thresh = 50;

    private static int N = 11;

    // returns sequence of squares detected on the image.
    // the sequence is stored in the specified memory storage
    static void findSquares( Mat image,  VectorOfVectorOfPoint squares )
    {
        squares.Clear();

        var pyr = new Mat();
        var timg = new Mat();

        var gray0 = new Mat(); (image.Size, CV_8U),
        
        var gray = new Mat();

        // down-scale and upscale the image to filter out the noise
        CvInvoke.PyrDown(image, pyr);
        CvInvoke.PyrUp(image, timg);

        Mat contours = new Mat();

        // find squares in every color plane of the image
        for (int c = 0; c < 3; c++)
        {
            int[] ch = new int[] { c, 0 };

            CvInvoke.MixChannels(timg, gray0, ch);

            //mixChannels(&timg, 1, &gray0, 1, ch, 1);

            // try several threshold levels
            for (int l = 0; l < N; l++)
            {
                // hack: use Canny instead of zero threshold level.
                // Canny helps to catch squares with gradient shading
                if (l == 0)
                {
                    // apply Canny. Take the upper threshold from slider
                    // and set the lower to 0 (which forces edges merging)
                   CvInvoke.Canny(gray0, gray, 0, thresh, 5);
                        // dilate canny output to remove potential
                        // holes between edge segments
                        var elt = new Mat();
                        CvInvoke.Dilate(gray, gray, elt, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(1));
                }
                else
                {
                    // apply threshold if l!=0:
                    //     tgray(x,y) = gray(x,y) < (l+1)*255/N ? 255 : 0
                    gray = gray0 >= (l + 1) * 255 / N;
                }

                    Mat hier = new Mat();
                // find contours and store them all as a list
             CvInvoke.FindContours(gray, contours, hier, RetrType.List, ChainApproxMethod.ChainApproxSimple);

               VectorOfPoint approx;

                // test each contour
                for (int i = 0; i < contours.Size; i++)
                {
                    // approximate contour with accuracy proportional
                    // to the contour perimeter
                    approxPolyDP(Mat(contours[i]), approx, arcLength(Mat(contours[i]), true) * 0.02, true);

                    // square contours should have 4 vertices after approximation
                    // relatively large area (to filter out noisy contours)
                    // and be convex.
                    // Note: absolute value of an area is used because
                    // area may be positive or negative - in accordance with the
                    // contour orientation
                    if (approx.size() == 4 &&
                        fabs(contourArea(Mat(approx))) > 1000 &&
                        isContourConvex(Mat(approx)))
                    {
                        double maxCosine = 0;

                        for (int j = 2; j < 5; j++)
                        {
                            // find the maximum cosine of the angle between joint edges
                            double cosine = fabs(angle(approx[j % 4], approx[j - 2], approx[j - 1]));
                            maxCosine = MAX(maxCosine, cosine);
                        }

                        // if cosines of all angles are small
                        // (all angles are ~90 degree) then write quandrange
                        // vertices to resultant sequence
                        if (maxCosine < 0.3)
                            squares.push_back(approx);
                    }
                }
            }
        }
    }/**/
    }

}
