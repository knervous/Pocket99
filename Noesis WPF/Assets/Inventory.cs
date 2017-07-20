using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

    public class Inventory : INotifyPropertyChanged
    {
        public Item PrimarySlot { get; set; }
        public Item SecondarySlot { get; set; }
        public Item RangedSlot { get; set; }
        public Item LeftEarSlot { get; set; }
        public Item RightEarSlot { get; set; }
        public Item NeckSlot { get; set; }
        public Item HeadSlot { get; set; }
        public Item FaceSlot { get; set; }
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
        public Item Slot1, Slot2, Slot3, Slot4, Slot5, Slot6, Slot7, Slot8;

        public event PropertyChangedEventHandler PropertyChanged;

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
        PrimarySlot = new Item();
        SecondarySlot = new Item();
        RangedSlot = new Item();
        LeftEarSlot = new Item();
        RightEarSlot = new Item();
        NeckSlot = new Item();
        HeadSlot = new Item();
        FaceSlot = new Item();
        ShoulderSlot = new Item();
        BackSlot = new Item();
        ArmSlot = new Item();
        LeftWristSlot = new Item();
        RightWristSlot = new Item();
        WaistSlot = new Item();
        LeftFingerSlot = new Item();
        RightFingerSlot = new Item();
        LegSlot = new Item();
        FeetSlot = new Item();
        GloveSlot = new Item();
        ChestSlot = new Item();
        Slot1= new Item();
        Slot2 = new Item();
        Slot3 = new Item();
        Slot4 = new Item();
        Slot5 = new Item();
        Slot6 = new Item();
        Slot7 = new Item();
        Slot8 = new Item();
    }
        
    }
