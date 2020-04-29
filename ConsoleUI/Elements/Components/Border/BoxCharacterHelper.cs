using System;

// ReSharper disable SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault

namespace ConsoleUI.Elements.Components.Border
{
	internal static class BoxCharacterHelper
	{
		private const Char LightVertical = '│';
		private const Char LightHorizontal = '─';
		private const Char LightDownLightLeft = '┐';
		private const Char LightDownLightRight = '┌';
		private const Char LightUpLightLeft = '┘';
		private const Char LightUpLightRight = '└';
		private const Char LightDownLightHorizontal = '┬';
		private const Char LightUpLightHorizontal = '┴';
		private const Char LightVerticalLightLeft = '┤';
		private const Char LightVerticalLightRight = '├';
		private const Char LightVerticalLightHorizontal = '┼';
		private const Char HeavyHorizontal = '━';
		private const Char HeavyVertical = '┃';
		private const Char LightTripleDashHorizontal = '┄';
		private const Char HeavyTripleDashHorizontal = '┅';
		private const Char LightTripleDashVertical = '┆';
		private const Char HeavyTripleDashVertical = '┇';
		private const Char LightDownHeavyRight = '┍';
		private const Char HeavyDownLightRight = '┎';
		private const Char HeavyDownHeavyRight = '┏';
		private const Char LightDownHeavyLeft = '┑';
		private const Char HeavyDownLightLeft = '┒';
		private const Char HeavyDownHeavyLeft = '┓';
		private const Char LightUpHeavyRight = '┕';
		private const Char HeavyUpLightRight = '┖';
		private const Char HeavyUpHeavyRight = '┗';
		private const Char LightUpHeavyLeft = '┙';
		private const Char HeavyUpLightLeft = '┚';
		private const Char HeavyUpHeavyLeft = '┛';
		private const Char LightVerticalHeavyRight = '┝';
		private const Char HeavyUpLightDownLightRight = '┞';
		private const Char LightUpHeavyDownLightRight = '┟';
		private const Char HeavyVerticalLightRight = '┠';
		private const Char HeavyUpLightDownHeavyRight = '┡';
		private const Char LightUpHeavyDownHeavyRight = '┢';
		private const Char HeavyVerticalHeavyRight = '┣';
		private const Char LightVerticalHeavyLeft = '┥';
		private const Char HeavyUpLightDownLightLeft = '┦';
		private const Char LightUpHeavyDownLightLeft = '┧';
		private const Char HeavyVerticalLightLeft = '┨';
		private const Char HeavyUpLightDownHeavyLeft = '┩';
		private const Char LightUpHeavyDownHeavyLeft = '┪';
		private const Char HeavyVerticalHeavyLeft = '┫';
		private const Char LightDownHeavyLeftLightRight = '┭';
		private const Char LightDownLightLeftHeavyRight = '┮';
		private const Char LightDownHeavyHorizontal = '┯';
		private const Char HeavyDownLightHorizontal = '┰';
		private const Char HeavyDownHeavyLeftLightRight = '┱';
		private const Char HeavyDownLightLeftHeavyRight = '┲';
		private const Char HeavyDownHeavyHorizontal = '┳';
		private const Char LightUpHeavyLeftLightRight = '┵';
		private const Char LightUpLightLeftHeavyRight = '┶';
		private const Char LightUpHeavyHorizontal = '┷';
		private const Char HeavyUpLightHorizontal = '┸';
		private const Char HeavyUpHeavyLeftLightRight = '┹';
		private const Char HeavyUpLightLeftHeavyRight = '┺';
		private const Char HeavyUpHeavyHorizontal = '┻';
		private const Char LightVerticalHeavyLeftLightRight = '┽';
		private const Char LightVerticalLightLeftHeavyRight = '┾';
		private const Char LightVerticalHeavyHorizontal = '┿';
		private const Char HeavyUpLightDownLightHorizontal = '╀';
		private const Char LightUpHeavyDownLightHorizontal = '╁';
		private const Char HeavyVerticalLightHorizontal = '╂';
		private const Char HeavyUpLightDownHeavyLeftLightRight = '╃';
		private const Char HeavyUpLightDownLightLeftHeavyRight = '╄';
		private const Char LightUpHeavyDownHeavyLeftLightRight = '╅';
		private const Char LightUpHeavyDownLightLeftHeavyRight = '╆';
		private const Char HeavyUpLightDownHeavyHorizontal = '╇';
		private const Char LightUpHeavyDownHeavyHorizontal = '╈';
		private const Char HeavyVerticalHeavyLeftLightRight = '╉';
		private const Char HeavyVerticalLightLeftHeavyRight = '╊';
		private const Char HeavyVerticalHeavyHorizontal = '╋';
		private const Char LightDoubleDashHorizontal = '╌';
		private const Char HeavyDoubleDashHorizontal = '╍';
		private const Char LightDoubleDashVertical = '╎';
		private const Char HeavyDoubleDashVertical = '╏';
		private const Char DoubleHorizontal = '═';
		private const Char DoubleVertical = '║';
		private const Char LightDownDoubleRight = '╒';
		private const Char DoubleDownLightRight = '╓';
		private const Char DoubleDownDoubleRight = '╔';
		private const Char LightDownDoubleLeft = '╕';
		private const Char DoubleDownLightLeft = '╖';
		private const Char DoubleDownDoubleLeft = '╗';
		private const Char LightUpDoubleRight = '╘';
		private const Char DoubleUpLightRight = '╙';
		private const Char DoubleUpDoubleRight = '╚';
		private const Char LightUpDoubleLeft = '╛';
		private const Char DoubleUpLightLeft = '╜';
		private const Char DoubleUpDoubleLeft = '╝';
		private const Char LightVerticalDoubleRight = '╞';
		private const Char DoubleVerticalLightRight = '╟';
		private const Char DoubleVerticalDoubleRight = '╠';
		private const Char LightVerticalDoubleLeft = '╡';
		private const Char DoubleVerticalLightLeft = '╢';
		private const Char DoubleVerticalDoubleLeft = '╣';
		private const Char LightDownDoubleHorizontal = '╤';
		private const Char DoubleDownLightHorizontal = '╥';
		private const Char DoubleDownDoubleHorizontal = '╦';
		private const Char LightUpDoubleHorizontal = '╧';
		private const Char DoubleUpLightHorizontal = '╨';
		private const Char DoubleUpDoubleHorizontal = '╩';
		private const Char LightVerticalDoubleHorizontal = '╪';
		private const Char DoubleVerticalLightHorizontal = '╫';
		private const Char DoubleVerticalDoubleHorizontal = '╬';

		internal static Char GetVerticalHorizontal( BorderStyle _up, BorderStyle _down, BorderStyle _left, BorderStyle _right )
		{
			BorderStyle up = ReduceToHeaviest( _up );
			BorderStyle down = ReduceToHeaviest( _down );
			BorderStyle left = ReduceToHeaviest( _left );
			BorderStyle right = ReduceToHeaviest( _right );

			if ( up == BorderStyle.Double || down == BorderStyle.Double )
			{
				if ( left == BorderStyle.Double || left == BorderStyle.Heavy || right == BorderStyle.Double || right == BorderStyle.Heavy )
				{
					return DoubleVerticalDoubleHorizontal;
				}

				return DoubleVerticalLightHorizontal;
			}

			if ( left == BorderStyle.Double || right == BorderStyle.Double )
			{
				if ( up == BorderStyle.Heavy || down == BorderStyle.Heavy )
				{
					return DoubleVerticalDoubleHorizontal;
				}

				return LightVerticalDoubleHorizontal;
			}

			return up switch
			{
				BorderStyle.Light => ( down switch
				{
					BorderStyle.Light => ( left switch
					{
						BorderStyle.Light => ( right switch
						{
							BorderStyle.Light => LightVerticalLightHorizontal,
							BorderStyle.Heavy => LightVerticalLightLeftHeavyRight,
							_ => throw new ArgumentOutOfRangeException()
						} ),
						BorderStyle.Heavy => ( right switch
						{
							BorderStyle.Light => LightVerticalHeavyLeftLightRight,
							BorderStyle.Heavy => LightVerticalHeavyHorizontal,
							_ => throw new ArgumentOutOfRangeException()
						} ),
						_ => throw new ArgumentOutOfRangeException()
					} ),
					BorderStyle.Heavy => ( left switch
					{
						BorderStyle.Light => ( right switch
						{
							BorderStyle.Light => LightUpHeavyDownLightHorizontal,
							BorderStyle.Heavy => LightUpHeavyDownLightLeftHeavyRight,
							_ => throw new ArgumentOutOfRangeException()
						} ),
						BorderStyle.Heavy => ( right switch
						{
							BorderStyle.Light => LightUpHeavyDownHeavyLeftLightRight,
							BorderStyle.Heavy => LightUpHeavyDownHeavyHorizontal,
							_ => throw new ArgumentOutOfRangeException()
						} ),
						_ => throw new ArgumentOutOfRangeException()
					} ),
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Heavy => ( down switch
				{
					BorderStyle.Light => ( left switch
					{
						BorderStyle.Light => ( right switch
						{
							BorderStyle.Light => HeavyUpLightDownLightHorizontal,
							BorderStyle.Heavy => HeavyUpLightDownLightLeftHeavyRight,
							_ => throw new ArgumentOutOfRangeException()
						} ),
						BorderStyle.Heavy => ( right switch
						{
							BorderStyle.Light => HeavyUpLightDownHeavyLeftLightRight,
							BorderStyle.Heavy => HeavyUpLightDownHeavyHorizontal,
							_ => throw new ArgumentOutOfRangeException()
						} ),
						_ => throw new ArgumentOutOfRangeException()
					} ),
					BorderStyle.Heavy => ( left switch
					{
						BorderStyle.Light => ( right switch
						{
							BorderStyle.Light => HeavyVerticalLightHorizontal,
							BorderStyle.Heavy => HeavyVerticalLightLeftHeavyRight,
							_ => throw new ArgumentOutOfRangeException()
						} ),
						BorderStyle.Heavy => ( right switch
						{
							BorderStyle.Light => HeavyVerticalHeavyLeftLightRight,
							BorderStyle.Heavy => HeavyVerticalHeavyHorizontal,
							_ => throw new ArgumentOutOfRangeException()
						} ),
						_ => throw new ArgumentOutOfRangeException()
					} ),
					_ => throw new ArgumentOutOfRangeException()
				} ),
				_ => throw new ArgumentOutOfRangeException()
			};
		}

		internal static Char GetHorizontalTop( BorderStyle _left, BorderStyle _right, BorderStyle _up )
		{
			BorderStyle up = ReduceToHeaviest( _up );
			BorderStyle left = ReduceToHeaviest( _left );
			BorderStyle right = ReduceToHeaviest( _right );

			return up switch
			{
				BorderStyle.Light => ( left switch
				{
					BorderStyle.Light => right switch
					{
						BorderStyle.Light => LightUpLightHorizontal,
						BorderStyle.Heavy => LightUpLightLeftHeavyRight,
						BorderStyle.Double => LightUpDoubleHorizontal,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Heavy => right switch
					{
						BorderStyle.Light => LightUpHeavyLeftLightRight,
						BorderStyle.Heavy => LightUpHeavyHorizontal,
						BorderStyle.Double => LightUpDoubleHorizontal,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Double => LightUpDoubleHorizontal,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Heavy => ( left switch
				{
					BorderStyle.Light => right switch
					{
						BorderStyle.Light => HeavyUpLightHorizontal,
						BorderStyle.Heavy => HeavyUpLightLeftHeavyRight,
						BorderStyle.Double => DoubleUpDoubleHorizontal,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Heavy => right switch
					{
						BorderStyle.Light => HeavyUpHeavyLeftLightRight,
						BorderStyle.Heavy => HeavyUpHeavyHorizontal,
						BorderStyle.Double => DoubleUpDoubleHorizontal,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Double => DoubleUpDoubleHorizontal,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Double => ( left switch
				{
					BorderStyle.Light => right switch
					{
						BorderStyle.Light => DoubleUpLightHorizontal,
						BorderStyle.Heavy => DoubleUpDoubleHorizontal,
						BorderStyle.Double => DoubleUpDoubleHorizontal,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Heavy => DoubleUpDoubleHorizontal,
					BorderStyle.Double => DoubleUpDoubleHorizontal,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				_ => throw new ArgumentOutOfRangeException()
			};
		}

		internal static Char GetHorizontalDown( BorderStyle _left, BorderStyle _right, BorderStyle _down )
		{
			BorderStyle down = ReduceToHeaviest( _down );
			BorderStyle left = ReduceToHeaviest( _left );
			BorderStyle right = ReduceToHeaviest( _right );

			return down switch
			{
				BorderStyle.Light => ( left switch
				{
					BorderStyle.Light => right switch
					{
						BorderStyle.Light => LightDownLightHorizontal,
						BorderStyle.Heavy => LightDownLightLeftHeavyRight,
						BorderStyle.Double => LightDownDoubleHorizontal,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Heavy => right switch
					{
						BorderStyle.Light => LightDownHeavyLeftLightRight,
						BorderStyle.Heavy => LightDownHeavyHorizontal,
						BorderStyle.Double => LightDownDoubleHorizontal,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Double => LightDownDoubleHorizontal,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Heavy => ( left switch
				{
					BorderStyle.Light => right switch
					{
						BorderStyle.Light => HeavyDownLightHorizontal,
						BorderStyle.Heavy => HeavyDownLightLeftHeavyRight,
						BorderStyle.Double => DoubleDownDoubleHorizontal,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Heavy => right switch
					{
						BorderStyle.Light => HeavyDownHeavyLeftLightRight,
						BorderStyle.Heavy => HeavyDownHeavyHorizontal,
						BorderStyle.Double => DoubleDownDoubleHorizontal,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Double => DoubleDownDoubleHorizontal,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Double => ( left switch
				{
					BorderStyle.Light => right switch
					{
						BorderStyle.Light => DoubleDownLightHorizontal,
						BorderStyle.Heavy => DoubleDownDoubleHorizontal,
						BorderStyle.Double => DoubleDownDoubleHorizontal,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Heavy => DoubleDownDoubleHorizontal,
					BorderStyle.Double => DoubleDownDoubleHorizontal,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				_ => throw new ArgumentOutOfRangeException()
			};
		}

		internal static Char GetHorizontal( BorderStyle _current )
		{
			BorderStyle current = ReduceToHeaviest( _current, true );

			return current switch
			{
				BorderStyle.Light => LightHorizontal,
				BorderStyle.Heavy => HeavyHorizontal,
				BorderStyle.Double => DoubleHorizontal,
				BorderStyle.LightDoubleDash => LightDoubleDashHorizontal,
				BorderStyle.HeavyDoubleDash => HeavyDoubleDashHorizontal,
				BorderStyle.LightTripleDash => LightTripleDashHorizontal,
				BorderStyle.HeavyTripleDash => HeavyTripleDashHorizontal,
				_ => throw new ArgumentOutOfRangeException( nameof( current ), current, null )
			};
		}

		internal static Char GetDownLeft( BorderStyle _down, BorderStyle _left )
		{
			BorderStyle down = ReduceToHeaviest( _down );
			BorderStyle left = ReduceToHeaviest( _left );

			return down switch
			{
				BorderStyle.Light => ( left switch
				{
					BorderStyle.Light => LightDownLightLeft,
					BorderStyle.Heavy => LightDownHeavyLeft,
					BorderStyle.Double => LightDownDoubleLeft,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Heavy => ( left switch
				{
					BorderStyle.Light => HeavyDownLightLeft,
					BorderStyle.Heavy => HeavyDownHeavyLeft,
					BorderStyle.Double => DoubleDownDoubleLeft,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Double => ( left switch
				{
					BorderStyle.Light => DoubleDownLightLeft,
					BorderStyle.Heavy => DoubleDownDoubleLeft,
					BorderStyle.Double => DoubleDownDoubleLeft,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				_ => throw new ArgumentOutOfRangeException()
			};
		}

		internal static Char GetVerticalLeft( BorderStyle _up, BorderStyle _down, BorderStyle _left )
		{
			BorderStyle up = ReduceToHeaviest( _up );
			BorderStyle down = ReduceToHeaviest( _down );
			BorderStyle left = ReduceToHeaviest( _left );

			return up switch
			{
				BorderStyle.Light => ( down switch
				{
					BorderStyle.Light => left switch
					{
						BorderStyle.Light => LightVerticalLightLeft,
						BorderStyle.Heavy => LightVerticalHeavyLeft,
						BorderStyle.Double => LightVerticalDoubleLeft,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Heavy => left switch
					{
						BorderStyle.Light => LightUpHeavyDownLightLeft,
						BorderStyle.Heavy => LightUpHeavyDownHeavyLeft,
						BorderStyle.Double => DoubleVerticalDoubleLeft,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Double => left switch
					{
						BorderStyle.Light => DoubleVerticalLightLeft,
						BorderStyle.Heavy => DoubleVerticalDoubleLeft,
						BorderStyle.Double => DoubleVerticalDoubleLeft,
						_ => throw new ArgumentOutOfRangeException()
					},
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Heavy => ( down switch
				{
					BorderStyle.Light => left switch
					{
						BorderStyle.Light => HeavyUpLightDownLightLeft,
						BorderStyle.Heavy => HeavyUpLightDownHeavyLeft,
						BorderStyle.Double => DoubleVerticalDoubleLeft,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Heavy => left switch
					{
						BorderStyle.Light => HeavyVerticalLightLeft,
						BorderStyle.Heavy => HeavyVerticalHeavyLeft,
						BorderStyle.Double => DoubleVerticalDoubleLeft,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Double => DoubleVerticalDoubleLeft,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Double => ( left switch
				{
					BorderStyle.Light => DoubleVerticalLightLeft,
					BorderStyle.Heavy => DoubleVerticalDoubleLeft,
					BorderStyle.Double => DoubleVerticalDoubleLeft,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				_ => throw new ArgumentOutOfRangeException()
			};
		}

		internal static Char GetUpLeft( BorderStyle _up, BorderStyle _left )
		{
			BorderStyle up = ReduceToHeaviest( _up );
			BorderStyle left = ReduceToHeaviest( _left );

			return up switch
			{
				BorderStyle.Light => ( left switch
				{
					BorderStyle.Light => LightUpLightLeft,
					BorderStyle.Heavy => LightUpHeavyLeft,
					BorderStyle.Double => LightUpDoubleLeft,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Heavy => ( left switch
				{
					BorderStyle.Light => HeavyUpLightLeft,
					BorderStyle.Heavy => HeavyUpHeavyLeft,
					BorderStyle.Double => DoubleUpDoubleLeft,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Double => ( left switch
				{
					BorderStyle.Light => DoubleUpLightLeft,
					BorderStyle.Heavy => DoubleUpDoubleLeft,
					BorderStyle.Double => DoubleUpDoubleLeft,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				_ => throw new ArgumentOutOfRangeException()
			};
		}

		internal static Char GetVerticalRight( BorderStyle _up, BorderStyle _down, BorderStyle _right )
		{
			BorderStyle up = ReduceToHeaviest( _up );
			BorderStyle down = ReduceToHeaviest( _down );
			BorderStyle right = ReduceToHeaviest( _right );

			return up switch
			{
				BorderStyle.Light => ( down switch
				{
					BorderStyle.Light => right switch
					{
						BorderStyle.Light => LightVerticalLightRight,
						BorderStyle.Heavy => LightVerticalHeavyRight,
						BorderStyle.Double => LightVerticalDoubleRight,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Heavy => right switch
					{
						BorderStyle.Light => LightUpHeavyDownLightRight,
						BorderStyle.Heavy => LightUpHeavyDownHeavyRight,
						BorderStyle.Double => DoubleVerticalDoubleRight,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Double => right switch
					{
						BorderStyle.Light => DoubleVerticalLightRight,
						BorderStyle.Heavy => DoubleVerticalDoubleRight,
						BorderStyle.Double => DoubleVerticalDoubleRight,
						_ => throw new ArgumentOutOfRangeException()
					},
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Heavy => ( down switch
				{
					BorderStyle.Light => right switch
					{
						BorderStyle.Light => HeavyUpLightDownLightRight,
						BorderStyle.Heavy => HeavyUpLightDownHeavyRight,
						BorderStyle.Double => DoubleVerticalDoubleRight,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Heavy => right switch
					{
						BorderStyle.Light => HeavyVerticalLightRight,
						BorderStyle.Heavy => HeavyVerticalHeavyRight,
						BorderStyle.Double => DoubleVerticalDoubleRight,
						_ => throw new ArgumentOutOfRangeException()
					},
					BorderStyle.Double => DoubleVerticalDoubleRight,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Double => ( right switch
				{
					BorderStyle.Light => DoubleVerticalLightRight,
					BorderStyle.Heavy => DoubleVerticalDoubleRight,
					BorderStyle.Double => DoubleVerticalDoubleRight,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				_ => throw new ArgumentOutOfRangeException()
			};
		}

		internal static Char GetUpRight( BorderStyle _up, BorderStyle _right )
		{
			BorderStyle up = ReduceToHeaviest( _up );
			BorderStyle right = ReduceToHeaviest( _right );

			return up switch
			{
				BorderStyle.Light => ( right switch
				{
					BorderStyle.Light => LightUpLightRight,
					BorderStyle.Heavy => LightUpHeavyRight,
					BorderStyle.Double => LightUpDoubleRight,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Heavy => ( right switch
				{
					BorderStyle.Light => HeavyUpLightRight,
					BorderStyle.Heavy => HeavyUpHeavyRight,
					BorderStyle.Double => DoubleUpDoubleRight,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Double => ( right switch
				{
					BorderStyle.Light => DoubleUpLightRight,
					BorderStyle.Heavy => DoubleUpDoubleRight,
					BorderStyle.Double => DoubleUpDoubleRight,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				_ => throw new ArgumentOutOfRangeException()
			};
		}

		internal static Char GetDownRight( BorderStyle _down, BorderStyle _right )
		{
			BorderStyle down = ReduceToHeaviest( _down );
			BorderStyle right = ReduceToHeaviest( _right );

			return down switch
			{
				BorderStyle.Light => ( right switch
				{
					BorderStyle.Light => LightDownLightRight,
					BorderStyle.Heavy => LightDownHeavyRight,
					BorderStyle.Double => LightDownDoubleRight,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Heavy => ( right switch
				{
					BorderStyle.Light => HeavyDownLightRight,
					BorderStyle.Heavy => HeavyDownHeavyRight,
					BorderStyle.Double => DoubleDownDoubleRight,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				BorderStyle.Double => ( right switch
				{
					BorderStyle.Light => DoubleDownLightRight,
					BorderStyle.Heavy => DoubleDownDoubleRight,
					BorderStyle.Double => DoubleDownDoubleRight,
					_ => throw new ArgumentOutOfRangeException()
				} ),
				_ => throw new ArgumentOutOfRangeException()
			};
		}

		internal static Char GetVertical( BorderStyle _current )
		{
			BorderStyle current = ReduceToHeaviest( _current, true );

			return current switch
			{
				BorderStyle.Light => LightVertical,
				BorderStyle.Heavy => HeavyVertical,
				BorderStyle.Double => DoubleVertical,
				BorderStyle.LightDoubleDash => LightDoubleDashVertical,
				BorderStyle.HeavyDoubleDash => HeavyDoubleDashVertical,
				BorderStyle.LightTripleDash => LightTripleDashVertical,
				BorderStyle.HeavyTripleDash => HeavyTripleDashVertical,
				_ => throw new ArgumentOutOfRangeException( nameof( current ), current, null )
			};
		}

		private static BorderStyle ReduceToHeaviest( BorderStyle _borderStyle, Boolean _canBeDash = false )
		{
			if ( _borderStyle.HasFlag( BorderStyle.Double ) )
			{
				return BorderStyle.Double;
			}

			if ( _borderStyle.HasFlag( BorderStyle.HeavyTripleDash ) )
			{
				return _canBeDash ? BorderStyle.HeavyTripleDash : BorderStyle.Heavy;
			}

			if ( _borderStyle.HasFlag( BorderStyle.HeavyDoubleDash ) )
			{
				return _canBeDash ? BorderStyle.HeavyDoubleDash : BorderStyle.Heavy;
			}

			if ( _borderStyle.HasFlag( BorderStyle.Heavy ) )
			{
				return BorderStyle.Heavy;
			}

			if ( _borderStyle.HasFlag( BorderStyle.LightTripleDash ) )
			{
				return _canBeDash ? BorderStyle.LightTripleDash : BorderStyle.Light;
			}

			if ( _borderStyle.HasFlag( BorderStyle.LightDoubleDash ) )
			{
				return _canBeDash ? BorderStyle.LightDoubleDash : BorderStyle.Light;
			}

			return BorderStyle.Light;
		}
	}
}