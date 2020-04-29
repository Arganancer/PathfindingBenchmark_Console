using System.Threading;
using ConsoleUI;
using Program.Execution;
using Program.Log;
using Program.Views.TestExecution;
using TheWorld = World.World;

namespace Program
{
	internal class Main
	{
		public TheWorld World { get; }
		public TestManager TestManager { get; }
		public NavTestLogManager NavTestLogManager { get; }
		public MainWindow MainWindow { get; }
		public TestExecutionMain TestExecutionMain { get; }

		private Thread m_PathfindingThread;

		public Main()
		{
			World = new TheWorld( 3000, 3000 );
			TestManager = new TestManager( this );
			MainWindow = new MainWindow( 46, 189 );
			NavTestLogManager = new NavTestLogManager( this );
			TestExecutionMain = new TestExecutionMain( this );
		}

		public void Run()
		{
			MainWindow.Update();
			MainWindow.Draw();
			m_PathfindingThread = new Thread( RunPathfinding );
			m_PathfindingThread.Start();
			while ( true )
			{
				MainWindow.Update();
				MainWindow.Draw();
			}
		}

		private void RunPathfinding()
		{
			World.CreateWorld();
			TestManager.RunTests();
		}
	}
}