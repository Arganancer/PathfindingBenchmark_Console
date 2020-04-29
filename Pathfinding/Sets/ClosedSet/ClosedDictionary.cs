using System;
using System.Collections.Generic;
using Pathfinding.Algorithm.Nodes;
using World.Geometry;

namespace Pathfinding.Sets.ClosedSet
{
	public class ClosedDictionary : ClosedSet
	{
		private readonly Dictionary<Vector3i, PathNode> m_ClosedSet = new Dictionary<Vector3i, PathNode>();

		public ClosedDictionary() => Structure = "Dictionary";

		public override void Add( PathNode _pathNode )
		{
			m_ClosedSet.Add( new Vector3i( _pathNode.Position ), _pathNode );
		}

		public override Boolean Contains( PathNode _pathNode )
		{
			return Contains( _pathNode.Position );
		}

		public override Boolean Contains( Vector3i _position )
		{
			return m_ClosedSet.ContainsKey( _position );
		}

		public override void Clear()
		{
			m_ClosedSet.Clear();
		}
	}
}