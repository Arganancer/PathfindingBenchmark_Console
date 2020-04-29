using System;

namespace ConsoleUI.Elements.Components.Scrollbar
{
	public static class BlockElementChars
	{
		public const Char EmptyBlock = ' ';
		public const Char LowerOneEighthBlock = '\u2581';
		public const Char LowerOneQuarterBlock = '\u2582';
		public const Char LowerThreeEighthsBlock = '\u2583';
		public const Char LowerHalfBlock = '\u2584';
		public const Char LowerFiveEighthsBlock = '\u2585';
		public const Char LowerThreeQuartersBlock = '\u2586';
		public const Char LowerSevenEighthsBlock = '\u2587';
		public const Char FullBlock = '\u2588';

		public static Char GetChar( Single _value )
		{
			Double fraction = _value - Math.Truncate( _value );

			if ( fraction <= 0.25f )
			{
				return FullBlock;
			}

			if ( fraction <= 0.75f )
			{
				return LowerHalfBlock;
			}

			return EmptyBlock;
		}
	}
}