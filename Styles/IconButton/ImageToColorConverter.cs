using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SuperbarButton
{
    public class ImageToColorConverter: IValueConverter
    {
        #region Implementation of IValueConverter

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Image img = value as Image;
            if (img != null)
            {
                BitmapSource src = img.Source as BitmapSource;
                if (src != null)
                {
                    if (src.Format != PixelFormats.Rgb24)
                    {
                        // Если изображение не в формате RGB24, то переводим его в этот формат
                        src = new FormatConvertedBitmap(src, PixelFormats.Rgb24, null, 1);
                    }
                    // Ужимаем иконку до размера 1x1
                    TransformedBitmap bmp = new TransformedBitmap(src, new ScaleTransform(1.0/src.Width, 1.0/src.PixelHeight));
                    // Достаём цвет пиксела
                    byte[] pixels = new byte[3];
                    bmp.CopyPixels(pixels, 3, 0);
                    // Возвращаём цвет полученного пиксела
                    return Color.FromArgb(255, pixels[0], pixels[1], pixels[2]);
                }
            }
            return Colors.Yellow;
        }

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value that is produced by the binding target.</param><param name="targetType">The type to convert to.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
