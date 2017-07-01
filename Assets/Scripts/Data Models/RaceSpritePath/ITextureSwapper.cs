using SpriterDotNet;
using SpriterDotNetUnity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data_Models.RaceSpritePath
{
    public interface ITextureSwapper
    {
        UnityAnimator animator { get; set; }
        Inventory inventory { get; set; }
        bool rangedVisible { get; set; }

        void swapGloves();
        void swapWrist();
        void swapArms();
        void swapHead();
        void swapChest();
        void swapLegs();
        void swapFeet();
        void swapPrimary();
        void swapSecondary();
        void swapRanged();

        void refresh();

    }
}
