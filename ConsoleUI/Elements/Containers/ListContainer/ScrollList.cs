/********************************************************************************
*                                                                               *
* This document and any information contained herein is the property of         *
* Eddyfi NDT Inc. and is protected under copyright law. It must be considered   *
* proprietary and confidential and must not be disclosed, used, or reproduced,  *
* in whole or in part, without the written authorization of Eddyfi NDT Inc.     *
*                                                                               *
* Le présent document et l’information qu’il contient sont la propriété         *
* exclusive d’Eddyfi NDT Inc. et sont protégés par la loi sur le droit          *
* d’auteur. Ce document est confidentiel et ne peut être divulgué, utilisé ou   *
* reproduit, en tout ou en partie, sans l'autorisation écrite d’Eddyfi NDT Inc. *
*                                                                               *
********************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ConsoleUI.Elements.BaseElements;
using ConsoleUI.Elements.Components.Border;
using ConsoleUI.Elements.Components.Scrollbar;
using ConsoleUI.Elements.Containers.GridContainer;
using ConsoleUI.Inputs;
using ConsoleUI.Inputs.Mouse;
using ConsoleUI.Settings;

namespace ConsoleUI.Elements.Containers.ListContainer
{
	public class ScrollList : Grid
	{
		private Boolean m_IsMouseInside;

		private readonly List<ListItem> m_ListItems;
		private Int32 m_CurrentIndex;
		private Int32 m_VisibleRows;
		private Int32 m_ScrollSkipInterval = 1;
		private Boolean m_ScrollBarHidden;
		private UInt16 m_ScrollBarWidth;
		private readonly ContainerElement m_Content;
		private ThemeColor m_ScrollBarHoverForegroundColor;
		private ThemeColor m_ScrollBarBackgroundColor;
		private ThemeColor m_ScrollBarForegroundColor;

		public override UInt16 Bottom
		{
			get => base.Bottom;
			set
			{
				base.Bottom = value;
				m_Content.Bottom = InnerHeight;
				ScrollBar.Bottom = InnerHeight;
			}
		}

		public override UInt16 Right
		{
			get => base.Right;
			set
			{
				base.Right = value;
				m_Content.Right = Right;
			}
		}

		public Boolean ScrollBarHidden
		{
			get => m_ScrollBarHidden;
			set
			{
				if ( SetProperty( ref m_ScrollBarHidden, value ) )
				{
					ScrollBar.Hidden = value;
				}
			}
		}

		public UInt16 ScrollBarWidth
		{
			get => m_ScrollBarWidth;
			set
			{
				if ( SetProperty( ref m_ScrollBarWidth, value ) )
				{
					ScrollBar.Right = value;
				}
			}
		}

		public ThemeColor ScrollBarHoverForegroundColor
		{
			get => m_ScrollBarHoverForegroundColor;
			set
			{
				if ( SetProperty( ref m_ScrollBarHoverForegroundColor, value ) )
				{
					if ( ScrollBar != null )
					{
						ScrollBar.HoverForegroundColor = value;
					}
				}
			}
		}

		public ThemeColor ScrollBarBackgroundColor
		{
			get => m_ScrollBarBackgroundColor;
			set
			{
				if ( SetProperty( ref m_ScrollBarBackgroundColor, value ) )
				{
					if ( ScrollBar != null )
					{
						ScrollBar.BackgroundColor = value;
					}
				}
			}
		}

		public ThemeColor ScrollBarForegroundColor
		{
			get => m_ScrollBarForegroundColor;
			set
			{
				if ( SetProperty( ref m_ScrollBarForegroundColor, value ) )
				{
					if ( ScrollBar != null )
					{
						ScrollBar.ForegroundColor = value;
					}
				}
			}
		}

		public ScrollBar ScrollBar { get; }

		public Int32 ScrollSkipInterval
		{
			get => m_ScrollSkipInterval;
			set => SetProperty( ref m_ScrollSkipInterval, value );
		}

		public ScrollList()
		{
			m_Content = new ContainerElement();
			ScrollBar = new ScrollBar
			{
				HoverForegroundColor = ScrollBarHoverForegroundColor,
				ForegroundColor = ScrollBarForegroundColor,
				BackgroundColor = ScrollBarBackgroundColor,
				Right = ScrollBarWidth,
				Hidden = ScrollBarHidden
			};
			AddChild( m_Content, 0, 0 );
			AddChild( ScrollBar, 1, 0 );
			ScrollBar.ScrollBarClick += OnScrollBarClick;

			m_CurrentIndex = -1;
			m_ListItems = new List<ListItem>();

			SubscribeToPropertyChanged( nameof( ScrollBarWidth ), OnScrollBarWidthChanged );
			SubscribeToPropertyChanged( nameof( ScrollBarHidden ), OnScrollBarHiddenChanged );
			SubscribeToPropertyChanged( nameof( InnerHeight ), OnInnerHeightChanged );
			InputManager.MouseMove += OnMouseMove;
			InputManager.MouseButton += OnMouseButton;
		}

		public override void AddChild( Element _child )
		{
			if ( _child is Border border )
			{
				base.AddChild( border );
			}
			else
			{
				AddChild( _child, Guid.NewGuid() );
			}
		}

		public virtual void AddChild( Element _child, Guid _id )
		{
			lock ( ChildLock )
			{
				ListItem wrapper = new ListItem( _id );
				wrapper.AddChild( _child );
				if ( m_CurrentIndex == m_ListItems.Count - 1 )
				{
					m_CurrentIndex++;
				}

				m_ListItems.Add( wrapper );
				RefreshView();

				ScrollBar.NbOfElementRows = (UInt32)m_ListItems.Count;
			}
		}

		protected virtual void OnScrollBarWidthChanged()
		{
			if ( ScrollBarHidden )
			{
				m_Content.Right = Right;
			}
			else
			{
				m_Content.Right = (UInt16)( InnerWidth - ScrollBarWidth );
			}
		}

		protected virtual void OnScrollBarHiddenChanged()
		{
			if ( ScrollBarHidden )
			{
				m_Content.Right = Right;
			}
			else
			{
				m_Content.Right = (UInt16)( Right - ScrollBarWidth - 1 );
			}
		}

		protected Boolean UpdateListItem( Guid _id, Action<ListItem> _update )
		{
			lock ( ChildLock )
			{
				ListItem listItemToUpdate = m_ListItems.FirstOrDefault( _listItem => _listItem.ItemID == _id );
				if ( listItemToUpdate != null )
				{
					_update( listItemToUpdate );
					return true;
				}

				return false;
			}
		}

		protected virtual void OnInnerHeightChanged()
		{
			lock ( ChildLock )
			{
				m_VisibleRows = InnerHeight + 1;
				RefreshView();
			}
		}

		private void OnScrollBarClick( Object _sender, ScrollBarClickEventArgs _e )
		{
			m_CurrentIndex = _e.NewCurrentIndex;
			RefreshView();
		}

		private void RefreshView()
		{
			lock ( ChildLock )
			{
				Int32 startIndex = Math.Max( m_CurrentIndex - ( m_VisibleRows - 1 ), 0 );
				Int32 range = Math.Min( ( m_CurrentIndex + 1 ) - startIndex, m_VisibleRows );
				range = Math.Min( range, m_ListItems.Count - startIndex );

				List<ListItem> visibleListItems = m_ListItems.GetRange( startIndex, range );

				m_Content.ClearChildren();

				for ( UInt16 i = 0; i < visibleListItems.Count; i++ )
				{
					ListItem visibleItem = visibleListItems[i];
					visibleItem.Top = i;
					visibleItem.Hidden = false;
					visibleItem.SetDirty( true );
					m_Content.AddChild( visibleItem );
				}

				ScrollBar.FirstVisibleElementPosition = (UInt32)startIndex;
				ScrollBar.LastVisibleElementPosition = (UInt32)( startIndex + range );
			}

			SetDirty();
		}

		private void OnMouseButton( Object _sender, MouseButtonEventArgs _e )
		{
			if ( m_IsMouseInside && _e.EventFlags == MouseEventFlags.MouseWheeled )
			{
				if ( _e.ButtonState.HasFlag( MouseButtonState.MouseWheelScrollUp ) )
				{
					ScrollUp();
				}
				else if ( _e.ButtonState.HasFlag( MouseButtonState.MouseWheelScrollDown ) )
				{
					ScrollDown();
				}
			}
		}

		private void OnMouseMove( Object _sender, MouseMoveEventArgs _e )
		{
			m_IsMouseInside = ContainsCoord( _e.CurrentCoord );
		}

		private void ScrollUp()
		{
			lock ( ChildLock )
			{
				m_CurrentIndex = Math.Max( m_CurrentIndex - ScrollSkipInterval, m_VisibleRows - 1 );
				RefreshView();
			}
		}

		private void ScrollDown()
		{
			lock ( ChildLock )
			{
				m_CurrentIndex = Math.Min( m_CurrentIndex + ScrollSkipInterval, m_ListItems.Count - 1 );
				RefreshView();
			}
		}

		~ScrollList()
		{
			UnsubscribeFromPropertyChanged( nameof( InnerHeight ), OnInnerHeightChanged );
			InputManager.MouseMove -= OnMouseMove;
			InputManager.MouseButton -= OnMouseButton;
		}
	}
}