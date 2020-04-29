using System;

namespace Program.EventArguments
{
	public class RangeUpdateEventArgs : EventArgs
	{
		public String OldRangeText { get; }
		public String NewRangeText { get; }

		public RangeUpdateEventArgs( String _oldRangeText, String _newRangeText )
		{
			OldRangeText = _oldRangeText;
			NewRangeText = _newRangeText;
		}
	}
}