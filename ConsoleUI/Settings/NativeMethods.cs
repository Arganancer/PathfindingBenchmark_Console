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
using System.Runtime.InteropServices;
using ConsoleUI.Inputs;

namespace ConsoleUI.Settings
{
	/// <summary>
	/// List of all console functions: https://docs.microsoft.com/en-us/windows/console/console-functions
	/// </summary>
	internal static class NativeMethods
	{
		public const Int32 StdInputHandle = -10;
		public const Int32 QuickEditMode = 64;
		public const Int32 ExtendedFlags = 128;
		public const Int32 ScClose = 0xF060;
		public const Int32 ScMinimize = 0xF020;
		public const Int32 ScMaximize = 0xF030;
		public const Int32 ScSize = 0xF000;
		public const Int32 MfBycommand = 0x00000000;
		public const Int32 EnableMouseInput = 0x0010;
		public const Int32 EnableQuickEditMode = 0x0040;
		public const Int32 EnableExtendedFlags = 0x0080;

		public const Int32 KeyEvent = 1;
		public const Int32 MouseEvent = 2;

		[DllImport( "user32.dll" )]
		[return: MarshalAs( UnmanagedType.I4 )]
		public static extern Int32 DeleteMenu( IntPtr _hMenu, Int32 _nPosition, Int32 _wFlags );		
		
		[DllImport( "user32.dll" )]
		public static extern IntPtr GetSystemMenu( IntPtr _hWnd, Boolean _bRevert );

		/// <summary>
		/// <see href="https://docs.microsoft.com/en-us/windows/console/getconsolemode"/>
		/// </summary>
		[DllImport( "kernel32.dll", SetLastError = true )]
		[return: MarshalAs( UnmanagedType.Bool )]
		public static extern Boolean GetConsoleMode( ConsoleHandle _hConsoleHandle, ref Int32 _lpMode );

		/// <summary>
		/// <see href="https://docs.microsoft.com/en-us/windows/console/setconsolemode"/>
		/// </summary>
		[DllImport( "kernel32.dll", SetLastError = true )]
		[return: MarshalAs( UnmanagedType.Bool )]
		public static extern Boolean SetConsoleMode( ConsoleHandle _hConsoleHandle, Int32 _dwMode );

		/// <summary>
		/// <see href="https://docs.microsoft.com/en-us/windows/console/getconsolemode"/>
		/// </summary>
		[DllImport( "kernel32.dll", SetLastError = true )]
		[return: MarshalAs( UnmanagedType.Bool )]
		public static extern Boolean GetConsoleMode( IntPtr _hConsoleHandle, out Int32 _lpMode );

		/// <summary>
		/// <see href="https://docs.microsoft.com/en-us/windows/console/setconsolemode"/>
		/// </summary>
		[DllImport( "kernel32.dll", SetLastError = true )]
		[return: MarshalAs( UnmanagedType.Bool )]
		public static extern Boolean SetConsoleMode( IntPtr _hConsoleHandle, Int32 _ioMode );

		/// <summary>
		/// <see href="https://docs.microsoft.com/en-us/windows/console/getstdhandle"/>
		/// </summary>
		[DllImport( "kernel32.dll", SetLastError = true )]
		public static extern ConsoleHandle GetStdHandle( Int32 _nStdHandle );

		/// <summary>
		/// <see href="https://docs.microsoft.com/en-us/windows/console/readconsoleinput"/>
		/// </summary>
		[DllImport( "kernel32.dll", SetLastError = true )]
		[return: MarshalAs( UnmanagedType.Bool )]
		public static extern Boolean ReadConsoleInput( ConsoleHandle _hConsoleInput, ref InputRecord _lpBuffer, UInt32 _nLength, ref UInt32 _lpNumberOfEventsRead );

		/// <summary>
		/// <see href="https://docs.microsoft.com/en-us/windows/console/getconsolewindow"/>
		/// </summary>
		[DllImport( "kernel32.dll", ExactSpelling = true )]
		public static extern IntPtr GetConsoleWindow();

		/// <summary>
		/// The GetNumberOfConsoleInputEvents function reports the total number of unread input records in the input buffer,
		/// including keyboard, mouse, and window-resizing input records.
		/// <see href="https://docs.microsoft.com/en-us/windows/console/getnumberofconsoleinputevents"/>
		/// </summary>
		[DllImport( "kernel32.dll", SetLastError = true )]
		[return: MarshalAs( UnmanagedType.Bool )]
		public static extern Boolean GetNumberOfConsoleInputEvents( ConsoleHandle _hConsoleInput, ref UInt32 _nbOfEvents );
	}
}