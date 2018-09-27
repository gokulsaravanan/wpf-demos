﻿using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Invoice
{
    /// <summary>
    /// Helper Class to export invoice details to a Word document
    /// </summary>
    class ExportWord
    {
        /// <summary>
        /// Export the UI data to Word document.
        /// </summary>
        /// <param name="invoiceItems">The InvoiceItems.</param>
        /// <param name="billInfo">The BillingInformation.</param>
        /// <param name="totalDue">The TotalDue.</param>
        public void CreateWord(IList<InvoiceItem> invoiceItems, BillingInformation billInfo, double totalDue)
        {
            //Load Template document stream
            Assembly assembly = typeof(MainPage).Assembly;
            Stream inputStream = assembly.GetManifestResourceStream("Invoice.Assets.Template.docx");
            //Create instance
            WordDocument document = new WordDocument();
            //Open Template document
            document.Open(inputStream, FormatType.Word2013);
            //Dispose input stream
            inputStream.Dispose();

            //Set Clear Fields to false
            document.MailMerge.ClearFields = false;
            //Create Mail Merge Data Table
            MailMergeDataTable mailMergeDataTable = new MailMergeDataTable("Invoice", invoiceItems);
            //Executes mail merge using the data generated in the app.
            document.MailMerge.ExecuteGroup(mailMergeDataTable);

            //Mail Merge Billing information
            string[] fieldNames = { "Name", "Address", "Date", "InvoiceNumber", "DueDate", "TotalDue" };
            string[] fieldValues = { billInfo.Name, billInfo.Address, billInfo.Date.ToString("d"), billInfo.InvoiceNumber, billInfo.DueDate.ToString("d"), totalDue.ToString("#,###.00", CultureInfo.InvariantCulture) };
            document.MailMerge.Execute(fieldNames, fieldValues);

            //Create Output file in Document library location
            document.Save("Invoice.docx",FormatType.Word2013);

            //Save as Docx format
            //Message box confirmation to view the created PDF document.
            if (MessageBox.Show("Do you want to view the Word file?", "Word Document  Created",
                 MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                //Launching the PDF file using the default Application.[Acrobat Reader]
                System.Diagnostics.Process.Start("Invoice.docx");
                //this.Close();
            }
            //Close instance
            document.Close();

           
        }
    }
}
