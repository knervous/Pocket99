#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#if NOESIS
using Assets.Scripts.Data_Models;
using Noesis;
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
    public partial class Equipment : UserControl
    {
        public static List<Border> equip_ = new List<Border>();

        public static Border leftEar, neck, face, head, rightEar, leftFinger, leftWrist, arms, hands,
            rightWrist, rightFinger, shoulders, back, chest, belt, legs, boots, primary, offhand, ranged, ammo;

        public Equipment()
        {
            Initialized += OnInitialized;
            InitializeComponent();
            //DataContext = new Inventory();
        }
#if NOESIS
        private void InitializeComponent()
        {
            Noesis.GUI.LoadComponent(this, "Assets/NoesisGUI/Interface/InventoryUI/Equipment.xaml");
        }
#endif
        private void OnInitialized(object sender, EventArgs e)
        {

            var items = FindName("EquipSlots") as StackPanel;
            Button butt = FindName("Inventory_BackButton") as Button;
            leftEar = FindName("Inventory_LeftEar") as Border;
            neck = FindName("Inventory_Neck") as Border;
            face = FindName("Inventory_Face") as Border;
            head = FindName("Inventory_Head") as Border;
            rightEar = FindName("Inventory_RightEar") as Border;
            leftFinger = FindName("Inventory_LeftFinger") as Border;
            leftWrist = FindName("Inventory_LeftWrist") as Border;
            arms = FindName("Inventory_Arms") as Border;
            hands = FindName("Inventory_Hands") as Border;
            rightWrist = FindName("Inventory_RightWrist") as Border;
            rightFinger = FindName("Inventory_RightFinger") as Border;
            shoulders = FindName("Inventory_Shoulders") as Border;
            back = FindName("Inventory_Back") as Border;
            chest = FindName("Inventory_Chest") as Border;
            belt = FindName("Inventory_Belt") as Border;
            legs = FindName("Inventory_Legs") as Border;
            boots = FindName("Inventory_Feet") as Border;
            primary = FindName("Inventory_Primary") as Border;
            offhand = FindName("Inventory_Offhand") as Border;
            ranged = FindName("Inventory_Ranged") as Border;
            ammo = FindName("Inventory_Ammo") as Border;

            butt.Click += (object s, RoutedEventArgs r) => {
                Inv.ToggleVisibility();
            };

            foreach (var panel in items.Children)
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
                            n.MouseRightButtonDown += Inv.InspectItem;
                            equip_.Add(n);
                        }
                    }
                }
                else { continue; }

            }
        }

        public static void LoadInventory(Inventory inv)
        {
            //  leftEar.Background = (ImageBrush)FindResource("icon" + inv.LeftEarSlot.icon);
        }
    }


}
