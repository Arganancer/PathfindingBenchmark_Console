using System;
using World.GraphData;
using World.Logging;

namespace World
{
	public class World
	{
		public Int32 WorldWidth { get; }
		public Int32 WorldHeight { get; }
		public NodeMap NodeMap { get; private set; }

		public event EventHandler<LogEventArgs> NewLog;

		public event EventHandler<WorldSizeChangedEventArgs> WorldSizeChanged;

		public World( Int32 _worldWidth, Int32 _worldHeight )
		{
			WorldWidth = _worldWidth;
			WorldHeight = _worldHeight;
			OnWorldSizeChanged( new WorldSizeChangedEventArgs( WorldWidth, WorldHeight ) );
		}

		public void CreateWorld()
		{
			NodeMap = new NodeMap( WorldWidth, WorldHeight );
			NodeMap.NewLog += NewLog;
			NodeMap.InitializeMap();
		}

		protected void OnLog( LogEventArgs _e )
		{
			NewLog?.Invoke( this, _e );
		}

		protected void OnWorldSizeChanged( WorldSizeChangedEventArgs _e )
		{
			WorldSizeChanged?.Invoke( this, _e );
		}
	}
}