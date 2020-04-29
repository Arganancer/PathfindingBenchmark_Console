using System;
using System.Drawing;
using ConsoleUI.Settings;

namespace World.Logging
{
	public class LogItem
	{
		public ThemeColor Color { get; set; }
		public String Log { get; set; }
		public Guid LogId { get; }

		public LogItem( Guid _logId, String _log, ThemeColor _color )
		{
			Color = _color;
			Log = _log;
			LogId = _logId;
		}
	}
}