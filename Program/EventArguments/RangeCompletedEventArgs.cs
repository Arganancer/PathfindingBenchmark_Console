using System;
using System.Collections.Generic;
using Program.Log;

namespace Program.EventArguments
{
	public class RangeCompletedEventArgs : EventArgs
	{
		public readonly String OldRangeText;
		public readonly List<NavTestLogCollection> Logs;

		public RangeCompletedEventArgs( String _oldRangeText, List<NavTestLogCollection> _logs )
		{
			OldRangeText = _oldRangeText;
			Logs = _logs;
		}
	}
}