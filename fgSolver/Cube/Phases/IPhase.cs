using System;
using System.Collections.Generic;
using RevengeCube;

namespace RevengeSolver
{
	/// <summary>
	/// Interface for 
	/// </summary>
	public interface IPhase
	{

		LinkedList<Twist> search(Cube cube);

		void scramble (Cube cube, int count);

	}
}

