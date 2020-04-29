using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ConsoleUI.Elements.BaseElements;
using ConsoleUI.Elements.Components.Border;
using ConsoleUI.Elements.Containers.GridContainer;
using ConsoleUI.Settings;
using Program.EventArguments;
using Program.Log;
using Program.Utilities;

namespace Program.Views.TestExecution
{
	internal class AveragesView : Grid
	{
		private readonly Main m_Main;
		private readonly List<NavTestAverage> m_Averages;

		public AveragesView( Main _main )
		{
			m_Main = _main;
			TopBorder = BorderStyle.Light;
			BottomBorder = BorderStyle.Light;
			LeftBorder = BorderStyle.Light;
			RightBorder = BorderStyle.Light;
			BorderColor = ThemeColor.Primary;
			m_Averages = new List<NavTestAverage>();

			m_Main.NavTestLogManager.NavigationLogAdd += OnLogAdded;
			UpdateRowTitles();
		}

		private void OnLogAdded( Object _sender, NavigationLogAddEventArgs _navigationLogAddEventArgs )
		{
			if ( m_Averages.All( _logAverage => _logAverage.Name != _navigationLogAddEventArgs.Name ) )
			{
				m_Averages.Add( new NavTestAverage( _navigationLogAddEventArgs.Name, _navigationLogAddEventArgs.OpenList, _navigationLogAddEventArgs.ClosedList ) );
			}

			NavTestAverage updatedNavTestAverage = m_Averages.First( _logAverage => _logAverage.Name == _navigationLogAddEventArgs.Name );
			updatedNavTestAverage.Recalculate( m_Main.NavTestLogManager.GetLog( updatedNavTestAverage.Name ) );

			UpdateAverages();
		}

		private void UpdateAverages()
		{
			if ( m_Averages.Count != m_Main.TestManager.NavItems.Count )
			{
				return;
			}

			for ( Int32 i = 0; i < m_Averages.Count; i++ )
			{
				UpdateAverage( (UInt16)( i + 1 ), m_Averages[i] );
			}
		}

		private void UpdateRowTitles()
		{
			for ( UInt16 column = 0; column < 5; column++ )
			{
				for ( UInt16 row = 0; row < 14; row++ )
				{
					AddChild( new TextElement { ForegroundColor = ThemeColor.PrimaryDark }, column, row );
				}
			}

			UpdateCellContent<TextElement>( 0, OpenListRowIndex, _textElement => _textElement.Text = "Open List:" );
			UpdateCellContent<TextElement>( 0, ClosedListRowIndex, _textElement => _textElement.Text = "Closed List:" );
			UpdateCellContent<TextElement>( 0, TotalTimeRowIndex, _textElement => _textElement.Text = "Total Time:" );
			UpdateCellContent<TextElement>( 0, AverageTimeRowIndex, _textElement => _textElement.Text = "Average Time:" );
			UpdateCellContent<TextElement>( 0, FastestTimeRowIndex, _textElement => _textElement.Text = "Fastest Time:" );
			UpdateCellContent<TextElement>( 0, SlowestTimeRowIndex, _textElement => _textElement.Text = "Slowest Time:" );
		}

		private void UpdateAverage( UInt16 _column, NavTestAverage _navTestAverage )
		{
			NavTestAverage baseline = m_Averages.FirstOrDefault( _logAverage => _logAverage.Name == "V2" );
			Boolean isBaseline = baseline == null || baseline.Name == _navTestAverage.Name;

			UpdateCellContent<TextElement>( _column, 0, _textElement =>
			{
				_textElement.Text = _navTestAverage.Name;
				_textElement.Right = SpaceBetweenColumns;
				_textElement.ForegroundColor = ThemeColor.VariantLight;
			} );

			UpdateCellContent<TextElement>( _column, OpenListRowIndex, _textElement =>
			{
				_textElement.Text = _navTestAverage.OpenList;
				_textElement.ForegroundColor = ThemeColor.Primary;
			} );
			UpdateCellContent<TextElement>( _column, ClosedListRowIndex, _textElement =>
			{
				_textElement.Text = _navTestAverage.ClosedList;
				_textElement.ForegroundColor = ThemeColor.Primary;
			} );

			ThemeColor totalTimeColor = AverageThemeUtils.GetColor( _navTestAverage.TotalTime, m_Averages.Select( _item => _item.TotalTime ), true );
			UpdateCellContent<TextElement>( _column, TotalTimeRowIndex, _textElement =>
			{
				_textElement.ForegroundColor = totalTimeColor;
				_textElement.Text = $"{_navTestAverage.TotalTime:N2}ms";
			} );

			ThemeColor averageColor = AverageThemeUtils.GetColor( _navTestAverage.AverageTime, m_Averages.Select( _item => _item.AverageTime ), true );
			UpdateCellContent<TextElement>( _column, AverageTimeRowIndex, _textElement =>
			{
				_textElement.ForegroundColor = averageColor;
				_textElement.Text = $"{_navTestAverage.AverageTime:N2}ms";
			} );

			ThemeColor fastestTimeColor = AverageThemeUtils.GetColor( _navTestAverage.FastestTime, m_Averages.Select( _item => _item.FastestTime ), true );
			UpdateCellContent<TextElement>( _column, FastestTimeRowIndex, _textElement =>
			{
				_textElement.ForegroundColor = fastestTimeColor;
				_textElement.Text = $"{_navTestAverage.FastestTime:N3}ms";
			} );

			ThemeColor slowestTimeColor = AverageThemeUtils.GetColor( _navTestAverage.SlowestTime, m_Averages.Select( _item => _item.SlowestTime ), true );
			UpdateCellContent<TextElement>( _column, SlowestTimeRowIndex, _textElement =>
			{
				_textElement.ForegroundColor = slowestTimeColor;
				_textElement.Text = $"{_navTestAverage.SlowestTime:N2}ms";
			} );

			if ( isBaseline )
			{
				return;
			}

			UpdateCellContent<TextElement>( _column, TotalTimeMultiplierRowIndex, _textElement =>
			{
				_textElement.ForegroundColor = totalTimeColor;
				_textElement.Text = GetComparison( _navTestAverage.TotalTime, baseline.TotalTime );
			} );
			UpdateCellContent<TextElement>( _column, AverageTimeMultiplierRowIndex, _textElement =>
			{
				_textElement.ForegroundColor = averageColor;
				_textElement.Text = GetComparison( _navTestAverage.AverageTime, baseline.AverageTime );
			} );
			UpdateCellContent<TextElement>( _column, FastestTimeMultiplierRowIndex, _textElement =>
			{
				_textElement.ForegroundColor = fastestTimeColor;
				_textElement.Text = GetComparison( _navTestAverage.FastestTime, baseline.FastestTime );
			} );
			UpdateCellContent<TextElement>( _column, SlowestTimeMultiplierRowIndex, _textElement =>
			{
				_textElement.ForegroundColor = slowestTimeColor;
				_textElement.Text = GetComparison( _navTestAverage.SlowestTime, baseline.SlowestTime );
			} );
		}

		private String GetComparison( Double _value, Double _baseline )
		{
			Double multiplier = _baseline / _value;
			return $"{multiplier:N2}";
		}

		internal class NavTestAverage
		{
			public String Name { get; }
			public String OpenList { get; }
			public String ClosedList { get; }

			public Double AverageTime { get; private set; }
			public Double TotalTime { get; private set; }
			public Double FastestTime { get; private set; }
			public Double SlowestTime { get; private set; }

			public NavTestAverage( String _name, String _openList, String _closedList )
			{
				Name = _name;
				OpenList = _openList;
				ClosedList = _closedList;
			}

			public void Recalculate( NavTestLogCollection _log )
			{
				AverageTime = _log.AverageSpeed();
				TotalTime = _log.TotalTime();
				FastestTime = _log.MinSpeed();
				SlowestTime = _log.MaxSpeed();
			}
		}

		private const UInt16 OpenListRowIndex = 1;
		private const UInt16 ClosedListRowIndex = 2;
		private const UInt16 TotalTimeRowIndex = 3;
		private const UInt16 TotalTimeMultiplierRowIndex = 4;
		private const UInt16 AverageTimeRowIndex = 6;
		private const UInt16 AverageTimeMultiplierRowIndex = 7;
		private const UInt16 FastestTimeRowIndex = 9;
		private const UInt16 FastestTimeMultiplierRowIndex = 10;
		private const UInt16 SlowestTimeRowIndex = 12;
		private const UInt16 SlowestTimeMultiplierRowIndex = 13;

		private const Int32 SpaceBetweenColumns = 18;
	}
}