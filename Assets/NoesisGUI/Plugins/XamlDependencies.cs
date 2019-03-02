using Noesis;
using System;
using System.Collections.Generic;

/// <summary>
/// Helper classes to inject dependencies to XAMLs
/// </summary>
namespace NoesisGUIExtensions
{
    public class Dependency : DependencyObject
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source", typeof(Uri), typeof(Dependency), new PropertyMetadata(null));
    }

    public class Xaml : DependencyObject
    {
        public static readonly DependencyProperty DependenciesProperty = DependencyProperty.Register(
            "Dependencies", typeof(List<Dependency>), typeof(Xaml), new PropertyMetadata(null));
    }
}