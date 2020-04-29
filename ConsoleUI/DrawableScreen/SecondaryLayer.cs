using System;
using System.Drawing;

namespace ConsoleUI.DrawableScreen
{
	public class SecondaryLayer : Layer
	{
		private readonly PrimaryLayer m_PrimaryLayer;

		public SecondaryLayer( Int32 _windowWidth, Int32 _windowHeight, Int32 _layerIndex, PrimaryLayer _primaryLayer ) : base( _windowWidth, _windowHeight, _layerIndex )
			=> m_PrimaryLayer = _primaryLayer;

		public override void AddContent( LayerCoordContent _content, Coord _cursorPosition )
		{
			if ( _content != Content[_cursorPosition.X, _cursorPosition.Y] )
			{
				base.AddContent( _content, _cursorPosition );
				m_PrimaryLayer.AddContent( _content, _cursorPosition );
			}
		}

		public void AddContent( Char _character, Coord _cursorPosition, Color _foregroundColor, Color _backgroundColor, Guid _elementID )
		{
			AddContent( new LayerCoordContent( _character, _foregroundColor, _backgroundColor, _elementID, LayerIndex ), _cursorPosition );
		}

		public void ClearElement( Guid _elementID )
		{
			for ( UInt16 x = 0; x < Content.GetLength( 0 ); x++ )
			{
				for ( UInt16 y = 0; y < Content.GetLength( 1 ); y++ )
				{
					LayerCoordContent content = Content[x, y];
					if ( content != null && content.ElementID == _elementID )
					{
						Content[x, y] = null;
						m_PrimaryLayer.RemoveContent( _elementID, new Coord( x, y ) );
					}
				}
			}
		}

		public void AddContent( String _text, Coord _cursorPosition, Color _foregroundColor, Color _backgroundColor, Guid _elementID )
		{
			for ( UInt16 x = 0; x < _text.Length; x++ )
			{
				AddContent( new LayerCoordContent( _text[x], _foregroundColor, _backgroundColor, _elementID, LayerIndex ), new Coord( (UInt16)( _cursorPosition.X + x ), _cursorPosition.Y ) );
			}
		}
	}
}