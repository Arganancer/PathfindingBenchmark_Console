using System;
using Program.Log;

namespace Program.EventArguments
{
	public class NavigationTestEventArgs : EventArgs
	{
		public readonly NavTestLogItem NavTestLogItem;
		public readonly String Name;
		public readonly String OpenList;
		public readonly String ClosedList;

		public NavigationTestEventArgs( NavTestLogItem _navTestLogItem, String _name, String _openList, String _closedList )
		{
			NavTestLogItem = _navTestLogItem;
			Name = _name;
			OpenList = _openList;
			ClosedList = _closedList;
		}
	}
}