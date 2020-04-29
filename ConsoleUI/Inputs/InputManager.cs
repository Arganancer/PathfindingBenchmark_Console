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
using System.ComponentModel;
using ConsoleUI.Inputs.Keyboard;
using ConsoleUI.Inputs.Mouse;
using ConsoleUI.Settings;

namespace ConsoleUI.Inputs
{
	internal class InputManager
	{
		private readonly ConsoleHandle m_Handle;

		private Coord m_LastCoord;
		private readonly MouseButtonState m_LastButtonState;

		public static event EventHandler<MouseMoveEventArgs> MouseMove;
		public static event EventHandler<MouseButtonEventArgs> MouseButton;
		public static event EventHandler<KeyEventArgs> KeyEvent;

		public InputManager()
		{
			m_Handle = NativeMethods.GetStdHandle( NativeMethods.StdInputHandle );
			m_LastCoord = new Coord { X = 0, Y = 0 };
			m_LastButtonState = MouseButtonState.None;
		}

		public void ProcessInputs()
		{
			UInt32 nbOfUnreadInputEvents = 0;

			if ( !NativeMethods.GetNumberOfConsoleInputEvents( m_Handle, ref nbOfUnreadInputEvents ) )
			{
				throw new Win32Exception();
			}

			for ( Int32 i = 0; i < nbOfUnreadInputEvents; i++ )
			{
				UInt32 recordsRead = 0;
				InputRecord record = new InputRecord();
				if ( !NativeMethods.ReadConsoleInput( m_Handle, ref record, 1, ref recordsRead ) )
				{
					throw new Win32Exception();
				}

				switch ( record.EventType )
				{
					case NativeMethods.MouseEvent:
						MouseEvent( record.MouseEvent );
						break;
					case NativeMethods.KeyEvent:
						OnKeyEvent( new KeyEventArgs( 
							record.KeyEvent.bKeyDown, 
							(ConsoleKey)record.KeyEvent.wVirtualKeyCode, 
							(ControlKeyState)record.KeyEvent.dwControlKeyState ) );
						break;
				}
			}
		}

		protected virtual void OnMouseMove( MouseMoveEventArgs _e )
		{
			MouseMove?.Invoke( this, _e );
		}

		protected virtual void OnMouseButton( MouseButtonEventArgs _e )
		{
			MouseButton?.Invoke( this, _e );
		}

		protected virtual void OnKeyEvent( KeyEventArgs _e )
		{
			KeyEvent?.Invoke( this, _e );
		}

		private void MouseEvent( MouseEventRecord _recordMouseEvent )
		{
			MouseButtonState buttonState = (MouseButtonState)_recordMouseEvent.dwButtonState;
			MouseEventFlags eventFlags = (MouseEventFlags)_recordMouseEvent.dwEventFlags;
			ControlKeyState controlKeyState = (ControlKeyState)_recordMouseEvent.dwControlKeyState;
			Coord mousePosition = _recordMouseEvent.dwMousePosition;

			if ( eventFlags.HasFlag( MouseEventFlags.MouseMoved ) )
			{
				if ( mousePosition != m_LastCoord )
				{
					OnMouseMove( new MouseMoveEventArgs( mousePosition, m_LastCoord ) );
					m_LastCoord = mousePosition;
				}

				return;
			}

			MouseButtonState changedButtons = buttonState ^ m_LastButtonState;
			foreach ( MouseButtonState singleButton in (MouseButtonState[])Enum.GetValues( typeof( MouseButtonState ) ) )
			{
				if ( changedButtons.HasFlag( singleButton ) )
				{
					OnMouseButton( new MouseButtonEventArgs( singleButton, eventFlags, controlKeyState ) );
				}
			}
		}
	}
}