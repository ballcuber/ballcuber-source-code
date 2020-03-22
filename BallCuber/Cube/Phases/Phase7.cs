using System.Collections.Generic;
using System.Text;
using System.Linq;
using RevengeCube;

namespace RevengeSolver
{

	public class Phase7: IPhase
	{

		private class Node : BFSearch<long, Twist>.INode
		{

			private readonly int[] _cornerPosition;
			private readonly int[] _pairPosition;
			private readonly Twist _move;
			private Node predecessor = null;

			public Node (int[] cornerPosition, 
			            int[] pairPosition, Twist move)
			{
				_cornerPosition = cornerPosition;
				_pairPosition = pairPosition;
				_move = move;
			}

			public long getID ()
			{
				long retValue = 0;
				int index = 0;
				int parity = Twist.getParity (_pairPosition);
				// The goal configuration should have the edges placed in the correct layer
				foreach (int edge  in _pairPosition) {
					retValue += ((edge / 4) << index);
					index += 2;
				}
				// Also the corners have to be placed in the correct slice
				foreach (int corner in _cornerPosition) {
					retValue += ((corner / 2) << index);
					index += 2;
				}
				retValue += (parity << index);
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
				Node retObject = new Node (move.apply (_cornerPosition, Type.Corners),
					                 move.apply (_pairPosition, Type.EdgePairs), move);
				retObject.predecessor = this;
				return retObject;
			}



		}

		private readonly Twist[] generators;
		private readonly BFSearch<long, Twist> bfSearch;

		public Phase7 ()
		{

			generators = new Twist[] {
				Twist.U, Twist.D,
				Twist.U2, Twist.D2, Twist.R2, Twist.L2, Twist.F2, Twist.B2,
				Twist.U_, Twist.D_
			};

			bfSearch = new BFSearch<long, Twist> (generators);
		}

		public LinkedList<Twist> search (Cube cube)
		{
			Node goalNode = new Node (Enumerable.Range (0, 8).ToArray (), Enumerable.Range (0, 12).ToArray (), null);
			bfSearch.goalID = goalNode.getID ();
			return bfSearch.search (new Node (cube.CornerPosition, cube.PairPosition, null));
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

