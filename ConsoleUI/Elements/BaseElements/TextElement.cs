using System;
using ConsoleUI.Settings;

namespace ConsoleUI.Elements.BaseElements
{
	public class TextElement : Element
	{
		private String m_Text;

		public String Text
		{
			get => m_Text;
			set
			{
				if ( SetProperty( ref m_Text, value ) )
				{
					if ( !UseHorizontalAnchors && Right == 0 )
					{
						Right = (UInt16)m_Text.Length;
					}
				}
			}
		}

		public TextElement() => m_Text = "";

		protected override void DrawIfDirty()
		{
			MainWindow.Window.AddContent( m_Text.PadRight( InnerWidth ), AbsoluteInnerTopLeft, ForegroundColor.GetColor(), BackgroundColor.GetColor(), ID, 0 );
		}
	}
}