using System;

namespace World.Logging
{
	public class LogEventArgs : EventArgs
	{
		public LogItem LogItem { get; }

		public LogEventArgs( LogItem _logItem )
		{
			LogItem = _logItem;
		}
	}
}