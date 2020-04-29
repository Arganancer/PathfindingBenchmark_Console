using System;

namespace Program.Utilities
{
	public static class Rand
	{
		private static readonly Random Random = new Random();

		public static Int32 Next( Int32 _max )
		{
			return Random.Next( _max );
		}

		public static Int32 Next( Int32 _min, Int32 _max )
		{
			return Random.Next( _min, _max );
		}

		public static Single Next( Single _min, Single _max )
		{
			return (Single)( ( Percentage() * ( _max - _min ) ) + _min );
		}

		public static Double Percentage()
		{
			return Random.NextDouble();
		}
	}
}