using System.Collections.Generic;
using System.Linq;
using RevengeCube;

namespace RevengeSolver
{
	public class Phase1: IPhase
	{

		private class Node : BFSearch<int, Twist>.INode
		{

			private readonly int[] _centerPosition;
			private readonly Twist _move;
			private Node predecessor = null;

			public Node (int[] centerPosition, Twist move)
			{
				_centerPosition = centerPosition;
				_move = move;
			}

			public int getID ()
			{
				int retValue = 0;
				int index = 0;
				foreach (int center in _centerPosition) {
					retValue += ((center/2==0 ? 1: 0) << index);
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
				Node retObject = new Node (move.apply (_centerPosition, Type.Centers), move);
				retObject.predecessor = this;
				return retObject;
			}

		}

		private readonly Twist[] generators;
		private readonly BFSearch<int, Twist> bfSearch;

		public Phase1 ()
		{
			generators = new Twist[] {
				Twist.R,  Twist.L,  Twist.F,  Twist.B,  Twist.U,  Twist.D,
				Twist.R2, Twist.L2, Twist.F2, Twist.B2, Twist.U2, Twist.D2,
				Twist.R_, Twist.L_, Twist.F_, Twist.B_, Twist.U_, Twist.D_,
				Twist.r,  Twist.l,  Twist.f,  Twist.b,  Twist.u,  Twist.d,
				Twist.r2, Twist.l2, Twist.f2, Twist.b2, Twist.u2, Twist.d2,
				Twist.r_, Twist.l_, Twist.f_, Twist.b_, Twist.u_, Twist.d_
			};

			bfSearch = new BFSearch<int, Twist> (generators);
		}

		public LinkedList<Twist> search (Cube cube)
		{
			Node goalNode = new Node (Cube.OriginalCube.CenterPosition , null);
			bfSearch.goalID = goalNode.getID ();
			return bfSearch.search (new Node (cube.CenterPosition, null));
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

