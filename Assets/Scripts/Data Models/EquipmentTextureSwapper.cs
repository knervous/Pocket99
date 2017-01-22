using Assets.Scripts.Data_Models.RaceSpritePath;
using SpriterDotNetUnity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.Scripts.Data_Models
{
    public sealed class EquipmentTextureSwapper
    {
        public Dictionary<int, string[]> WeaponIndex;
        public ITextureSwapper CharModel { get; set; }

        private static EquipmentTextureSwapper instance = null;
        private static readonly object padlock = new object();


        EquipmentTextureSwapper()
        {
            PopulateWeaponIndex();
        }

        public static EquipmentTextureSwapper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new EquipmentTextureSwapper();
                    }
                    return instance;
                }
            }
        }

        public void SetCharacterModel(string model, UnityAnimator anim, Inventory inv)
        {
            switch(model)
            {
                case "Barbarian":
                    CharModel = new Barbarian(anim, inv);
                    break;
                case "Dark Elf":
                   // CharModel = new DarkElf(anim, inv);
                    break;
            }
        }

        private void PopulateWeaponIndex()
        {
            WeaponIndex = new Dictionary<int, string[]>();

            TextAsset textFile = Resources.Load("Text/WeaponIndex") as TextAsset;
            string[] fLines = Regex.Split(textFile.text, "\n|\r|\r\n");
            foreach (var line in fLines)
            {
                string[] element = Regex.Split(line, " # ");
                int index;
                Int32.TryParse(element[0], out index);
                if(element.Length == 4)
                WeaponIndex.Add(index, new string[]{ element[1], element[2], element[3]});
            }
        }

        


    };


}
