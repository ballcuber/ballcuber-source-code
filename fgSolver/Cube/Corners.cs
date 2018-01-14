using System;
using RevengeCube;

namespace RevengeCube {

	/// <summary>
	/// A class representing all corner pieces. Corner pieces are labeled 
	/// by refering to the three faces meeting at location of the corner.
	/// Eg. the corner where the Upper, the Left and the Back face meet 
	/// is labeled: ULB.
	/// </summary>
	public class Corner {

		public static Corner ULB = new Corner (Faces.U, Faces.L, Faces.B);
		public static Corner URF = new Corner (Faces.U, Faces.R, Faces.F);
		public static Corner URB = new Corner (Faces.U, Faces.B, Faces.R);
		public static Corner ULF = new Corner (Faces.U, Faces.F, Faces.L);
		public static Corner DLF = new Corner (Faces.D, Faces.L, Faces.F);
		public static Corner DRB = new Corner (Faces.D, Faces.R, Faces.B);
		public static Corner DRF = new Corner (Faces.D, Faces.F, Faces.R);
		public static Corner DLB = new Corner (Faces.D, Faces.B, Faces.L);

		public static Corner[] order = new Corner[]{ ULB, URF, URB, ULF, DLF, DRB, DRF, DLB };

		private readonly Faces _face1;
		private readonly Faces _face2;
		private readonly Faces _face3;

		private Corner (Faces face1, Faces face2, Faces face3) {
			this._face1 = face1;
			this._face2 = face2;
			this._face3 = face3;
		}

		public Faces face1 {
			get { return _face1; }
		}

		public Faces face2 {
			get { return _face2; }
		}

		public Faces face3 {
			get { return _face3; }
		}

		public int index {
			get {
				int idx = 0;
				foreach (Corner corner in order) {
					if (this.Equals (corner))
						return idx;
					idx++;
				}
				return -1;
			}
		}

		public override bool Equals (object obj) {
			var item = obj as Corner;

			if (item == null) {
				return false;
			}

			return this._face1.Equals (item._face1) && this._face2.Equals (item._face2) && this._face3.Equals (item._face3);
		}

		public override int GetHashCode () {
			return this._face1.GetHashCode () + 9 * this._face2.GetHashCode () + 31 * this._face3.GetHashCode ();
		}

	}
}

