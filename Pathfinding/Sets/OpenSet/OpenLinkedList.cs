using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding.Algorithm.Nodes;
using World.Geometry;

namespace Pathfinding.Sets.OpenSet
{
	public class OpenLinkedList : OpenSet
	{
		private readonly LinkedList<PathNode> m_PathNodes = new LinkedList<PathNode>();

		public OpenLinkedList() => Structure = "LinkedList";

		public override Boolean Any() => m_PathNodes.Any();

		public override void Add( PathNode _pathNode )
		{
			LinkedListNode<PathNode> node = m_PathNodes.EnumerateNodes().FirstOrDefault( _innerPathNode => _innerPathNode.Value.F > _pathNode.F );
			if ( node is null )
			{
				m_PathNodes.AddLast( _pathNode );
			}
			else
			{
				m_PathNodes.AddBefore( node, _pathNode );
			}
		}

		public override PathNode Remove( PathNode _pathNode )
		{
			PathNode returnValue = m_PathNodes.First( _node => _node.Position == _pathNode.Position );
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
			return Get( _position ) != null;
		}

		public override PathNode Get( Vector3i _position )
		{
			return m_PathNodes.FirstOrDefault( _node => _node.Position == _position );
		}

		public override void Clear()
		{
			m_PathNodes.Clear();
		}
	}

	internal static class LinkedListExtensions
	{
		public static IEnumerable<LinkedListNode<T>> EnumerateNodes<T>( this LinkedList<T> _list )
		{
			LinkedListNode<T> node = _list.First;
			while ( node != null )
			{
				yield return node;
				node = node.Next;
			}
		}
	}
}