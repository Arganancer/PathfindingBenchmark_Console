using System;

namespace Pathfinding.Utils
{
	public static class Rand
	{
		private static readonly Random Random = new Random( 123456 );

		public static Int32 Next( Int32 _minValue, Int32 _maxValue )
		{
			return Random.Next( _minValue, _maxValue );
		}

		public static Int32 Next( Int32 _maxValue )
		{
			return Random.Next( 0, _maxValue );
		}

		/// <summary>
		/// Returns a random value between 0 and 1.
		/// </summary>
		/// <returns></returns>
		public static Double Percent()
		{
			return Random.NextDouble();
		}
	}
}