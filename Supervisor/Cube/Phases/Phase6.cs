using System.Collections.Generic;
using System.Text;
using System.Linq;
using RevengeCube;

namespace RevengeSolver
{

	public class Phase6: IPhase
	{

		private class Node : BFSearch<long, Twist>.INode
		{

			private readonly int[] _cornerOrientation;
			private readonly int[] _pairPosition;
			private readonly Twist _move;
			private Node predecessor = null;

			public Node (int[] cornerOrientation, 
			            int[] pairPosition, Twist move)
			{
				_cornerOrientation = cornerOrientation;
				_pairPosition = pairPosition;
				_move = move;
			}

			public long getID ()
			{
				long retValue = 0;
				int index = 0;
				// The configuration that will be accepted as goal must have the same corner orientations like
				// the cube in its original position
				foreach (int corner in _cornerOrientation) {
					retValue += (corner << index);
					index+=2;
				}
				//  The goal-configuration also needs to have the correct edges placed in the middle layer
				for(int i = 0; i<12; i++){
					int edge = _pairPosition[i];
					retValue += ((edge/8) << index);
					index += 1;
				}
				return retValue;
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

			public BFSearch<long , Twist>.INode move (Twist move)
			{
				Node retObject = new Node (move.apply (_cornerOrientation, Type.Corners,orientation: true),
					                 move.apply (_pairPosition, Type.EdgePairs), move);
				retObject.predecessor = this;
				return retObject;
			}

		}

		private readonly Twist[] generators;
		private readonly BFSearch<long, Twist> bfSearch;

		public Phase6 ()
		{

			generators = new Twist[] {
				Twist.F, Twist.B, Twist.U, Twist.D,
				Twist.U2, Twist.D2, Twist.R2, Twist.L2, Twist.F2, Twist.B2,
				Twist.F_, Twist.B_, Twist.U_, Twist.D_
			};

			bfSearch = new BFSearch<long, Twist> (generators);
		}

		public LinkedList<Twist> search (Cube cube)
		{
			Node goalNode = new Node (new int[8], Enumerable.Range (0, 12).ToArray (), null);
			bfSearch.goalID = goalNode.getID ();
			return bfSearch.search (new Node (cube.CornerOrientation, cube.PairPosition, null));
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

