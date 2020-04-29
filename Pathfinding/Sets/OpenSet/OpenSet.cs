using System;
using Pathfinding.Algorithm.Nodes;
using Pathfinding.Sets.ClosedSet;
using World.Geometry;

namespace Pathfinding.Sets.OpenSet
{
	public abstract class OpenSet : IOpenSet
	{
		public String Structure { get; set; }

		public abstract Boolean Any();

		public abstract void Add( PathNode _pathNode );

		public abstract PathNode Remove( PathNode _pathNode );

		/// <summary>
		/// Return true if the pathNode exists, else false.
		/// </summary>
		public abstract Boolean Update( Vector3i _position, Single _gValue );

		public abstract PathNode Pop();

		public abstract Boolean Contains( Vector3i _position );

		public abstract PathNode Get( Vector3i _position );

		public abstract void Clear();
	}
}