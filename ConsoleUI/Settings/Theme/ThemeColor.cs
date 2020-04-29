using System.Drawing;

namespace ConsoleUI.Settings
{
	public enum ThemeColor
	{
		Primary,
		PrimaryLight,
		PrimaryDark,
		Secondary,
		SecondaryLight,
		SecondaryDark,
		VariantLight,
		VariantDark,
		Background,
		Surface,
		Error,
		Worst,
		SecondWorst,
		Middle,
		SecondBest,
		Best,
	}

	public static class ThemeColorExtensions
	{
		public static Color GetColor( this ThemeColor _themeColor )
		{
			return _themeColor switch
			{
				ThemeColor.Primary => Theme.Primary,
				ThemeColor.PrimaryLight => Theme.PrimaryLight,
				ThemeColor.PrimaryDark => Theme.PrimaryDark,
				ThemeColor.Secondary => Theme.Secondary,
				ThemeColor.SecondaryLight => Theme.SecondaryLight,
				ThemeColor.SecondaryDark => Theme.SecondaryDark,
				ThemeColor.VariantLight => Theme.VariantLight,
				ThemeColor.VariantDark => Theme.VariantDark,
				ThemeColor.Background => Theme.Background,
				ThemeColor.Surface => Theme.Surface,
				ThemeColor.Error => Theme.Error,
				ThemeColor.Worst => Theme.Worst,
				ThemeColor.SecondWorst => Theme.SecondWorst,
				ThemeColor.Middle => Theme.Middle,
				ThemeColor.SecondBest => Theme.SecondBest,
				ThemeColor.Best => Theme.Best,
				_ => Theme.Primary
			};
		}

		public static Color GetDarkerColor( this ThemeColor _themeColor )
		{
			return _themeColor switch
			{
				ThemeColor.Primary => Theme.PrimaryDark,
				ThemeColor.PrimaryLight => Theme.PrimaryDark,
				ThemeColor.PrimaryDark => Theme.PrimaryDark,
				ThemeColor.Secondary => Theme.SecondaryDark,
				ThemeColor.SecondaryLight => Theme.SecondaryDark,
				ThemeColor.SecondaryDark => Theme.SecondaryDark,
				ThemeColor.VariantLight => Theme.VariantDark,
				ThemeColor.VariantDark => Theme.VariantDark,
				ThemeColor.Background => Theme.Background,
				ThemeColor.Surface => Theme.Background,
				ThemeColor.Error => Theme.PrimaryDark,
				ThemeColor.Worst => Theme.PrimaryDark,
				ThemeColor.SecondWorst => Theme.PrimaryDark,
				ThemeColor.Middle => Theme.PrimaryDark,
				ThemeColor.SecondBest => Theme.PrimaryDark,
				ThemeColor.Best => Theme.PrimaryDark,
				_ => Theme.PrimaryDark
			};
		}

		public static Color GetLighterColor( this ThemeColor _themeColor )
		{
			
			return _themeColor switch
			{
				ThemeColor.Primary => Theme.PrimaryLight,
				ThemeColor.PrimaryLight => Theme.PrimaryLight,
				ThemeColor.PrimaryDark => Theme.Primary,
				ThemeColor.Secondary => Theme.SecondaryLight,
				ThemeColor.SecondaryLight => Theme.SecondaryLight,
				ThemeColor.SecondaryDark => Theme.Secondary,
				ThemeColor.VariantLight => Theme.VariantLight,
				ThemeColor.VariantDark => Theme.VariantLight,
				ThemeColor.Background => Theme.Surface,
				ThemeColor.Surface => Theme.Surface,
				ThemeColor.Error => Theme.Error,
				ThemeColor.Worst => Theme.Worst,
				ThemeColor.SecondWorst => Theme.SecondWorst,
				ThemeColor.Middle => Theme.Middle,
				ThemeColor.SecondBest => Theme.SecondBest,
				ThemeColor.Best => Theme.Best,
				_ => Theme.PrimaryLight
			};
		}
	}
}