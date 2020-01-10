using System.Collections.Generic;
using System.Linq;
using RevengeCube;

namespace RevengeSolver
{
	public class Phase2: IPhase
	{

		private class Node : BFSearch<int, Twist>.INode, IDAStar< Twist>.INode
		{
			
			private readonly int[] _edgePosition;
			private readonly int[] _edgeOrientation;
			private readonly int[] _centerPosition;
			private readonly Twist _move;
			private readonly int _stage;
			private Node predecessor = null;

			public Node (
				int[] edgePosition,
				int[] edgeOrientation,
				int[] centerPosition,
				int stage, Twist move)
			{
				_centerPosition = centerPosition;
				_edgePosition = edgePosition;
				_edgeOrientation = edgeOrientation;
				_move = move;
				_stage = stage;
			}

			public int getID ()
			{
				int retValue = 0;
				int index = 0;
				foreach (int center in _centerPosition) {
					retValue += ((center / 2 == 1 ? 1 : 0) << index);
					index++;
				}
				retValue += Twist.getParity(_edgePosition) << index;
				return retValue;
			}

			public float getDistance ()
			{
				float retValue = 0;
				// First stage: the center pieces of opposing faces must only have colors of those two faces
				for (int i = 0; i < 8; i++) {
					retValue += _centerPosition[i] / 2 == 0 ? 0 : 2;
					retValue += _centerPosition[i + 16] / 2 == 2 ? 0 : 2;}
				if (Twist.getParity(_edgePosition) != 0) retValue += 2;
				// Second stage: up and down faces must have equal scheme; this is not in the paper, but is essential
				if (_stage > 1) {
					for (int i = 0; i < 2; i++) {
						retValue += _centerPosition[i] == _centerPosition[i + 2] ? 0 : 2;
						retValue += _centerPosition[i + 4] == _centerPosition[i + 6] ? 0 : 2;
					}
				}
				// Third stage: the parity of the edge pairs must be equal
				if (_stage > 2) {

					int count = 0;
					foreach (int edge in _edgeOrientation) {
						count += edge == 1 ? 1 : 0;
					}
					if (count / 2 % 2 != 0)
						retValue += 2;
					//foreach (int parity in Twist.getPairParity(_edgePosition, _edgeOrientation)) {
					//	if (parity != 1) {
					//		retValue += 2;
					//		break;
					//	}
					//}
				}
				return retValue / 8f; 
			}


			public bool shouldAvoid (Twist move)
			{
				if (_move != null) {
					return _move.Inverse == move;
				}
				return false;
			}

			public LinkedList<Twist> getMoves ()
			{
				if (predecessor == null) {
					return new LinkedList<Twist> ();
				}
				LinkedList<Twist> retList = predecessor.getMoves ();
				retList.AddLast (_move);
				return retList;
			}



			BFSearch<int , Twist>.INode BFSearch<int , Twist>.INode.move (Twist move)
			{
				return this.move (move);
			}

			IDAStar< Twist>.INode IDAStar<Twist>.INode.move (Twist move)
			{
				return this.move (move);
			}

			private Node move (Twist move)
			{
				Node retObject = new Node (
					                 move.apply (_edgePosition, Type.Edges),
					                 move.apply (_edgeOrientation, Type.Edges, orientation: true),
					                 move.apply (_centerPosition, Type.Centers),
					                 _stage, move);
				retObject.predecessor = this;
				return retObject;
			}

		}

		private readonly Twist[] generators;
		private readonly BFSearch<int, Twist> BFSearch;
		private readonly IDAStar<Twist> IDASearch;

		public Phase2 ()
		{
			generators = new Twist[] {
				Twist.R, Twist.L, Twist.F, Twist.B, Twist.U, Twist.D,
				Twist.R2, Twist.L2, Twist.F2, Twist.B2, Twist.U2, Twist.D2,
				Twist.R_, Twist.L_, Twist.F_, Twist.B_, Twist.U_, Twist.D_,
				Twist.r, Twist.l,
				Twist.r2, Twist.l2, Twist.f2, Twist.b2, Twist.u2, Twist.d2,
				Twist.r_, Twist.l_
			};

			BFSearch = new BFSearch<int, Twist> (generators);
			IDASearch = new IDAStar<Twist> (generators);
		}

		public LinkedList<Twist> search (Cube cube)
		{
			cube = cube.Clone (cloneTwists: false);
			BFSearch.goalID = new Node (Cube.OriginalCube.EdgePosition, 
				Cube.OriginalCube.EdgeOrientation, Cube.OriginalCube.CenterPosition, 0, null).getID ();
			for (int stage = 1; stage <= 3; stage++) {
				Node node = new Node (cube.EdgePosition, 
					            cube.EdgeOrientation, cube.CenterPosition, stage, null);
				LinkedList<Twist> twists =
					stage == 1 ?
					BFSearch.search (node) :
					IDASearch.search (node, 20);
				cube.twist (twists);
			}
			return cube.Twists;
		}

		public void scramble (Cube cube, int count)
		{
            System.Random random = new System.Random ();
			for (int i = 0; i < count; i++) {
				cube.twist (generators [random.Next (generators.Length)]);
			}
		}
	}
}

