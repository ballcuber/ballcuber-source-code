using Emgu.CV.Structure;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fgSolver
{
    public class Pastille
    {
        public Quadrilateral Quadrilateral;
        public int m;
        public int n;

        public Hsv MeanColorHSV;

        public Bgr MeanColorBGR;

        public Vector2 Center;

        public Color Color;
    }

    public class Quadrilateral
    {
        // indique que c'est un losange
        public bool IsParallelogram { get; } = false;

        // indique que c'est un losange
        public bool IsSquare { get; } = false;

        public Point[] Points { get; }

        public Vector2 Center { get; }

        public Vector2[] Vectors { get; }

        public double[] Angles { get; }

        public float[] Lengths { get; }

        public float MaxLength { get; }

        public float AverageLength { get; }

        // public Vector2 Right { get; }

        public Vector2 Bottom { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points">les points du contour</param>
        /// <param name="percentLengthTolerance">Entre 0 et 1 (0% et 100%) : tolérance en % sur la longueur des côtés du losange</param>
        public Quadrilateral(Point[] points, double percentLengthTolerance, double percentAngleTolerance)
        {
            // ce n'est pas un losange
            if (points == null || points.Length != 4) return;

            this.Points = points;

            this.Vectors = new Vector2[points.Length];

            // coordonnées des vecteurs : Point d'arrivée - Point de départ
            for (int i = 0; i < 4; i++)
            {
                var i_1 = i == 3 ? 0 : i + 1;
                this.Vectors[i].X = points[i_1].X - points[i].X;
                this.Vectors[i].Y = points[i_1].Y - points[i].Y;

            }

            // magnitude des vecteurs calculés une fois pour toute
            Lengths = new float[4];
            for(int i = 0; i < 4; i++)
            {
                Lengths[i] = Vectors[i].LengthFast;

                if (Lengths[i] == 0) return; // on ne tolère pas les longueurs nulles
            }

            // tolérance sur les côtés opposés égaux
            for (int i = 0; i < 2; i++)
            {
                if (Math.Abs(1 - Lengths[i] / Lengths[i + 2]) > percentLengthTolerance) return;
            }

            Angles = new double[4];

            // calculs des angles
            for (int i = 0; i < 4; i++)
            {
                var i_1 = i == 0 ? 3 : i - 1;
                Angles[i] = Math.Atan2(Vectors[i_1].Y * Vectors[i].X - Vectors[i].Y * Vectors[i_1].X, -Vectors[i].X * Vectors[i_1].X -Vectors[i].Y * Vectors[i_1].Y);
            }

            // tolérance sur les angles opposés égaux
            for (int i = 0; i < 2; i++)
            {
                if (Math.Abs(1 - Angles[i] / Angles[i + 2]) > percentAngleTolerance) return;
            }

            Center = new Vector2((float)Points.Average((x) => x.X), (float)points.Average((x) => x.Y));

            MaxLength = Lengths.Max();
            AverageLength = Lengths.Average();

            var vect1 = (Vectors[0] - Vectors[2]).Normalized();
            var vect2 = (Vectors[1] - Vectors[3]).Normalized();

            if (Math.Abs(vect1.X) > Math.Abs(vect2.X))
            {
                //Right = new Vector2(Math.Abs(vect1.X), vect1.Y);
                Bottom = new Vector2(vect2.X, Math.Abs(vect2.Y));
            }
            else
            {
                //Right = new Vector2(Math.Abs(vect2.X), vect2.Y);
                Bottom = new Vector2(vect1.X, Math.Abs(vect1.Y));
            }


            IsParallelogram = true;


            // tolérance sur les côtés adjacents
            for (int i = 0; i < 3; i+=2)
            {
                if (Math.Abs(1 - Lengths[i] / Lengths[i + 1]) > percentLengthTolerance) return;
            }


            // tolérance sur les angles opposés égaux
            for (int i = 0; i < 3; i+=2)
            {
                if (Math.Abs(1 - Angles[i] / Angles[i + 1]) > percentAngleTolerance) return;
            }

            IsSquare = true;
        }

    }

    public class QuadrilateralCenterComparer: IEqualityComparer<Quadrilateral>
    {
        private double _percentTolerance;

        // poucentage de tolérance pour considérer que 2 quadri sont aux même endroit
        public QuadrilateralCenterComparer(double percentTolerance)
        {
            _percentTolerance = percentTolerance;
        }

        public bool Equals(Quadrilateral x, Quadrilateral y)
        {
            var distance = (x.Center - y.Center).Length;

            return distance < _percentTolerance * x.MaxLength && distance < _percentTolerance * y.MaxLength;
        }

        public int GetHashCode(Quadrilateral obj)
        {
            return 0;
        }
    }
}
