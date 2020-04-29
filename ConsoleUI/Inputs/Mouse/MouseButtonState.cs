using System;

namespace ConsoleUI.Inputs.Mouse
{
	/// <summary>
	/// <see href="https://docs.microsoft.com/en-us/windows/console/mouse-event-record-str"/>
	/// </summary>
	[Flags]
	public enum MouseButtonState
	{
		None = 0x0000,
		Left = 0x0001,
		Middle = 0x0004,
		ThirdFromLeft = 0x0008,
		FourthFromLeft = 0x0010,
		Right = 0x0002,
		MouseWheelScrollUp = 0x780000,
		MouseWheelScrollDown = -0x780000 // 0xFF880000
	}
}