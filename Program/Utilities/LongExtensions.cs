using System;

namespace Program.Utilities
{
	public static class LongExtensions
	{
		public static String GetBytesUsedString( this Int64 _bytes )
		{
			if ( _bytes < 1024 )
			{
				return $"{_bytes}bytes";
			}

			Single kilobytesUsed = _bytes / 1024.0f;

			if ( !( kilobytesUsed >= 1024 ) )
			{
				return $"{kilobytesUsed:N0}KB";
			}

			Single megabytesUsed = kilobytesUsed / 1024.0f;
			return $"{megabytesUsed:N0}MB";
		}
	}
}