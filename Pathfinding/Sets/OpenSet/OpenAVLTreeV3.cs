using System;
using System.Collections.Generic;
using Pathfinding.Algorithm.Nodes;
using Pathfinding.DataStructures;
using Pathfinding.Sets.ClosedSet;
using World.Geometry;

namespace Pathfinding.Sets.OpenSet
{
	public class OpenAVLTreeV3 : AvlTreeV3<Single, PathNode>, IOpenSet
	{
		private readonly Dictionary<Vector3i, AVLNode<Single, PathNode>> m_PosDictionary = new Dictionary<Vector3i, AVLNode<Single, PathNode>>();

		public String Structure { get; set; } = "AVLTreeV3/Dictionary";

		public override void Clear()
		{
			m_PosDictionary.Clear();
			base.Clear();
		}

		public Boolean Any()
		{
			return m_Root != null;
		}

		public void Add( PathNode _pathNode )
		{
			AVLNode<Single, PathNode> node = Insert( _pathNode.F, _pathNode, ( _f, _node ) =>
			{
				_node.F = _f + 1;
				return _f + 1;
			} );
			m_PosDictionary.Add( _pathNode.Position, node );
		}

		public PathNode Remove( PathNode _pathNode )
		{
			if ( m_PosDictionary.TryGetValue( _pathNode.Position, out AVLNode<Single, PathNode> returnValue ) )
			{
				Delete( returnValue );
			}

			return returnValue?.Value;
		}

		public Boolean Update( Vector3i _position, Single _gValue )
		{
			if ( !m_PosDictionary.TryGetValue( _position, out AVLNode<Single, PathNode> pathNode ) )
			{
				return false;
			}

			if ( !( pathNode.Value.G > _gValue ) || !pathNode.IsValid )
			{
				return true;
			}

			Delete( pathNode );
			pathNode.Value.G = _gValue;
			pathNode.Value.F = _gValue + pathNode.Value.H;
			Insert( pathNode.Value.F, pathNode.Value, ( _f, _node ) =>
			{
				_node.F = _f + 1;
				return _f + 1;
			} );

			return true;
		}

		public Boolean Contains( Vector3i _position )
		{
			return m_PosDictionary.ContainsKey( _position );
		}

		public PathNode Get( Vector3i _position )
		{
			return m_PosDictionary[_position].Value;
		}
	}
}