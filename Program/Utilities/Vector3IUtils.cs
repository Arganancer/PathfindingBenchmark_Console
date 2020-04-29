using System;
using World.Geometry;

namespace Program.Utilities
{
	public static class Vector3IUtils
	{
		public static (Vector3i start, Vector3i end) GenerateStartEndPositions( Int32 _worldWidth, Int32 _worldHeight, Int32 _minimumDistance, Int32 _maximumDistance )
		{
			Vector3i startPos;
			Vector3i endPos;
			
			Int32 attempts = 0;

			if ( _minimumDistance < _worldWidth / 2 )
			{
				do
				{
					++attempts;
					startPos = GetRandomPosition( _worldWidth, _worldHeight );
					endPos = GetRandomEndPosition( startPos, _minimumDistance, _maximumDistance );
				} while ( endPos.X < 0 || endPos.Z < 0 || endPos.X >= _worldWidth || endPos.Z >= _worldHeight );
			}
			else
			{
				Single distance;
				do
				{
					++attempts;
					startPos = GetRandomPosition( _worldWidth, _worldHeight );
					endPos = GetRandomPosition( _worldWidth, _worldHeight );
					distance = Distance( startPos, endPos );
				} while ( !( distance >= _minimumDistance && distance <= _maximumDistance ) );
			}

			return ( startPos, endPos );
		}

		private static Single Distance( Vector3i _a, Vector3i _b )
		{
			Single x = Math.Abs( _a.X - _b.X );
			Single z = Math.Abs( _a.Z - _b.Z );

			return (Int32)Math.Sqrt( ( x * x ) + ( z * z ) );
		}

		private static Vector3i GetRandomPosition( Int32 _worldWidth, Int32 _worldHeight )
		{
			return new Vector3i( Rand.Next( 0, _worldWidth ), 0,
				Rand.Next( 0, _worldHeight ) );
		}

		private static Vector3f MakeLength( this Vector3f _vector, Single _desiredLength )
		{
			Single magnitude = _vector.Magnitude();
			Single x = ( _vector.X / magnitude ) * _desiredLength;
			Single z = ( _vector.Z / magnitude ) * _desiredLength;

			return new Vector3f( (Int32)x, 0, (Int32)z );
		}

		private static Vector3f RandomDirection()
		{
			Single radian = Rand.Next( -(Single)Math.PI / 2.0f, (Single)Math.PI / 2.0f );
			return new Vector3f( (Single)Math.Cos( radian ), 0, (Single)Math.Sin( radian ) );
		}

		private static Vector3i GetRandomEndPosition( Vector3i _startPosition, Int32 _minimumDistance, Int32 _maximumDistance )
		{
			Int32 desiredDistance = Rand.Next( _minimumDistance, _maximumDistance );
			Vector3f relativeEndPosition = RandomDirection().MakeLength( desiredDistance );
			return _startPosition + (Vector3i)relativeEndPosition;
		}

		private static Vector3i Direction( Vector3i _start, Vector3i _end )
		{
			return new Vector3i( _end.X - _start.X, 0, _end.Z - _start.Z );
		}

		private static Single Magnitude( this Vector3f _vector )
		{
			return (Single)Math.Sqrt( ( _vector.X * _vector.X ) + ( _vector.Z * _vector.Z ) );
		}
	}
}