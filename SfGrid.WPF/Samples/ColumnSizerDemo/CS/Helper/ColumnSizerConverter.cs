#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Syncfusion.UI.Xaml.Grid;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Media;

namespace ColumnSizerDemo
{
    class GridColumnSizerConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int? index = value as int?;
            if (index == 0)
                return GridLengthUnitType.Auto;
            else if (index == 1)
                return GridLengthUnitType.AutoLastColumnFill;
            else if (index == 2)
                return GridLengthUnitType.AutoWithLastColumnFill;
            else if (index == 3)
                return GridLengthUnitType.None;
            else if (index == 4)
                return GridLengthUnitType.SizeToCells;
            else if (index == 5)
                return GridLengthUnitType.SizeToHeader;
            else
                return GridLengthUnitType.Star;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class RangeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var range = value as double?;
            if (range < 0.0)
                return new SolidColorBrush(Colors.Red);
            else
                return new SolidColorBrush(Colors.Green);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return true;
        }
    }
}