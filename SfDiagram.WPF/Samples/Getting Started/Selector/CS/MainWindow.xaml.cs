﻿using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Syncfusion.Windows.Shared;
using Syncfusion.UI.Xaml.Diagram.Controls;

namespace ResizerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ChromelessWindow
    {

        public MainWindow()
        {
            InitializeComponent();

            //Register CollectionChanged event
            nodes.CollectionChanged += Nodes_CollectionChanged;

            //Initialize PageSettings and Snapping
            InitializeDiagram();

            //Info - for Diagram Events and Commands
            IGraphInfo info = diagramControl.Info as IGraphInfo;

            //Event to notify the when Items added
            info.ItemAdded += info_ItemAdded;
        }

        private void info_ItemAdded(object sender, ItemAddedEventArgs args)
        {
            //Items added due to clipboard operation
            if (args.ItemSource == ItemSource.ClipBoard)
            {
                if (args.Item is INode)
                {
                    Node node = args.Item as Node;
                    node.ShapeStyle = App.Current.Resources["nodeshapestyle"] as Style;
                    if (node.Annotations != null)
                    {
                        foreach (AnnotationEditorViewModel vm in (node.Annotations as AnnotationCollection))
                        {
                            vm.ViewTemplate = App.Current.Resources["nodeviewtemplate"] as DataTemplate;
                        }
                    }
                }
            }
        }

        private void InitializeDiagram()
        {
            diagramControl.KnownTypes = GetKnownTypes;
            diagramControl.CommandManager.View = (Control)App.Current.MainWindow;
        }

        //Helps to serialize the shape
        private IEnumerable<Type> GetKnownTypes()
        {
            List<Type> known = new List<Type>()
            {
                typeof(Shapes)
            };
            foreach (var item in known)
            {
                yield return item;
            }
        }

        //To Represent Selection Changes
        private void ChangeResizer(string s)
        {
            if (diagramControl != null)
            {
                switch (s)
                {
                    case "VisioSelector":
                        diagramControl.SFSelector.Style = this.Resources["VisioSelector"] as Style;
                        break;
                    case "OldSelector":
                        diagramControl.SFSelector.Style = this.Resources["OldSelector"] as Style;
                        break;
                    case "CustomSelector":
                        diagramControl.SFSelector.Style = this.Resources["CustomSelector"] as Style;
                        break;
                }
            }
        }

        //To Represent Node Collection
        void Nodes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                Node newNode = (e.NewItems[0] as Node);
                if (Style2.IsChecked == true)
                {
                    ChangeResizer("VisioSelector");
                }
                else if (Style3.IsChecked == true)
                {
                    ChangeResizer("CustomSelector");
                }
            }
        }

        //Get style 
        private void Style1_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton send = sender as RadioButton;

            switch (send.Name)
            {
                case "Style1":
                    ChangeResizer("OldSelector");
                    break;
                case "Style2":
                    Style3.IsChecked = false;
                    Style4.IsChecked = false;
                    (diagramControl.SelectedItems as SelectorViewModel).Commands = null;
                    ChangeResizer("VisioSelector");
                    break;
                case "Style3":
                    Style4.IsChecked = false;
                    Style2.IsChecked = false;
                    (diagramControl.SelectedItems as SelectorViewModel).Commands = new QuickCommandCollection();
                    QuickCommandViewModel senddtoback = AddQuickCommand(new Thickness(-60, -60, 0, 0), new Point(0, 0), "SendToBack", (diagramControl.Info as IGraphInfo).Commands.SendToBack);
                    QuickCommandViewModel bringtofront = AddQuickCommand(new Thickness(0, -60, 0, 0), new Point(0.5, 0), "Bringtofront", (diagramControl.Info as IGraphInfo).Commands.BringToFront);
                    QuickCommandViewModel delete = AddQuickCommand(new Thickness(60, -60, 0, 0), new Point(1, 0), "Delete", (diagramControl.Info as IGraphInfo).Commands.Delete);
                    QuickCommandViewModel draw = AddQuickCommand(new Thickness(60, 0, 0, 0), new Point(1, 0.5), "Draw", (diagramControl.Info as IGraphInfo).Commands.Draw);
                    ChangeResizer("CustomSelector");
                    break;
                case "Style4":
                    Style3.IsChecked = false;
                    Style2.IsChecked = false;
                    (diagramControl.SelectedItems as SelectorViewModel).Commands = new QuickCommandCollection();
                    QuickCommandViewModel Quickcommands_Delete = AddQuickCommand(new Thickness(0, 50, 0, 0), new Point(.5, 1), "Delete", (diagramControl.Info as IGraphInfo).Commands.Delete);
                    QuickCommandViewModel Quickcommands_Draw = AddQuickCommand(new Thickness(50, 0, 0, 0), new Point(1, 0.5), "Draw1", (diagramControl.Info as IGraphInfo).Commands.Draw);
                    QuickCommandViewModel Quickcommands_Duplicate = AddQuickCommand(new Thickness(50, 50, 0, 0), new Point(1, 1), "Duplicate", (diagramControl.Info as IGraphInfo).Commands.Duplicate);
                    diagramControl.SFSelector.ClearValue(Selector.StyleProperty);
                    break;
            }
        }
        private QuickCommandViewModel AddQuickCommand(Thickness margin, Point offset, string content, ICommand command)
        {
            QuickCommandViewModel quickCommand = new QuickCommandViewModel()
            {
                Margin = margin,
                OffsetX = offset.X,
                OffsetY = offset.Y,
                Command = command,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                VisibilityMode = VisibilityMode.Node,
                Content = GetTemplate(content),
                Shape = "F1M23.467,11.733C23.467,18.213 18.214,23.466 11.734,23.466 5.253,23.466 0,18.213 0,11.733 0,5.253 5.253,0 11.734,0 18.214,0 23.467,5.253 23.467,11.733",
                ShapeStyle=this.Resources["QuickCommandstyle"] as Style,
            };
            if (content == "Draw" || content == "Draw1" || content == "Duplicate")
            {
                DrawParameter drawParameter = new DrawParameter(DrawingTool.Connector, null, null, null, null, NullSourceTarget.SelectionAsSource | NullSourceTarget.CloneSourceAsTarget);
                DupilicateParameter duplicateParameter = new DupilicateParameter() { DragClone = true };
                quickCommand.DragCommand = command;
                if (content == "Duplicate")
                {
                    quickCommand.CommandParameter = duplicateParameter;
                }
                else
                {
                    quickCommand.CommandParameter = drawParameter;
                }
            }
            ((diagramControl.SelectedItems as SelectorViewModel).Commands as QuickCommandCollection).Add(quickCommand);
            return quickCommand;
        }
        /// <summary>
        /// Constructs a new instance of the <see cref="GetTemplate(string)"/> method.
        /// </summary>
        /// <param name="item">Gets the item value</param>
        /// <returns>Returns the string</returns>
        private object GetTemplate(string item)
        {
            object s = null;
            if (item == "Delete")
            {
                s = "M0.54700077,2.2130003 L7.2129992,2.2130003 7.2129992,8.8800011 C7.2129992,9.1920013 7.1049975,9.4570007 6.8879985,9.6739998 6.6709994,9.8910007 6.406,10 6.0939997,10 L1.6659999,10 C1.3539997,10 1.0890004,9.8910007 0.87200136,9.6739998 0.65500242,9.4570007 0.54700071,9.1920013 0.54700077,8.8800011 z M2.4999992,0 L5.2600006,0 5.8329986,0.54600048 7.7599996,0.54600048 7.7599996,1.6660004 0,1.6660004 0,0.54600048 1.9270014,0.54600048 z";
            }
            else if (item == "Draw")
            {
                s = "M3.9730001,0 L8.9730001,5.0000007 3.9730001,10.000001 3.9730001,7.0090005 0,7.0090005 0,2.9910006 3.9730001,2.9910006 z";
            }
            else if (item == "Duplicate")
            {
                s = "M0,2.4879999 L0.986,2.4879999 0.986,9.0139999 6.9950027,9.0139999 6.9950027,10 0.986,10 C0.70400238,10 0.47000122,9.9060001 0.28100207,9.7180004 0.09400177,9.5300007 0,9.2959995 0,9.0139999 z M3.0050011,0 L9.0140038,0 C9.2960014,0 9.5300026,0.093999863 9.7190018,0.28199956 9.906002,0.47000027 10,0.70399952 10,0.986 L10,6.9949989 C10,7.2770004 9.906002,7.5160007 9.7190018,7.7110004 9.5300026,7.9069996 9.2960014,8.0049992 9.0140038,8.0049992 L3.0050011,8.0049992 C2.7070007,8.0049992 2.4650002,7.9069996 2.2770004,7.7110004 2.0890007,7.5160007 1.9950027,7.2770004 1.9950027,6.9949989 L1.9950027,0.986 C1.9950027,0.70399952 2.0890007,0.47000027 2.2770004,0.28199956 2.4650002,0.093999863 2.7070007,0 3.0050011,0 z";
            }
            else if (item == "Draw1")
            {
                s = "F1M423.144,677.5312L423.144,671.2692L410.147,671.2692L410.147,666.4982L423.144,666.4982L423.144,661.0822L431.368,669.3062z";
            }
            else if (item == "SendToBack")
            {
                s = "M13.16,12.829 L22.881001,12.829 22.881001,22.549 13.16,22.549 z M10.713628,4.2319999 L17.194,4.2319999 17.194,11.770805 12.101501,11.770805 12.101501,17.393 4.0339999,17.393 4.0339999,10.713544 10.713628,10.713544 z M0,0 L9.5889998,0 9.5889998,9.5900002 0,9.5900002 z";
            }
            else if (item == "Bringtofront")
            {
                s = "M19.288976,10.746001 L23.283999,10.746001 23.283999,21.425001 12.604999,21.425001 12.604999,17.566621 19.288976,17.566621 z M5.1669993,3.4440002 L18.048999,3.4440002 18.048999,16.327 5.1669993,16.327 z M0,0 L10.677999,0 10.677999,2.2043381 3.9269123,2.2043381 3.9269123,10.678 0,10.678 z";
            }
            return s;
        }
    }

    //To Represent SfDiagram Commands
    public class CustomDiagramControl : SfDiagram
    {
        public CustomDiagramControl()
        {
            CustomSelector selector = new CustomSelector();
            selector.Graph = (this.Info as IGraphInfo);
            selector.Graph.Commands.Delete.Execute(null);
            selector.Graph.Commands.BringToFront.Execute(null);
            selector.Graph.Commands.SendToBack.Execute(null);
            selector.Graph.Commands.Draw.Execute(null);
            SelectedItems = selector;
            selector.ZIndex = 10000;
            selector.Nodes = new ObservableCollection<object>();
            selector.Connectors = new ObservableCollection<object>();
            selector.Groups = new ObservableCollection<object>();
        }

        //Apply Selectors

        public Selector SFSelector = new Selector();

        protected override Selector GetSelectorForItemOverride(object item)
        {
            return SFSelector;
        }

        protected override Connector GetConnectorForItemOverride(object item)
        {
            return new Connector();
        }
    }

    //To Represent Selector
    public class CustomSelector : SelectorViewModel
    {
        public IGraphInfo Graph { get; set; }
    }

    //To Represent ContentControl class
    public class SelectorItem : ContentControl
    {
        public SelectorItem()
        {
            this.Loaded += SelectorItem_Loaded;
        }
        //Item Loaded
        void SelectorItem_Loaded(object sender, RoutedEventArgs e)
        {
            this.MouseDown += SelectorItem_MouseDown;
            this.MouseLeftButtonDown += SelectorItem_MouseLeftButtonDown;
            this.MouseLeftButtonUp += SelectorItem_MouseLeftButtonUp;
            this.Unloaded += SelectorItem_Unloaded;
        }
        //Item UnLoaded
        void SelectorItem_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= SelectorItem_Unloaded;
            this.MouseDown -= SelectorItem_MouseDown;
            this.MouseLeftButtonDown -= SelectorItem_MouseLeftButtonDown;
            this.MouseLeftButtonUp -= SelectorItem_MouseLeftButtonUp;

        }

        private bool isPressed { get; set; }

        void SelectorItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isPressed = false;
        }

        void SelectorItem_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            CustomSelector selectorViewModel = this.DataContext as CustomSelector;
            if (e.LeftButton == MouseButtonState.Pressed && Command != null)
            {
                INode n = (INode)(selectorViewModel.Nodes as ICollection<object>).FirstOrDefault();
                DrawParameter param = new DrawParameter(DrawingTool.Connector, e, null,
                    n, null);
                this.Command.Execute(param);
                isPressed = false;
            }
            e.Handled = true;
        }

        void SelectorItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isPressed = true;
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(SelectorItem), new PropertyMetadata(null));
    }

    public class AnnotationCollection : ObservableCollection<IAnnotation>
    {

    }
}
