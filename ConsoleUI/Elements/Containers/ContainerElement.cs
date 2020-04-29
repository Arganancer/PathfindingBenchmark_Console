using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ConsoleUI.Elements.BaseElements;
using ConsoleUI.Elements.Components.Border;
using ConsoleUI.Settings;

namespace ConsoleUI.Elements.Containers
{
	public class ContainerElement : Element
	{
		private BorderStyle m_TopBorder;
		private BorderStyle m_BottomBorder;
		private BorderStyle m_LeftBorder;
		private BorderStyle m_RightBorder;
		private ThemeColor m_BorderColor;
		private Boolean m_AutoHeight;
		private Boolean m_AutoWidth;

		protected readonly Object ChildLock = new Object();

		public List<Element> Children { get; }

		/// <summary>ad
		/// When true, the container's width will adjust to contain all of its children.
		/// </summary>
		public Boolean AutoWidth
		{
			get => m_AutoWidth;
			set => SetProperty( ref m_AutoWidth, value );
		}

		/// <summary>
		/// When true, the container's height will adjust to contain all of its children.
		/// </summary>
		public Boolean AutoHeight
		{
			get => m_AutoHeight;
			set => SetProperty( ref m_AutoHeight, value );
		}

		public ThemeColor BorderColor
		{
			get => m_BorderColor;
			set => SetProperty( ref m_BorderColor, value );
		}

		public BorderStyle TopBorder
		{
			get => m_TopBorder;
			set
			{
				m_TopBorder = value;
				if ( UpdateBorder( value, BorderPosition.Top ) )
				{
					SetDirty();
					RecalculateInnerTopLeft();
					RecalculateInnerHeight();
				}
			}
		}

		public BorderStyle BottomBorder
		{
			get => m_BottomBorder;
			set
			{
				m_BottomBorder = value;
				if ( UpdateBorder( value, BorderPosition.Bottom ) )
				{
					SetDirty();
					RecalculateInnerBottomRight();
					RecalculateInnerHeight();
				}
			}
		}

		public BorderStyle LeftBorder
		{
			get => m_LeftBorder;
			set
			{
				m_LeftBorder = value;
				if ( UpdateBorder( value, BorderPosition.Left ) )
				{
					SetDirty();
					RecalculateInnerTopLeft();
					RecalculateInnerWidth();
				}
			}
		}

		public BorderStyle RightBorder
		{
			get => m_RightBorder;
			set
			{
				m_RightBorder = value;
				if ( UpdateBorder( value, BorderPosition.Right ) )
				{
					SetDirty();
					RecalculateInnerBottomRight();
					RecalculateInnerWidth();
				}
			}
		}

		public Boolean HasTopBorder => TopBorder != BorderStyle.None;
		public Boolean HasBottomBorder => BottomBorder != BorderStyle.None;
		public Boolean HasLeftBorder => LeftBorder != BorderStyle.None;
		public Boolean HasRightBorder => RightBorder != BorderStyle.None;

		public ContainerElement() => Children = new List<Element>();

		public override void SetDirty( Boolean _recursive = false )
		{
			base.SetDirty( _recursive );
			if ( _recursive )
			{
				foreach ( Element child in Children )
				{
					child.SetDirty( true );
				}
			}
		}

		public override void Draw()
		{
			base.Draw();
			lock ( ChildLock )
			{
				foreach ( Element child in Children )
				{
					child.Draw();
				}
			}
		}

		public virtual void AddChild( Element _child )
		{
			if ( ReferenceEquals( _child, this ) )
			{
				throw new Exception( $"{nameof( ContainerElement )} {_child.GetType()} added itself as its own child. This is not allowed as it would create an infinite recursion of calls." );
			}

			lock ( ChildLock )
			{
				_child.Parent = this;
				Children.Add( _child );
				FitToChildren();
			}
		}

		public void RemoveChild( Element _child )
		{
			lock ( ChildLock )
			{
				Children.Remove( _child );
				SetDirty();
			}
		}

		public void ClearContent()
		{
			String contentClearString = "".PadLeft( InnerWidth );
			for ( Int32 i = HasTopBorder ? 1 : 0; i < Height + ( HasBottomBorder ? 0 : 1 ); i++ )
			{
				Console.SetCursorPosition( Left + ( HasLeftBorder ? 1 : 0 ), Top + i );
				Console.Write( contentClearString );
			}
		}

		public Border GetBorder( BorderPosition _position )
		{
			return _position switch
			{
				BorderPosition.Top => new Border { Style = TopBorder, Direction = Direction.Horizontal, Position = BorderPosition.Top, Parent = this },
				BorderPosition.Bottom => new Border { Style = BottomBorder, Direction = Direction.Horizontal, Position = BorderPosition.Bottom, Parent = this },
				BorderPosition.Left => new Border { Style = LeftBorder, Direction = Direction.Vertical, Position = BorderPosition.Left, Parent = this },
				BorderPosition.Right => new Border { Style = RightBorder, Direction = Direction.Vertical, Position = BorderPosition.Right, Parent = this },
				_ => throw new ArgumentOutOfRangeException( nameof( _position ), _position, null )
			};
		}

		public IEnumerable<Border> GetBorders()
		{
			List<Border> borders = Children.OfType<Border>().ToList();

			foreach ( ContainerElement childLayout in Children.OfType<ContainerElement>() )
			{
				borders.AddRange( childLayout.GetBorders() );
			}

			return borders;
		}

		protected override void RecalculateInnerWidth()
		{
			base.RecalculateInnerWidth();
			UInt16 modifier = (UInt16)( HasLeftBorder ? HasRightBorder ? 2 : 1 : HasRightBorder ? 1 : 0 );
			UInt16 innerWidth = base.InnerWidth;
			UInt16 innerWidthAfterModifier = (UInt16)( innerWidth - modifier );
			InnerWidth = (UInt16)( innerWidthAfterModifier <= innerWidth ? innerWidthAfterModifier : 0 );
		}

		protected override void RecalculateInnerHeight()
		{
			base.RecalculateInnerHeight();
			UInt16 modifier = (UInt16)( HasTopBorder ? HasBottomBorder ? 2 : 1 : HasBottomBorder ? 1 : 0 );
			UInt16 innerHeight = base.InnerHeight;
			UInt16 innerHeightAfterModifier = (UInt16)( innerHeight - modifier );
			InnerHeight = (UInt16)( innerHeightAfterModifier <= innerHeight ? innerHeightAfterModifier : 0 );
		}

		protected override void RecalculateInnerTopLeft()
		{
			base.RecalculateInnerTopLeft();
			InnerTopLeft = new Coord(
				(UInt16)( InnerTopLeft.X + ( HasLeftBorder ? 1 : 0 ) ),
				(UInt16)( InnerTopLeft.Y + ( HasTopBorder ? 1 : 0 ) ) );
		}

		protected override void RecalculateInnerBottomRight()
		{
			base.RecalculateInnerBottomRight();
			InnerBottomRight = new Coord(
				(UInt16)( InnerBottomRight.X + ( HasRightBorder ? 1 : 0 ) ),
				(UInt16)( InnerBottomRight.Y + ( HasBottomBorder ? 1 : 0 ) ) );
		}

		protected override void DrawIfDirty()
		{
		}

		protected void FitToChildren()
		{
			if ( AutoWidth )
			{
				Right = (UInt16)( Children.Count > 0 ? Children.Select( _child => _child.Right ).Max() : 0 );
				SetDirty();
			}

			if ( AutoHeight )
			{
				Bottom = (UInt16)( Children.Count > 0 ? Children.Select( _child => _child.Bottom ).Max() : 0 );
				SetDirty();
			}
		}

		private void AddChild( Border _border )
		{
			AddChild( _border as Element );
			BoxDrawer.UpdateBorder( _border );
		}

		private void RemoveChild( Border _border )
		{
			RemoveChild( _border as Element );
			BoxDrawer.RemoveBorder( _border );
		}

		/// <summary>
		/// Returns true if the border was updated.
		/// </summary>
		private Boolean UpdateBorder( BorderStyle _style, BorderPosition _position )
		{
			if ( _style == BorderStyle.None )
			{
				Border border = Children.OfType<Border>().FirstOrDefault( _border => _border.Position == _position );
				if ( border == null )
				{
					return false;
				}

				RemoveChild( border );
				return true;
			}

			Border oldBorder = Children.OfType<Border>().FirstOrDefault( _border => _border.Position == _position );
			if ( oldBorder != null )
			{
				oldBorder.Style = _style;
			}
			else
			{
				AddChild( GetBorder( _position ) );
			}

			return true;
		}

		protected internal void ClearChildren()
		{
			lock ( ChildLock )
			{
				foreach ( Element child in Children )
				{
					child.Hidden = true;
				}

				Children.Clear();
			}
		}
	}
}