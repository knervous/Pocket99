#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#if NOESIS
using Noesis;
#else
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;
using System.Diagnostics;
using System.Collections.Generic;
#endif
using UIInventory;

namespace MainUI
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        public Main()
        {
            Initialized += OnInitialized;
            InitializeComponent();
        }

#if NOESIS
        private void InitializeComponent()
        {
            Noesis.GUI.LoadComponent(this, "Assets/NoesisGUI/Interface/MainUI/Main.xaml");
        }
#endif

        private void OnInitialized(object sender, EventArgs e)
        {
            Inv.ToggleVisibility();
            Button inventoryButton = (Button)this.FindName("InventoryButton");
            inventoryButton.Click += OpenInventory;
        }

        private void OpenInventory(object sender, RoutedEventArgs args)
        {
           
            Inv.ToggleVisibility();
        }
    }
}
