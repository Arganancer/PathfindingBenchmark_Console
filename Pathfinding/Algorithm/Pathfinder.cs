using System;
using System.Collections.Generic;
using Pathfinding.Algorithm.Nodes;
using Pathfinding.Sets.ClosedSet;
using World.Geometry;
using TheWorld = World.World;

namespace Pathfinding.Algorithm
{
	public abstract class Pathfinder
	{
		public Pathfinder( TheWorld _world )
		{
			World = _world;
		}

		public abstract Stack<PathNode> FindPath( Vector3i _startNode, Vector3i _endNode, IOpenSet _openSet, IClosedSet _closedSet );

		protected abstract Stack<PathNode> ReconstructPath( PathNode _lastNode );

		protected abstract Single GetH( Vector3i _a, Vector3i _b );

		protected TheWorld World { get; }
	}
}