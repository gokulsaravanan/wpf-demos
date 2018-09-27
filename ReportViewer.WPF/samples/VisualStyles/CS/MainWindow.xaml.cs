#region Copyright Syncfusion Inc. 2001 - 2015
// Copyright Syncfusion Inc. 2001 - 2015. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
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
using Syncfusion.Windows.Reports.Viewer;
using Microsoft.Win32;
using System.Windows.Threading;
using System.IO;
using System.Collections;
using Syncfusion.Windows.Reports;

namespace CompanySalesDemo
{
    public partial class MainWindow : Window
    {
        #region Constants

        const string templateDirectory = @"../../../../../../../../../Common/Data/ReportTemplate/";
        const string fileName = "Company Sales.rdl";

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        #endregion

        #region Events

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.reportViewerControl.ProcessingMode = ProcessingMode.Local;
            this.reportViewerControl.ReportPath = System.IO.Path.Combine(new DirectoryInfo(templateDirectory).FullName, fileName);
            this.reportViewerControl.DataSources.Add(new ReportDataSource("Sales", new AdventureWorks().GetData()));
            this.reportViewerControl.RefreshReport();

        }

        #endregion
    }
}
