using System;
using System.Drawing;

namespace ConsoleUI.DrawableScreen
{
	public class LayerCoordContent
	{
		public Char Character { get; }
		public Color ForegroundColor { get; }
		public Color BackgroundColor { get; }
		public Guid ElementID { get; }
		public Int32 LayerIndex { get; }
		public Boolean NeedsDrawing { get; set; }

		public LayerCoordContent( Char _character, Color _foregroundColor, Color _backgroundColor, Guid _elementId, Int32 _layerIndex )
		{
			Character = _character;
			ForegroundColor = _foregroundColor;
			BackgroundColor = _backgroundColor;
			ElementID = _elementId;
			LayerIndex = _layerIndex;
			NeedsDrawing = true;
		}

		public override Boolean Equals( Object _obj )
		{
			if ( ReferenceEquals( null, _obj ) )
			{
				return false;
			}

			if ( ReferenceEquals( this, _obj ) )
			{
				return true;
			}

			if ( _obj.GetType() != GetType() )
			{
				return false;
			}

			return Equals( (LayerCoordContent)_obj );
		}

		public override Int32 GetHashCode()
		{
			return HashCode.Combine( Character, ForegroundColor, BackgroundColor, ElementID, LayerIndex, NeedsDrawing );
		}

		public static Boolean operator ==( LayerCoordContent _a, LayerCoordContent _b )
		{
			return _a?.Equals( _b ) ?? ReferenceEquals( _b, null );
		}

		public static Boolean operator !=( LayerCoordContent _a, LayerCoordContent _b )
		{
			return !( _a == _b );
		}

		protected Boolean Equals( LayerCoordContent _other )
		{
			return !( _other is null ) && 
			       Character == _other.Character && 
			       ForegroundColor.Equals( _other.ForegroundColor ) && 
			       BackgroundColor.Equals( _other.BackgroundColor );
		}
	}
}