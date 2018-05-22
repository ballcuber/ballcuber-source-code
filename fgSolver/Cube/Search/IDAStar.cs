using System;
using System.Collections.Generic;
using RevengeCube;

namespace RevengeSolver
{
	/// <summary>
	/// Implementation of the IDA* search algorithm
	/// </summary>
	public class IDAStar<M>
	{
		
		public interface INode{

			float getDistance();

			LinkedList<M> getMoves();

			INode move(M move);

			Boolean shouldAvoid(M move);
		}

		private readonly M[] moves;

		public IDAStar(M[] moves){
			this.moves = moves;
		}

		public LinkedList<M> search(INode node, int maxDepth) {
			for (int depth = 0; depth <= maxDepth; depth++) {
				INode goal = searchPhase(node, depth);
				if (goal != null) {
					return goal.getMoves();
				}
			}
			return null;
		}

		private INode searchPhase(INode node, int depth) {
			if (depth == 0) {
				if (node.getDistance() <= 0) {
					return node;
				}
			} else if (node.getDistance() <= depth) {
				foreach (M move in moves) {
					if (node.shouldAvoid(move))
						continue;
					INode twistedNode =node.move(move);
					INode goal = searchPhase(twistedNode, depth - 1);
					if (goal != null) {
						return goal;
					}
				}
			}
			return null;
		}

	}
}

