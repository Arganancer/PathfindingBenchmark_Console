using System;
using System.Diagnostics;

namespace ConsoleUI.Inputs.Mouse
{
// ReSharper disable InconsistentNaming
#pragma warning disable 649
	/// <summary>
	/// Mouse event record structure: https://docs.microsoft.com/en-us/windows/console/mouse-event-record-str
	/// </summary>
	[DebuggerDisplay( "{dwMousePosition.X}, {dwMousePosition.Y}" )]
	internal struct MouseEventRecord
	{
		public Coord dwMousePosition;
		public Int32 dwButtonState;
		public Int32 dwControlKeyState;
		public Int32 dwEventFlags;
	}
#pragma warning restore 649
}