using System;

namespace ConsoleUI.Inputs.Mouse
{
	public class MouseMoveEventArgs : EventArgs
	{
		public Coord CurrentCoord { get; }
		public Coord PreviousCoord { get; }

		public MouseMoveEventArgs( Coord _currentCoord, Coord _previousCoord )
		{
			CurrentCoord = _currentCoord;
			PreviousCoord = _previousCoord;
		}
	}
}