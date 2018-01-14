using System;

namespace RevengeCube
{

	/// <summary>
	/// A class representing all center pieces. Each face 
	/// has 4 center pieces. Hence all center pieces are 
	/// labeled U1..U4, R1..R4.....
	/// Center pieces of given color are group theoretically 
	/// not distinguishable and the index 1 to 4 only refers
	/// to the pieces position on the respective face.  
	/// 
	public class Center
	{

		public static Center U1 = new Center (Faces.U);
		public static Center U2 = new Center (Faces.U);
		public static Center U3 = new Center (Faces.U);
		public static Center U4 = new Center (Faces.U);
		public static Center R1 = new Center (Faces.R);
		public static Center R2 = new Center (Faces.R);
		public static Center R3 = new Center (Faces.R);
		public static Center R4 = new Center (Faces.R);
		public static Center F1 = new Center (Faces.F);
		public static Center F2 = new Center (Faces.F);
		public static Center F3 = new Center (Faces.F);
		public static Center F4 = new Center (Faces.F);
		public static Center D1 = new Center (Faces.D);
		public static Center D2 = new Center (Faces.D);
		public static Center D3 = new Center (Faces.D);
		public static Center D4 = new Center (Faces.D);
		public static Center L1 = new Center (Faces.L);
		public static Center L2 = new Center (Faces.L);
		public static Center L3 = new Center (Faces.L);
		public static Center L4 = new Center (Faces.L);
		public static Center B1 = new Center (Faces.B);
		public static Center B2 = new Center (Faces.B);
		public static Center B3 = new Center (Faces.B);
		public static Center B4 = new Center (Faces.B);

		public static Center[] order = {
			R1, R2, R3, R4,
			L1, L2, L3, L4,
			U1, U2, U3, U4,
			D1, D2, D3, D4,
			F1, F2, F3, F4,
			B1, B2, B3, B4
		};

		private Faces _face;

		private Center (Faces face)
		{
			_face = face;
		}

		public Faces face {
			get{ return _face; }
		}

		public int index {
			get {
				int idx = 0;
				foreach (Center center in order) {
					if (this.Equals (center))
						return idx;
					idx++;
				}
				return -1;
			}
		}

	}
}

