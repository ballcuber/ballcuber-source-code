using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RevengeCube
{

    public class ColorCube : ICloneable
    {

        /*                         U
 *                        -------------------
 *                       |  0 |  1 |  2 |  3 |
 *                       ---------------------
 *                       |  4 |  5 |  6 |  7 |
 *                       ---------------------
 *                       |  8 |  9 | 10 | 11 |
 *                       ---------------------
 *                       | 12 | 13 | 14 | 15 |
 *           L            -------------------            R                     B
 *  -------------------   -------------------   -------------------   -------------------
 * | 16 | 17 | 18 | 19 | | 32 | 33 | 34 | 35 | | 48 | 49 | 50 | 51 | | 64 | 65 | 66 | 67 |
 * --------------------- --------------------- --------------------- ---------------------
 * | 20 | 21 | 22 | 23 | | 36 | 37 | 38 | 39 | | 52 | 53 | 54 | 55 | | 68 | 69 | 70 | 71 |
 * --------------------- ----------F---------- --------------------- ---------------------
 * | 24 | 25 | 26 | 27 | | 40 | 41 | 42 | 43 | | 56 | 57 | 58 | 59 | | 72 | 73 | 74 | 75 |
 * --------------------- --------------------- --------------------- ---------------------
 * | 28 | 29 | 30 | 31 | | 44 | 45 | 46 | 47 | | 60 | 61 | 62 | 63 | | 76 | 77 | 78 | 79 |
 *  -------------------   -------------------   -------------------   -------------------
 *                        -------------------
 *                       | 80 | 81 | 82 | 83 |
 *                       ---------------------
 *                       | 84 | 85 | 86 | 87 |
 *                       ---------------------
 *                       | 88 | 89 | 90 | 91 |
 *                       ---------------------
 *                       | 92 | 93 | 94 | 95 |
 *                        -------------------
 *                                 D
 */


        public static Color[] _order = { Color.White, Color.Red, Color.Blue, Color.Orange, Color.Green, Color.Yellow , Color.Transparent};

        private Color[] _colors = new Color[6 * 16];
        public static Dictionary<Faces, Color> colorDictionary = new Dictionary<Faces, Color>()
        {
            {Faces.U, Color.White },
            {Faces.L, Color.Red },
            {Faces.F, Color.Blue },
            {Faces.R, Color.Orange },
            {Faces.B, Color.Green },
            {Faces.D, Color.Yellow },
            {Faces.UNKNOWN, Color.Transparent }
        };
        private static int[,] EDGE_LOCATIONS = {{ 1, 66 }, { 65, 2 }, { 17, 4 }, { 7, 50 }, { 8, 18 }, { 49, 11 }, { 33, 13 }, { 14, 34 },
            { 71, 20 }, { 55, 68 }, { 23, 36 }, { 39, 52 }, { 40, 27 }, { 56, 43 }, { 24, 75 }, { 72, 59 },
            { 81, 45 }, { 46, 82 }, { 30, 84 }, { 87, 61 }, { 88, 29 }, { 62, 91 }, { 78, 93 }, { 94, 77 }
        };
        private static int[,] CORNER_LOCATIONS = { { 0, 16, 67 },  { 15, 48, 35 }, { 3,  64, 51 }, { 12,  32, 19 },
            { 80, 31, 44 }, { 95, 63, 76 }, { 83, 47, 60 }, { 92, 79, 28 }
        };
        private static int[] CENTER_LOCATIONS = { 53, 54, 57, 58,
            21, 22, 25, 26,
            5,  6,  9,  10,
            85, 86, 89, 90,
            37, 38, 41, 42,
            69, 70, 73, 74
        };

        public DateTime CreationDate {get;}

		public ColorCube ()
		{
            // par défaut, le cube est entier
            for(int c = 0; c < 6; c++)
            {
                for(int i = 0; i < 16; i++)
                {
                    _colors[c * 16 + i] = _order[c];
                }
            }

            CreationDate = DateTime.Now;

        }

        private ColorCube(DateTime creationDate)
        {
            this.CreationDate = creationDate;
        }

        public bool IsSolved
        {
            get
            {
                var solvedCube =  new ColorCube();
                for(int i=0;i< _colors.Length; i++)
                {
                    if (_colors[i] != solvedCube.colors[i]) return false;
                }
                return true;
            }
        }

        public void setColors(Color[] colors)
        {
            if (colors == null) return;

            for (int i = 0; i < Math.Min(colors.Length, this._colors.Length); i++)
            {
                this._colors[i] = colors[i];
            }
        }



        public void setColors(Cube cube){
			setColors (cube.EdgePosition, cube.CornerPosition, cube.CornerOrientation, cube.CenterPosition);
		}

		public void setColors (int[] edges, int[] corners, int[] orientations, int[] centers)
		{
			
			for (int i = 0; i < _colors.Length; i++) {
				_colors [i] = _order [i / 16];
			}
			for (int i = 0; i < 24; i++) {
				_colors [EDGE_LOCATIONS [i, 0]] = colorDictionary [Edge.order [edges [i]].face1];
				_colors [EDGE_LOCATIONS [i, 1]] = colorDictionary [Edge.order [edges [i]].face2];
			}
			for (int i = 0; i < 8; i++) {
				int offset = orientations [i];
				_colors [CORNER_LOCATIONS [i, mod (0 + offset, 3)]] = colorDictionary [Corner.order [corners [i]].face1];
				_colors [CORNER_LOCATIONS [i, mod (1 + offset, 3)]] = colorDictionary [Corner.order [corners [i]].face2];
				_colors [CORNER_LOCATIONS [i, mod (2 + offset, 3)]] = colorDictionary [Corner.order [corners [i]].face3];
			}
			for (int i = 0; i < 24; i++) {
				_colors[CENTER_LOCATIONS[i]] = colorDictionary[(Faces)centers[i]];
			}

		}

		public Color[] colors {
			get { return _colors; }
		}

		private int mod (int k, int n)
		{
			return ((k %= n) < 0) ? k + n : k;
		}

        public ColorCube CloneCube()
        {
            var cube = new ColorCube(CreationDate);
            cube.setColors(_colors);
            return cube;
        }

        public object Clone()
        {
            return CloneCube();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ColorCube)) return false;

            return (ColorCube)obj == this;
        }

        public static bool operator ==(ColorCube obj1, ColorCube obj2)
        {
            if ((object)obj1 == null ^ (object)obj2 == null) return false;

            if ((object)obj1 == null && (object)obj2 == null) return true;

            if (obj1.colors.Length != obj2.colors.Length) return false;

            for (int i = 0; i < obj2.colors.Length; i++)
            {
                if (obj1.colors[i] != obj2.colors[i]) return false;
            }

            return obj1.CreationDate == obj2.CreationDate;
        }

        public static bool operator !=(ColorCube obj1, ColorCube obj2)
        {
            return !(obj1 == obj2);
        }

        public Faces[] FaceColors
        {
            get
            {
                var f = new Faces[_colors.Length];

                for(int i = 0; i < f.Length; i++)
                {
                    f[i] = colorDictionary.First(x => x.Value == _colors[i]).Key;
                }

                return f;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

