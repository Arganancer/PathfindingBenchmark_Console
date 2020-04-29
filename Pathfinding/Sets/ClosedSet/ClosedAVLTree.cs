using System;
using Pathfinding.Algorithm.Nodes;
using Pathfinding.DataStructures;
using World.Geometry;

namespace Pathfinding.Sets.ClosedSet
{
	public class ClosedAVLTree : ClosedSet
	{
		private readonly AvlTree<Vector3i, PathNode> m_PosTree = new AvlTree<Vector3i, PathNode>();

		public ClosedAVLTree()
		{
			Structure = "AVL Tree";
		}

		public override void Add( PathNode _pathNode )
		{
			m_PosTree.Insert( _pathNode.Position, _pathNode );
		}

		public override Boolean Contains( PathNode _pathNode )
		{
			return m_PosTree.Search( _pathNode.Position ) != null;
		}

		public override Boolean Contains( Vector3i _position )
		{
			return m_PosTree.Search( _position ) != null;
		}

		public override void Clear()
		{
			m_PosTree.Clear();
		}
	}
}