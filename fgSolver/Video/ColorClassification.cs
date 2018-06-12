using RevengeCube;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fgSolver
{
    public sealed class ColorClassification
    {
        public class ClassifiedPoint
        {
            public ClassificationGroup Group;

            public Color MeasuredColor;

            public ClassifiedPoint(Color measuredColor)
            {
                this.MeasuredColor = measuredColor;
            }

            public Dictionary<ClassifiedPoint, int> Distance = new Dictionary<ClassifiedPoint, int>(95);


        }

        public class ClassificationGroup
        {
            public int ID;

            public ClassificationGroup(int index)
            {
                ID = index;
            }

            public List<ClassifiedPoint> Points = new List<ClassifiedPoint>(8);

            public Dictionary<ClassifiedPoint, double> Distance = new Dictionary<ClassifiedPoint, double>(95);

            public void Add(ClassifiedPoint point)
            {
                Points.Add(point);
                point.Group = this;
                Distance.Remove(point);

                foreach (var itm in point.Distance)
                {
                    if (itm.Key.Group == null && !Points.Contains(itm.Key))
                    {
                        double pointDist;

                        if (!Distance.TryGetValue(itm.Key, out pointDist) || pointDist > itm.Value)
                        {
                            Distance[itm.Key] = itm.Value; // le point le plus proche du goupe est le point le plus proche parmis toutes les distances des points du groupe aux autres points
                        }
                    }
                }
            }

            public void AddNearest()
            {
                ClassifiedPoint minPoint = Distance.First().Key;
                double minDist = Distance.First().Value;
                foreach (var item in Distance)
                {
                    if (item.Value < minDist)
                    {
                        minPoint = item.Key;
                        minDist = item.Value;
                    }
                }

                Add(minPoint);
            }

            public Color AverageColor;

            public void UpdateAverageColor()
            {
                AverageColor= Color.FromArgb((int)Points.Average((x) => x.MeasuredColor.R), (int)Points.Average((x) => x.MeasuredColor.G), (int)Points.Average((x) => x.MeasuredColor.B));
            }

            // courleur théorique (bleu, vert, jaune, rouge, ...)
            public Color TheoreticalColor;
        }



        public static ColorCube GetCube(IList<Color> colors, IList<ColorAssociation> colorsAssociation)
        {
            var result = Classify(colors);

            result.Groups.ForEach((x) => x.UpdateAverageColor());


            var orderedRGroups = result.Groups.OrderBy((x) => x.AverageColor.R).ToList();

            if (orderedRGroups[0].AverageColor.B < orderedRGroups[1].AverageColor.B)
            {
                orderedRGroups[0].TheoreticalColor = Color.Green;
                orderedRGroups[1].TheoreticalColor = Color.Blue;
            }
            else
            {
                orderedRGroups[0].TheoreticalColor = Color.Blue;
                orderedRGroups[1].TheoreticalColor = Color.Green;
            }

            orderedRGroups[5].TheoreticalColor = Color.Orange;

            var orderedGGroups = orderedRGroups.Skip(2).Take(3).OrderBy((x) => x.AverageColor.G).ToList();

            orderedGGroups[0].TheoreticalColor = Color.Red;

            
            if(orderedGGroups[1].AverageColor.B < orderedGGroups[2].AverageColor.B)
            {
                orderedGGroups[1].TheoreticalColor = Color.Yellow;
                orderedGGroups[2].TheoreticalColor = Color.White;
            }
            else
            {
                orderedGGroups[1].TheoreticalColor = Color.White;
                orderedGGroups[2].TheoreticalColor = Color.Yellow;
            }

            var cube = new ColorCube();

            Console.WriteLine("R;G;B;ID;Color");
            result.Points.ForEach((x) => Console.WriteLine(x.MeasuredColor.R + ";" + x.MeasuredColor.G + ";" + x.MeasuredColor.B + ";" + x.Group.ID + ";" + x.Group.TheoreticalColor.Name));

            cube.setColors(result.Points.Select((x) => x.Group.TheoreticalColor).ToArray());

            return cube;
        }

        static void Perm<T>(T[] arr, int k)
        {
            if (k >= arr.Length)
            {
               // Print(arr);
            }
            else
            {
                Perm(arr, k + 1);
                for (int i = k + 1; i < arr.Length; i++)
                {
                    Swap(ref arr[k], ref arr[i]);
                    Perm(arr, k + 1);
                    Swap(ref arr[k], ref arr[i]);
                }
            }
        }

        private static void Swap<T>(ref T item1, ref T item2)
        {
            T temp = item1;
            item1 = item2;
            item2 = temp;
        }

        public class ClassificationResult
        {
            public List<ClassifiedPoint> Points;
            public List<ClassificationGroup> Groups;
        }

        public class Distance
        {
            public ClassifiedPoint Point1;
            public ClassifiedPoint Point2;
            public int PointDistance;

            public Distance(ClassifiedPoint p1, ClassifiedPoint p2, int d)
            {
                Point1 = p1;
                Point2 = p2;
                PointDistance = d;
            }
        }

        public static ClassificationResult Classify(IList<Color> colors)
        {
            if (colors == null || colors.Count() != 96) throw new Exception("Impossible d'obtenir le cube car une quantité différente de 96 couleurs a été transmise au classifier");

            var groups = new List<ClassificationGroup>(6);


            var points = new List<ClassifiedPoint>(96);

            for (int i = 0; i < 96; i++)
            {
                points.Add(new ClassifiedPoint(colors[i]));
            }

            //  distances entre points deux à deux trier par ordre croissant
            var distances = new List<Distance>();

            for (int i = 0; i < 96; i++)
            {
                for (int j = i + 1; j < 96; j++)
                {
                    var dx = (points[i].MeasuredColor.R - points[j].MeasuredColor.R);
                    var dy = (points[i].MeasuredColor.G - points[j].MeasuredColor.G);
                    var dz = (points[i].MeasuredColor.B - points[j].MeasuredColor.B);
                    var d = dx * dx + dy * dy + dz * dz;

                    points[i].Distance[points[j]] = d;
                    points[j].Distance[points[i]] = d;

                    distances.Add(new Distance(points[i], points[j], d));
                }
            }

            distances.Sort((x, y) => x.PointDistance - y.PointDistance);

            for (int igGrp = 0; igGrp < 6; igGrp++)
            {
                // trouver la paire de point la plus proche qui n'appartient pas encore à un groupe
                var nearest = distances.FirstOrDefault((x) => x.Point1.Group == null && x.Point2.Group == null);

                var group = new ClassificationGroup(igGrp);

                // les ajouter au groupe
                group.Add(nearest.Point1);
                group.Add(nearest.Point2);

                // ajouter les 14 plus proches
                for (int i = 0; i < 14; i++)
                {
                    group.AddNearest();
                }

                groups.Add(group);
            }

            var res = new ClassificationResult();
            res.Groups = groups;
            res.Points = points;

            return res;
        }
    }
}
