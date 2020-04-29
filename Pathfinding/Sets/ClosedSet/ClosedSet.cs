using System;
using Pathfinding.Algorithm.Nodes;
using World.Geometry;

namespace Pathfinding.Sets.ClosedSet
{
	public abstract class ClosedSet : IClosedSet
	{
		public String Structure { get; set; }

		public abstract void Add( PathNode _pathNode );

		public abstract Boolean Contains( PathNode _pathNode );

		public abstract Boolean Contains( Vector3i _position );

		public abstract void Clear();
	}
}