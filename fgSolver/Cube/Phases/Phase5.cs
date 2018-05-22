using System.Collections.Generic;
using System.Text;
using System.Linq;
using RevengeCube;

namespace RevengeSolver
{

	public class Phase5: IPhase
	{

		private class Node : BFSearch<int, Twist>.INode
		{

			private readonly int[] _pairOrientation;
			private readonly Twist _move;
			private Node predecessor = null;

			public Node (int[] pairOrientation, Twist move)
			{
				_pairOrientation = pairOrientation;
				_move = move;
			}

			public int getID ()
			{
				int retValue = 0;
				int index = 0;
				// make sure that only configurations which have the same edge orientation
				// as the cube's original position will be accepted as goal
				foreach (int edge in _pairOrientation) {
					retValue += (edge << index);
					index++;
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

			public BFSearch<int , Twist>.INode move (Twist move)
			{
				Node retObject = new Node (move.apply (_pairOrientation, Type.EdgePairs, orientation: true), move);
				retObject.predecessor = this;
				return retObject;
			}

		}

		private readonly Twist[] generators;
		private readonly BFSearch<int, Twist> bfSearch;

		public Phase5 ()
		{

			generators = new Twist[] {
				Twist.F, Twist.B, Twist.U, Twist.D, Twist.R, Twist.L,
				Twist.U2, Twist.D2, Twist.R2, Twist.L2, Twist.F2, Twist.B2,
				Twist.F_, Twist.B_, Twist.U_, Twist.D_,Twist.R_, Twist.L_
			};

			bfSearch = new BFSearch<int, Twist> (generators);
		}

		public LinkedList<Twist> search (Cube cube)
		{
			Node goalNode = new Node (new int[12], null);
			bfSearch.goalID = goalNode.getID ();
			return bfSearch.search (new Node (cube.PairOrientation, null));
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

