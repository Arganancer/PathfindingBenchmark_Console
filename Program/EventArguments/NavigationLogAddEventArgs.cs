using System;

namespace Program.EventArguments
{
	public class NavigationLogAddEventArgs : EventArgs
	{
		public readonly String Name;
		public readonly String OpenList;
		public readonly String ClosedList;

		public NavigationLogAddEventArgs( String _name, String _openList, String _closedList )
		{
			Name = _name;
			OpenList = _openList;
			ClosedList = _closedList;
		}
	}
}