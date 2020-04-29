using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Program.Utilities
{
	public static class MemoryUsage
	{
		/// <summary>
		/// The actual, exposed method:
		/// </summary>
		public static Int64 SizeInBytes( params Object[] _someObjects )
		{
			Size temp = new Size( _someObjects );
			Int64 tempSize = temp.GetSizeInBytes();
			return tempSize;
		}

		/// <summary>
		/// Nice way to calculate the size of managed object!
		/// </summary>
		internal class Size
		{
			private readonly Int32 m_PointerSize = Environment.Is64BitOperatingSystem ? sizeof( Int64 ) : sizeof( Int32 );
			private Int32 m_ExistingReferenceCount;
			private readonly Object[] m_Objs;
			private readonly HashSet<Object> m_References;

			public Size( params Object[] _objs )
			{
				m_Objs = _objs;

				m_References = new HashSet<Object>();

				foreach ( Object obj in m_Objs )
				{
					m_References.Add( obj );
				}
			}

			public Int64 GetSizeInBytes()
			{
				Int64 size = 0;

				foreach ( Object obj in m_Objs )
				{
					size += GetSizeInBytes( m_Objs );
				}

				return size;
			}

			/// <summary>
			/// The core functionality. Recurrently calls itself when an object appears to have fields 
			/// until all fields have been  visited, or were "visited" (calculated) already.
			/// </summary>
			/// <returns></returns>
			private Int64 GetSizeInBytes<T>( T _obj )
			{
				if ( ReferenceEquals( _obj, null ) )
				{
					return sizeof( Int32 );
				}

				Type type = _obj.GetType();

				if ( type.IsPrimitive )
				{
					return Type.GetTypeCode( type ) switch
					{
						TypeCode.Boolean => sizeof( Boolean ),
						TypeCode.Byte => sizeof( Byte ),
						TypeCode.SByte => sizeof( SByte ),
						TypeCode.Char => sizeof( Char ),
						TypeCode.Single => sizeof( Single ),
						TypeCode.Double => sizeof( Double ),
						TypeCode.Int16 => sizeof( Int16 ),
						TypeCode.UInt16 => sizeof( UInt16 ),
						TypeCode.Int32 => sizeof( Int32 ),
						TypeCode.UInt32 => sizeof( UInt32 ),
						TypeCode.Int64 => sizeof( Int64 ),
						TypeCode.UInt64 => sizeof( UInt64 ),
						_ => sizeof( Int64 )
					};
				}

				if ( _obj is Decimal )
				{
					return sizeof( Decimal );
				}

				if ( _obj is String s )
				{
					return sizeof( Char ) * s.Length;
				}

				if ( type.IsEnum )
				{
					Type underlyingType = Enum.GetUnderlyingType( type );
					return Marshal.SizeOf( underlyingType );
				}

				if ( type.IsArray )
				{
					Int64 size = m_PointerSize;
					IEnumerable casted = (IEnumerable)_obj;
					foreach ( Object item in casted )
					{
						size += GetSizeInBytes( item );
					}

					return size;
				}

				if ( _obj is Pointer )
				{
					return m_PointerSize;
				}

				{
					Int64 size = 0;
					Type t = type;
					while ( t != null )
					{
						size += m_PointerSize;
						FieldInfo[] fields = t.GetFields( BindingFlags.Instance | BindingFlags.Public |
						                                  BindingFlags.NonPublic | BindingFlags.DeclaredOnly );
						foreach ( FieldInfo field in fields )
						{
							Object tempVal = field.GetValue( _obj );
							if ( !m_References.Contains( tempVal ) )
							{
								m_References.Add( tempVal );
								size += GetSizeInBytes( tempVal );
							}
							else
							{
								++m_ExistingReferenceCount;
							}
						}

						t = t.BaseType;
					}

					return size;
				}
			}
		}
	}
}