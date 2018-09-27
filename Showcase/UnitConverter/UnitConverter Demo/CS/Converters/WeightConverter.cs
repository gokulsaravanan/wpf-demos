﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UnitConverterDemo
{
    public class WeightConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return value;

            if (value == null)
            {
                return null;
            }
            double _value = Double.Parse(value.ToString());

            if (parameter.Equals("KG"))
            {
                return Math.Round(_value * 0.001,2);
            }
            else if (parameter.Equals("P"))
            {
                return Math.Round(_value * 0.002205,2);
            }
            else if (parameter.Equals("Ounces"))
            {
                return Math.Round(_value * 0.035274,2);
            }
            else if (parameter.Equals("Tons"))
            {
                return Math.Round(_value * 1.10231131092439E-6,2);
            }
            else if (parameter.Equals("mg"))
            {
                return Math.Round(_value * 1000,2);
            }
            else
            {
                return Decimal.Parse(_value.ToString());
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return value;

            double _value = Double.Parse(value.ToString());

            if (parameter.Equals("KG"))
            {
                return Math.Round(_value /0.001,2);
            }
            else if (parameter.Equals("P"))
            {
                return Math.Round(_value / 0.002205,2);
            }
            else if (parameter.Equals("Ounces"))
            {
                return Math.Round(_value / 0.035274,2);
            }
            else if (parameter.Equals("Tons"))
            {
                return Math.Round(_value / 1.10231131092439E-6,2);
            }
            else if (parameter.Equals("mg"))
            {
                return Math.Round(_value / 1000,2);
            }
            else
            {
                return Decimal.Parse(_value.ToString());
            }
        }
    }
}
