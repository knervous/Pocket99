using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data_Models
{
    public class Inventory
    {
        public Item PrimarySlot { get; set; }
        public Item SecondarySlot { get; set; }
        public Item RangedSlot { get; set; }
        public Item LeftEarSlot { get; set; }
        public Item RightEarSlot { get; set; }
        public Item NeckSlot { get; set; }
        public Item HeadSlot { get; set; }
        public Item ShoulderSlot { get; set; }
        public Item BackSlot { get; set; }
        public Item ArmSlot { get; set; }
        public Item LeftWristSlot { get; set; }
        public Item RightWristSlot { get; set; }
        public Item WaistSlot { get; set; }
        public Item LeftFingerSlot { get; set; }
        public Item RightFingerSlot { get; set; }
        public Item LegSlot { get; set; }
        public Item FeetSlot { get; set; }
        public Item GloveSlot { get; set; }
        public Item ChestSlot { get; set; }

        public static class SlotById
        {
            public const int PrimarySlot = 0;
            public const int SecondarySlot = 1;
            public const int RangedSlot = 2;
            public const int LeftEarSlot = 3;
            public const int RightEarSlot = 4;
            public const int NeckSlot = 5;
            public const int HeadSlot = 6;
            public const int ShoulderSlot = 7;
            public const int BackSlot = 8;
            public const int ArmSlot = 9;
            public const int LeftWristSlot = 10;
            public const int RightWristSlot = 11;
            public const int WaistSlot = 12;
            public const int LeftFingerSlot = 13;
            public const int RightFingerSlot = 14;
            public const int LegSlot = 15;
            public const int FeetSlot = 16;
            public const int GloveSlot = 17;
            public const int ChestSlot = 18;
        }
        public Inventory()
        {

        }
        
    }
}
