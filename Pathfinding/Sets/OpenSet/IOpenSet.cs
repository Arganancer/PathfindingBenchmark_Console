using System;
using Pathfinding.Algorithm.Nodes;
using World.Geometry;

namespace Pathfinding.Sets.ClosedSet
{
	public interface IOpenSet
	{
		String Structure { get; set; }

		Boolean Any();

		void Add( PathNode _pathNode );

		PathNode Remove( PathNode _pathNode );

		/// <summary>
		/// Return true if the pathNode exists, else false.
		/// </summary>
		Boolean Update( Vector3i _position, Single _gValue );

		PathNode Pop();

		Boolean Contains( Vector3i _position );

		PathNode Get( Vector3i _position );

		void Clear();
	}
}
