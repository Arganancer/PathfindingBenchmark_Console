using System;

namespace ConsoleUI.Elements.Components.Border
{
	[Flags]
	public enum BorderStyle : Byte
	{
		None,
		Light,
		Heavy,
		Double,
		LightDoubleDash,
		HeavyDoubleDash,
		LightTripleDash,
		HeavyTripleDash
	}
}