using System;
using System.Runtime.CompilerServices;
using Console = Colorful.Console;

namespace ConsoleUI.Elements.BaseElements
{
	public abstract partial class Element
	{
		private UIMode m_UIMode;

		public Guid ID { get; }

		public UIMode UIMode
		{
			get => m_UIMode;
			set => SetProperty( ref m_UIMode, value );
		}

		protected Element()
		{
			ID = Guid.NewGuid();
			IsDirty = true;
			SubscribeToProperties();
		}

		public virtual void Draw()
		{
			if ( !IsDirty )
			{
				return;
			}

			DrawIfDirty();
			IsDirty = false;
		}

		public virtual void SetDirty( Boolean _recursive = false )
		{
			IsDirty = true;
		}

		public Boolean ContainsCoord( Coord _coord )
		{
			Coord topleft = AbsoluteTopLeft;
			Coord bottomRight = AbsoluteBottomRight;
			return _coord.X >= topleft.X &&
			       _coord.X <= bottomRight.X &&
			       _coord.Y >= topleft.Y &&
			       _coord.Y <= bottomRight.Y;
		}

		public void ClearElement()
		{
			MainWindow.Window.ClearElement( ID, 0 );
		}

		/// <summary>
		/// Returns true if the element was set;
		/// </summary>
		protected Boolean SetProperty<T>( ref T _property, T _value, [CallerMemberName] String _propertyName = "" )
		{
			if ( _property?.Equals( _value ) ?? false )
			{
				return false;
			}

			RaisePrePropertyChanged( _propertyName );

			_property = _value;
			SetDirty();
			RaisePropertyChanged( _propertyName );

			return true;
		}

		protected abstract void DrawIfDirty();

		protected Boolean IsDirty { get; private set; }

		~Element() => UnsubscribeFromProperties();
	}
}