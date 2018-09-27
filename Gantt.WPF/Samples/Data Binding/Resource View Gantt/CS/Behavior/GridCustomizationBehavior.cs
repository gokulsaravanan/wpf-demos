﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using Syncfusion.Windows.Controls.Gantt;
using Syncfusion.Windows.Controls.Grid;

namespace ResourceView
{
    /// <summary>
    /// Behavior class for the GanttLoaded event
    /// </summary>
    class GridCustomizationBehavior : Behavior<GanttControl>
    {
        /// <summary>
        /// Called when [attached].
        /// </summary>
        protected override void OnAttached()
        {
            this.AssociatedObject.Loaded += new System.Windows.RoutedEventHandler(AssociatedObject_Loaded);
        }

        /// <summary>
        /// Handles the Loaded event of the Gantt control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AssociatedObject.GanttGrid.ReadOnly = true;

            // Customizing the header text of the Grid.
            this.AssociatedObject.GanttGrid.Columns[0].HeaderText = "Resource Name";
            this.AssociatedObject.GanttGrid.Columns[1].HeaderText = "Start Date";
            this.AssociatedObject.GanttGrid.Columns[2].HeaderText = "Finish Date";
            this.AssociatedObject.ScrollGanttChartTo(new DateTime(2012, 01, 06));
        }

        /// <summary>
        /// Called when [detaching].
        /// </summary>
        protected override void OnDetaching()
        {
            this.AssociatedObject.Loaded -= new System.Windows.RoutedEventHandler(AssociatedObject_Loaded); 
        }
    }
}
