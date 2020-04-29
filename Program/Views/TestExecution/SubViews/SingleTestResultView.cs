using System;
using System.Drawing;
using ConsoleUI.Elements.BaseElements;
using ConsoleUI.Elements.Components.Border;
using ConsoleUI.Elements.Containers.ListContainer;
using ConsoleUI.Settings;
using Program.EventArguments;
using Program.Utilities;

namespace Program.Views.TestExecution.SubViews
{
	internal class SingleTestResultView : ScrollList
	{
		private readonly Main m_Main;

		public SingleTestResultView( Main _main )
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

			m_Main.TestManager.NewTest += OnNewTest;
			m_Main.TestManager.NavigationTestFinished += OnNavigationTestFinished;
		}

		private void OnNavigationTestFinished( Object _sender, NavigationTestEventArgs _e )
		{
			String log = $"{AddSpaces( _e.Name, 8 )}" +
			             $"Time: {AddSpaces( $"{_e.NavTestLogItem.ExecutionTime:N4}", 12 )}" +
			             //$"Memory: {AddSpaces( $"{_e.NavTestLogItem.BytesUsed.GetBytesUsedString()}", 10 )}";
			             $"Steps: {AddSpaces( $"{_e.NavTestLogItem.NbOfSteps}", 10 )}";

			AddChild( new TextElement { Text = log, ForegroundColor = ThemeColor.PrimaryDark }, Guid.NewGuid() );
		}

		private void OnNewTest( Object _sender, NewTestEventArgs _e )
		{
			String testText = $"Test #{_e.TestNumber:N0}";
			Int32 linesToAdd = InnerWidth - testText.Length - 1;
			String lines = "";
			for ( Int32 i = 0; i < linesToAdd; i++ )
			{
				lines += "-";
			}

			testText += lines;

			AddChild( new TextElement { Text = testText, ForegroundColor = ThemeColor.VariantLight }, Guid.NewGuid() );
		}

		private String AddSpaces( String _text, Int32 _desiredLength )
		{
			while ( _text.Length < _desiredLength )
			{
				_text += " ";
			}

			return _text;
		}

		~SingleTestResultView()
		{
			m_Main.TestManager.NewTest -= OnNewTest;
			m_Main.TestManager.NavigationTestFinished -= OnNavigationTestFinished;
		}
	}
}