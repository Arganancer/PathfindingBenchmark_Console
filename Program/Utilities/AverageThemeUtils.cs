using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleUI.Settings;

namespace Program.Utilities
{
	public static class AverageThemeUtils
	{
		public static ThemeColor GetColor( Double _value, IEnumerable<Double> _values, Boolean _bestIsLowest )
		{
			Double[] orderedValues = ( _bestIsLowest ? _values.OrderBy( _d => _d ) : _values.OrderByDescending( _d => _d ) ).ToArray();

			for ( Int32 i = 0; i < orderedValues.Length; i++ )
			{
				if ( _bestIsLowest ? _value <= orderedValues[i] : _value >= orderedValues[i] )
				{
					return i switch
					{
						0 => ThemeColor.Best,
						1 => orderedValues.Length switch
						{
							2 => ThemeColor.Worst,
							3 => ThemeColor.Middle,
							_ => ThemeColor.SecondBest
						},
						2 => orderedValues.Length switch
						{
							3 => ThemeColor.Worst,
							4 => ThemeColor.SecondWorst,
							_ => ThemeColor.Middle
						},
						3 => orderedValues.Length switch
						{
							4 => ThemeColor.Worst,
							_ => ThemeColor.SecondWorst
						},
						_ => ThemeColor.Worst
					};
				}
			}

			return ThemeColor.Worst;
		}
	}
}
