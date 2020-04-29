using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ConsoleUI.Inputs.Keyboard;
using ConsoleUI.Inputs.Mouse;

namespace ConsoleUI.Inputs
{
	/// <summary>
	/// Input record structure: https://docs.microsoft.com/en-us/windows/console/input-record-str
	/// </summary>
	// ReSharper disable once UseNameofExpression
	[DebuggerDisplay( "EventType: {EventType}" )]
	[StructLayout( LayoutKind.Explicit )]
	internal struct InputRecord
	{
		[FieldOffset( 0 )]
		public Int16 EventType;

		[FieldOffset( 4 )]
		public KeyEventRecord KeyEvent;

		[FieldOffset( 4 )]
		public MouseEventRecord MouseEvent;
	}
}