using System;
using System.Collections.Generic;
using System.Linq;
using Program.EventArguments;
using Program.Execution;

namespace Program.Log
{
	internal class NavTestLogManager
	{
		private readonly Main m_Main;

		public event EventHandler<RangeCompletedEventArgs> RangeCompleted;

		public event EventHandler<NavigationLogAddEventArgs> NavigationLogAdd;

		public NavTestLogManager( Main _main )
		{
			m_Main = _main;
			Logs = new List<NavTestLogCollection>();
			m_Main.TestManager.NavigationTestFinished += OnNavTestFinished;
			m_Main.TestManager.RangeUpdated += OnRangeUpdated;
		}

		public NavTestLogCollection GetLog( String _name )
		{
			return Logs.First( _log => _log.Name == _name );
		}

		public NavTestLog GetLogRange( String _name, String _range )
		{
			return Logs.First( _log => _log.Name == _name ).Logs.First( _log => _log.RangeName == _range );
		}

		public void OnRangeUpdated( Object? _sender, RangeUpdateEventArgs _rangeUpdateEventArgs )
		{
			foreach ( NavTestLogCollection log in Logs )
			{
				log.OnRangeUpdated( _rangeUpdateEventArgs );
			}

			OnRangeCompleted( new RangeCompletedEventArgs( _rangeUpdateEventArgs.OldRangeText, Logs ) );
		}

		protected virtual void OnRangeCompleted( RangeCompletedEventArgs _e )
		{
			RangeCompleted?.Invoke( this, _e );
		}

		protected virtual void OnNavigationLogAdd( NavigationLogAddEventArgs _e )
		{
			NavigationLogAdd?.Invoke( this, _e );
		}

		private void OnNavTestFinished( Object _sender, NavigationTestEventArgs _navigationTestEventArgs )
		{
			if ( Logs.All( _log => _log.Name != _navigationTestEventArgs.Name ) )
			{
				NavTestLogCollection newCollection = new NavTestLogCollection( _navigationTestEventArgs.Name, "", "" );
				newCollection.OnRangeUpdated( new RangeUpdateEventArgs( "", TestManager.CurrentRangeName ) );
				Logs.Add( newCollection );
			}

			NavTestLogCollection navTestLog = Logs.First( _log => _log.Name == _navigationTestEventArgs.Name );
			navTestLog.Add( _navigationTestEventArgs.NavTestLogItem );

			OnNavigationLogAdd( new NavigationLogAddEventArgs( _navigationTestEventArgs.Name, _navigationTestEventArgs.OpenList, _navigationTestEventArgs.ClosedList ) );
		}

		public readonly List<NavTestLogCollection> Logs;

		~NavTestLogManager()
		{
			m_Main.TestManager.NavigationTestFinished -= OnNavTestFinished;
			m_Main.TestManager.RangeUpdated -= OnRangeUpdated;
		}
	}
}