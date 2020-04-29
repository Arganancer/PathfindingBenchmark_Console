using System;
using System.Collections.Generic;
using ConsoleUI.Elements.Containers;

namespace ConsoleUI.Elements.BaseElements
{
	public abstract partial class Element
	{
		private readonly Dictionary<String, List<Action>> m_PropertyChangedSubscribers = new Dictionary<String, List<Action>>();
		private readonly Dictionary<String, List<Action>> m_PrePropertyChangedSubscribers = new Dictionary<String, List<Action>>();

		public void SubscribeToPropertyChanged( String _propertyName, Action _onPropertyChanged )
		{
			if ( !m_PropertyChangedSubscribers.TryGetValue( _propertyName, out List<Action> subscribers ) )
			{
				subscribers = new List<Action>();
				m_PropertyChangedSubscribers.Add( _propertyName, subscribers );
			}

			subscribers.Add( _onPropertyChanged );
			_onPropertyChanged();
		}

		public void UnsubscribeFromPropertyChanged( String _propertyName, Action _onPropertChanged )
		{
			if ( m_PropertyChangedSubscribers.TryGetValue( _propertyName, out List<Action> subscribers ) )
			{
				subscribers.Remove( _onPropertChanged );
			}
		}

		public void SubscribeToPrePropertyChanged( String _propertyName, Action _onPrePropertyChanged )
		{
			if ( !m_PrePropertyChangedSubscribers.TryGetValue( _propertyName, out List<Action> subscribers ) )
			{
				subscribers = new List<Action>();
				m_PrePropertyChangedSubscribers.Add( _propertyName, subscribers );
			}

			subscribers.Add( _onPrePropertyChanged );
		}

		public void UnsubscribeFromPrePropertyChanged( String _propertyName, Action _onPrePropertyChanged )
		{
			if ( m_PrePropertyChangedSubscribers.TryGetValue( _propertyName, out List<Action> subscribers ) )
			{
				subscribers.Remove( _onPrePropertyChanged );
			}
		}

		protected void RaisePropertyChanged( String _propertyName )
		{
			if ( !m_PropertyChangedSubscribers.TryGetValue( _propertyName, out List<Action> subscribers ) )
			{
				return;
			}

			foreach ( Action onPropertyChanged in subscribers )
			{
				onPropertyChanged();
			}
		}

		protected void RaisePrePropertyChanged( String _propertyName )
		{
			if ( !m_PrePropertyChangedSubscribers.TryGetValue( _propertyName, out List<Action> subscribers ) )
			{
				return;
			}

			foreach ( Action onPrePropertyChanged in subscribers )
			{
				onPrePropertyChanged();
			}
		}

		protected virtual void OnTopChanged()
		{
			RecalculateTopLeft();
			RecalculateHeight();
		}

		protected virtual void OnBottomChanged()
		{
			RecalculateBottomRight();
			RecalculateHeight();
		}

		protected virtual void OnLeftChanged()
		{
			RecalculateTopLeft();
			RecalculateWidth();
		}

		protected virtual void OnRightChanged()
		{
			RecalculateBottomRight();
			RecalculateWidth();
		}

		protected virtual void OnTopLeftChanged()
		{
			RecalculateInnerTopLeft();
			RecalculateAbsoluteTopLeft();
		}

		protected virtual void OnBottomRightChanged()
		{
			RecalculateInnerBottomRight();
			RecalculateAbsoluteBottomRight();
		}

		protected virtual void OnParentChanged()
		{
			RecalculateAbsoluteTopLeft();
			RecalculateAbsoluteBottomRight();
			RecalculateTop();
			RecalculateBottom();
			RecalculateLeft();
			RecalculateRight();
		}

		protected virtual void OnBottomPaddingChanged()
		{
			RecalculateInnerHeight();
			RecalculateInnerBottomRight();
		}

		protected virtual void OnTopPaddingChanged()
		{
			RecalculateInnerHeight();
			RecalculateInnerTopLeft();
		}

		protected virtual void OnHeightChanged()
		{
			RecalculateInnerHeight();
		}

		protected virtual void OnRightPaddingChanged()
		{
			RecalculateInnerWidth();
			RecalculateInnerBottomRight();
		}

		protected virtual void OnLeftPaddingChanged()
		{
			RecalculateInnerWidth();
			RecalculateInnerTopLeft();
		}

		protected virtual void OnWidthChanged()
		{
			RecalculateInnerWidth();
		}

		protected virtual void OnParentAbsoluteTopLeftChanged()
		{
			RecalculateAbsoluteTopLeft();
			RecalculateAbsoluteBottomRight();
		}

		protected virtual void OnInnerBottomRightChanged()
		{
			RecalculateAbsoluteInnerBottomRight();
		}

		protected virtual void OnInnerTopLeftChanged()
		{
			RecalculateAbsoluteInnerTopLeft();
		}

		protected virtual void OnRightAnchorChanged()
		{
			RecalculateRight();
		}

		protected virtual void OnLeftAnchorChanged()
		{
			RecalculateLeft();
		}

		protected virtual void OnBottomAnchorChanged()
		{
			RecalculateBottom();
		}

		protected virtual void OnTopAnchorChanged()
		{
			RecalculateTop();
		}

		protected virtual void OnUseVerticalAnchorsChanged()
		{
			RecalculateTop();
			RecalculateBottom();
		}

		protected virtual void OnUseHorizontalAnchorsChanged()
		{
			RecalculateLeft();
			RecalculateRight();
		}

		protected virtual void OnParentAbsoluteInnerBottomRightChanged()
		{
			RecalculateBottom();
			RecalculateRight();
			RecalculateAbsoluteInnerBottomRight();
		}

		protected virtual void OnParentAbsoluteInnerTopLeftChanged()
		{
			RecalculateTop();
			RecalculateLeft();
			RecalculateAbsoluteInnerTopLeft();
		}

		protected virtual void OnParentInnerWidthChanged()
		{
			RecalculateLeft();
			RecalculateRight();
		}

		protected virtual void OnParentInnerHeightChanged()
		{
			RecalculateTop();
			RecalculateBottom();
		}

		protected virtual void OnUIModeChanged()
		{
			if ( Parent != null )
			{
				UIMode = Parent.UIMode;
			}
		}

		protected virtual void SubscribeToParent()
		{
			Parent?.SubscribeToPropertyChanged( nameof( AbsoluteTopLeft ), OnParentAbsoluteTopLeftChanged );
			Parent?.SubscribeToPropertyChanged( nameof( AbsoluteInnerTopLeft ), OnParentAbsoluteInnerTopLeftChanged );
			Parent?.SubscribeToPropertyChanged( nameof( AbsoluteInnerBottomRight ), OnParentAbsoluteInnerBottomRightChanged );
			Parent?.SubscribeToPropertyChanged( nameof( InnerHeight ), OnParentInnerHeightChanged );
			Parent?.SubscribeToPropertyChanged( nameof( InnerWidth ), OnParentInnerWidthChanged );
			Parent?.SubscribeToPropertyChanged( nameof( UIMode ), OnUIModeChanged );
		}

		protected virtual void UnsubscribeFromParent( ContainerElement _parent )
		{
			_parent?.UnsubscribeFromPropertyChanged( nameof( AbsoluteTopLeft ), OnParentAbsoluteTopLeftChanged );
			_parent?.UnsubscribeFromPropertyChanged( nameof( AbsoluteInnerTopLeft ), OnParentAbsoluteInnerTopLeftChanged );
			_parent?.UnsubscribeFromPropertyChanged( nameof( AbsoluteInnerBottomRight ), OnParentAbsoluteInnerBottomRightChanged );
			_parent?.UnsubscribeFromPropertyChanged( nameof( InnerHeight ), OnParentInnerHeightChanged );
			_parent?.UnsubscribeFromPropertyChanged( nameof( InnerWidth ), OnParentInnerWidthChanged );
			_parent?.UnsubscribeFromPropertyChanged( nameof( UIMode ), OnUIModeChanged );
		}

		private void SubscribeToProperties()
		{
			SubscribeToPropertyChanged( nameof( Width ), OnWidthChanged );
			SubscribeToPropertyChanged( nameof( LeftPadding ), OnLeftPaddingChanged );
			SubscribeToPropertyChanged( nameof( RightPadding ), OnRightPaddingChanged );
			SubscribeToPropertyChanged( nameof( Height ), OnHeightChanged );
			SubscribeToPropertyChanged( nameof( TopPadding ), OnTopPaddingChanged );
			SubscribeToPropertyChanged( nameof( BottomPadding ), OnBottomPaddingChanged );
			SubscribeToPropertyChanged( nameof( Parent ), OnParentChanged );
			SubscribeToPropertyChanged( nameof( BottomRight ), OnBottomRightChanged );
			SubscribeToPropertyChanged( nameof( TopLeft ), OnTopLeftChanged );
			SubscribeToPropertyChanged( nameof( Left ), OnLeftChanged );
			SubscribeToPropertyChanged( nameof( Top ), OnTopChanged );
			SubscribeToPropertyChanged( nameof( Right ), OnRightChanged );
			SubscribeToPropertyChanged( nameof( Bottom ), OnBottomChanged );
			SubscribeToPropertyChanged( nameof( UseHorizontalAnchors ), OnUseHorizontalAnchorsChanged );
			SubscribeToPropertyChanged( nameof( UseVerticalAnchors ), OnUseVerticalAnchorsChanged );
			SubscribeToPropertyChanged( nameof( TopAnchor ), OnTopAnchorChanged );
			SubscribeToPropertyChanged( nameof( BottomAnchor ), OnBottomAnchorChanged );
			SubscribeToPropertyChanged( nameof( LeftAnchor ), OnLeftAnchorChanged );
			SubscribeToPropertyChanged( nameof( RightAnchor ), OnRightAnchorChanged );
			SubscribeToPropertyChanged( nameof( InnerTopLeft ), OnInnerTopLeftChanged );
			SubscribeToPropertyChanged( nameof( InnerBottomRight ), OnInnerBottomRightChanged );
		}

		private void UnsubscribeFromProperties()
		{
			UnsubscribeFromPropertyChanged( nameof( Width ), OnWidthChanged );
			UnsubscribeFromPropertyChanged( nameof( LeftPadding ), OnLeftPaddingChanged );
			UnsubscribeFromPropertyChanged( nameof( RightPadding ), OnRightPaddingChanged );
			UnsubscribeFromPropertyChanged( nameof( Height ), OnHeightChanged );
			UnsubscribeFromPropertyChanged( nameof( TopPadding ), OnTopPaddingChanged );
			UnsubscribeFromPropertyChanged( nameof( BottomPadding ), OnBottomPaddingChanged );
			UnsubscribeFromPropertyChanged( nameof( Parent ), OnParentChanged );
			UnsubscribeFromPropertyChanged( nameof( BottomRight ), OnBottomRightChanged );
			UnsubscribeFromPropertyChanged( nameof( TopLeft ), OnTopLeftChanged );
			UnsubscribeFromPropertyChanged( nameof( Left ), OnLeftChanged );
			UnsubscribeFromPropertyChanged( nameof( Top ), OnTopChanged );
			UnsubscribeFromPropertyChanged( nameof( Right ), OnRightChanged );
			UnsubscribeFromPropertyChanged( nameof( Bottom ), OnBottomChanged );
			UnsubscribeFromPropertyChanged( nameof( UseHorizontalAnchors ), OnUseHorizontalAnchorsChanged );
			UnsubscribeFromPropertyChanged( nameof( UseVerticalAnchors ), OnUseVerticalAnchorsChanged );
			UnsubscribeFromPropertyChanged( nameof( TopAnchor ), OnTopAnchorChanged );
			UnsubscribeFromPropertyChanged( nameof( BottomAnchor ), OnBottomAnchorChanged );
			UnsubscribeFromPropertyChanged( nameof( LeftAnchor ), OnLeftAnchorChanged );
			UnsubscribeFromPropertyChanged( nameof( RightAnchor ), OnRightAnchorChanged );
			UnsubscribeFromPropertyChanged( nameof( InnerTopLeft ), OnInnerTopLeftChanged );
			UnsubscribeFromPropertyChanged( nameof( InnerBottomRight ), OnInnerBottomRightChanged );
			UnsubscribeFromParent( Parent );
		}
	}
}