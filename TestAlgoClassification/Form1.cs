﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestAlgoClassification
{
    //    public class Distance
    //    {
    //        public int[] IndexPoint = new int[2] { -1, -1 };
    //        public double DistanceValue;
    //    }

    public class ClassifiedPoint
    {
        public ClassificationGroup Group;

        public double R, G, B;

        public ClassifiedPoint(double r, double g, double b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public Dictionary<ClassifiedPoint, double> Distance = new Dictionary<ClassifiedPoint, double>(95);


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
                if (itm.Key.Group == null && !Points.Contains(itm.Key)) {
                    double pointDist;

                    if (!Distance.TryGetValue(itm.Key, out pointDist) || pointDist>itm.Value)
                    {
                        Distance[itm.Key] = itm.Value; // le point le plus proche du goupe est le point le plus proche parmis toutes les distances des points du groupe aux autres points
                    }
                }
            }
        }

        public void AddNearest()
        {
            ClassifiedPoint minPoint= Distance.First().Key;
            double minDist = Distance.First().Value ;
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
    }

    public partial class Form1 : Form
    {
        public  double[,] values = new double[,] {{67.6555,94.036,184.72},{68.612625,85.903375,186.0205},{119.463875,124.015375,196.165875},{75.129125,105.091375,177.480625},{62.767625,85.488375,188.25575},{18.836875,200.758125,167.124},{38.531125,210.260625,142.368625},{18.457875,212.21875,155.713375},{120.883125,115.066,186.1395},{18.9265,229.856625,165.827625},{18.04725,235.392,159.768625},{117.789,154.132625,190.609},{18.837125,237.28875,170.4645},{38.623125,246.51,139.5505},{117.559625,162.64225,187.7825},{123.58675,159.23525,107.95125},{21.33868571,59.09496963,191.2698007},{75.81204874,101.0266195,183.8811141},{121.3134585,95.22979442,132.533495},{122.6872668,90.2342273,133.9720847},{69.46026835,90.24052729,190.8940001},{71.83925533,95.54668293,189.6517604},{76.05742552,102.8910826,187.8970675},{120.2632946,104.5695449,132.601569},{16.43135913,190.4781727,163.5154658},{74.21018043,119.1687228,184.896721},{117.6116044,144.0466631,202.2183509},{24.92287994,39.95362058,172.425295},{116.7435613,156.3017239,193.5201442},{17.08300604,213.0021571,155.7948932},{41.09286021,199.7342867,142.5336815},{20.94993473,52.69205442,170.8986332},{82.02093742,121.8514569,178.8743149},{113.7378317,167.0690447,202.1826857},{14.95063106,112.2046273,136.9777294},{15.13507502,119.9857519,136.3168077},{118.8916289,119.0380829,201.6797789},{119.2938836,123.3711403,205.7906851},{32.27938233,22.16335951,172.7160448},{40.92189765,131.1712372,147.9401396},{128.8501086,80.7144284,128.2337543},{19.02201344,39.9727346,180.7176859},{30.44778972,20.24439282,166.4566715},{115.4722525,166.1405129,202.6924271},{116.4773058,158.2881237,202.3343682},{124.5933383,112.431841,123.0979483},{16.39123993,187.1034154,151.1708136},{42.91020951,186.5306331,136.681041},{15.11366213,148.2142857,165.1845238},{15.02409297,146.4095805,150.6439909},{48.23044218,16.02947846,163.5697279},{115.5677438,138.7202381,135.5393991},{20.92460317,55.30725624,187.4401927},{118.2477324,125.4869615,198.5626417},{78.75113379,106.7324263,184.4365079},{41.54676871,127.204932,147.5498866},{73.61763039,112.832483,186.6643991},{40.00907029,174.2602041,143.9274376},{125.7261905,105.6845238,129.2636054},{15.25963719,151.8982426,146.1519274},{74.58560091,126.8732993,184.6587302},{118.7108844,135.4764739,117.5238095},{20.40816327,50.48526077,175.2848639},{22.99659864,41.62386621,167},{41.98198598,130.4095602,146.2365069},{40.53311681,14.68268041,164.9809564},{52.70398638,117.5228606,140.3933156},{114.1139946,169.061538,204.9628358},{38.95589794,150.4441916,151.671828},{129.323458,77.23343276,128.5124843},{125.1822465,105.5033596,133.3160002},{126.9809776,87.32547358,133.570491},{38.9994917,169.0948559,151.8312754},{16.85103509,179.8315172,164.1397785},{39.93331741,155.8519924,148.8966777},{15.59721512,174.2404446,148.467587},{123.4745177,137.3280997,118.5446392},{74.29431613,125.3108687,185.1450503},{126.0285602,116.7693217,121.7898041},{43.15660781,192.3773757,139.9004123},{17.72540744,136.605256,148.8819662},{81.3394091,129.5765467,186.890542},{56.18405868,18.52676268,171.0409063},{53.98183318,19.29124182,169.5049979},{41.48308558,156.5918718,152.641577},{125.5346436,80.56576464,136.4345706},{33.35253946,31.84460606,176.7084031},{35.57263179,34.08028781,176.5290288},{24.79679974,51.56327251,193.0783318},{117.9336409,136.379856,212.8644241},{43.17985128,161.1468809,145.9294317},{118.3273418,129.7251598,129.7318386},{118.0666146,140.8860485,216.5679264},{75.57478605,117.0448147,190.2497106},{115.7259063,164.7346891,207.0132423},{43.11218712,191.2995637,141.7111532}};

        public List<ClassificationGroup> groups = new List<ClassificationGroup>(6);

        public Form1()
        {
            InitializeComponent();

            var points = new List<ClassifiedPoint>(96);

            for (int i = 0; i < 96; i++)
            {
                points.Add(new ClassifiedPoint(values[i, 0], values[i, 1], values[i, 2]));
            }

            //  distances entre points deux à deux trier par ordre croissant
            var distances = new SortedList<double, Tuple<ClassifiedPoint, ClassifiedPoint>>();

            for (int i = 0; i < 96; i++)
            {
                for (int j = i + 1; j < 96; j++)
                {
                    var dx = (points[i].R - points[j].R);
                    var dy = (points[i].G - points[j].G);
                    var dz = (points[i].B - points[j].B);
                    var d = dx * dx + dy * dy + dz * dz;

                    points[i].Distance[points[j]] = d;
                    points[j].Distance[points[i]] = d;

                    distances.Add(d, new Tuple<ClassifiedPoint, ClassifiedPoint>(points[i], points[j]));
                }
            }

            for (int igGrp = 0; igGrp < 6; igGrp++)
            {
                // trouver la paire de point la plus proche qui n'appartient pas encore à un groupe
                var nearest = distances.FirstOrDefault((x) => x.Value.Item1.Group == null && x.Value.Item2.Group == null);

                var group = new ClassificationGroup(igGrp);

                // les ajouter au groupe
                group.Add(nearest.Value.Item1);
                group.Add(nearest.Value.Item2);

                // ajouter les 14 plus proches
                for(int i = 0; i < 14; i++)
                {
                    group.AddNearest();
                }

                groups.Add(group);
            }

            Console.WriteLine("R;G;B;group");

            foreach (var point in groups.SelectMany((x) => x.Points))
            {
                Console.Write(point.R);
                Console.Write(";");
                Console.Write(point.G);
                Console.Write(";");
                Console.Write(point.B);
                Console.Write(";");
                Console.Write(point.Group.ID);
                Console.WriteLine();
            }

            //    var nbSample = values.GetLength(0);



            //    distances = new Distance[nbSample, nbSample];
            //    valuesGroup = new int[nbSample];
            //    groupValue = new int[6, 16];

            //    for (int f = 0; f < groupValue.Length; f++)
            //    {
            //        for(int p = 0; p < 16; p++)
            //        {
            //            groupValue[f,p] = -1;
            //        }
            //    }

            //    double minDist = double.PositiveInfinity;
            //    int minDistI = -1;
            //    int minDistJ = -1;

            //    for (int i = 0; i < nbSample; i++)
            //    {
            //        valuesGroup[i] = -1;

            //        distances[i, i].IndexPoint[0] = i;
            //        distances[i, i].IndexPoint[1] = i;
            //        distances[i, i].DistanceValue = 0;

            //        for (int j = i + 1; j < nbSample; j++)
            //        {
            //            var dx = (values[i, 0] - values[j, 0]);
            //            var dy = (values[i, 1] - values[j, 1]);
            //            var dz = (values[i, 2] - values[j, 2]);
            //            var d = dx * dx + dy * dy + dz * dz;
            //            distances[i, j].IndexPoint[0] = i;
            //            distances[i, j].IndexPoint[1] = j;
            //            distances[i, j].DistanceValue = d;

            //            distances[j, i].IndexPoint[0] = j;
            //            distances[j, i].IndexPoint[1] = i;
            //            distances[j, i].DistanceValue = d;

            //            if (d < minDist)
            //            {
            //                minDist = d;
            //                minDistI = i;
            //                minDistJ = j;
            //            }
            //        }
            //    }

            //    // mettre deux point dans le groupe 0
            //    valuesGroup[minDistI] = 0;
            //    valuesGroup[minDistJ] = 0;
            //    groupValue[0, 0] = minDistI;
            //    groupValue[0, 1] = minDistJ;

            //    //rechercher le plus proche d'un des deux
            //    for (int i = 0; i < nbSample; i++)
            //    {

            //    }

        }




        //public Distance[,] distances;

        //public int[] valuesGroup; // i : index (0-95), value : Index group (0-5)

        //public int[,] groupValue; // int [6,16]
    }
}
