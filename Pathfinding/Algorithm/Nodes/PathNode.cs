using System;
using System.Collections.Generic;
using World.Geometry;
using World.GraphData;

namespace Pathfinding.Algorithm.Nodes
{
	[Serializable]
	public class PathNode : IComparable<PathNode>, IComparable<Vector3i>
	{
		public Vector3i Position { get; }
		public PathNode Parent { get; set; }
		public Single F { get; set; }
		public Single G { get; set; }
		public Single H { get; set; }

		public PathNode( Node _node ) => Position = _node.Position;

		public PathNode( Vector3i _position ) => Position = _position;

		public PathNode( Vector3i _position, Single _f, Single _g = 0, Single _h = 0 )
		{
			Position = _position;
			F = _f;
			G = _g;
			H = _h;
		}

		public PathNode( PathNode _pathNode )
		{
			Position = _pathNode.Position;
			Parent = _pathNode.Parent;
			F = _pathNode.F;
			G = _pathNode.G;
			H = _pathNode.H;
		}

		public override Int32 GetHashCode()
		{
			return Position.GetHashCode();
		}

		public Int32 CompareTo( Vector3i _other )
		{
			if ( ReferenceEquals( Position, _other ) )
			{
				return 0;
			}

			if ( ReferenceEquals( Position, _other ) )
			{
				return 1;
			}

			return Comparer<Vector3i>.Default.Compare( Position, _other );
		}

		public Int32 CompareTo( PathNode _other )
		{
			return CompareTo( _other.Position );
		}
	}
}