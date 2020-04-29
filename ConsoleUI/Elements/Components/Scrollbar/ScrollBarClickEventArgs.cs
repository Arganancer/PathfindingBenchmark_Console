using System;

namespace ConsoleUI.Elements.Components.Scrollbar
{
	public class ScrollBarClickEventArgs : EventArgs
	{
		public UInt16 NewCurrentIndex { get; }

		public ScrollBarClickEventArgs( UInt16 _newCurrentIndex )
		{
			NewCurrentIndex = _newCurrentIndex;
		}
	}
}