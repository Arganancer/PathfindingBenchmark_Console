using System;
using System.Collections.Generic;
using System.Drawing;
using ConsoleUI.Settings;
using Program.EventArguments;
using World.Geometry;
using World.Logging;

namespace Program.Execution
{
	internal class Test
	{
		private static Int32 m_CurrentTestNumber;

		private readonly List<NavItem> m_NavItems;

		public event EventHandler<LogEventArgs> Log;

		public event EventHandler<NewTestEventArgs> NewTest;

		public Test( List<NavItem> _navItems )
		{
			m_NavItems = _navItems;
		}

		public void RunTest( Vector3i _startPos, Vector3i _endPos )
		{
			OnNewtest( new NewTestEventArgs( _startPos, _endPos, ++m_CurrentTestNumber ) );
			CreateTest( _startPos, _endPos );
		}

		protected void OnLog( LogItem _e )
		{
			Log?.Invoke( this, new LogEventArgs( _e ) );
		}

		protected void OnNewtest( NewTestEventArgs _e )
		{
			NewTest?.Invoke( this, _e );
		}

		private void CreateTest( Vector3i _startPos, Vector3i _endPos )
		{
			Guid id = Guid.NewGuid();
			foreach ( NavItem navItem in m_NavItems )
			{
				try
				{
					// https://docs.microsoft.com/en-us/dotnet/api/system.gc.trystartnogcregion?redirectedfrom=MSDN&view=netframework-4.8#overloads
					// The TryStartNoGCRegion(Int64) method attempts to place the garbage collector in no GC region latency mode,
					// which disallows garbage collection while an app executes a critical region of code.
					// If the runtime is unable to initially allocate the requested amount of memory,
					// the garbage collector performs a full blocking garbage collection in an attempt to free additional memory.
					// The garbage collector enters no GC region latency mode if it is able to allocate the required amount of memory,
					// which in this case is actually 2 * totalSize bytes
					// (it attempts to allocate totalSize bytes for the small object heap and totalSize bytes for the large object heap).
					GC.TryStartNoGCRegion( 2048 * 100000 );

					OnLog( new LogItem( id, $"Running Test #{m_CurrentTestNumber:N0} - {navItem.Name}.", ThemeColor.Secondary ) );
					navItem.RunTest( _startPos, _endPos );
				}
				finally
				{
					GC.EndNoGCRegion();
				}
			}

			OnLog( new LogItem( id, $"Running Test #{m_CurrentTestNumber:N0} - Done", ThemeColor.SecondaryDark ) );
		}
	}
}