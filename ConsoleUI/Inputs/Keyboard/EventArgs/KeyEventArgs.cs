using System;
using ConsoleUI.Inputs.Mouse;

namespace ConsoleUI.Inputs.Keyboard
{
	public class KeyEventArgs : EventArgs
	{
		public Boolean KeyDown { get; }
		public ConsoleKey Key { get; }
		public ControlKeyState ControleKeyState { get; }

		public KeyEventArgs( Boolean _keyDown, ConsoleKey _key, ControlKeyState _controleKeyState )
		{
			KeyDown = _keyDown;
			Key = _key;
			ControleKeyState = _controleKeyState;
		}
	}
}