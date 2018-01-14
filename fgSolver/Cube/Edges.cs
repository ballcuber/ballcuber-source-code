using System;
using System.Collections.Generic;

namespace RevengeCube
{
	/// <summary>
	/// A class that represents all edge pieces. An edge piece is labeled by 
	/// by its corresponding faces. Eg. edge UB is the edge at the interface
	/// of face U and B. The edge pairs UB and BU are such, that UB is to the 
	/// left and BU is the right edge when looking in direction U -> B  
	/// 
	/// </summary>
	public class Edge
	{

		public static Edge UB = new Edge (Faces.U, Faces.B);
		public static Edge BU = new Edge (Faces.B, Faces.U);
		public static Edge LU = new Edge (Faces.L, Faces.U);
		public static Edge UR = new Edge (Faces.U, Faces.R);
		public static Edge UL = new Edge (Faces.U, Faces.L);
		public static Edge RU = new Edge (Faces.R, Faces.U);
		public static Edge FU = new Edge (Faces.F, Faces.U);
		public static Edge UF = new Edge (Faces.U, Faces.F);

		public static Edge BL = new Edge (Faces.B, Faces.L);
		public static Edge RB = new Edge (Faces.R, Faces.B);
		public static Edge LF = new Edge (Faces.L, Faces.F);
		public static Edge FR = new Edge (Faces.F, Faces.R);
		public static Edge FL = new Edge (Faces.F, Faces.L);
		public static Edge RF = new Edge (Faces.R, Faces.F);
		public static Edge LB = new Edge (Faces.L, Faces.B);
		public static Edge BR = new Edge (Faces.B, Faces.R);

		public static Edge DF = new Edge (Faces.D, Faces.F);
		public static Edge FD = new Edge (Faces.F, Faces.D);
		public static Edge LD = new Edge (Faces.L, Faces.D);
		public static Edge DR = new Edge (Faces.D, Faces.R);
		public static Edge DL = new Edge (Faces.D, Faces.L);
		public static Edge RD = new Edge (Faces.R, Faces.D);
		public static Edge BD = new Edge (Faces.B, Faces.D);
		public static Edge DB = new Edge (Faces.D, Faces.B);

		public static Edge[] order = new Edge[] {
			UB, BU, LU, UR, UL, RU, FU, UF,
			BL, RB, LF, FR, FL, RF, LB, BR,
			DF, FD, LD, DR, DL, RD, BD, DB
		};

		public static Edge[,] pairs = new Edge[,] {
			{ UB, BU }, { UF, FU }, { DF, FD }, { DB, BD },
			{ UL, LU }, { UR, RU }, { DL, LD }, { DR, RD },
			{ LB, BL }, { RB, BR }, { LF, FL }, { RF, FR },

		};

		private Faces _face1;
		private Faces _face2;

		private Edge (Faces face1, Faces face2)
		{
			this._face1 = face1;
			this._face2 = face2;

		}

		public static int[] twist (Twist twist, int[] positions)
		{
			return null;
		}

		public Faces face1 {
			get { return _face1; }
		}

		public Faces face2 {
			get { return _face2; }
		}

		public int index {
			get {
				int idx = 0;
				foreach (Edge edge in order) {
					if (this.Equals (edge))
						return idx;
					idx++;
				}
				return -1;
			}
		}

		public int pIndex {
			get {
				for (int i = 0; i < pairs.GetLength (0); i++) {
					if (this.Equals (pairs [i, 0]) || this.Equals (pairs [i, 1])) {
						return i;
					}
				}
				return -1;
			}
		}

		private int sign {
			get {
				for (int i = 0; i < pairs.GetLength (0); i++) {
					if (this.Equals (pairs [i, 0]))
						return 1;
					if (this.Equals (pairs [i, 1]))
						return -1;
				}
				return -1;
			}
		}

		public static int[] getPairs ()
		{
			int[] retArray = new int[order.Length];
			int idx = 0;
			foreach (Edge edge in order) {
				retArray [idx++] = edge.pIndex; 
			}
			return retArray;
		}

		public static int[] getSigns (){
			int[] retArray = new int[order.Length];
			int idx = 0;
			foreach (Edge edge in order) {
				retArray [idx++] = edge.sign; 
			}
			return retArray;
		}

		/// <summary>
		/// We implement Equals and GetHashCode to be able to identify an edge by its 
		/// two face(color) values and not by the instance.
		/// </summary>
		public override bool Equals (object obj)
		{
			var item = obj as Edge;

			if (item == null) {
				return false;
			}

			return this._face1.Equals (item._face1) && this._face2.Equals (item._face2);
		}

		public override int GetHashCode ()
		{
			return this._face1.GetHashCode () + 31 * this._face2.GetHashCode ();
		}
	}
		
}

