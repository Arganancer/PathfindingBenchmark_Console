using System;
using System.ComponentModel;
using System.Text;

namespace ConsoleUI.Settings
{
	internal static class ConsoleSettings
	{
		public static ConsoleHandle Handle { get; private set; }

		public static void SetConsoleSettings()
		{
			IntPtr handle = NativeMethods.GetConsoleWindow();
			IntPtr sysMenu = NativeMethods.GetSystemMenu( handle, false );

			if ( handle == IntPtr.Zero )
			{
				return;
			}

			Console.OutputEncoding = Encoding.UTF8;

			//NativeMethods.DeleteMenu( sysMenu, NativeMethods.ScClose, NativeMethods.Mf_Bycommand );
			NativeMethods.DeleteMenu( sysMenu, NativeMethods.ScMinimize, NativeMethods.MfBycommand );
			NativeMethods.DeleteMenu( sysMenu, NativeMethods.ScMaximize, NativeMethods.MfBycommand );
			NativeMethods.DeleteMenu( sysMenu, NativeMethods.ScSize, NativeMethods.MfBycommand );

			SetConsoleMode();
		}

		private static void SetConsoleMode()
		{
			Handle = NativeMethods.GetStdHandle( NativeMethods.StdInputHandle );

			Int32 mode = 0;
			if ( !NativeMethods.GetConsoleMode( Handle, ref mode ) )
			{
				throw new Win32Exception();
			}

			mode |= NativeMethods.EnableMouseInput;
			mode &= ~NativeMethods.QuickEditMode;
			mode |= NativeMethods.ExtendedFlags;

			if ( !NativeMethods.SetConsoleMode( Handle, mode ) )
			{
				throw new Win32Exception();
			}
		}
	}
}