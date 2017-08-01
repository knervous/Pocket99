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

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    ///
    
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
            float r = (float) Constants.WinHyp * .08f;
            Canvas mainCanvas = (Canvas)FindName("MainCanvas");
            UserControl healthMana = (UserControl)FindName("HealthMana");
            Button inventoryButton = (Button)this.FindName("InventoryButton");
            Button charButton = (Button)FindName("CharButton");


            mainCanvas.Width = Constants.WinWidth;
            mainCanvas.Height = Constants.WinHeight;
            // mainCanvas.Background = new SolidColorBrush(Colors.Blue);
            inventoryButton.Width = r;
            inventoryButton.Height = r;
            inventoryButton.Click += OpenInventory;

            charButton.Width = r;
            charButton.Height = r;
            charButton.Click += OpenCharacter;
            Canvas.SetLeft(charButton, r);

            Canvas.SetLeft(healthMana, r * 2);
        }

        private void OpenInventory(object sender, RoutedEventArgs args)
        {
           
            Inv.ToggleVisibility();
        }
        
        private void OpenCharacter(object sender, RoutedEventArgs args)
        {
            //Open Char stats yo
        }
    
    }
}
