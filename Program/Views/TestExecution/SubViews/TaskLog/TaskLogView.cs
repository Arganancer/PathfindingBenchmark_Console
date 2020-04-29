using System;
using System.Drawing;
using System.Linq;
using ConsoleUI.Elements.BaseElements;
using ConsoleUI.Elements.Components.Border;
using ConsoleUI.Elements.Containers.ListContainer;
using ConsoleUI.Settings;
using World.Logging;

namespace Program.Views.TestExecution.SubViews.TaskLog
{
	internal class TaskLogView : ScrollList
	{
		private readonly Main m_Main;

		public TaskLogView( Main _main )
		{
			m_Main = _main;
			TopBorder = BorderStyle.Light;
			BottomBorder = BorderStyle.Light;
			LeftBorder = BorderStyle.Light;
			RightBorder = BorderStyle.Light;
			BorderColor = ThemeColor.Primary;

			ScrollBarBackgroundColor = ThemeColor.PrimaryDark;
			ScrollBarForegroundColor = ThemeColor.Primary;
			ScrollBarHoverForegroundColor = ThemeColor.PrimaryLight;

			ScrollSkipInterval = 5;

			m_Main.World.NewLog += OnNewLog;
			m_Main.TestManager.NewLog += OnNewLog;
		}

		private void OnNewLog( Object _sender, LogEventArgs _e )
		{
			if ( !UpdateListItem( _e.LogItem.LogId, _item =>
			{
				if ( !( _item.Children.FirstOrDefault() is TextElement textElement ) )
				{
					return;
				}

				textElement.Text = _e.LogItem.Log;
				textElement.ForegroundColor = _e.LogItem.Color;
			} ) )
			{
				AddChild( new TextElement
				{
					Text = _e.LogItem.Log,
					ForegroundColor = _e.LogItem.Color,
					RightAnchor = 1.0f,
					BottomAnchor = 1.0f,
					UseHorizontalAnchors = true,
					UseVerticalAnchors = true
				}, _e.LogItem.LogId );
			}
		}

		~TaskLogView()
		{
			m_Main.World.NewLog -= OnNewLog;
			m_Main.TestManager.NewLog -= OnNewLog;
		}
	}
}