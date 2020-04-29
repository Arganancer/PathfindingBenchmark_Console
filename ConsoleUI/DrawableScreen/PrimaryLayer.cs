using System;
using ConsoleUI.Settings;

namespace ConsoleUI.DrawableScreen
{
	public class PrimaryLayer : Layer
	{
		private readonly Window m_ParentWindow;

		public PrimaryLayer( Int32 _windowWidth, Int32 _windowHeight, Window _parentWindow ) : base( _windowWidth, _windowHeight, 3 )
		{
			m_ParentWindow = _parentWindow;
			for ( Int32 x = 0; x < _windowWidth; x++ )
			{
				for ( Int32 y = 0; y < _windowHeight; y++ )
				{
					Content[x, y] = new LayerCoordContent( ' ', Theme.Background, Theme.Background, Guid.Empty, LayerIndex );
				}
			}
		}

		public override void AddContent( LayerCoordContent _content, Coord _cursorPosition )
		{
			if ( _content.LayerIndex <= Content[_cursorPosition.X, _cursorPosition.Y].LayerIndex )
			{
				_content.NeedsDrawing = true;
				base.AddContent( _content, _cursorPosition );
			}
		}

		public void RemoveContent( Guid _elementID, Coord _cursorPosition )
		{
			if ( Content[_cursorPosition.X, _cursorPosition.Y].ElementID == _elementID )
			{
				LayerCoordContent content = GetTopLayerCoordContent( _cursorPosition );
				content.NeedsDrawing = true;
				base.AddContent( content, _cursorPosition );
			}
		}

		private LayerCoordContent GetTopLayerCoordContent( Coord _cursorPosition )
		{
			foreach ( SecondaryLayer layer in m_ParentWindow.Layers )
			{
				LayerCoordContent content = layer.Content[_cursorPosition.X, _cursorPosition.Y];
				if ( content != null )
				{
					return content;
				}
			}

			return new LayerCoordContent( ' ', Theme.Background, Theme.Background, Guid.Empty, LayerIndex );
		}
	}
}