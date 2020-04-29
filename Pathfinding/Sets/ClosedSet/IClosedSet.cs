using System;
using Pathfinding.Algorithm.Nodes;
using World.Geometry;

namespace Pathfinding.Sets.ClosedSet
{
	public interface IClosedSet
	{
		String Structure { get; set; }

		void Add( PathNode _pathNode );

		Boolean Contains( PathNode _pathNode );

		Boolean Contains( Vector3i _position );

		void Clear();
	}
}
