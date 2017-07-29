#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#if NOESIS
using Assets.Scripts.Data_Models;
using Noesis;
using System.Collections.Generic;
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
        public static float WinHyp = UnityEngine.Mathf.Sqrt(WinWidth * WinWidth + WinHeight * WinHeight);
#else
        public static int WinHeight = UnityEngine.Screen.height;
        public static int WinWidth = UnityEngine.Screen.width;
        public static float WinHyp = UnityEngine.Mathf.Sqrt(WinWidth * WinWidth + WinHeight * WinHeight);
#endif
#else
        public static int WinHeight = 720;
        public static int WinWidth = 1280;
        public static double WinHyp = Math.Sqrt(WinWidth * WinWidth + WinHeight * WinHeight);
#endif

        public static Dictionary<int, string> ItemTypes = new Dictionary<int, string>() {
            { 0 ,  "1HS"},
            { 1 ,  "2HS"},       
            { 2 ,  "piercing"},
            { 3 ,  "1HB"},       
            { 4 ,  "2HB"},       
            { 5 ,  "Archery"},       
            { 7 ,  "Throwing"},       
            { 8 ,  "Shield"},       
            { 10,  "Defence (Armor)"},       
            { 11,  "Involves Tradeskills (Not sure how)"},       
            { 12,  "Lock Picking"},       
            { 14,  "Food (Right Click to use)"},       
            { 15,  "Drink (Right Click to use)"},       
            { 16,  "Light Source"},       
            { 17,  "Common Inventory Item"},       
            { 18,  "Bind Wound"},       
            { 19,  "Thrown Casting Items (Explosive potions etc)"},       
            { 20,  "Spells / Song Sheets"},       
            { 21,  "Potions"},       
            { 22,  "Fletched Arrows?..."},       
            { 23,  "Wind Instrument"},       
            { 24,  "Stringed Instrument"},       
            { 25,  "Brass Instrument"},       
            { 26,  "Drum Instrument"},       
            { 27,  "Ammo"},       
            { 29,  "Jewlery Items"},       
            { 31,  "Usually Readable Notes and Scrolls *i beleive this to display [This note is Rolle Up/Unrolled]" },      
            { 32,  "Usually Readable Books *i beleive this to display [This Book is Closed/Open]"},       
            { 33,  "Keys"},       
            { 35,  "2H Pierce"},       
            { 36,  "Fishing Pole"},       
            { 37,  "Fishing Bait"},       
            { 38,  "Alcoholic Beverage"},       
            { 40,  "Compass"},       
            { 42,  "Poison"},       
            { 45,  "H2H (Hand to Hand)"}      
        };

        public static Item ItemFromXamlName(string from)
        {
#if NOESIS
            Inventory inv = MainPlayer.instance.GetComponent<PlayerAttributes>().player.inventory_;
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
    


