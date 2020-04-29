using System;

namespace ConsoleUI.Elements.BaseElements.EventArguments
{
	public class PropertyChangedEventArgs<T> : EventArgs
	{
		public T NewValue { get; }

		public PropertyChangedEventArgs( T _newValue )
		{
			NewValue = _newValue;
		}
	}
}