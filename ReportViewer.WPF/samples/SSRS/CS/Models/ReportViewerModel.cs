﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;


using System.Collections;

namespace Syncfusion.Samples.ViewModel
{
    public class ReportModel
    {
        public string ReportPath { get; set; }
        public string ReportServerUrl { get; set; }
        public System.Net.NetworkCredential ReportServerCredential { get; set; }
        public Syncfusion.Windows.Reports.Viewer.ProcessingMode ProcessingMode { get; set; }
    }
}
