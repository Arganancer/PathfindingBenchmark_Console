using System;
using System.Collections.Generic;
using ConsoleUI.Settings;
using World.Geometry;
using World.Logging;

namespace World.GraphData
{
	public class NodeMap
	{
		private readonly Int32 m_Width;
		private readonly Int32 m_Height;

		public event EventHandler<LogEventArgs> NewLog;

		internal NodeMap( Int32 _width, Int32 _height )
		{
			m_Width = _width;
			m_Height = _height;
			Nodes = new Node[m_Width, m_Height];
		}

		public Node GetClosestNode( Vector3i _pos )
		{
			return Nodes[_pos.X, _pos.Z];
		}

		internal void InitializeMap()
		{
			Single total = Nodes.GetLength( 0 ) * Nodes.GetLength( 1 );
			Single currentIteration = 0;
			Int32 lastPercentage = 0;

			Guid mapInitId = Guid.NewGuid();
			OnNewLog( new LogItem( mapInitId, $"Initializing world of size {m_Width:N0} by {m_Height:N0}", ThemeColor.VariantLight ) );
			for ( Int32 x = 0; x < Nodes.GetLength( 0 ); x++ )
			{
				for ( Int32 z = 0; z < Nodes.GetLength( 1 ); z++ )
				{
					Single currentPercentage = ( ++currentIteration / total ) * 100.0f;
					if ( (Int32)currentPercentage > lastPercentage )
					{
						lastPercentage = (Int32)currentPercentage;

						OnNewLog( new LogItem( mapInitId, $"Initializing world of size {m_Width:N0} by {m_Height:N0} - {100:N0}% - {currentPercentage:N0}%.", ThemeColor.VariantLight ) );
					}

					Nodes[x, z] = new Node( new Vector3i( x, 1, z ) );
				}
			}

			OnNewLog( new LogItem( mapInitId, $"Initializing world of size {m_Width:N0} by {m_Height:N0} - {100:N0}% - Done.", ThemeColor.VariantDark ) );

			ConnectPaths();
		}

		protected void OnNewLog( LogItem _logItem )
		{
			NewLog?.Invoke( this, new LogEventArgs( _logItem ) );
		}

		private void ConnectPaths()
		{
			Single total = Nodes.GetLength( 0 ) * Nodes.GetLength( 1 );
			Single currentIteration = 0;
			Int32 lastPercentage = 0;

			Guid connectionId = Guid.NewGuid();
			OnNewLog( new LogItem( connectionId, "Placing walls and connecting neighboring nodes.", ThemeColor.VariantLight ) );
			for ( Int32 x = 0; x < Nodes.GetLength( 0 ); x++ )
			{
				for ( Int32 z = 0; z < Nodes.GetLength( 1 ); z++ )
				{
					Single currentPercentage = ( ++currentIteration / total ) * 100.0f;
					if ( (Int32)currentPercentage > lastPercentage )
					{
						lastPercentage = (Int32)currentPercentage;
						OnNewLog( new LogItem( connectionId, $"Placing walls and connecting neighboring nodes - {currentPercentage:N0}%.", ThemeColor.VariantLight ) );
					}

					Nodes[x, z].Neighbors = GetNeighoringNodes( x, z );
				}
			}

			OnNewLog( new LogItem( connectionId, $"Placing walls and connecting neighboring nodes - {100:N0}% - Done.", ThemeColor.VariantDark ) );
		}

		private List<Vector3i> GetNeighoringNodes( Int32 _xIndex, Int32 _zIndex )
		{
			List<Vector3i> neighboringWaypoints = new List<Vector3i>();
			for ( Int32 x = -1; x <= 1; x++ )
			{
				for ( Int32 z = -1; z <= 1; z++ )
				{
					if ( _xIndex + x >= 0 &&
					     _xIndex + x < Nodes.GetLength( 0 ) &&
					     _zIndex + z >= 0 &&
					     _zIndex + z < Nodes.GetLength( 1 ) )
					{
						// Add vertical walls
						if ( ( ( _xIndex + 1 ) % 10 == 0 && x == 1 ) ||
						     ( ( _xIndex + 1 ) % 10 == 1 && x == -1 ) )
						{
							// Add Vertical Doors
							if ( ( _zIndex + 1 ) % 10 != 4 )
							{
								continue;
							}
						}

						// Add Horizontal walls
						if ( ( ( _zIndex + 1 ) % 10 == 0 && z == 1 ) ||
						     ( ( _zIndex + 1 ) % 10 == 1 && z == -1 ) )
						{
							// Add Horizontal Doors
							if ( ( _xIndex + 1 ) % 10 != 5 )
							{
								continue;
							}
						}

						if ( !( x == 0 && z == 0 ) )
						{
							neighboringWaypoints.Add( Nodes[_xIndex + x, _zIndex + z].Position );
						}
					}
				}
			}

			return neighboringWaypoints;
		}

		public readonly Node[,] Nodes;
	}
}