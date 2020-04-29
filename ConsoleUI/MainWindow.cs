using System;
using ConsoleUI.DrawableScreen;
using ConsoleUI.Elements.BaseElements;
using ConsoleUI.Elements.Components.Border;
using ConsoleUI.Elements.Containers;
using ConsoleUI.Inputs;
using ConsoleUI.Settings;

namespace ConsoleUI
{
	public class MainWindow
	{
		public static Window Window;
		private ContainerElement m_RootElement;
		private InputManager m_InputManager;
		private UInt16 m_WindowHeight;
		private UInt16 m_WindowWidth;

		public UIMode UIMode
		{
			get => m_RootElement.UIMode;
			set => m_RootElement.UIMode = value;
		}

		public UInt16 WindowHeight
		{
			get => m_WindowHeight;
			private set
			{
				m_WindowHeight = value;
				BoxDrawer.SetWindowSize( WindowWidth, WindowHeight );
			}
		}

		public UInt16 WindowWidth
		{
			get => m_WindowWidth;
			private set
			{
				m_WindowWidth = value;
				BoxDrawer.SetWindowSize( WindowWidth, WindowHeight );
			}
		}

		public MainWindow( UInt16 _windowHeight, UInt16 _windowWidth )
		{
			WindowHeight = _windowHeight;
			WindowWidth = _windowWidth;
			Window = new Window( WindowWidth, WindowHeight );
			Initialize();
		}

		public void Update()
		{
			m_InputManager.ProcessInputs();
		}

		public void Draw()
		{
			m_RootElement.Draw();
			BoxDrawer.DrawBoxes();
			Window.Draw();
		}

		public void AddChild( Element _child )
		{
			m_RootElement.AddChild( _child );
		}

		public void RemoveChild( Element _child )
		{
			m_RootElement.RemoveChild( _child );
		}

		private void Initialize()
		{
			Console.SetWindowSize( WindowWidth, WindowHeight );

			Console.CursorVisible = false;
			Console.BufferWidth = Console.WindowWidth;
			Console.BufferHeight = Console.WindowHeight;

			ConsoleSettings.SetConsoleSettings();

			m_RootElement = new ContainerElement
			{
				Right = WindowWidth,
				Bottom = WindowHeight
			};

			m_InputManager = new InputManager();
		}

		public void SetDirty()
		{
			m_RootElement.SetDirty();
		}
	}
}