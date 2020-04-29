using System;
using System.Diagnostics;

namespace ConsoleUI
{
	/// <summary>
	/// Defines the coordinates of a character cell in a console screen buffer.
	/// The origin of the coordinate system (0,0) is at the top, left cell of the buffer.
	/// <see href="https://docs.microsoft.com/en-us/windows/console/coord-str"/>
	/// </summary>
	[DebuggerDisplay( "{X}, {Y}" )]
	public struct Coord
	{
		public UInt16 X;
		public UInt16 Y;

		public Coord( UInt16 _x, UInt16 _y )
		{
			X = _x;
			Y = _y;
		}

		public override Boolean Equals( Object _obj )
		{
			return _obj is Coord other && Equals( other );
		}

		public override Int32 GetHashCode()
		{
			return HashCode.Combine( X, Y );
		}

		public Boolean Equals( Coord _other )
		{
			return X == _other.X && Y == _other.Y;
		}

		public static Boolean operator ==( Coord _a, Coord _b )
		{
			return _a.X == _b.X && _a.Y == _b.Y;
		}

		public static Boolean operator !=( Coord _a, Coord _b )
		{
			return !( _a == _b );
		}

		public static Coord operator +( Coord _a, Coord _b )
		{
			return new Coord( (UInt16)( _a.X + _b.X ), (UInt16)( _a.Y + _b.Y ) );
		}

		public static Coord operator -( Coord _a, Coord _b )
		{
			return new Coord( (UInt16)( _a.X - _b.X ), (UInt16)( _a.Y - _b.Y ) );
		}
	}
}