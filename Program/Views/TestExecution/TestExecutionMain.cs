using ConsoleUI.Elements.Containers.GridContainer;
using Program.Views.TestExecution.SubViews;
using Program.Views.TestExecution.SubViews.TaskLog;

namespace Program.Views.TestExecution
{
	internal class TestExecutionMain
	{
		private readonly Main m_Main;

		public TestExecutionMain( Main _main )
		{
			m_Main = _main;
			InitWindow();
		}

		private void InitWindow()
		{
			Grid upperLeftGrid = new Grid();
			upperLeftGrid.AddChild( new WorldInformationView( m_Main ) { Right = 30, Bottom = 15, RightPadding = 1 }, 0, 0 );
			upperLeftGrid.AddChild( new AveragesView( m_Main ) { Right = 92, Bottom = 15, RightPadding = 1 }, 1, 0 );

			Grid bottomLeftGrid = new Grid();
			bottomLeftGrid.AddChild( new RangeStatsView( m_Main ) { AutoWidth = false, Right = 75, Bottom = 29, RightPadding = 1 }, 0, 0 );
			bottomLeftGrid.AddChild( new SingleTestResultView( m_Main ) { Right = 47, Bottom = 29, RightPadding = 1, ScrollBarWidth = 1 }, 1, 0 );

			Grid leftGrid = new Grid();
			leftGrid.AddChild( upperLeftGrid, 0, 0 );
			leftGrid.AddChild( bottomLeftGrid, 0, 1 );

			Grid mainGrid = new Grid();
			mainGrid.AddChild( leftGrid, 0, 0 );
			mainGrid.AddChild( new TaskLogView( m_Main ) { Right = 64, Bottom = 45, ScrollBarWidth = 1 }, 1, 0 );

			m_Main.MainWindow.AddChild( mainGrid );
		}
	}
}