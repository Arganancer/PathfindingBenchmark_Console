using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding.Algorithm.Nodes;
using Pathfinding.DataStructures;
using World.Geometry;

namespace Pathfinding.Sets.OpenSet
{
	public class OpenAVLTreeV2 : OpenSet
	{
		private readonly Dictionary<Vector3i, PathNode> m_PosDictionary = new Dictionary<Vector3i, PathNode>();
		private readonly AVLTreeV2<Single, PathNode> m_FTree = new AVLTreeV2<Single, PathNode>();

		public override Boolean Any() => m_FTree.Any();

		// For testing purposes.
		public OpenAVLTreeV2() => Structure = "AVLTreeV2/Dictionary";

		public override void Add( PathNode _pathNode )
		{
			m_FTree.Insert( _pathNode.F, _pathNode, ( _f, _node ) =>
			{
				_node.F = _f + 1;
				return _f + 1;
			} );
			m_PosDictionary.Add( _pathNode.Position, _pathNode );
		}

		public override PathNode Remove( PathNode _pathNode )
		{
			if ( m_PosDictionary.TryGetValue( _pathNode.Position, out PathNode returnValue ) )
			{
				m_FTree.Delete( returnValue.F );
			}

			return returnValue;
		}

		public override Boolean Update( Vector3i _position, Single _gValue )
		{
			if ( !m_PosDictionary.TryGetValue( _position, out PathNode pathNode ) )
			{
				return false;
			}

			if ( !( pathNode.G > _gValue ) )
			{
				return true;
			}

			m_FTree.Delete( pathNode.F );
			pathNode.G = _gValue;
			pathNode.F = _gValue + pathNode.H;
			m_FTree.Insert( pathNode.F, pathNode, ( _f, _node ) =>
			{
				_node.F = _f + 1;
				return _f + 1;
			} );

			return true;
		}

		public override PathNode Pop()
		{
			PathNode output = m_FTree.Pop();
			return output;
		}

		public override Boolean Contains( Vector3i _position )
		{
			return m_PosDictionary.ContainsKey( _position );
		}

		public override PathNode Get( Vector3i _position )
		{
			return m_PosDictionary[_position];
		}

		public override void Clear()
		{
			m_PosDictionary.Clear();
			m_FTree.Clear();
		}
	}
}