using System;

namespace World.Geometry
{
	public class Vector3i : IComparable<Vector3i>
	{
		public Int32 X { get; }
		public Int32 Y { get; }
		public Int32 Z { get; }

		public static Vector3i Zero => new Vector3i( 0, 0, 0 );

		public Vector3i( Vector3i _position )
		{
			X = _position.X;
			Y = _position.Y;
			Z = _position.Z;
		}

		public Vector3i( Vector3f _position )
		{
			X = (Int32)_position.X;
			Y = (Int32)_position.Y;
			Z = (Int32)_position.Z;
		}

		public Vector3i( Int32 _x, Int32 _y, Int32 _z )
		{
			X = _x;
			Y = _y;
			Z = _z;
		}

		public override Boolean Equals( Object _obj )
		{
			if ( ReferenceEquals( null, _obj ) )
			{
				return false;
			}

			if ( ReferenceEquals( this, _obj ) )
			{
				return true;
			}

			if ( _obj.GetType() != GetType() )
			{
				return false;
			}

			return Equals( (Vector3i)_obj );
		}

		public override Int32 GetHashCode()
		{
			unchecked
			{
				Int32 hash = X.GetHashCode() * 486187739;
				hash = HashCode.Combine( hash * 486187739, Y.GetHashCode() );
				hash = HashCode.Combine( hash * 486187739, Z.GetHashCode() );
				return hash;
			}
		}

		public static Boolean operator ==( Vector3i _a, Vector3i _b )
		{
			return _a.X == _b.X && _a.Y == _b.Y && _a.Z == _b.Z;
		}

		public static Boolean operator !=( Vector3i _a, Vector3i _b )
		{
			return !( _a == _b );
		}

		public static Vector3i operator +( Vector3i _a, Vector3i _b )
		{
			return new Vector3i( _a.X + _b.X, _a.Y + _b.Y, _a.Z + _b.Z );
		}

		public Int32 CompareTo( Vector3i _other )
		{
			if ( X > _other.X )
			{
				return 1;
			}

			if ( X < _other.X )
			{
				return -1;
			}

			if ( Y > _other.Y )
			{
				return 1;
			}

			if ( Y < _other.Y )
			{
				return -1;
			}

			if ( Z > _other.Z )
			{
				return 1;
			}

			if ( Z < _other.Z )
			{
				return -1;
			}

			return 0;
		}

		public Single Distance( Vector3i _other )
		{
			Single distX = Math.Abs( X - _other.X );
			Single distZ = Math.Abs( Z - _other.Z );

			return ( distX * distX ) + ( distZ * distZ );
		}

		private Boolean Equals( Vector3i _other )
		{
			return X == _other.X && Y == _other.Y && Z == _other.Z;
		}
	}
}