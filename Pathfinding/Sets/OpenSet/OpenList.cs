using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding.Algorithm.Nodes;
using World.Geometry;

namespace Pathfinding.Sets.OpenSet
{
	public class OpenList : OpenSet
	{
		private readonly List<PathNode> m_OpenList = new List<PathNode>();

		public OpenList()
		{
			Structure = "List";
		}

		public override Boolean Any() => m_OpenList.Any();

		public override void Add( PathNode _pathNode )
		{
			m_OpenList.Add( _pathNode );
		}

		public override PathNode Remove( PathNode _pathNode )
		{
			PathNode returnValue = m_OpenList.Find( _node => _node.Position == _pathNode.Position );
			m_OpenList.Remove( returnValue );
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
			PathNode output = m_OpenList.First();
			foreach ( PathNode pathNode in m_OpenList )
			{
				if ( pathNode.F < output.F )
				{
					output = pathNode;
				}
			}

			Remove( output );
			return output;
		}

		public override Boolean Contains( Vector3i _position )
		{
			return m_OpenList.Exists( _node => _node.Position == _position );
		}

		public override PathNode Get( Vector3i _position )
		{
			return m_OpenList.Find( _node => _node.Position == _position );
		}

		public override void Clear()
		{
			m_OpenList.Clear();
		}
	}
}