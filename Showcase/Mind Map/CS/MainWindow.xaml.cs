﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Syncfusion.Windows.Shared;

namespace MindMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ChromelessWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            MapViewModel floor = new MapViewModel();
            floor.GoBack = new DelegateCommand<object>(OnGoBack);
            this.DataContext = floor;
            //this.MouseDown += MainWindow_MouseDown;
        }

        //void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.RightButton == MouseButtonState.Pressed)
        //    {
        //        if (bar.Visibility == Visibility.Visible)
        //        {
        //            bar.Visibility = Visibility.Collapsed;
        //        }
        //        else
        //        {
        //            bar.Visibility = Visibility.Visible;
        //        }
        //    }
        //    else
        //    {
        //        bar.Visibility = Visibility.Collapsed;
        //    }
        //}

        //void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    bar.Visibility = Visibility.Collapsed;
        //}

        //private void Close(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //    bar.Visibility = Visibility.Collapsed;
        //}
        private void OnGoBack(object parameter)
        {
            MapViewModel VM = this.DataContext as MapViewModel;
            VM.GoBack = null;
            this.DataContext = null;
            //Frame.GoBack();
        }
    }
}
