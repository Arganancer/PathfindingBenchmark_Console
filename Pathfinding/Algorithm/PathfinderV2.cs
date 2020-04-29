using System;
using System.Collections.Generic;
using Pathfinding.Algorithm.Nodes;
using Pathfinding.Sets.ClosedSet;
using World.Geometry;
using World.GraphData;

namespace Pathfinding.Algorithm
{
	public class PathfinderV2 : Pathfinder
	{
		public PathfinderV2( World.World _world ) : base( _world )
		{
		}

		public override Stack<PathNode> FindPath( Vector3i _startNode, Vector3i _endNode, IOpenSet _openSet, IClosedSet _closedSet )
		{
			_openSet.Add( new PathNode( World.NodeMap.GetClosestNode( _startNode ) ) );
			Vector3i endNodePos = World.NodeMap.GetClosestNode( _endNode ).Position;

			while ( _openSet.Any() )
			{
				PathNode currentNode = _openSet.Pop();

				Node currentGraphNode = World.NodeMap.GetClosestNode( currentNode.Position );

				_closedSet.Add( currentNode );

				foreach ( Vector3i neighbor in currentGraphNode.Neighbors )
				{
					if ( _closedSet.Contains( neighbor ) )
					{
						continue;
					}

					PathNode currentNeighbor = new PathNode( neighbor );

					if ( neighbor == endNodePos )
					{
						currentNeighbor.Parent = currentNode;
						return ReconstructPath( currentNeighbor );
					}

					Single gValue = currentNode.G + neighbor.Distance( currentNode.Position );

					if ( _openSet.Update( neighbor, gValue ) )
					{
						continue;
					}

					currentNeighbor.G = gValue;
					currentNeighbor.H = GetH( neighbor, endNodePos );
					currentNeighbor.F = currentNeighbor.G + currentNeighbor.H;

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
	}
}