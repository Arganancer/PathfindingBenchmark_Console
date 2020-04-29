using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding.Algorithm.Nodes;
using Priority_Queue;
using World.Geometry;

namespace Pathfinding.Sets.OpenSet
{
	public class OpenPriorityQueueDictionary : OpenSet
	{
		private readonly Dictionary<Vector3i, FastPathNode> m_PosDictionary = new Dictionary<Vector3i, FastPathNode>();
		private readonly FastPriorityQueue<FastPathNode> m_PriorityQueue = new FastPriorityQueue<FastPathNode>( 100 );

		// For testing purposes.
		public OpenPriorityQueueDictionary() => Structure = "PriorityQueue/Dictionary";

		public override Boolean Any()
		{
			return m_PriorityQueue.Any();
		}

		public override void Add( PathNode _pathNode )
		{
			FastPathNode fastPathNode = new FastPathNode( _pathNode );
			if ( m_PriorityQueue.Count == m_PriorityQueue.MaxSize )
			{
				m_PriorityQueue.Resize( m_PriorityQueue.MaxSize * 2 );
			}

			m_PriorityQueue.Enqueue( fastPathNode, _pathNode.F );
			m_PosDictionary.Add( _pathNode.Position, fastPathNode );
		}

		public override PathNode Remove( PathNode _pathNode )
		{
			return null;
		}

		public override Boolean Update( Vector3i _position, Single _gValue )
		{
			if ( !m_PosDictionary.TryGetValue( _position, out FastPathNode pathNode ) )
			{
				return false;
			}

			if ( !( pathNode.G > _gValue ) )
			{
				return true;
			}

			pathNode.PathNode.G = _gValue;
			pathNode.PathNode.F = pathNode.G + pathNode.H;
			
			m_PriorityQueue.UpdatePriority( pathNode, pathNode.F );

			return true;
		}

		public override PathNode Pop()
		{
			PathNode dequeuedNode = m_PriorityQueue.Dequeue().PathNode;
			return dequeuedNode;
		}

		public override Boolean Contains( Vector3i _position )
		{
			return m_PosDictionary.ContainsKey( _position );
		}

		public override PathNode Get( Vector3i _position )
		{
			return m_PosDictionary[_position].PathNode;
		}

		public override void Clear()
		{
			m_PosDictionary.Clear();
			m_PriorityQueue.Clear();
		}

		internal class FastPathNode : FastPriorityQueueNode
		{
			public Single G => PathNode.G;
			public Single F => PathNode.F;
			public Single H => PathNode.H;
			public PathNode Parent => PathNode.Parent;
			public Vector3i Position => PathNode.Position;

			public FastPathNode( PathNode _pathNode ) => PathNode = _pathNode;

			public readonly PathNode PathNode;
		}
	}
}