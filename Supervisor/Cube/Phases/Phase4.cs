using System;
using System.Collections.Generic;
using RevengeCube;

namespace RevengeSolver
{
	public class Phase4: IPhase
	{

		private class Node : IDAStar<Twist>.INode
		{

			private readonly int[] _edgePosition;
			private readonly int[] _centerPosition;
			private readonly int[] _cornerPosition;
			private readonly Twist _move;
			private readonly int _stage;
			private Node predecessor = null;
			private static int[] pairs = Edge.getPairs();

			public Node ( int[] edgePosition,
				int[] centerPosition,
				int[] cornerPosition,
				int stage, Twist move)
			{
				_edgePosition = edgePosition;
				_centerPosition = centerPosition;
				_cornerPosition = cornerPosition;
				_stage = stage;
				_move = move;
			}

			public float getDistance() {
				float retValue = 0;

				if (_stage > 0) {
					retValue += pairs[_edgePosition[0]] == pairs[_edgePosition[1]] ? 0 : 2;
					retValue += pairs[_edgePosition[2]] == pairs[_edgePosition[4]] ? 0 : 2;
					retValue += pairs[_edgePosition[16]] == pairs[_edgePosition[17]] ? 0 : 2;
					retValue += pairs[_edgePosition[20]] == pairs[_edgePosition[18]] ? 0 : 2;
				}
				if (_stage > 1) {
					retValue += pairs[_edgePosition[3]] == pairs[_edgePosition[5]] ? 0 : 2;
					retValue += pairs[_edgePosition[6]] == pairs[_edgePosition[7]] ? 0 : 1;
					if (retValue == 0) {
						retValue += Twist.getParity(_cornerPosition) ==
							Twist.getParity(getPairConfiguration(_edgePosition)) ? 0 : 1;
					}
				}
				if (_stage > 2) {
					retValue += _centerPosition[8] == 2 ? 0 : 2;
					retValue += _centerPosition[8] == _centerPosition[9] ? 0 : 2;
					retValue += _centerPosition[9] == _centerPosition[10] ? 0 : 2;
				}
				if (_stage > 3) {
					retValue += _centerPosition[10] == _centerPosition[11] ? 0 : 2;
				}
				if (_stage > 4) {
					retValue += _centerPosition[0] == 0 ? 0 : 2;
					retValue += _centerPosition[0] == _centerPosition[1] ? 0 : 2;
					retValue += _centerPosition[1] == _centerPosition[2] ? 0 : 2;
				}
				if (_stage > 5) {
					retValue += _centerPosition[2] == _centerPosition[3] ? 0 : 2;
				}
				if (_stage > 6) {
					retValue += _centerPosition[16] == 4 ? 0 : 2;
					retValue += _centerPosition[16] == _centerPosition[17] ? 0 : 2;
					retValue += _centerPosition[17] == _centerPosition[18] ? 0 : 2;
				}
				if (_stage > 7) {
					retValue += _centerPosition[18] == _centerPosition[19] ? 0 : 2;
					if (retValue == 0) {
						retValue += Twist.getParity(_cornerPosition) ==
							Twist.getParity(getPairConfiguration(_edgePosition)) ? 0 : 1;
					}
				}
				return retValue / 8f;
			}


			public Boolean shouldAvoid(Twist move) {
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
					move.apply (_edgePosition, RevengeCube.Type.Edges),
					move.apply (_centerPosition, RevengeCube.Type.Centers),
					move.apply (_cornerPosition, RevengeCube.Type.Corners),
					_stage,
					 move);
				retObject.predecessor = this;
				return retObject;
			}

			private int[] getPairConfiguration(int[] edges) {
				int[] retArray = new int[12];
				int index = 0;
				foreach (int edge in edges) {
					retArray[pairs[index]] = pairs[edges[index]];
					index++;
				}
				return retArray;
			}

		}

		private readonly Twist[] generators;
		private readonly IDAStar<Twist> IDASearch;

		public Phase4 ()
		{
			generators = new Twist[] {
				Twist.U, Twist.D,
				Twist.R2, Twist.L2, Twist.F2, Twist.B2, Twist.U2, Twist.D2,
				Twist.U_, Twist.D_,
				Twist.r2, Twist.l2, Twist.f2, Twist.b2
			};

			IDASearch = new IDAStar<Twist> (generators);
		}

		public LinkedList<Twist> search (Cube cube)
		{
			cube = cube.Clone(cloneTwists:false);
			for (int phase = 1; phase <= 8; phase++) {
				Console.WriteLine(String.Format(" Solve stage {0} ", phase));
				LinkedList<Twist> twists = IDASearch.search( new Node(cube.EdgePosition,
					cube.CenterPosition,
					cube.CornerPosition,
					phase,
					null), 20);
				cube.twist(twists);
			}
			return cube.Twists;
		}

		public void scramble (Cube cube, int count)
		{
			Random random = new Random ();
			for (int i = 0; i < count; i++) {
				cube.twist (generators [random.Next (generators.Length)]);
			}
		}


	}
}

