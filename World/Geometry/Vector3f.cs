using System;

namespace World.Geometry
{
	public class Vector3f : IComparable<Vector3f>
	{
		public Single X { get; }
		public Single Y { get; }
		public Single Z { get; }

		public static Vector3f Zero => new Vector3f( 0, 0, 0 );

		public Vector3f( Vector3i _position )
		{
			X = _position.X;
			Y = _position.Y;
			Z = _position.Z;
		}

		public Vector3f( Vector3f _position )
		{
			X = _position.X;
			Y = _position.Y;
			Z = _position.Z;
		}

		public Vector3f( Single _x, Single _y, Single _z )
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

			return Equals( (Vector3f)_obj );
		}

		public override Int32 GetHashCode()
		{
			return HashCode.Combine( X, Y, Z );
		}

		public static Boolean operator ==( Vector3f _a, Vector3f _b )
		{
			return !ReferenceEquals( _a, null ) && !ReferenceEquals( _b, null ) &&
			       Math.Abs( _a.X - _b.X ) < Tolerance &&
			       Math.Abs( _a.Y - _b.Y ) < Tolerance &&
			       Math.Abs( _a.Z - _b.Z ) < Tolerance;
		}

		public static Boolean operator !=( Vector3f _a, Vector3f _b )
		{
			return !( _a == _b );
		}

		public static Vector3f operator +( Vector3f _a, Vector3f _b )
		{
			return new Vector3f( _a.X + _b.X, _a.Y + _b.Y, _a.Z + _b.Z );
		}

		public static implicit operator Vector3f( Vector3i _vector )
		{
			return new Vector3f( _vector );
		}

		public static implicit operator Vector3i( Vector3f _vector )
		{
			return new Vector3i( _vector );
		}

		public Int32 CompareTo( Vector3f _other )
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

		public Single Distance( Vector3f _other )
		{
			Single distX = Math.Abs( X - _other.X );
			Single distZ = Math.Abs( Z - _other.Z );

			return ( distX * distX ) + ( distZ * distZ );
		}

		private Boolean Equals( Vector3f _other )
		{
			return Math.Abs( X - _other.X ) < Tolerance && Math.Abs( Y - _other.Y ) < Tolerance && Math.Abs( Z - _other.Z ) < Tolerance;
		}

		private const Double Tolerance = 0.0001;
	}
}