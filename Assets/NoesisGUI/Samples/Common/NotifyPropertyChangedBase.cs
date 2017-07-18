﻿#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

using System;
using System.ComponentModel;

namespace Noesis.Samples
{
	/// <summary>
	/// Base class implementing INotifyPropertyChanged
	/// </summary>
	public class NotifyPropertyChangedBase: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}