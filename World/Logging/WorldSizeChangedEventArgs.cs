using System;

namespace World.Logging
{
	public class WorldSizeChangedEventArgs : EventArgs
	{
		public Int32 WorldWidth { get; }
		public Int32 WorldHeight { get; }

		public WorldSizeChangedEventArgs( Int32 _worldWidth, Int32 _worldHeight )
		{
			WorldWidth = _worldWidth;
			WorldHeight = _worldHeight;
		}
	}
}