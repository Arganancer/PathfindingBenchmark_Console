using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding.Algorithm.Nodes;
using World.Geometry;

namespace Pathfinding.Sets.OpenSet
{
	public class OpenSortedSet : OpenSet
	{
		private readonly SortedSet<PathNode> m_PathNodes = new SortedSet<PathNode>( new PathNodeComparer() );

		public override Boolean Any() => m_PathNodes.Any();

		public OpenSortedSet() => Structure = "SortedFSet";

		public override void Add( PathNode _pathNode )
		{
			m_PathNodes.Add( _pathNode );
		}

		public override PathNode Remove( PathNode _pathNode )
		{
			PathNode returnValue = m_PathNodes.FirstOrDefault( _innerNode => _innerNode.Position == _pathNode.Position );
			m_PathNodes.Remove( returnValue );
			return returnValue;
		}

		public override Boolean Update( Vector3i _position, Single _gValue )
		{
			PathNode pathNode = Get( _position );
			if ( pathNode == null )
			{
				return false;
			}

			if ( !( pathNode.G > _gValue ) )
			{
				return true;
			}

			Remove( pathNode );
			pathNode.G = _gValue;
			pathNode.F = _gValue + pathNode.H;
			Add( pathNode );

			return true;
		}

		public override PathNode Pop()
		{
			PathNode output = m_PathNodes.First();
			Remove( output );
			return output;
		}

		public override Boolean Contains( Vector3i _position )
		{
			return m_PathNodes.FirstOrDefault( _pathNode => _pathNode.Position == _position ) != null;
		}

		public override PathNode Get( Vector3i _position )
		{
			return m_PathNodes.FirstOrDefault( _pathNode => _pathNode.Position == _position );
		}

		public override void Clear()
		{
			m_PathNodes.Clear();
		}

		internal class PathNodeComparer : IComparer<PathNode>
		{
			public Int32 Compare( PathNode _x, PathNode _y )
			{
				return _x.F.CompareTo( _y.F );
			}
		}
	}
}