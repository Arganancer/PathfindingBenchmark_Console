using System;
using System.Collections.Generic;
using Pathfinding.Algorithm.Nodes;
using World.Geometry;

namespace Pathfinding.Sets.ClosedSet
{
	public class ClosedList : ClosedSet
	{
		private readonly List<Vector3i> m_ClosedSet = new List<Vector3i>();

		public ClosedList()
		{
			Structure = "List";
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