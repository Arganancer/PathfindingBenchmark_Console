using System;
using System.Collections.Generic;
using System.Drawing;
using ConsoleUI.Settings;
using Pathfinding.Algorithm;
using Pathfinding.Sets.ClosedSet;
using Pathfinding.Sets.OpenSet;
using Program.EventArguments;
using Program.Utilities;
using World.Geometry;
using World.Logging;

namespace Program.Execution
{
	internal class TestManager
	{
		private static readonly Int32[] DistanceRanges = { 3, 5, 10, 18, 25, 50, 75, 100, 150, 250, 400, 600, 900, 1200, 1500, 2000, 2500, 3000 };

		private Int32 m_CurrentIteration;
		private readonly Int32 m_NbOfIterationsPerRange;
		private Int32 m_CurrentMinimum = DistanceRanges[0];
		private Int32 m_CurrentMaximum = DistanceRanges[1];
		private Int32 m_CurrentMaxRangeIndex = 1;
		private readonly Main m_Main;
		public Int32 TotalIterations => m_NbOfIterationsPerRange * ( DistanceRanges.Length - 1 );
		public List<NavItem> NavItems { get; private set; }

		public event EventHandler<LogEventArgs> NewLog;
		public event EventHandler<NewTestEventArgs> NewTest;
		public event EventHandler<RangeUpdateEventArgs> RangeUpdated;
		public event EventHandler<NavigationTestEventArgs> NavigationTestFinished;
		public event EventHandler<Int32> TotalIterationsChanged;
		public event EventHandler<Int32> CurrentIterationChanged;

		public TestManager( Main _main )
		{
			m_Main = _main;
			m_NbOfIterationsPerRange = 30;
			OnTotalIterationsChanged( TotalIterations );
		}

		public void RunTests()
		{
			// Version 1
			//NavItems = new List<NavItem>
			//{
			//	new NavItem( new OpenDictionary(), new ClosedHashSet(), "V1", new PathfinderV1( m_Main.World ) ),
			//	new NavItem( new OpenPriorityQueueDictionary(), new ClosedHashSet(), "V2", new PathfinderV1( m_Main.World ) ),
			//	new NavItem( new OpenAVLTree(), new ClosedHashSet(), "V3", new PathfinderV1( m_Main.World ) ),
			//	new NavItem( new OpenAVLTreeDictionary(), new ClosedHashSet(), "V4", new PathfinderV1( m_Main.World ) ),
			//};

			// Version 2
			NavItems = new List<NavItem>
			{
				//new NavItem( new OpenList(), new ClosedList(), "V1", new PathfinderV1( m_Main.World ) ),
				//new NavItem( new OpenList(), new ClosedHashSet(), "V2", new PathfinderV1( m_Main.World ) ),
				new NavItem( new OpenAVLTree(), new ClosedHashSet(), "V2", new PathfinderV1( m_Main.World ) ),
				new NavItem( new OpenAVLTreeDictionary(), new ClosedHashSet(), "V3", new PathfinderV1( m_Main.World ) ),
			};

			foreach ( NavItem navItem in NavItems )
			{
				navItem.NavigationTestFinished += NavigationTestFinished;
			}

			Guid gdID = Guid.NewGuid();
			OnLog( new LogItem( gdID, "Waiting for GC.", ThemeColor.SecondaryLight ) );
			GC.Collect();
			GC.WaitForPendingFinalizers();
			OnLog( new LogItem( gdID, "GC Done.", ThemeColor.SecondaryDark ) );
			OnLog( new LogItem( Guid.NewGuid(), $"Updated Euclidean Distance Range: {m_CurrentMinimum:N0} - {m_CurrentMaximum:N0}", ThemeColor.VariantLight ) );
			CurrentRangeName = $"{m_CurrentMinimum:N0} - {m_CurrentMaximum:N0}";
			OnRangeUpdated( new RangeUpdateEventArgs( "", CurrentRangeName ) );
			OnLog( new LogItem( Guid.NewGuid(), "Starting Random Tests.", ThemeColor.VariantLight ) );

			while ( UpdateRange() )
			{
				OnCurrentIterationChanged( m_CurrentIteration );
				Test test = new Test( NavItems );
				( Vector3i start, Vector3i end ) = Vector3IUtils.GenerateStartEndPositions(
					m_Main.World.WorldWidth,
					m_Main.World.WorldHeight,
					m_CurrentMinimum,
					m_CurrentMaximum );
				test.Log += NewLog;
				test.NewTest += NewTest;
				test.RunTest( start, end );
			}
		}

		protected void OnRangeUpdated( RangeUpdateEventArgs _e )
		{
			RangeUpdated?.Invoke( this, _e );
		}

		protected void OnLog( LogItem _logItem )
		{
			NewLog?.Invoke( this, new LogEventArgs( _logItem ) );
		}

		protected void OnNewTest( NewTestEventArgs _e )
		{
			NewTest?.Invoke( this, _e );
		}

		protected void OnTotalIterationsChanged( Int32 _e )
		{
			TotalIterationsChanged?.Invoke( this, _e );
		}

		protected void OnCurrentIterationChanged( Int32 _e )
		{
			CurrentIterationChanged?.Invoke( this, _e );
		}

		private Boolean UpdateRange()
		{
			if ( ++m_CurrentIteration > NextIterationSwitch )
			{
				if ( DistanceRanges.Length - 1 == m_CurrentMaxRangeIndex )
				{
					OnRangeUpdated( new RangeUpdateEventArgs( $"{m_CurrentMinimum:N0} - {m_CurrentMaximum:N0}", "Finished." ) );
					OnLog( new LogItem( Guid.NewGuid(), "Finished running tests.", ThemeColor.VariantLight ) );
					return false;
				}

				String oldRangeText = $"{m_CurrentMinimum:N0} - {m_CurrentMaximum:N0}";

				m_CurrentMinimum = m_CurrentMaximum;
				m_CurrentMaximum = DistanceRanges[++m_CurrentMaxRangeIndex];

				CurrentRangeName = $"{m_CurrentMinimum:N0} - {m_CurrentMaximum:N0}";

				OnRangeUpdated( new RangeUpdateEventArgs( oldRangeText, CurrentRangeName ) );
				OnLog( new LogItem( Guid.NewGuid(), $"Updated Euclidean Distance Range: {CurrentRangeName}", ThemeColor.VariantLight ) );
			}

			return true;
		}

		private Int32 NextIterationSwitch => m_CurrentMaxRangeIndex * m_NbOfIterationsPerRange;

		// Test Info
		public static readonly Random Rand = new Random();
		public static String CurrentRangeName = "";

		~TestManager()
		{
			foreach ( NavItem navItem in NavItems )
			{
				navItem.NavigationTestFinished -= NavigationTestFinished;
			}
		}
	}
}