using System.Collections.Generic;
using RevengeCube;

namespace RevengeSolver
{
	public class Phase3: IPhase
	{

		private class Node : IDAStar<Twist>.INode
		{

			private readonly int[] _edgePosition;
			private readonly int[] _centerPosition;
			private readonly Twist _move;
			private readonly int _stage;
			private Node predecessor = null;
			private static int[] pairs = Edge.getPairs ();

			public Node (int[] edgePosition,
			              int[] centerPosition,
			              int stage, Twist move)
			{
				_edgePosition = edgePosition;
				_centerPosition = centerPosition;
				_stage = stage;
				_move = move;
			}

			public float getDistance ()
			{
				float retValue = 0;
				// First stage we impose the condition that center faces contain columns
				retValue += _centerPosition [0] == _centerPosition [2] ? 0 : 2;
				retValue += _centerPosition [1] == _centerPosition [3] ? 0 : 2;
				retValue += _centerPosition [4] == _centerPosition [6] ? 0 : 2;
				retValue += _centerPosition [5] == _centerPosition [7] ? 0 : 2;

				retValue += _centerPosition [16] == _centerPosition [18] ? 0 : 2;
				retValue += _centerPosition [17] == _centerPosition [19] ? 0 : 2;
				retValue += _centerPosition [20] == _centerPosition [22] ? 0 : 2;
				retValue += _centerPosition [21] == _centerPosition [23] ? 0 : 2;
				if (_stage > 1) {
					// second stage: we impose condition that two if the four "middle" edges are paired
					retValue += pairs [_edgePosition [9]] == pairs [_edgePosition [15]] ? 0 : 2;
					retValue += pairs [_edgePosition [8]] == pairs [_edgePosition [14]] ? 0 : 2;
				}
				if (_stage > 2) {
					// third stage: we make sure the two remaining edges in the middle are paired
					retValue += pairs [_edgePosition [10]] == pairs [_edgePosition [12]] ? 0 : 2;
					retValue += pairs [_edgePosition [11]] == pairs [_edgePosition [13]] ? 0 : 2;
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

			public IDAStar< Twist>.INode move (Twist move)
			{
				Node retObject = new Node (
					                 move.apply (_edgePosition, Type.Edges),
					                 move.apply (_centerPosition, Type.Centers),
					                 _stage,
					                 move);
				retObject.predecessor = this;
				return retObject;
			}

		}

		private readonly Twist[] generators;
		private readonly IDAStar<Twist> IDASearch;

		public Phase3 ()
		{
			generators = new Twist[] {
				Twist.F, Twist.B, Twist.U, Twist.D,
				Twist.R2, Twist.L2, Twist.F2, Twist.B2, Twist.U2, Twist.D2,
				Twist.F_, Twist.B_, Twist.U_, Twist.D_,
				Twist.r2, Twist.l2, Twist.f2, Twist.b2, Twist.u2, Twist.d2
			};

			IDASearch = new IDAStar<Twist> (generators);
		}

		public LinkedList<Twist> search (Cube cube)
		{
			cube = cube.Clone (cloneTwists: false);
			for (int phase = 1; phase <= 3; phase++) {
                System.Console.WriteLine (string.Format (" Solve stage {0} ", phase));
				LinkedList<Twist> twists = IDASearch.search (new Node (cube.EdgePosition,
					                           cube.CenterPosition,
					                           phase,
					                           null), 20);
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

