using System;
using System.Collections.Generic;
using Pathfinding.Algorithm.Nodes;
using Pathfinding.Sets.ClosedSet;
using Pathfinding.Sets.OpenSet;
using World.Geometry;
using World.GraphData;

namespace Pathfinding.Algorithm
{
	public class PathfinderV1 : Pathfinder
	{
		public override Stack<PathNode> FindPath( Vector3i _startNode, Vector3i _endNode, IOpenSet _openSet, IClosedSet _closedSet )
		{
			_openSet.Add( new PathNode( World.NodeMap.GetClosestNode( _startNode ) ) );
			Vector3i endNodePos = World.NodeMap.GetClosestNode( _endNode ).Position;

			while ( _openSet.Any() )
			{
				PathNode currentNode = _openSet.Pop();

				if ( currentNode.Position == endNodePos )
				{
					return ReconstructPath( currentNode );
				}

				_closedSet.Add( currentNode );

				Node currentOrigin = World.NodeMap.GetClosestNode( currentNode.Position );

				foreach ( Vector3i neighbor in currentOrigin.Neighbors )
				{
					if ( _closedSet.Contains( neighbor ) )
					{
						continue;
					}

					Single gValue = currentNode.G + neighbor.Distance( currentNode.Position );

					if ( _openSet.Update( neighbor, gValue ) )
					{
						continue;
					}

					PathNode currentNeighbor = new PathNode( neighbor )
					{
						G = gValue, 
						H = GetH( neighbor, endNodePos )
					};

					currentNeighbor.F = gValue + currentNeighbor.H;
					currentNeighbor.Parent = currentNode;

					_openSet.Add( currentNeighbor );
				}
			}

			return null;
		}

		protected override Stack<PathNode> ReconstructPath( PathNode _lastNode )
		{
			Stack<PathNode> path = new Stack<PathNode>();
			PathNode current = _lastNode;
			while ( current != null )
			{
				path.Push( current );
				current = current.Parent;
			}

			return path;
		}

		protected override Single GetH( Vector3i _a, Vector3i _b )
		{
			return _a.Distance( _b );
		}

		public PathfinderV1( World.World _world ) : base( _world )
		{
		}
	}
}