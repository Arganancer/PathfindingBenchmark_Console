using System;
using System.Linq;
using Pathfinding.Algorithm.Nodes;
using Pathfinding.DataStructures;
using World.Geometry;

namespace Pathfinding.Sets.OpenSet
{
	public class OpenAVLTree : OpenSet
	{
		private readonly AvlTree<Vector3i, PathNode> m_PosTree = new AvlTree<Vector3i, PathNode>();
		private readonly AvlTree<Single, PathNode> m_FTree = new AvlTree<Single, PathNode>();

		// For testing purposes.
		public OpenAVLTree()
		{
			Structure = "AVL Tree x 2";
		}

		public override Boolean Any() => m_FTree.Any();

		public override void Add( PathNode _pathNode )
		{
			m_FTree.Insert( _pathNode.F, _pathNode );
			m_PosTree.Insert( _pathNode.Position, _pathNode );
		}

		public override PathNode Remove( PathNode _pathNode )
		{
			PathNode node = m_PosTree.Delete( _pathNode.Position );
			m_FTree.Delete( node.F, node );
			return node;
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
			PathNode output = m_FTree.Pop();
			m_PosTree.Delete( output.Position );
			return output;
		}

		public override Boolean Contains( Vector3i _position )
		{
			return m_PosTree.Search( _position ) != null;
		}

		public override PathNode Get( Vector3i _position )
		{
			return m_PosTree.Search( _position );
		}

		public override void Clear()
		{
			m_PosTree.Clear();
			m_FTree.Clear();
		}
	}
}