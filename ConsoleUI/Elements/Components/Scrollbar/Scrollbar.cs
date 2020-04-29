using System;
using ConsoleUI.Elements.BaseElements;
using ConsoleUI.Elements.Containers.ListContainer;
using ConsoleUI.Inputs;
using ConsoleUI.Inputs.Mouse;
using ConsoleUI.Settings;

namespace ConsoleUI.Elements.Components.Scrollbar
{
	public sealed class ScrollBar : Element
	{
		private UInt32 m_NbOfElementRows;
		private UInt32 m_FirstVisibleElementPosition;
		private UInt32 m_LastVisibleElementPosition;
		private ListDirection m_ListDirection;
		private ThemeColor m_HoverForegroundColor;
		private Boolean m_IsMouseInside;
		private Coord m_MousePosition;
		private Boolean m_IsDragging;

		public ThemeColor HoverForegroundColor
		{
			get => m_HoverForegroundColor;
			set => SetProperty( ref m_HoverForegroundColor, value );
		}

		public UInt32 NbOfElementRows
		{
			get => m_NbOfElementRows;
			set => SetProperty( ref m_NbOfElementRows, value );
		}

		public UInt32 FirstVisibleElementPosition
		{
			get => m_FirstVisibleElementPosition;
			set => SetProperty( ref m_FirstVisibleElementPosition, value );
		}

		public UInt32 LastVisibleElementPosition
		{
			get => m_LastVisibleElementPosition;
			set => SetProperty( ref m_LastVisibleElementPosition, value );
		}

		public ListDirection ListDirection
		{
			get => m_ListDirection;
			set => SetProperty( ref m_ListDirection, value );
		}

		public event EventHandler<ScrollBarClickEventArgs> ScrollBarClick;

		public ScrollBar()
		{
			UseVerticalAnchors = true;
			TopAnchor = 0.0f;
			BottomAnchor = 1.0f;

			InputManager.MouseMove += OnMouseMove;
			InputManager.MouseButton += OnMouseButton;
		}

		protected override void DrawIfDirty()
		{
			Int32 startBlockElementVerticalPos = 0;
			Char startBlockElement = BlockElementChars.FullBlock;
			Int32 endBlockElementVerticalPos = InnerHeight;
			Char endBlockElement = BlockElementChars.EmptyBlock;
			if ( !( NbOfVisibleElements >= NbOfElementRows || NbOfElementRows == 0 ) )
			{
				Single startPercentage = (Single)FirstVisibleElementPosition / NbOfElementRows;
				Single relativeVerticalStartPos = InnerHeight * startPercentage;
				startBlockElementVerticalPos = (Int32)relativeVerticalStartPos;
				startBlockElement = BlockElementChars.GetChar( relativeVerticalStartPos );


				Single endPercentage = (Single)LastVisibleElementPosition / NbOfElementRows;
				Single relativeVerticalEndPos = InnerHeight * endPercentage;
				endBlockElementVerticalPos = (Int32)relativeVerticalEndPos + 1;
				endBlockElement = BlockElementChars.GetChar( relativeVerticalEndPos );
			}

			for ( UInt16 y = AbsoluteInnerTopLeft.Y; y <= AbsoluteInnerBottomRight.Y; y++ )
			{
				ThemeColor foregroundColor = m_IsMouseInside || m_IsDragging ? HoverForegroundColor : ForegroundColor;
				ThemeColor backgroundColor = BackgroundColor;
				Char currentBlockElement = BlockElementChars.EmptyBlock;
				if ( y == startBlockElementVerticalPos + AbsoluteInnerTopLeft.Y )
				{
					currentBlockElement = startBlockElement;
				}
				else if ( y == endBlockElementVerticalPos + AbsoluteInnerTopLeft.Y )
				{
					foregroundColor = BackgroundColor;
					backgroundColor = m_IsMouseInside || m_IsDragging ? HoverForegroundColor : ForegroundColor;
					currentBlockElement = endBlockElement;
				}
				else if ( y > startBlockElementVerticalPos + AbsoluteInnerTopLeft.Y && y < endBlockElementVerticalPos + AbsoluteInnerTopLeft.Y )
				{
					currentBlockElement = BlockElementChars.FullBlock;
				}

				MainWindow.Window.AddContent( "".PadRight( InnerWidth, currentBlockElement ), new Coord( AbsoluteInnerTopLeft.X, y ), foregroundColor.GetColor(), backgroundColor.GetColor(), ID, 0 );
			}
		}

		private void OnMouseButton( Object _sender, MouseButtonEventArgs _e )
		{
			if ( _e.ButtonState.HasFlag( MouseButtonState.Left ) )
			{
				if ( m_IsMouseInside )
				{
					m_IsDragging = true;
					DragScrollBar();
				}
			}
			else
			{
				m_IsDragging = false;
			}
		}

		private void OnMouseMove( Object _sender, MouseMoveEventArgs _e )
		{
			m_MousePosition = _e.CurrentCoord;
			Boolean mouseIsInside = ContainsCoord( m_MousePosition );

			if ( m_IsMouseInside != mouseIsInside )
			{
				m_IsMouseInside = mouseIsInside;
				SetDirty();
			}

			if ( m_IsDragging )
			{
				DragScrollBar();
			}
		}

		private void DragScrollBar()
		{
			Single verticalMousePositionPercentage = ( (Single)m_MousePosition.Y - AbsoluteInnerTopLeft.Y ) / InnerHeight;
			Int32 newIndex = (UInt16)( ( NbOfElementRows * verticalMousePositionPercentage ) + ( NbOfVisibleElements * 0.5f ) );
			newIndex = (Int32)Math.Min( Math.Max( newIndex, NbOfVisibleElements - 1 ), NbOfElementRows - 1 );
			OnScrollBarClick( new ScrollBarClickEventArgs( (UInt16)newIndex ) );
			SetDirty();
		}

		private void OnScrollBarClick( ScrollBarClickEventArgs _e )
		{
			ScrollBarClick?.Invoke( this, _e );
		}

		private UInt32 NbOfVisibleElements => m_LastVisibleElementPosition - m_FirstVisibleElementPosition;
	}
}