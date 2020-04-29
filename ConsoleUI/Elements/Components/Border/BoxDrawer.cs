using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ConsoleUI.Elements.Containers;
using ConsoleUI.Settings;

namespace ConsoleUI.Elements.Components.Border
{
	internal static class BoxDrawer
	{
		private static readonly Guid ID = Guid.NewGuid();
		private static List<Border> m_Borders = new List<Border>();

		private static (BorderStyle style, ThemeColor foregroundColor, ThemeColor backgroundColor)[,] m_WindowGrid;
		private static Int32 m_WindowWidth;
		private static Int32 m_WindowHeight;
		public static Boolean IsDirty { get; private set; }

		public static void SetWindowSize( Int32 _windowWidth, Int32 _windowHeight )
		{
			m_WindowWidth = _windowWidth;
			m_WindowHeight = _windowHeight;
			m_WindowGrid = new (BorderStyle, ThemeColor, ThemeColor)[_windowWidth, _windowHeight];
		}

		public static void UpdateBorder( Border _border )
		{
			if ( !m_Borders.Contains( _border ) )
			{
				m_Borders.Add( _border );
			}

			IsDirty = true;
		}

		public static void RemoveBorder( Border _border )
		{
			if ( !m_Borders.Remove( _border ) )
			{
				IsDirty = true;
			}
		}

		public static void DrawBoxes( IEnumerable<ContainerElement> _layoutElements )
		{
			m_Borders = _layoutElements.SelectMany( _layoutElement => _layoutElement.GetBorders() ).ToList();
			DrawBoxes();
		}

		public static void DrawBoxes()
		{
			if ( !IsDirty )
			{
				return;
			}

			RefreshGridMapping();
			DrawGrid();

			IsDirty = false;
		}

		private static void DrawGrid()
		{
			MainWindow.Window.ClearElement( ID, 0 );
			for ( UInt16 x = 0; x < m_WindowGrid.GetLength( 0 ); x++ )
			{
				for ( UInt16 y = 0; y < m_WindowGrid.GetLength( 1 ); y++ )
				{
					( BorderStyle style, ThemeColor foregroundColor, ThemeColor backgroundColor ) = m_WindowGrid[x, y];
					if ( style == BorderStyle.None )
					{
						continue;
					}

					MainWindow.Window.AddContent( GetBoxChar( x, y ), new Coord( x, y ), foregroundColor.GetColor(), backgroundColor.GetColor(), ID, 0 );
				}
			}
		}

		private static Char GetBoxChar( Int32 _xPos, Int32 _yPos )
		{
			BorderStyle current = m_WindowGrid[_xPos, _yPos].style;
			BorderStyle left = _xPos > 0 ? m_WindowGrid[_xPos - 1, _yPos].style : BorderStyle.None;
			BorderStyle right = _xPos + 1 < m_WindowWidth ? m_WindowGrid[_xPos + 1, _yPos].style : BorderStyle.None;

			BorderStyle up = _yPos > 0 ? m_WindowGrid[_xPos, _yPos - 1].style : BorderStyle.None;
			BorderStyle down = _yPos + 1 < m_WindowHeight ? m_WindowGrid[_xPos, _yPos + 1].style : BorderStyle.None;

			if ( left != BorderStyle.None )
			{
				if ( right != BorderStyle.None )
				{
					if ( up != BorderStyle.None )
					{
						return down != BorderStyle.None
							? BoxCharacterHelper.GetVerticalHorizontal( up, down, left, right )
							: BoxCharacterHelper.GetHorizontalTop( left, right, up );
					}

					return down != BorderStyle.None
						? BoxCharacterHelper.GetHorizontalDown( left, right, down )
						: BoxCharacterHelper.GetHorizontal( current );
				}

				if ( up == BorderStyle.None )
				{
					return BoxCharacterHelper.GetDownLeft( down, left );
				}

				return down != BorderStyle.None
					? BoxCharacterHelper.GetVerticalLeft( up, down, left )
					: BoxCharacterHelper.GetUpLeft( up, left );
			}

			if ( right != BorderStyle.None )
			{
				if ( up != BorderStyle.None )
				{
					return down != BorderStyle.None
						? BoxCharacterHelper.GetVerticalRight( up, down, right )
						: BoxCharacterHelper.GetUpRight( up, right );
				}

				return down != BorderStyle.None
					? BoxCharacterHelper.GetDownRight( down, right )
					: ' ';
			}

			return up != BorderStyle.None && down != BorderStyle.None
				? BoxCharacterHelper.GetVertical( current )
				: ' ';
		}

		private static void RefreshGridMapping()
		{
			m_WindowGrid = new (BorderStyle style, ThemeColor foregroundColor, ThemeColor backgroundColor)[m_WindowWidth, m_WindowHeight];
			foreach ( Border border in m_Borders )
			{
				Coord topLeft = border.AbsoluteTopLeft;
				UInt16 top = topLeft.Y;
				UInt16 left = topLeft.X;

				Coord bottomRight = border.AbsoluteBottomRight;
				UInt16 bottom = bottomRight.Y;
				UInt16 right = bottomRight.X;

				if ( border.Direction == Direction.Vertical )
				{
					for ( Int32 y = top; y <= bottom; y++ )
					{
						m_WindowGrid[left, y] = ( m_WindowGrid[left, y].style | border.Style, border.ForegroundColor, border.BackgroundColor );
					}
				}
				else
				{
					for ( Int32 x = left; x <= right; x++ )
					{
						m_WindowGrid[x, top] = ( m_WindowGrid[x, top].style | border.Style, border.ForegroundColor, border.BackgroundColor );
					}
				}
			}
		}
	}
}