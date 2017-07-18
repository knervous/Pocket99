#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#if NOESIS
using Assets.Scripts.Data_Models;
using Noesis;
using UnityEngine;
#else
using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;
#endif


using System.Collections.Generic;


namespace UIInventory
{
    /// <summary>
    /// Interaction logic for Equipment.xaml
    /// </summary>
    public partial class InventorySlots : UserControl
    {
        public static List<Border> slots_ = new List<Border>();

        public static Border slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8;

        public InventorySlots()
        {
            Initialized += OnInitialized;
            InitializeComponent();
            //DataContext = new Inventory();
        }
#if NOESIS
        private void InitializeComponent()
        {
            Noesis.GUI.LoadComponent(this, "Assets/NoesisGUI/Interface/InventoryUI/InventorySlots.xaml");
        }
#endif
        private void OnInitialized(object sender, EventArgs e)
        {

            var slots = FindName("InvSlots") as StackPanel;
            slot1 = FindName("Inventory_Slot1") as Border;
            slot2 = FindName("Inventory_Slot2") as Border;
            slot3 = FindName("Inventory_Slot3") as Border;
            slot4 = FindName("Inventory_Slot4") as Border;
            slot5 = FindName("Inventory_Slot5") as Border;
            slot6 = FindName("Inventory_Slot6") as Border;
            slot7 = FindName("Inventory_Slot7") as Border;
            slot8 = FindName("Inventory_Slot8") as Border;


            foreach (var panel in slots.Children)
            {
                if (typeof(StackPanel) == panel.GetType())
                {
                    var p = (StackPanel)panel;
                    foreach (var item in p.Children)
                    {
                        var n = (Border)item;
                        if (n.Name.Contains("Inventory"))
                        {
                            n.MouseDown += Inv.DragItem;
                            slots_.Add(n);
                        }
                    }
                }
                else { continue; }

            }
        }
    }


}
