using System;
using ConsoleUI.Elements.BaseElements;
using ConsoleUI.Elements.Components.Border;
using ConsoleUI.Elements.Containers.GridContainer;
using ConsoleUI.Settings;
using Program.EventArguments;
using World.Logging;

namespace Program.Views.TestExecution
{
	internal class WorldInformationView : Grid
	{
		private readonly Main m_Main;
		private readonly TextElement WorldWidth;
		private readonly TextElement WorldHeight;
		private readonly TextElement TotalNodes;
		private readonly TextElement RandomIterations;
		private readonly TextElement CurrentRange;
		private Int32 m_CurrentIteration;
		private Int32 m_TotalIterations;

		public WorldInformationView( Main _main )
		{
			m_Main = _main;

			TopBorder = BorderStyle.Light;
			BottomBorder = BorderStyle.Light;
			LeftBorder = BorderStyle.Light;
			RightBorder = BorderStyle.Light;
			BorderColor = ThemeColor.Primary;

			AddChild( new TextElement { Text = "World Width:", ForegroundColor = ThemeColor.PrimaryDark, Bottom = 2 }, 0, 0 );
			AddChild( new TextElement { Text = "World Height:", ForegroundColor = ThemeColor.PrimaryDark, Bottom = 2 }, 0, 1 );
			AddChild( new TextElement { Text = "Total Nodes:", ForegroundColor = ThemeColor.PrimaryDark, Bottom = 2 }, 0, 2 );
			AddChild( new TextElement { Text = "Iterations:", ForegroundColor = ThemeColor.PrimaryDark, Bottom = 2 }, 0, 3 );
			AddChild( new TextElement { Text = "Current Range:", ForegroundColor = ThemeColor.PrimaryDark, Bottom = 2 }, 0, 4 );

			WorldWidth = new TextElement { ForegroundColor = ThemeColor.Primary };
			WorldHeight = new TextElement { ForegroundColor = ThemeColor.Primary };
			TotalNodes = new TextElement { ForegroundColor = ThemeColor.Primary };
			RandomIterations = new TextElement { ForegroundColor = ThemeColor.Primary };
			CurrentRange = new TextElement { ForegroundColor = ThemeColor.Primary };

			AddChild( WorldWidth, 1, 0 );
			AddChild( WorldHeight, 1, 1 );
			AddChild( TotalNodes, 1, 2 );
			AddChild( RandomIterations, 1, 3 );
			AddChild( CurrentRange, 1, 4 );

			_main.TestManager.RangeUpdated += OnRangeUpdated;
			_main.TestManager.TotalIterationsChanged += OnTotalIterationsChanged;
			OnTotalIterationsChanged( this, _main.TestManager.TotalIterations );
			_main.TestManager.CurrentIterationChanged += OnCurrentIterationChanged;

			_main.World.WorldSizeChanged += OnWorldSizeChanged;
			OnWorldSizeChanged( this, new WorldSizeChangedEventArgs( _main.World.WorldWidth, _main.World.WorldHeight ) );
		}

		private void OnWorldSizeChanged( Object _sender, WorldSizeChangedEventArgs _worldSizeChangedEventArgs )
		{
			WorldWidth.Text = $"{_worldSizeChangedEventArgs.WorldWidth:N0}";
			WorldHeight.Text = $"{_worldSizeChangedEventArgs.WorldHeight:N0}";
			TotalNodes.Text = $"{_worldSizeChangedEventArgs.WorldWidth * _worldSizeChangedEventArgs.WorldHeight:N0}";
		}

		private void OnTotalIterationsChanged( Object _sender, Int32 _totalIterations )
		{
			m_TotalIterations = _totalIterations;
			RandomIterations.Text = $"{m_CurrentIteration}/{m_TotalIterations}";
		}

		private void OnCurrentIterationChanged( Object _sender, Int32 _currentIteration )
		{
			m_CurrentIteration = _currentIteration;
			RandomIterations.Text = $"{m_CurrentIteration}/{m_TotalIterations}";
		}

		private void OnRangeUpdated( Object _sender, RangeUpdateEventArgs _rangeUpdateEventArgs )
		{
			if ( !_rangeUpdateEventArgs.NewRangeText.Contains( "Finished", StringComparison.OrdinalIgnoreCase ) )
			{
				CurrentRange.Text = $"{_rangeUpdateEventArgs.NewRangeText}";
			}
		}
	}
}