using System;
using System.Drawing;

namespace ConsoleUI.DrawableScreen
{
	public abstract class Layer
	{
		public LayerCoordContent[,] Content { get; }

		protected Layer( Int32 _windowWidth, Int32 _windowHeight, Int32 _layerIndex )
		{
			Content = new LayerCoordContent[_windowWidth, _windowHeight];
			LayerIndex = _layerIndex;
		}

		public virtual void AddContent( LayerCoordContent _content, Coord _cursorPosition )
		{
			Content[_cursorPosition.X, _cursorPosition.Y] = _content;
		}

		protected Int32 LayerIndex { get; }
	}
}