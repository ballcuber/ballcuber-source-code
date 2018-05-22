using System.Collections.Generic;
using System.Text;
using System.Linq;
using RevengeCube;

namespace RevengeSolver {


	public class Phase8: IPhase {

		private class Node : BFSearch<string, Twist>.INode {

			private readonly int[] _cornerPosition;
			private readonly int[] _pairPosition;
			private readonly Twist _move;
			private Node predecessor = null;

			public Node(int[] cornerPosition, 
				int[] pairPosition, Twist move){
				_cornerPosition = cornerPosition;
				_pairPosition = pairPosition;
				_move = move;
			}

			public string getID () {
				StringBuilder stringBuilder = new StringBuilder ();
				foreach (int pos in _cornerPosition) {
					stringBuilder.AppendFormat("{0:X1}", pos);
				}
				foreach (int pos in _pairPosition) {
					stringBuilder.AppendFormat("{0:X1}", pos);
				}
				return stringBuilder.ToString ();
			}

			public LinkedList<Twist> getMoves () {
				if (predecessor == null) {
					return new LinkedList<Twist> ();
				}
				LinkedList<Twist> retList = predecessor.getMoves ();
				retList.AddLast (_move);
				return retList;
			}

			public BFSearch<string , Twist>.INode move (Twist move) {
				Node retObject = new Node(move.apply(_cornerPosition, Type.Corners),
					move.apply(_pairPosition, Type.EdgePairs), move);
				retObject.predecessor = this;
				return retObject;
			}

		}

		private readonly Twist[] generators;
		private readonly BFSearch<string, Twist> bfSearch;

		public Phase8 () {

			generators = new Twist[] {
				Twist.U2, Twist.D2, Twist.R2, Twist.L2, Twist.F2, Twist.B2
			};

			bfSearch = new BFSearch<string, Twist> (generators);
		}

		public LinkedList<Twist> search(Cube cube){
			Node goalNode = new Node (Enumerable.Range(0,8).ToArray (), Enumerable.Range(0,12).ToArray (), null);
			bfSearch.goalID = goalNode.getID ();
			return bfSearch.search (new Node(cube.CornerPosition, cube.PairPosition, null));
		} 

		public void scramble(Cube cube, int count){
            System.Random random = new System.Random ();
			for(int i=0 ; i<count; i++){
				cube.twist(generators[random.Next(generators.Length)]);
			}
		}


	}
}

