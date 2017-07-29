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

#endif
namespace UserInterface
{
    public class Constants
    {
#if NOESIS
#if UNITY_EDITOR
        public static int WinHeight = UnityEngine.Screen.height;
        public static int WinWidth = UnityEngine.Screen.width;
        public static float WinHyp = UnityEngine.Mathf.Sqrt(WinWidth * WinWidth + WinHeight * WinHeight);
#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
        public static int WinHeight = UnityEngine.Screen.width;
        public static int WinWidth = UnityEngine.Screen.height;
        public static float WinHyp = UnityEngine.Mathf.Sqrt(width * width + height * height);
#endif
#else
        public static int WinHeight = 720;
        public static int WinWidth = 1280;
        public static double WinHyp = Math.Sqrt(WinWidth * WinWidth + WinHeight * WinHeight);
#endif

        //public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        //{
        //    //get parent item
        //    DependencyObject parentObject = VisualTreeHelper.GetParent(child);

        //    //we've reached the end of the tree
        //    if (parentObject == null) return null;

        //    //check if the parent matches the type we're looking for
        //    T parent = parentObject as T;
        //    if (parent != null)
        //        return parent;
        //    else
        //        return FindParent<T>(parentObject);
        //}
        public static Item ItemFromXamlName(String from)
        {
#if NOESIS

#else
            Inventory inv = new Inventory() {
                LeftEarSlot = new Item() {
                    Name = "Fake Item"
                }
            };
#endif
            if (from.StartsWith("Inventory_Slot"))
            {
                switch (from[from.Length - 1])
                {
                    case '1':
                        return inv.Slot1;
                    case '2':
                        return inv.Slot2;
                    case '3':
                        return inv.Slot3;
                    case '4':
                        return inv.Slot4;
                    case '5':
                        return inv.Slot5;
                    case '6':
                        return inv.Slot6;
                    case '7':
                        return inv.Slot7;
                    case '8':
                        return inv.Slot8;
                }
            }
            else
            {
                switch (from)
                {
                    case "Inventory_LeftEar":
                        return inv.LeftEarSlot;
                    case "Inventory_Neck":
                        return inv.NeckSlot;
                    case "Inventory_Face":
                        return inv.FaceSlot;
                    case "Inventory_Head":
                        return inv.HeadSlot;
                    case "Inventory_RightEar":
                        return inv.RightEarSlot;
                    case "Inventory_LeftFinger":
                        return inv.LeftFingerSlot;
                    case "Inventory_LeftWrist":
                        return inv.LeftWristSlot;
                    case "Inventory_Arms":
                        return inv.ArmSlot;
                    case "Inventory_Hands":
                        return inv.GloveSlot;
                    case "Inventory_RightWrist":
                        return inv.RightWristSlot;
                    case "Inventory_RightFinger":
                        return inv.RightFingerSlot;
                    case "Inventory_Shoulders":
                        return inv.ShoulderSlot;
                    case "Inventory_Chest":
                        return inv.ChestSlot;
                    case "Inventory_Back":
                        return inv.BackSlot;
                    case "Inventory_Belt":
                        return inv.WaistSlot;
                    case "Inventory_Legs":
                        return inv.LegSlot;
                    case "Inventory_Feet":
                        return inv.FeetSlot;
                    case "Inventory_Primary":
                        return inv.PrimarySlot;
                    case "Inventory_Offhand":
                        return inv.SecondarySlot;
                    case "Inventory_Ranged":
                        return inv.RangedSlot;
                    case "Inventory_Ammo":
                        return inv.RangedSlot;
                }
                
            }
            return new Item();
        }
    }
}
    


