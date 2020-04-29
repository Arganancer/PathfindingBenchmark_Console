using System;
using Microsoft.Win32.SafeHandles;

namespace ConsoleUI.Settings
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class ConsoleHandle : SafeHandleMinusOneIsInvalid
	{
		public ConsoleHandle() : base( false )
		{
		}

		protected override Boolean ReleaseHandle()
		{
			return true; //releasing console handle is not our business
		}
	}
}