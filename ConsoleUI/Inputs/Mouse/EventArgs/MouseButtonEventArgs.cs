using System;

namespace ConsoleUI.Inputs.Mouse
{
	public class MouseButtonEventArgs : EventArgs
	{
		public MouseButtonState ButtonState { get; }
		public MouseEventFlags EventFlags { get; }
		public ControlKeyState KeyState { get; }
		public MouseButtonEventArgs( MouseButtonState _buttonState, MouseEventFlags _eventFlags, ControlKeyState _keyState )
		{
			ButtonState = _buttonState;
			EventFlags = _eventFlags;
			KeyState = _keyState;
		}
	}
}