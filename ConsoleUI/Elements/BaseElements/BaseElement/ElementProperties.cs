using System;
using System.Drawing;
using ConsoleUI.Elements.Containers;
using ConsoleUI.Settings;

namespace ConsoleUI.Elements.BaseElements
{
	public abstract partial class Element
	{
		private ContainerElement m_Parent;
		private UInt16 m_Left;
		private UInt16 m_Top;
		private UInt16 m_Right;
		private UInt16 m_Bottom;
		private UInt16 m_LeftPadding;
		private UInt16 m_RightPadding;
		private UInt16 m_TopPadding;
		private UInt16 m_BottomPadding;
		private UInt16 m_LeftMargin;
		private UInt16 m_RightMargin;
		private UInt16 m_TopMargin;
		private UInt16 m_BottomMargin;
		private Single m_LeftAnchor;
		private Single m_RightAnchor;
		private Single m_TopAnchor;
		private Single m_BottomAnchor;
		private Boolean m_Hidden;
		private ThemeColor m_ForegroundColor = ThemeColor.Primary;
		private ThemeColor m_BackgroundColor = ThemeColor.Background;
		private Boolean m_UseHorizontalAnchors;
		private Boolean m_UseVerticalAnchors;
		private UInt16 m_Width;
		private UInt16 m_Height;
		private Coord m_BottomRight;
		private Coord m_TopLeft;
		private Coord m_AbsoluteTopLeft;
		private Coord m_AbsoluteBottomRight;
		private UInt16 m_InnerHeight;
		private UInt16 m_InnerWidth;
		private Coord m_InnerTopLeft;
		private Coord m_AbsoluteInnerTopLeft;
		private Coord m_InnerBottomRight;
		private Coord m_AbsoluteInnerBottomRight;

		public virtual ContainerElement Parent
		{
			get => m_Parent;
			set
			{
				ContainerElement oldParent = m_Parent;
				if ( SetProperty( ref m_Parent, value ) )
				{
					UnsubscribeFromParent( oldParent );
					SubscribeToParent();
				}
			}
		}

		public virtual UInt16 InnerWidth
		{
			get => m_InnerWidth;
			protected set => SetProperty( ref m_InnerWidth, value );
		}

		public virtual UInt16 InnerHeight
		{
			get => m_InnerHeight;
			protected set => SetProperty( ref m_InnerHeight, value );
		}

		/// <summary>
		/// If set to true, element will use <see cref="LeftAnchor"/> and <see cref="RightAnchor"/>
		/// to position itself vertically.
		/// </summary>
		public virtual Boolean UseHorizontalAnchors
		{
			get => m_UseHorizontalAnchors;
			set => SetProperty( ref m_UseHorizontalAnchors, value );
		}

		/// <summary>
		/// If set to true, element will use <see cref="TopAnchor"/> and <see cref="BottomAnchor"/> to position itself vertically.
		/// If set to false, element will use
		/// </summary>
		public virtual Boolean UseVerticalAnchors
		{
			get => m_UseVerticalAnchors;
			set => SetProperty( ref m_UseVerticalAnchors, value );
		}

		/// <summary>
		/// This property determines the background color for the space inside the element.
		/// </summary>
		public virtual ThemeColor BackgroundColor
		{
			get => m_BackgroundColor;
			set => SetProperty( ref m_BackgroundColor, value );
		}

		/// <summary>
		/// This property determines the foreground element of any characters inside the space.
		/// </summary>
		public virtual ThemeColor ForegroundColor
		{
			get => m_ForegroundColor;
			set => SetProperty( ref m_ForegroundColor, value );
		}

		/// <summary>
		/// Screen coordinate of the top left corner of the element relative to its parent.
		/// </summary>
		public virtual Coord TopLeft
		{
			get => m_TopLeft;
			set => SetProperty( ref m_TopLeft, value );
		}

		/// <summary>
		/// Absolute screen coordinate of the top left corner of the element.
		/// </summary>
		public virtual Coord AbsoluteTopLeft
		{
			get => m_AbsoluteTopLeft;
			protected set => SetProperty( ref m_AbsoluteTopLeft, value );
		}

		public virtual Coord InnerTopLeft
		{
			get => m_InnerTopLeft;
			protected set => SetProperty( ref m_InnerTopLeft, value );
		}

		public virtual Coord AbsoluteInnerTopLeft
		{
			get => m_AbsoluteInnerTopLeft;
			protected set => SetProperty( ref m_AbsoluteInnerTopLeft, value );
		}

		/// <summary>
		/// Screen coordinate of the bottom right corner of the element relative to its parent.
		/// </summary>
		public virtual Coord BottomRight
		{
			get => m_BottomRight;
			set => SetProperty( ref m_BottomRight, value );
		}

		/// <summary>
		/// Absolute screen coordinate of the bottom right corner of the element.
		/// </summary>
		public virtual Coord AbsoluteBottomRight
		{
			get => m_AbsoluteBottomRight;
			protected set => SetProperty( ref m_AbsoluteBottomRight, value );
		}

		public virtual Coord InnerBottomRight
		{
			get => m_InnerBottomRight;
			protected set => SetProperty( ref m_InnerBottomRight, value );
		}

		public virtual Coord AbsoluteInnerBottomRight
		{
			get => m_AbsoluteInnerBottomRight;
			protected set => SetProperty( ref m_AbsoluteInnerBottomRight, value );
		}

		/// <summary>
		/// Position of the left side of the element relative to its parent.
		/// </summary>
		public virtual UInt16 Left
		{
			get => m_Left;
			set => SetProperty( ref m_Left, value );
		}

		/// <summary>
		/// Position of the right side of the element relative to its parent.
		/// </summary>
		public virtual UInt16 Right
		{
			get => m_Right;
			set => SetProperty( ref m_Right, value );
		}

		/// <summary>
		/// Position of the top side of the element relative to its parent.
		/// </summary>
		public virtual UInt16 Top
		{
			get => m_Top;
			set => SetProperty( ref m_Top, value );
		}

		/// <summary>
		/// Position of the bottom side of the element relative to its parent.
		/// </summary>
		public virtual UInt16 Bottom
		{
			get => m_Bottom;
			set => SetProperty( ref m_Bottom, value );
		}

		/// <summary>
		/// Total width of the element.
		/// </summary>
		public virtual UInt16 Width
		{
			get => m_Width;
			protected set => SetProperty( ref m_Width, value );
		}

		/// <summary>
		/// Total height of the element
		/// </summary>
		public virtual UInt16 Height
		{
			get => m_Height;
			protected set => SetProperty( ref m_Height, value );
		}

		/// <summary>
		/// Percentage representing the horizontal position of the left side of the element relative to its parent.
		/// A value of 0 means leftmost, while a value of 1 means rightmost.
		/// Any value below 0 will be rounded up to 0, and any value above 1 will be rounded down to 1.
		/// </summary>
		public virtual Single LeftAnchor
		{
			get => m_LeftAnchor;
			set => SetProperty( ref m_LeftAnchor, value );
		}

		/// <summary>
		/// Percentage representing the horizontal position of the right side of the element relative to its parent.
		/// A value of 0 means leftmost, while a value of 1 means rightmost.
		/// Any value below 0 will be rounded up to 0, and any value above 1 will be rounded down to 1.
		/// </summary>
		public virtual Single RightAnchor
		{
			get => m_RightAnchor;
			set => SetProperty( ref m_RightAnchor, value );
		}

		/// <summary>
		/// Percentage representing the vertical position of the top side of the element relative to its parent.
		/// A value of 0 means topmost, while a value of 1 means bottommost.
		/// Any value below 0 will be rounded up to 0, and any value above 1 will be rounded down to 1.
		/// </summary>
		public virtual Single TopAnchor
		{
			get => m_TopAnchor;
			set => SetProperty( ref m_TopAnchor, value );
		}

		/// <summary>
		/// Percentage representing the vertical position of the bottom side of the element relative to its parent.
		/// A value of 0 means topmost, while a value of 1 means bottommost.
		/// Any value below 0 will be rounded up to 0, and any value above 1 will be rounded down to 1.
		/// </summary>
		public virtual Single BottomAnchor
		{
			get => m_BottomAnchor;
			set => SetProperty( ref m_BottomAnchor, value );
		}

		/// <summary>
		/// Spacing on the inner left side of the element.
		/// If a left border is defined, spacing starts where the border ends.
		/// </summary>
		public virtual UInt16 LeftPadding
		{
			get => m_LeftPadding;
			set => SetProperty( ref m_LeftPadding, value );
		}

		/// <summary>
		/// Spacing on the inner right side of the element.
		/// If a right border is defined, spacing starts where the border ends.
		/// </summary>
		public virtual UInt16 RightPadding
		{
			get => m_RightPadding;
			set => SetProperty( ref m_RightPadding, value );
		}

		/// <summary>
		/// Spacing on the inner top side of the element.
		/// If a top border is defined, spacing starts where the border ends.
		/// </summary>
		public virtual UInt16 TopPadding
		{
			get => m_TopPadding;
			set => SetProperty( ref m_TopPadding, value );
		}

		/// <summary>
		/// Spacing on the inner bottom side of the element.
		/// If a bottom border is defined, spacing starts where the border ends.
		/// </summary>
		public virtual UInt16 BottomPadding
		{
			get => m_BottomPadding;
			set => SetProperty( ref m_BottomPadding, value );
		}

		/// <summary>
		/// Spacing on the outer left side of the element.
		/// If a left border is defined, spacing starts where the border ends.
		/// </summary>
		public virtual UInt16 LeftMargin
		{
			get => m_LeftMargin;
			set => SetProperty( ref m_LeftMargin, value );
		}

		/// <summary>
		/// Spacing on the outer right side of the element.
		/// If a right border is defined, spacing starts where the border ends.
		/// </summary>
		public virtual UInt16 RightMargin
		{
			get => m_RightMargin;
			set => SetProperty( ref m_RightMargin, value );
		}

		/// <summary>
		/// Spacing on the outer top side of the element.
		/// If a top border is defined, spacing starts where the border ends.
		/// </summary>
		public virtual UInt16 TopMargin
		{
			get => m_TopMargin;
			set => SetProperty( ref m_TopMargin, value );
		}

		/// <summary>
		/// Spacing on the outer bottom side of the element.
		/// If a bottom border is defined, spacing starts where the border ends.
		/// </summary>
		public virtual UInt16 BottomMargin
		{
			get => m_BottomMargin;
			set => SetProperty( ref m_BottomMargin, value );
		}

		/// <summary>
		/// If set to false, the element will not be rendered.
		/// If the element was previously rendered, it will be removed.
		/// </summary>
		public Boolean Hidden
		{
			get => m_Hidden;
			set
			{
				if ( SetProperty( ref m_Hidden, value ) )
				{
					if ( value )
					{
						ClearElement();
					}
				}
			}
		}

		protected virtual void RecalculateInnerTopLeft()
		{
			InnerTopLeft = new Coord( (UInt16)( TopLeft.X + LeftPadding ), (UInt16)( TopLeft.Y + TopPadding ) );
		}

		protected virtual void RecalculateAbsoluteInnerTopLeft()
		{
			AbsoluteInnerTopLeft = InnerTopLeft + Parent?.m_AbsoluteInnerTopLeft ?? new Coord();
		}

		protected virtual void RecalculateInnerBottomRight()
		{
			InnerBottomRight = new Coord( (UInt16)( BottomRight.X - RightPadding ), (UInt16)( BottomRight.Y - BottomPadding ) );
		}

		protected virtual void RecalculateAbsoluteInnerBottomRight()
		{
			AbsoluteInnerBottomRight = InnerBottomRight + Parent?.m_AbsoluteInnerTopLeft ?? new Coord();
		}

		protected virtual void RecalculateTop()
		{
			if ( UseVerticalAnchors )
			{
				Top = (UInt16)( Parent?.InnerHeight * TopAnchor ?? 0 );
			}
		}

		protected virtual void RecalculateBottom()
		{
			if ( UseHorizontalAnchors )
			{
				Bottom = (UInt16)( Parent?.InnerHeight * BottomAnchor ?? 0 );
			}
		}

		protected virtual void RecalculateLeft()
		{
			if ( UseHorizontalAnchors )
			{
				Left = (UInt16)( Parent?.InnerWidth * LeftAnchor ?? 0 );
			}
		}

		protected virtual void RecalculateRight()
		{
			if ( UseHorizontalAnchors )
			{
				Right = (UInt16)( Parent?.InnerWidth * RightAnchor ?? 0 );
			}
		}

		protected virtual void RecalculateInnerWidth()
		{
			InnerWidth = (UInt16)( Width - LeftPadding - RightPadding );
		}

		protected virtual void RecalculateInnerHeight()
		{
			InnerHeight = (UInt16)( Height - TopPadding - BottomPadding );
		}

		protected virtual void RecalculateAbsoluteBottomRight()
		{
			AbsoluteBottomRight = Parent is null ? BottomRight : BottomRight + Parent.AbsoluteTopLeft;
		}

		protected virtual void RecalculateAbsoluteTopLeft()
		{
			AbsoluteTopLeft = Parent is null ? TopLeft : TopLeft + Parent.AbsoluteTopLeft;
		}

		protected virtual void RecalculateTopLeft()
		{
			TopLeft = new Coord( Left, Top );
		}

		protected virtual void RecalculateBottomRight()
		{
			BottomRight = new Coord( Right, Bottom );
		}

		protected virtual void RecalculateHeight()
		{
			Height = (UInt16)( Bottom >= Top ? Bottom - Top : 0 );
		}

		protected virtual void RecalculateWidth()
		{
			Width = (UInt16)( Right >= Left ? Right - Left : 0 );
		}
	}
}