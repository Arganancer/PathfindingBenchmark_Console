using System;
using System.Drawing;
using System.Text;
using ConsoleUI.Settings;
using Console = Colorful.Console;

namespace ConsoleUI.DrawableScreen
{
	public class Window
	{
		private readonly Int32 m_WindowWidth;
		private readonly Int32 m_WindowHeight;
		private SecondaryLayer[] m_Layers;

		public SecondaryLayer[] Layers => m_Layers;

		public PrimaryLayer PrimaryLayer { get; }

		public Window( Int32 _windowWidth, Int32 _windowHeight )
		{
			Console.BackgroundColor = Theme.Background;
			Console.ForegroundColor = Theme.Background;
			m_WindowWidth = _windowWidth;
			m_WindowHeight = _windowHeight;
			m_Layers = new SecondaryLayer[0];
			PrimaryLayer = new PrimaryLayer( _windowWidth, _windowHeight, this );
		}

		public void ClearElement( Guid _elementID, Int32 _layerIndex )
		{
			if ( _layerIndex < Layers.Length && Layers[_layerIndex] != null )
			{
				Layers[_layerIndex].ClearElement( _elementID );
			}
		}

		public void AddContent( String _text, Coord _cursorPosition, Color _foregroundColor, Color _backgroundColor, Guid _elementID, Int32 _layerIndex )
		{
			CreateLayerIfNotExist( _layerIndex );
			Layers[_layerIndex].AddContent( _text, _cursorPosition, _foregroundColor, _backgroundColor, _elementID );
		}

		public void AddContent( Char _character, Coord _cursorPosition, Color _foregroundColor, Color _backgroundColor, Guid _elementID, Int32 _layerIndex )
		{
			CreateLayerIfNotExist( _layerIndex );
			Layers[_layerIndex].AddContent( _character, _cursorPosition, _foregroundColor, _backgroundColor, _elementID );
		}

		public void Draw()
		{
			for ( UInt16 y = 0; y < m_WindowHeight; y++ )
			{
				for ( UInt16 x = 0; x < m_WindowWidth; x++ )
				{
					LayerCoordContent baseContent = PrimaryLayer.Content[x, y];
					if ( !baseContent.NeedsDrawing )
					{
						continue;
					}

					baseContent.NeedsDrawing = false;
					Coord cursorPosition = new Coord( x, y );
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append( baseContent.Character );
					x++;
					for ( ; x < m_WindowWidth; x++ )
					{
						LayerCoordContent nextContent = PrimaryLayer.Content[x, y];

						if ( !ContentCanBeDrawnTogether( baseContent, nextContent ) )
						{
							if ( !nextContent.NeedsDrawing )
							{
								break;
							}

							nextContent.NeedsDrawing = false;

							// Write base content.
							Console.SetCursorPosition( cursorPosition.X, cursorPosition.Y );
							Console.ForegroundColor = baseContent.ForegroundColor;
							Console.BackgroundColor = baseContent.BackgroundColor;
							Console.Write( stringBuilder.ToString() );

							// Start new base content.
							baseContent = nextContent;
							stringBuilder.Clear();
							cursorPosition = new Coord( x, y );
						}

						stringBuilder.Append( nextContent.Character );
					}

					// Finalize row content.
					Console.SetCursorPosition( cursorPosition.X, cursorPosition.Y );
					Console.ForegroundColor = baseContent.ForegroundColor;
					Console.BackgroundColor = baseContent.BackgroundColor;
					Console.Write( stringBuilder.ToString() );
				}
			}
		}

		private void CreateLayerIfNotExist( Int32 _layerIndex )
		{
			if ( _layerIndex >= Layers.Length )
			{
				Array.Resize( ref m_Layers, _layerIndex + 1 );
				Layers[_layerIndex] = new SecondaryLayer( m_WindowWidth, m_WindowHeight, _layerIndex, PrimaryLayer );
			}
			else if ( Layers[_layerIndex] is null )
			{
				Layers[_layerIndex] = new SecondaryLayer( m_WindowWidth, m_WindowHeight, _layerIndex, PrimaryLayer );
			}
		}

		private Boolean ContentCanBeDrawnTogether( LayerCoordContent _base, LayerCoordContent _other )
		{
			return _base.BackgroundColor == _other.BackgroundColor && ( _base.ForegroundColor == _other.ForegroundColor || _other.Character == ' ' );
		}
	}
}