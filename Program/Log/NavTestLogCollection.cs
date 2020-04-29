using System;
using System.Collections.Generic;
using System.Linq;
using Program.EventArguments;

namespace Program.Log
{
	public class NavTestLogCollection
	{
		public readonly String Name;
		public readonly String OpenList;
		public readonly String ClosedList;

		public String CurrentRangeName;

		public readonly List<NavTestLog> Logs;

		public NavTestLogCollection( String _name, String _openList, String _closedList )
		{
			Name = _name;
			OpenList = _openList;
			ClosedList = _closedList;
			Logs = new List<NavTestLog>();
		}

		public NavTestLog GetLogRange( String _range )
		{
			return Logs.First( _log => _log.RangeName == _range );
		}

		public void Add( NavTestLogItem _item )
		{
			Logs.First( _log => _log.RangeName == CurrentRangeName ).NavTestLogItems.Add( _item );
		}

		public void OnRangeUpdated( RangeUpdateEventArgs _rangeUpdateEventData )
		{
			CurrentRangeName = _rangeUpdateEventData.NewRangeText;
			Logs.Add( new NavTestLog( Name, OpenList, ClosedList, CurrentRangeName ) );
		}

		public Double TotalTime()
		{
			Double sum = 0;
			foreach ( NavTestLog log in Logs )
			{
				sum += log.NavTestLogItems.Sum( _logItem => _logItem.ExecutionTime );
			}

			return sum;
		}

		public Double AverageSpeed()
		{
			List<Double> times = new List<Double>();
			foreach ( NavTestLog log in Logs )
			{
				times.AddRange( log.NavTestLogItems.Select( _logItem => _logItem.ExecutionTime ).ToList() );
			}

			return times.Average();
		}

		public Double MinSpeed()
		{
			List<Double> times = new List<Double>();
			foreach ( NavTestLog log in Logs )
			{
				times.AddRange( log.NavTestLogItems.Select( _logItem => _logItem.ExecutionTime ).ToList() );
			}

			return times.Min();
		}

		public Double MaxSpeed()
		{
			List<Double> times = new List<Double>();
			foreach ( NavTestLog log in Logs )
			{
				times.AddRange( log.NavTestLogItems.Select( _logItem => _logItem.ExecutionTime ).ToList() );
			}

			return times.Max();
		}
	}
}