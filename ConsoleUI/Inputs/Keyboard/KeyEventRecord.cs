using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace ConsoleUI.Inputs.Keyboard
{
	/// <summary>
	/// Key event record structure: https://docs.microsoft.com/en-us/windows/console/key-event-record-str
	/// </summary>
	// ReSharper disable once UseNameofExpression
	[DebuggerDisplay( "KeyCode: {wVirtualKeyCode}" )]
	[StructLayout( LayoutKind.Explicit )]
	internal struct KeyEventRecord
	{
		[FieldOffset( 0 )]
		[MarshalAs( UnmanagedType.Bool )]
		public Boolean bKeyDown;

		[FieldOffset( 4 )]
		public UInt16 wRepeatCount;

		[FieldOffset( 6 )]
		public UInt16 wVirtualKeyCode;

		[FieldOffset( 8 )]
		public UInt16 wVirtualScanCode;

		[FieldOffset( 10 )]
		public Char UnicodeChar;

		[FieldOffset( 10 )]
		public Byte AsciiChar;

		[FieldOffset( 12 )]
		public Int32 dwControlKeyState;
	}
}