﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLDiagramDesigner
{
    public class NewNode : INotifyPropertyChanged
    {
        private NewNodeType m_NewNodeType;

        public NewNodeType NewNodeType
        {
            get
            {
                return m_NewNodeType;
            }
            set
            {
                if (m_NewNodeType != value)
                {
                    m_NewNodeType = value;
                    OnPropertyChanged("NewNodeType");
                }
            }
        }

        protected virtual void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public enum NewNodeType
    {
        Class,
        Interface,
        Note,
        Text,
        Save,
        Load
    }
}
