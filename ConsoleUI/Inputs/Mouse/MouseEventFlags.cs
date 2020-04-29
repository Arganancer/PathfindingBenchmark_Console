namespace ConsoleUI.Inputs.Mouse
{
	/// <summary>
	/// <see href="https://docs.microsoft.com/en-us/windows/console/mouse-event-record-str"/>
	/// </summary>
	public enum MouseEventFlags
	{
		None = 0x0000,
		DoubleClick = 0x0002,
		MouseHorizontalWheeled = 0x0008,
		MouseMoved = 0x0001,
		MouseWheeled = 0x0004
	}
}