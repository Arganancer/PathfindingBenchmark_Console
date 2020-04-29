using System;
using System.Collections.Generic;
using System.Linq;

namespace Program.Log
{
	public class NavTestLog
	{
		public NavTestLog( String _name, String _openList, String _closedList, String _rangeName )
		{
			Name = _name;
			OpenList = _openList;
			ClosedList = _closedList;
			RangeName = _rangeName;
			NavTestLogItems = new List<NavTestLogItem>();
		}

		public Double TotalTime()
		{
			return NavTestLogItems.Sum( _logItem => _logItem.ExecutionTime );
		}

		public Double AverageSpeed()
		{
			return NavTestLogItems.Average( _logItem => _logItem.ExecutionTime );
		}

		public Double MinSpeed()
		{
			return NavTestLogItems.Min( _logItem => _logItem.ExecutionTime );
		}

		public Double MaxSpeed()
		{
			return NavTestLogItems.Max( _logItem => _logItem.ExecutionTime );
		}

		public Int64 AverageBytesUsed()
		{
			return (Int64)NavTestLogItems.Average( _logItem => _logItem.BytesUsed );
		}

		public readonly String Name;
		public readonly String OpenList;
		public readonly String ClosedList;
		public readonly String RangeName;

		public readonly List<NavTestLogItem> NavTestLogItems;
	}
}