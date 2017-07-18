﻿#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#if NOESIS
using Noesis;
#else
using System;
using System.Windows;
using System.Windows.Controls.Primitives;
#endif

namespace Noesis.Samples
{
    public class ElementExtensions: DependencyObject
    {
        public ElementExtensions()
        {
        }
        
        #region FocusOnLoaded
        public static DependencyProperty FocusOnLoadedProperty = DependencyProperty.RegisterAttached(
            "FocusOnLoaded", typeof(bool), typeof(ElementExtensions), new PropertyMetadata(false, OnFocusOnLoadedChanged));
        
        public static bool GetFocusOnLoaded(DependencyObject d)
        {
            return (bool)d.GetValue(FocusOnLoadedProperty);
        }
        
        public static void SetFocusOnLoaded(DependencyObject d, bool value)
        {
            d.SetValue(FocusOnLoadedProperty, value);
        }

        private static void OnFocusOnLoadedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = d as FrameworkElement;
            if (element != null && (bool)e.NewValue == true)
            {
                element.Loaded += (s, a) => element.Focus();
            }
        }
        #endregion
        
        #region FocusOnHover
        public static DependencyProperty FocusOnHoverProperty = DependencyProperty.RegisterAttached(
            "FocusOnHover", typeof(bool), typeof(ElementExtensions), new PropertyMetadata(false, OnFocusOnHoverChanged));
        
        public static bool GetFocusOnHover(DependencyObject d)
        {
            return (bool)d.GetValue(FocusOnHoverProperty);
        }
        
        public static void SetFocusOnHover(DependencyObject d, bool value)
        {
            d.SetValue(FocusOnHoverProperty, value);
        }

        private static void OnFocusOnHoverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = d as UIElement;
            if (element != null && (bool)e.NewValue == true)
            {
                element.Focus();
            }
        }
        #endregion
        
        #region SelectOnHover
        public static DependencyProperty SelectOnHoverProperty = DependencyProperty.RegisterAttached(
            "SelectOnHover", typeof(bool), typeof(ElementExtensions), new PropertyMetadata(false, OnSelectOnHoverChanged));
        
        public static bool GetSelectOnHover(DependencyObject d)
        {
            return (bool)d.GetValue(SelectOnHoverProperty);
        }
        
        public static void SetSelectOnHover(DependencyObject d, bool value)
        {
            d.SetValue(SelectOnHoverProperty, value);
        }

        private static void OnSelectOnHoverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = d as UIElement;
            if (element != null && (bool)e.NewValue == true)
            {
                Selector.SetIsSelected(element, true);
            }
        }
        #endregion
    }
}
