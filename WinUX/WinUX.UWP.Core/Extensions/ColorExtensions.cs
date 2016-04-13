namespace WinUX.Extensions
{
    using Windows.UI;
    using Windows.UI.Xaml.Media;

    using WinUX.Enums;

    public static class ColorExtensions
    {
        private static readonly Color NearBlack = Colors.Black.Lighten(10);

        private static readonly Color NearWhite = Colors.Black.Lighten(90);

        /// <summary>
        /// Converts a <see cref="Color"/> value to a <see cref="SolidColorBrush"/>.
        /// </summary>
        /// <param name="color">
        /// The <see cref="Color"/> to converter.
        /// </param>
        /// <returns>
        /// Returns a <see cref="SolidColorBrush"/> containing the color.
        /// </returns>
        public static SolidColorBrush ToSolidColorBrush(this Color color)
        {
            return new SolidColorBrush(color);
        }

        /// <summary>
        /// Darkens a color by a given amount.
        /// </summary>
        /// <param name="color">The current color.</param>
        /// <param name="amount">The amount to darken by.</param>
        /// <returns>Returns a <see cref="Color"/> value representing the given <see cref="Color"/> darker.</returns>
        public static Color Darken(this Color color, float amount)
        {
            var val = amount * 0.1f;
            return Lerp(color, NearBlack, val);
        }

        /// <summary>
        /// Lightens a color by a given amount.
        /// </summary>
        /// <param name="color">The current color.</param>
        /// <param name="amount">The amount to lighten by.</param>
        /// <returns>Returns a <see cref="Color"/> value representing the given <see cref="Color"/> lighter.</returns>

        public static Color Lighten(this Color color, float amount)
        {
            var val = amount * 0.1f;
            return Lerp(color, NearWhite, val);
        }

        /// <summary>
        /// Converts a color value to a string representation of the value in hex.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <returns>Returns a string containing the hex value.</returns>
        public static string ToHex(this Color color)
        {
            return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        /// <summary>
        /// Converts a color value to an AccentColor value.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <returns>Returns an AccentColor representaiton of the Color value.</returns>
        public static AccentColor ToAccentColor(this Color color)
        {
            var hexValue = color.ToHex();

            switch (hexValue)
            {
                case "#FFA4C400":
                    return AccentColor.Lime; // Lime
                case "#FF60A917":
                    return AccentColor.Green; // Green
                case "#FF008A00":
                    return AccentColor.Emerald; // Emerald
                case "#FF00ABA9":
                case "#FF0099BC":
                case "#FF2D7D9A":
                    return AccentColor.Teal; // Teal
                case "#FF1BA1E2":
                    return AccentColor.Cyan; // Cyan
                case "#FF3E65FF":
                case "#FF0078D7":
                case "#FF0063B1":
                    return AccentColor.Cobalt; // Cobalt
                case "#FFAA00FF":
                    return AccentColor.Violet; // Violet
                case "#FFF472D0":
                case "#FFE74856":
                    return AccentColor.Pink; // Pink
                case "#FFD80073":
                    return AccentColor.Magenta; // Magenta
                case "#FFA20025":
                case "#FFE51400":
                case "#FFE81123":
                    return AccentColor.Red; // Crimson/Red
                case "#FFFA6800":
                case "#FFFF8C00":
                case "#FFF7630C":
                    return AccentColor.Orange; // Orange
                case "#FFF0A30A":
                    return AccentColor.Amber; // Amber
                case "#FFE3C800":
                case "#FFFFB900":
                    return AccentColor.Yellow; // Yellow
            }

            return AccentColor.Indigo; // Indigo (Default)
        }

        private static Color Lerp(this Color color, Color target, float amount)
        {
            float startRed = color.R;
            float startGreen = color.G;
            float startBlue = color.B;

            float endRed = target.R;
            float endGreen = target.G;
            float endBlue = target.B;

            var r = (byte)startRed.Lerp(endRed, amount);
            var g = (byte)startGreen.Lerp(endGreen, amount);
            var b = (byte)startBlue.Lerp(endBlue, amount);

            return Color.FromArgb(color.A, r, g, b);
        }
    }
}