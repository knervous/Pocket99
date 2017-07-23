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


namespace UserInterface
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
        private void AssignStaticEquipSlots()
        {
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
        }
        private void OnInitialized(object sender, EventArgs e)
        {
            var icons = FindName("EquipIcons") as StackPanel;
            var items = FindName("EquipSlots") as StackPanel;
            Canvas canvas = FindName("EquipmentCanvas") as Canvas;
            Button butt = FindName("Inventory_BackButton") as Button;
            butt.Click += (object s, RoutedEventArgs r) => {
                Inv.ToggleVisibility();
            };
            AssignStaticEquipSlots();

            foreach (var panel in items.Children)
            {
                if (typeof(StackPanel) == panel.GetType())
                {
                    var p = (StackPanel)panel;
                    foreach (var item in p.Children)
                    {
                        var n = (Border)item;
                            n.MouseDown += Inv.DragItem;
                            n.MouseRightButtonUp += Inv.InspectItem;
                            n.MouseRightButtonDown += Inv.InspectItem;
                            equip_.Add(n);
                            n.Width = Constants.WinHyp / 12;
                            n.Height = Constants.WinHyp / 12;
                    }
                }
                else { continue; }

            }

            foreach (var panel in icons.Children)
            {
                if (typeof(StackPanel) == panel.GetType())
                {
                    var p = (StackPanel)panel;
                    foreach (var item in p.Children)
                    {
                        if (typeof(Border) == item.GetType())
                        {
                            var n = (Border)item;
                            n.Width = Constants.WinHyp / 12;
                            n.Height = Constants.WinHyp / 12;
                        }else if(typeof(Button) == item.GetType())
                        {
                            var n = (Button)item;
                            n.Width = Constants.WinHyp / 12;
                            n.Height = Constants.WinHyp / 12;
                        }
                    }
                }
                else { continue; }

            }

            Border platinum = (Border)FindName("Platinum");
            Border gold = (Border)FindName("Gold");
            Border silver = (Border)FindName("Silver");
            Border copper = (Border)FindName("Copper");
            CurrencyElementResize(platinum, .025f, .7f);
            CurrencyElementResize(gold, .2f, .7f);
            CurrencyElementResize(silver, .025f, .85f);
            CurrencyElementResize(copper, .2f, .85f);
        }


        private void CurrencyElementResize(Border el, float left, float top)
        {
            StackPanel elChild = el.Child as StackPanel;
            el.Width = Constants.WinWidth * .15f;
            el.Height = Constants.WinHeight * .075f;
            Canvas.SetLeft(el, Constants.WinWidth * left);
            Canvas.SetTop(el, Constants.WinHeight * top);
            foreach (var p in elChild.Children)
            {
                if (typeof(Border) == p.GetType())
                {
                    var n = p as Border;
                    n.Width = Constants.WinHyp * .02f;
                    n.Height = Constants.WinHyp * .02f;
                }
                else if (typeof(TextBlock) == p.GetType())
                {
                    var n = p as TextBlock;
                    n.Height = Constants.WinHyp * .02f;
                    n.Width = Constants.WinHyp * .05f;
                }
            }
        }

    }


}
