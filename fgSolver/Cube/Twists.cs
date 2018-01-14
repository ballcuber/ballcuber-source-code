using System;
using System.Linq;

namespace RevengeCube
{

	public enum Type
	{
		Corners,
		Edges,
		EdgePairs,
		Centers
	}

	/// <summary>
	/// A class that represents all twists of rubik's revenge cube. The class also implements 
	/// the logic to apply the twist to a permutation array representing the corner, edge and center pieces.  
	/// </summary>
	public class Twist
	{
		public static Twist U = new Twist ("U", new Edge[,] { { Edge.UF, Edge.UR, Edge.UB, Edge.UL },{ Edge.FU, Edge.RU, Edge.BU, Edge.LU }},new Corner[]{ Corner.ULF, Corner.URF, Corner.URB, Corner.ULB },new Center[,]{ { Center.U3, Center.U4, Center.U2, Center.U1 } });
		public static Twist D = new Twist ("D", new Edge[,] { { Edge.DL, Edge.DB, Edge.DR, Edge.DF },{ Edge.LD, Edge.BD, Edge.RD, Edge.FD }},new Corner[]{ Corner.DRF, Corner.DLF, Corner.DLB, Corner.DRB },new Center[,]{ { Center.D3, Center.D4, Center.D2, Center.D1 } });
		public static Twist F = new Twist ("F", new Edge[,] {{ Edge.FL, Edge.FD, Edge.FR, Edge.FU },{ Edge.LF, Edge.DF, Edge.RF, Edge.UF }}, new Corner[]{ Corner.DLF, Corner.DRF, Corner.URF, Corner.ULF }, new Center[,]{ { Center.F3, Center.F4, Center.F2, Center.F1 }} );
		public static Twist B = new Twist ("B", new Edge[,] {{ Edge.BU, Edge.BR, Edge.BD, Edge.BL },{ Edge.UB, Edge.RB, Edge.DB, Edge.LB }}, new Corner[]{ Corner.ULB, Corner.URB, Corner.DRB, Corner.DLB }, new Center[,]{ { Center.B3, Center.B4, Center.B2, Center.B1 } } );
		public static Twist R = new Twist ("R", new Edge[,] {{ Edge.RF, Edge.RD, Edge.RB, Edge.RU },{ Edge.FR, Edge.DR, Edge.BR, Edge.UR }}, new Corner[]{ Corner.DRF, Corner.DRB, Corner.URB, Corner.URF }, new Center[,]{ { Center.R3, Center.R4, Center.R2, Center.R1 } } );
		public static Twist L = new Twist ("L", new Edge[,] {{ Edge.LU, Edge.LB, Edge.LD, Edge.LF, }, { Edge.UL, Edge.BL, Edge.DL, Edge.FL }}, new Corner[]{ Corner.ULF, Corner.ULB, Corner.DLB, Corner.DLF }, new Center[,]{ { Center.L3, Center.L4, Center.L2, Center.L1 } } );

		public static Twist U2 = new Twist ("U2", U, 2);
		public static Twist U_ = new Twist ("U'", U, 3);
		public static Twist D2 = new Twist ("D2", D, 2);
		public static Twist D_ = new Twist ("D'", D, 3);
		public static Twist F2 = new Twist ("F2", F, 2);
		public static Twist F_ = new Twist ("F'", F, 3);
		public static Twist B2 = new Twist ("B2", B, 2);
		public static Twist B_ = new Twist ("B'", B, 3);
		public static Twist R2 = new Twist ("R2", R, 2);
		public static Twist R_ = new Twist ("R'", R, 3);
		public static Twist L2 = new Twist ("L2", L, 2);
		public static Twist L_ = new Twist ("L'", L, 3);

		public static Twist u = new Twist (name: "u", edgeCycles: new Edge[,]{ { Edge.LF, Edge.FR, Edge.RB, Edge.BL } },cornerCycle: new Corner[]{ }, centerCycles: new Center[,] {{ Center.F1, Center.R1, Center.B1, Center.L1 }, {Center.F2,Center.R2, Center.B2,Center.L2}});
		public static Twist d = new Twist (name: "d", edgeCycles: new Edge[,]{ { Edge.LB, Edge.BR, Edge.RF, Edge.FL } },cornerCycle: new Corner[]{ }, centerCycles: new Center[,] {{ Center.L3, Center.B3, Center.R3, Center.F3 }, {Center.L4,Center.B4,Center.R4,Center.F4}});
		public static Twist f = new Twist ("f", new Edge[,]{ { Edge.UL, Edge.LD, Edge.DR, Edge.RU } },new Corner[]{ }, new Center[,] {{ Center.U3, Center.L4, Center.D2, Center.R1 }, {Center.U4,Center.L2,Center.D1,Center.R3}});
		public static Twist b = new Twist ("b", new Edge[,]{ { Edge.UR, Edge.RD, Edge.DL, Edge.LU  } },new Corner[]{ }, new Center[,] {{ Center.L1, Center.U2, Center.R4, Center.D3 }, {Center.L3,Center.U1,Center.R2,Center.D4}});
		public static Twist r = new Twist ("r", new Edge[,]{ { Edge.UF, Edge.FD, Edge.DB, Edge.BU } },new Corner[]{ }, new Center[,] {{ Center.U2, Center.F2, Center.D2, Center.B3 }, {Center.U4,Center.F4,Center.D4,Center.B1}});
		public static Twist l = new Twist ("l", new Edge[,]{ { Edge.UB, Edge.BD, Edge.DF, Edge.FU } },new Corner[]{ }, new Center[,] {{ Center.U1, Center.B4, Center.D1, Center.F1 }, {Center.U3,Center.B2,Center.D3,Center.F3}});

		public static Twist u2 = new Twist ("u2", u, 2);
		public static Twist u_ = new Twist ("u'", u, 3);
		public static Twist d2 = new Twist ("d2", d, 2);
		public static Twist d_ = new Twist ("d'", d, 3);
		public static Twist f2 = new Twist ("f2", f, 2);
		public static Twist f_ = new Twist ("f'", f, 3);
		public static Twist b2 = new Twist ("b2", b, 2);
		public static Twist b_ = new Twist ("b'", b, 3);
		public static Twist r2 = new Twist ("r2", r, 2);
		public static Twist r_ = new Twist ("r'", r, 3);
		public static Twist l2 = new Twist ("l2", l, 2);
		public static Twist l_ = new Twist ("l'", l, 3);

        public static Twist[] Twists = { U, D, F, B, R, L, U2, U_, D2, D_, F2, F_, B2, B_, R2, R_, L2, L_, u, d, f, b, r, l, u2, u_, d2, d_, f2, f_, b2, b_, r2, r_, l2, l_ };

		private readonly string _name;
		private readonly int[] edgePermutation;
		private readonly int[] pairPermutation;
		private readonly int[] cornerPermutation;
		private readonly int[] centerPermutation;
		private readonly int[] cornerOrientation;
		private readonly int[] edgeOrientation;
		private readonly int[] pairOrientation;
		private Twist inverse;

		private Twist (string name, Edge[,] edgeCycles, Corner[] cornerCycle, Center[,] centerCycles)
		{

			this._name = name;
			edgePermutation = Enumerable.Range (0, 24).ToArray ();
			int length = edgeCycles.GetLength (1);
			for (int i = 0; i < edgeCycles.GetLength (0); i++) {
				for (int j = 0; j < length; j++) {
					int index1 = edgeCycles [i, j].index;
					int index2 = edgeCycles [i, (j + 1) % length].index;
					edgePermutation [index1] = index2;
				}
			}
			pairPermutation = Enumerable.Range (0, 12).ToArray ();
			length = edgePermutation.Length;
			int[] pairs = Edge.getPairs (); 
			for (int j = 0; j < length; j++) {
				pairPermutation [pairs [j]] = pairs [edgePermutation [j]];
			}
			cornerPermutation = Enumerable.Range (0, 8).ToArray ();
			cornerOrientation = new int[8];
			length = cornerCycle.Length;
			Faces baseFace = getBaseFace (cornerCycle);
			for (int j = 0; j < length; j++) {
				int index1 = cornerCycle [j].index;
				int index2 = cornerCycle [(j + 1) % length].index;
				cornerPermutation [index1] = index2;
				int faceIdx1 = getCornerFaceIndex (baseFace, cornerCycle [j]);
				int faceIdx2 = getCornerFaceIndex (baseFace, cornerCycle [(j + 1) % length]);
				cornerOrientation [index1] = (faceIdx1 - faceIdx2) % 3;
			}
			centerPermutation = Enumerable.Range (0, 24).ToArray ();
			length = centerCycles.GetLength (1);
			for (int i = 0; i < centerCycles.GetLength (0); i++) {
				for (int j = 0; j < length; j++) {
					int index1 = centerCycles [i, j].index;
					int index2 = centerCycles [i, (j + 1) % length].index;
					centerPermutation [index1] = index2;
				}
			}

			pairOrientation = new int[12];
			edgeOrientation = new int[24];
			int[] signs = Edge.getSigns ();
			for (int i = 0; i < 24; i++) {
				pairOrientation [pairs [edgePermutation [i]]] = signs [i] * signs [edgePermutation [i]] == 1 ? 0 : 1;
				edgeOrientation [edgePermutation [i]] = signs [i] * signs [edgePermutation [i]] == 1 ? 0 : 1;
			}

		}

		/// <summary>
		/// The Twist R2 equals two consecutive R twists. Consequently we 
		/// can easily generate the corresponding permutation arrays by applying 
		/// R twice. 
		/// </summary>
		private Twist (string name, Twist twist, int num)
		{
			_name = name;
			cornerPermutation = Enumerable.Range (0, 8).ToArray ();
			centerPermutation = Enumerable.Range (0, 24).ToArray ();
			edgePermutation = Enumerable.Range (0, 24).ToArray ();
			pairPermutation = Enumerable.Range (0, 12).ToArray ();
			cornerOrientation = new int[8];
			pairOrientation = new int[12];
			edgeOrientation = new int[24];
			for (int i = 0; i < num; i++) {
				cornerPermutation = twist.apply (cornerPermutation, Type.Corners);
				edgePermutation = twist.apply (edgePermutation, Type.Edges);
				centerPermutation = twist.apply (centerPermutation, Type.Centers);
				cornerOrientation = twist.apply (cornerOrientation, Type.Corners, orientation: true);
				pairPermutation = twist.apply (pairPermutation, Type.EdgePairs);
				pairOrientation = twist.apply (pairOrientation, Type.EdgePairs, orientation: true);
				edgeOrientation = twist.apply (edgeOrientation, Type.Edges, orientation: true);
			}
			// Set the inverse elements; the inverse used so the search algorithm 
			// does not need to return and search where its coming from
			switch (num) {
			case 2:
						this.inverse = this;
				break;
			case 3:
				this.inverse = twist;
				twist.inverse = this;
				break;
			default:
				throw new SystemException ("Not supported");	
			}
		}

		public string Name {
			get{ return _name; }
		}

		public Twist Inverse {
			get{ return inverse; }
		}

		public static int getParity (int[] array)
		{
			int count = 0;
			for (int i = 0; i < array.Length; i++) {
				for (int j = i + 1; j < array.Length; j++) {
					count += (array [j] < array [i]) ? 1 : 0;
				}
			}
			return  count % 2;
		}

		/// <summary>
		/// Apply the twist to specified configuration, type and orientation.
		/// </summary>
		public int[] apply (int[] configuration, Type type, Boolean orientation = false)
		{
			int[] retArray = new int[configuration.Length];
			int[] permutations = type == Type.Edges ? edgePermutation : 
				type == Type.Corners ? cornerPermutation : 
				type == Type.EdgePairs ? pairPermutation : 
				centerPermutation; 
			for (int i = 0; i < retArray.Length; i++) {
				retArray [i] = configuration [permutations [i]]; 
			}
			if (orientation) {
				int[] orientations = type == Type.Corners ? cornerOrientation : 
					type == Type.EdgePairs ? pairOrientation : edgeOrientation;
				for (int i = 0; i < retArray.Length; i++) {
					retArray [i] = mod (retArray [i] + orientations [i], Math.Max (24 / retArray.Length, 2));
				}
			}
			return retArray;
		}


		private Faces getBaseFace (Corner[] cornerCycle)
		{
			int[] faceArray = new int[6];
			foreach (Corner corner in cornerCycle) {
				faceArray [(int)(corner.face1)]++;
				faceArray [(int)(corner.face2)]++;
				faceArray [(int)(corner.face3)]++;
			}
			Faces retValue = Faces.B;
			int max = -1;
			foreach (Faces face in Enum.GetValues(typeof(Faces))) {
				int index = (int)face;
				if (faceArray [index] > max) {
					max = faceArray [index];
					retValue = face;
				}
			}
			return retValue;
		}

		private int getCornerFaceIndex (Faces face, Corner corner)
		{
			if (corner.face1 == face)
				return 0;
			if (corner.face2 == face)
				return 1;
			if (corner.face3 == face)
				return 2;
			throw new System.ArgumentException ("The face passed is not valid");
		}

		private int mod (int k, int n)
		{
			return ((k %= n) < 0) ? k + n : k;
		}

	}

}

