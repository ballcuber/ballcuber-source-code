using System;
using System.Collections.Generic;
using System.Linq;

namespace RevengeCube
{
	/// <summary>
	/// 
	/// Class representing Rubik's revenge cube. The state of the cube is 
	/// unequivocally defined by the cube's edge positions, corner positions, 
	/// corner orientations (each corner has 3 possible orientations) and 
	/// the center pieces.
	/// 
	/// </summary>
	public class Cube
	{
		/// <summary>
		/// oCube is the cube in its original configuration.
		/// Therefor we disallow any twist. 
		/// </summary>
		private class oCube: Cube
		{
			public new LinkedList<Twist> Twists{
				get{ throw new MissingMethodException ("Method for oCube not supported"); }
			}

			public new void twist (LinkedList<Twist> twists)
			{ 
				throw new MissingMethodException ("Method for oCube not supported");
			}

			public new void twist (Twist twist)
			{
				throw new MissingMethodException ("Method for oCube not supported");
			}
		}


		private int[] _cornerPosition;
		private int[] _edgePosition;
		private int[] _centerPosition;
		private int[] _cornerOrientation;

		private LinkedList<Twist> _twists;

		public static Cube OriginalCube = new oCube ();

        private DateTime _creationDate;

		public Cube ()
		{
			_twists = new LinkedList<Twist> ();
			_cornerPosition = Enumerable.Range (0, 8).ToArray ();
			_cornerOrientation = new int[8];
			_edgePosition = Enumerable.Range (0, 24).ToArray ();
			_centerPosition = (from c in Center.order
			                   select (int)c.face).ToArray ();
            _creationDate = DateTime.Now;

        }

        private Cube(int[] cornerPosition, int[] cornerOrientation,
            int[] edgePosition, int[] centerPosition, LinkedList<Twist> twists, DateTime creationDate)
        {
            _twists = twists;
            _cornerPosition = cornerPosition;
            _cornerOrientation = cornerOrientation;
            _edgePosition = edgePosition;
            _centerPosition = centerPosition;

            _creationDate = creationDate;
        }

        public int[] CornerPosition {
			get{ return _cornerPosition; }
		}

		public int[] CornerOrientation {
			get{ return _cornerOrientation; }
		}

		public int[] CenterPosition {
			get{ return _centerPosition; }
		}

		public int[] EdgePosition {
			get{ return _edgePosition; }
		}



		public Cube Clone(Boolean cloneTwists = true){
			return new Cube ((int[])_cornerPosition.Clone(), 
				(int[])_cornerOrientation.Clone(),
				(int[])_edgePosition.Clone(),
				(int[])_centerPosition.Clone(),
				cloneTwists ? new LinkedList<Twist>(_twists) : new LinkedList<Twist>(),
                _creationDate
			);
		}

		public int[] PairPosition {
			get { 
				int[] retArray = new int[_edgePosition.Length / 2];
				int[] pairs = Edge.getPairs ();
				for (int i = 0; i < _edgePosition.Length; i++) {
					retArray [pairs [i]] = pairs [_edgePosition [i]];
				}
				return retArray;
			}
		}

		public int[] PairOrientation {
			get { 
				int[] retArray = new int[_edgePosition.Length / 2];
				int[] pairs = Edge.getPairs ();
				int[] signs = Edge.getSigns ();
				for (int i = 0; i < 24; i++) {
					retArray [pairs [i]] = signs [i] * signs [_edgePosition [i]] == 1 ? 0 : 1;
				}
				return retArray;
			}
		}

		public int[] EdgeOrientation {
			get { 
				int[] retArray = new int[_edgePosition.Length];
				int[] signs = Edge.getSigns ();
				for (int i = 0; i < 24; i++) {
					retArray [i] = signs [i] * signs [_edgePosition [i]] == 1 ? 0 : 1;
				}
				return retArray;
			}
		}

		public LinkedList<Twist> Twists {
			get{ return _twists; }
		}

		public void twist (LinkedList<Twist> twists)
		{ 
			foreach (Twist twist in twists) {
				this.twist (twist);
			}
		}

		public void twist (Twist twist)
		{
			_cornerPosition = twist.apply (_cornerPosition, Type.Corners);
			_cornerOrientation = twist.apply (_cornerOrientation, Type.Corners, orientation: true);
			_edgePosition = twist.apply (_edgePosition, Type.Edges);
			_centerPosition = twist.apply (_centerPosition, Type.Centers);
			_twists.AddLast (twist);
		}

	}
}

