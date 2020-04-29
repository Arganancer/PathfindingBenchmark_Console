using System;
using System.Collections.Generic;
using Pathfinding.Algorithm.Nodes;
using World.Geometry;

namespace Pathfinding.Sets.ClosedSet
{
	public class ClosedHashSet : ClosedSet
	{
		private readonly HashSet<Vector3i> m_ClosedSet = new HashSet<Vector3i>();

		public ClosedHashSet()
		{
			Structure = "HashSet";
		}

		public override void Add( PathNode _pathNode )
		{
			m_ClosedSet.Add( _pathNode.Position );
		}

		public override Boolean Contains( PathNode _pathNode )
		{
			return m_ClosedSet.Contains( _pathNode.Position );
		}

		public override Boolean Contains( Vector3i _position )
		{
			return m_ClosedSet.Contains( _position );
		}

		public override void Clear()
		{
			m_ClosedSet.Clear();
		}
	}
}