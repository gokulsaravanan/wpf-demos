﻿using Syncfusion.UI.Xaml.Gauges;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LinearGaugeDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ChromelessWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            cmb_RangePosition.ItemsSource = Enum.GetValues(typeof(LinearRangesPosition));
            cmb_TickPosition.ItemsSource = Enum.GetValues(typeof(LinearTicksPosition));
            cmb_LabelPosition.ItemsSource = Enum.GetValues(typeof(LinearLabelsPosition));
            cmb_PointerTypes.ItemsSource = Enum.GetValues(typeof(LinearPointerType));
          
            cmb_RangePosition.SelectedIndex = 0;
            cmb_TickPosition.SelectedIndex = 0;
            cmb_LabelPosition.SelectedIndex = 0;
            cmb_PointerTypes.SelectedIndex = 0;
           
        }
        
    }
}
