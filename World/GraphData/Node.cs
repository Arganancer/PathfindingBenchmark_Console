using System.Collections.Generic;
using World.Geometry;

namespace World.GraphData
{
	public class Node
	{
		public List<Vector3i> Neighbors { get; set; }
		public Vector3i Position { get; set; }

		public Node( Vector3i _pos )
		{
			Position = _pos;
		}
	}
}