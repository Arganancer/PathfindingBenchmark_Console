using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleUI.Elements.BaseElements;
using ConsoleUI.Elements.Components.Border;
using ConsoleUI.Elements.Containers.GridContainer;
using ConsoleUI.Settings;
using Program.EventArguments;
using Program.Log;
using Program.Utilities;

namespace Program.Views.TestExecution.SubViews
{
	internal class RangeStatsView : Grid
	{
		private readonly Main m_Main;
		private UInt16 m_CurrentRowIndex = 1;
		private Boolean m_HasPrintedNames;

		public RangeStatsView( Main _main )
		{
			m_Main = _main;
			TopBorder = BorderStyle.Light;
			BottomBorder = BorderStyle.Light;
			LeftBorder = BorderStyle.Light;
			RightBorder = BorderStyle.Light;
			BorderColor = ThemeColor.Primary;

			m_Main.NavTestLogManager.RangeCompleted += OnRangeCompleted;
		}

		public void AddNewRow( String _rangeName, List<NavTestLogCollection> _logs )
		{
			if ( !m_HasPrintedNames )
			{
				for ( Int32 i = 0; i < _logs.Count; i++ )
				{
					AddChild( new TextElement { Text = _logs[i].Name, Right = SpaceBetweenColumns, ForegroundColor = ThemeColor.VariantLight }, (UInt16)( i + 1 ), 0 );
				}

				m_HasPrintedNames = true;
			}

			AddChild( new TextElement { Text = _rangeName, Right = SpaceBetweenColumns - 1, ForegroundColor = ThemeColor.VariantLight }, 0, m_CurrentRowIndex );
			for ( Int32 i = 0; i < _logs.Count; i++ )
			{
				NavTestLog logRange = _logs[i].GetLogRange( _rangeName );
				AddChild( new TextElement { Text = $"{logRange.AverageSpeed():N4}ms", ForegroundColor = AverageThemeUtils.GetColor( logRange.AverageSpeed(), _logs.Select( _item => _item.GetLogRange( _rangeName ).AverageSpeed() ), true ) }, (UInt16)( i + 1 ), m_CurrentRowIndex );
			}

			++m_CurrentRowIndex;
		}

		private void OnRangeCompleted( Object _sender, RangeCompletedEventArgs _rangeCompletedEventArgs )
		{
			if ( _rangeCompletedEventArgs.OldRangeText != "" )
			{
				AddNewRow( _rangeCompletedEventArgs.OldRangeText, _rangeCompletedEventArgs.Logs );
			}
		}

		private const UInt16 SpaceBetweenColumns = 14;

		~RangeStatsView() => m_Main.NavTestLogManager.RangeCompleted -= OnRangeCompleted;
	}
}