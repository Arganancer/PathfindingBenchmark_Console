using System;
using System.Collections.Generic;
using System.Diagnostics;
using Pathfinding.Algorithm;
using Pathfinding.Algorithm.Nodes;
using Pathfinding.Sets.ClosedSet;
using Pathfinding.Sets.OpenSet;
using Program.EventArguments;
using Program.Log;
using Program.Utilities;
using World.Geometry;

namespace Program.Execution
{
	public class NavItem
	{
		private readonly OpenSet m_OpenSet;
		private readonly ClosedSet m_ClosedSet;
		private readonly Pathfinder m_Pathfinder;
		public readonly String Name;

		public event EventHandler<NavigationTestEventArgs> NavigationTestFinished;

		public NavItem( OpenSet _openSet, ClosedSet _closedSet, String _name, Pathfinder _pathfinder )
		{
			Name = _name;
			m_OpenSet = _openSet;
			m_ClosedSet = _closedSet;
			m_Pathfinder = _pathfinder;

			m_Pathfinder.FindPath( new Vector3i( 0, 0, 0 ), new Vector3i( 15, 15, 15 ), _openSet, _closedSet );
		}

		public void RunTest( Vector3i _startPos, Vector3i _endPos )
		{
			//Int64 preTestMemory = GC.GetTotalMemory( false );
			Stopwatch watch = Stopwatch.StartNew();
			Stack<PathNode> path = m_Pathfinder.FindPath( _startPos, _endPos, m_OpenSet, m_ClosedSet );
			watch.Stop();
			//Int64 postTestMemory = GC.GetTotalMemory( false );

			//Int64 bytesUsed = MemoryUsage.SizeInBytes( m_OpenSet, m_ClosedSet );

			m_OpenSet.Clear();
			m_ClosedSet.Clear();

			NavigationTestFinished?.Invoke(
				this,
				new NavigationTestEventArgs(
					new NavTestLogItem( watch.Elapsed.TotalMilliseconds, path.Count, 0 /*bytesUsed*/ ),
					Name,
					m_OpenSet.Structure,
					m_ClosedSet.Structure ) );
		}
	}
}