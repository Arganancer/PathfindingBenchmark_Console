using System;
using System.Drawing;
using ConsoleUI.Elements.BaseElements;
using ConsoleUI.Elements.Containers;
using ConsoleUI.Settings;

namespace ConsoleUI.Elements.Components.Border
{
	public class Border : Element
	{
		private BorderStyle m_Style;
		private Direction m_Direction;

		public BorderStyle Style
		{
			get => m_Style;
			set => SetProperty( ref m_Style, value );
		}

		public Direction Direction
		{
			get => m_Direction;
			set => SetProperty( ref m_Direction, value );
		}

		public override ThemeColor ForegroundColor => Parent?.BorderColor ?? ThemeColor.Primary;

		public BorderPosition Position { get; set; }

		public Border()
		{
			SubscribeToPropertyChanged( nameof( TopMargin ), OnTopMarginChanged );
			SubscribeToPropertyChanged( nameof( BottomMargin ), OnBottomMarginChanged );
			SubscribeToPropertyChanged( nameof( LeftMargin ), OnLeftMarginChanged );
			SubscribeToPropertyChanged( nameof( RightMargin ), OnRightMarginChanged );
		}

		protected override void RecalculateBottom()
		{
			if ( Parent is null )
			{
				return;
			}

			if ( Position == BorderPosition.Top )
			{
				Bottom = (UInt16)( Parent.BottomPadding + TopMargin );
			}
			else
			{
				Bottom = (UInt16)( Parent.Height - Parent.BottomPadding - BottomMargin );
			}
		}

		protected override void RecalculateTop()
		{
			if ( Parent is null )
			{
				return;
			}

			if ( Position == BorderPosition.Bottom )
			{
				Top = (UInt16)( Parent.Height - Parent.BottomPadding - BottomMargin );
			}
			else
			{
				Top = (UInt16)( Parent.BottomPadding + TopMargin );
			}
		}

		protected override void RecalculateLeft()
		{
			if ( Parent is null )
			{
				return;
			}

			if ( Position == BorderPosition.Right )
			{
				Left = (UInt16)( Parent.Width - Parent.RightPadding - RightMargin );
			}
			else
			{
				Left = (UInt16)( Parent.LeftPadding + LeftMargin );
			}
		}

		protected override void RecalculateRight()
		{
			if ( Parent is null )
			{
				return;
			}

			if ( Position == BorderPosition.Left )
			{
				Right = (UInt16)( Parent.LeftPadding + LeftMargin );
			}
			else
			{
				Right = (UInt16)( Parent.Width - Parent.RightPadding - RightMargin );
			}
		}

		protected override void SubscribeToParent()
		{
			base.SubscribeToParent();
			Parent?.SubscribeToPropertyChanged( nameof( Width ), OnParentWidthChanged );
			Parent?.SubscribeToPropertyChanged( nameof( RightPadding ), OnParentRightPaddingChanged );
			Parent?.SubscribeToPropertyChanged( nameof( LeftPadding ), OnParentLeftPaddingChanged );
			Parent?.SubscribeToPropertyChanged( nameof( Height ), OnParentHeightChanged );
			Parent?.SubscribeToPropertyChanged( nameof( TopPadding ), OnParentTopPaddingChanged );
			Parent?.SubscribeToPropertyChanged( nameof( BottomPadding ), OnParentBottomPaddingChanged );
		}

		protected override void UnsubscribeFromParent( ContainerElement _parent )
		{
			base.UnsubscribeFromParent( _parent );
			Parent?.UnsubscribeFromPropertyChanged( nameof( Width ), OnParentWidthChanged );
			Parent?.UnsubscribeFromPropertyChanged( nameof( RightPadding ), OnParentRightPaddingChanged );
			Parent?.UnsubscribeFromPropertyChanged( nameof( LeftPadding ), OnParentLeftPaddingChanged );
			Parent?.UnsubscribeFromPropertyChanged( nameof( Height ), OnParentHeightChanged );
			Parent?.UnsubscribeFromPropertyChanged( nameof( TopPadding ), OnParentTopPaddingChanged );
			Parent?.UnsubscribeFromPropertyChanged( nameof( BottomPadding ), OnParentBottomPaddingChanged );
		}

		protected override void DrawIfDirty()
		{
			BoxDrawer.UpdateBorder( this );
		}

		private void OnRightMarginChanged()
		{
			RecalculateLeft();
			RecalculateRight();
		}

		private void OnLeftMarginChanged()
		{
			RecalculateLeft();
			RecalculateRight();
		}

		private void OnBottomMarginChanged()
		{
			RecalculateTop();
			RecalculateBottom();
		}

		private void OnTopMarginChanged()
		{
			RecalculateTop();
			RecalculateBottom();
		}

		private void OnParentBottomPaddingChanged()
		{
			RecalculateTop();
			RecalculateBottom();
		}

		private void OnParentTopPaddingChanged()
		{
			RecalculateTop();
			RecalculateBottom();
		}

		private void OnParentHeightChanged()
		{
			RecalculateTop();
			RecalculateBottom();
		}

		private void OnParentLeftPaddingChanged()
		{
			RecalculateLeft();
			RecalculateRight();
		}

		private void OnParentRightPaddingChanged()
		{
			RecalculateLeft();
			RecalculateRight();
		}

		private void OnParentWidthChanged()
		{
			RecalculateLeft();
			RecalculateRight();
		}

		~Border()
		{
			UnsubscribeFromPropertyChanged( nameof( TopMargin ), OnTopMarginChanged );
			UnsubscribeFromPropertyChanged( nameof( BottomMargin ), OnBottomMarginChanged );
			UnsubscribeFromPropertyChanged( nameof( LeftMargin ), OnLeftMarginChanged );
			UnsubscribeFromPropertyChanged( nameof( RightMargin ), OnRightMarginChanged );
		}
	}
}