﻿using System.Windows;

namespace ExcelExport
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
	public partial class App : Application
	{
		public App()
		{
			Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Syncfusion.Licensing.DemoCommon.FindLicenseKey());
		} 
	}
}
