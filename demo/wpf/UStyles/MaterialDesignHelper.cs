using System;
using System.Collections.Generic;
using System.Data.MaterialDesignToolkit.Wpf;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TestWPFUI.SQLiteCipher.UStyles
{
    /// <summary>
    /// MaterialDesign帮助类
    /// </summary>
    public static class MaterialDesignHelper
    {
        /// <summary>
        /// 切换明暗主题
        /// </summary>
        /// <param name="isDark"></param>
        public static void SwitchDarkLight(bool? isDark) => SwitchDarkLight(isDark.HasValue && isDark.Value);
        /// <summary>
        /// 切换明暗主题
        /// </summary>
        /// <param name="isDark"></param>
        public static void SwitchDarkLight(bool isDark)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            IBaseTheme baseTheme = isDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
            theme.SetBaseTheme(baseTheme);
            paletteHelper.SetTheme(theme);
        }
        /// <summary>
        /// 改变主颜色
        /// </summary>
        /// <param name="paletteHelper"></param>
        /// <param name="color"></param>
        public static void ChangePrimaryColor(this PaletteHelper paletteHelper, Color color)
        {
            ITheme theme = paletteHelper.GetTheme();

            theme.PrimaryLight = new ColorPair(color.Lighten(), theme.PrimaryLight.ForegroundColor);
            theme.PrimaryMid = new ColorPair(color, theme.PrimaryMid.ForegroundColor);
            theme.PrimaryDark = new ColorPair(color.Darken(), theme.PrimaryDark.ForegroundColor);

            paletteHelper.SetTheme(theme);
        }
        /// <summary>
        /// 改变主颜色
        /// </summary>
        /// <param name="color"></param>
        public static void ChangePrimaryColor(Color color)
        {
            ChangePrimaryColor(new PaletteHelper(), color);
        }
        /// <summary>
        /// 改变主颜色
        /// </summary>
        /// <param name="color"></param>
        public static void ChangeSecondaryColor(Color color)
        {
            ChangeSecondaryColor(new PaletteHelper(), color);
        }
        /// <summary>
        /// 改变次要颜色
        /// </summary>
        /// <param name="paletteHelper"></param>
        /// <param name="color"></param>
        public static void ChangeSecondaryColor(this PaletteHelper paletteHelper, Color color)
        {
            ITheme theme = paletteHelper.GetTheme();

            theme.SecondaryLight = new ColorPair(color.Lighten(), theme.SecondaryLight.ForegroundColor);
            theme.SecondaryMid = new ColorPair(color, theme.SecondaryMid.ForegroundColor);
            theme.SecondaryDark = new ColorPair(color.Darken(), theme.SecondaryDark.ForegroundColor);

            paletteHelper.SetTheme(theme);
        }
    }
}
