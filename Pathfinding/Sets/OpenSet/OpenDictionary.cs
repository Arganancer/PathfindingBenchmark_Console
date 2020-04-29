using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding.Algorithm.Nodes;
using World.Geometry;

namespace Pathfinding.Sets.OpenSet
{
	public class OpenDictionary : OpenSet
	{
		private readonly Dictionary<Vector3i, PathNode> m_OpenDictionary = new Dictionary<Vector3i, PathNode>();

		public override Boolean Any() => m_OpenDictionary.Any();

		public OpenDictionary() => Structure = "Dictionary";


		public override void Add( PathNode _pathNode )
		{
			m_OpenDictionary.Add( _pathNode.Position, _pathNode );
		}

		public override PathNode Remove( PathNode _pathNode )
		{
			PathNode returnValue = m_OpenDictionary[_pathNode.Position];
			m_OpenDictionary.Remove( returnValue.Position );
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
			PathNode output = m_OpenDictionary.First().Value;
			foreach ( KeyValuePair<Vector3i, PathNode> pathNode in m_OpenDictionary )
			{
				if ( pathNode.Value.F < output.F )
				{
					output = pathNode.Value;
				}
			}

			Remove( output );
			return output;
		}

		public override Boolean Contains( Vector3i _position )
		{
			return m_OpenDictionary.ContainsKey( _position );
		}

		public override PathNode Get( Vector3i _position )
		{
			m_OpenDictionary.TryGetValue( _position, out PathNode pathNode );
			return pathNode;
		}

		public override void Clear()
		{
			m_OpenDictionary.Clear();
		}
	}
}