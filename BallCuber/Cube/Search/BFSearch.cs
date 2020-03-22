using System;
using System.Collections.Generic;
using System.Collections;
using RevengeCube;


namespace RevengeSolver
{
	/// <summary>
	/// Implementation of the Breadth-first search algorithm
	/// </summary>
	public class BFSearch<T,M>
	{

		public interface INode
		{

			T getID ();

			LinkedList<M> getMoves ();

			INode move (M move);

		}
			
		private Queue<INode> nodeQueue = new Queue<INode> ();
		private HashSet<T> nodeSet = new HashSet<T> ();
		private T _goalID;
		private Object _lock = new Object ();
		private readonly M[] moves;

		public BFSearch (M[] moves)
		{
			this.moves = moves;
		}

		public T goalID {
			set { _goalID = value; } 
		}

		public LinkedList<M> search (INode node)
		{
			lock (_lock) {
				start ();
				T nodeID = node.getID ();
				if (nodeID.Equals (_goalID)) {
					stop ();
					return node.getMoves ();
				}

				nodeSet.Add (nodeID);
				nodeQueue.Enqueue (node);
				while (true) {
					INode bNode = nodeQueue.Dequeue ();

					foreach (M move in this.moves) {
						INode mNode = bNode.move (move);
						T id = mNode.getID ();
						if (nodeSet.Contains (id)) {
							continue;
						}
						if (id.Equals (_goalID)) {
							stop ();
							return mNode.getMoves ();
						}
						nodeSet.Add (id);
						nodeQueue.Enqueue (mNode);
					}
				}
			}
		}

		public List<M> search ()
		{
			return new List<M> ();
		}

		private void start ()
		{
			nodeSet.Clear ();
			nodeQueue.Clear ();
		}

		private void stop ()
		{
			nodeSet.Clear ();
			nodeQueue.Clear ();
		}

	}
}

