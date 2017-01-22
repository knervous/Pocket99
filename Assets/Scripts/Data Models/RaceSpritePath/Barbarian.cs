using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpriterDotNetUnity;
using UnityEngine;

namespace Assets.Scripts.Data_Models.RaceSpritePath
{
    public class Barbarian : ITextureSwapper
    {
        public Barbarian(UnityAnimator anim, Inventory inv)
        {
            animator = anim;
            inventory = inv;
        }

        public UnityAnimator animator { get; set; }
        public Inventory inventory { get; set; }
        public bool rangedVisible { get; set; }

        public void swapArms()
        {
            switch(inventory.ArmSlot.TextureId)
            {
                case 0: // naked
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left arm front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left arm back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left arm side"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right arm front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right arm back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right arm side"));
                    break;
                case 1: // leather
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left arm front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left arm back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left arm side"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right arm front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right arm back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right arm side"));
                    break;
                case 2: // mail
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left arm front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left arm back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left arm side"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right arm front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right arm back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right arm side"));
                    break;
                case 3: // plate
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left arm front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left arm back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left arm side"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right arm front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right arm back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right arm side"));
                    break;
                default: // robe?
                    break;
            }
            
        }

        public void swapChest()
        {
            switch (inventory.ChestSlot.TextureId)
            {
                case 0: //naked
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("chest arm front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("chest arm back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("chest arm side"));
                    break;
                case 1: //leather
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("chest arm front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("chest arm back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("chest arm side"));
                    break;
                case 2: // mail
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("chest arm front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("chest arm back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("chest arm side"));
                    break;
                case 3: // plate
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("chest arm front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("chest arm back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("chest arm side"));
                    break;
                default: // robe?
                    break;
            }
        }

        public void swapFeet()
        {
            switch (inventory.FeetSlot.TextureId)
            {
                case 0: //naked
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet side"));

                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet side"));
                    break;
                case 1: //leather
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet side"));

                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet side"));
                    break;
                case 2: // mail
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet side"));

                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet side"));
                    break;
                case 3: // plate
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet side"));

                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("feet side"));
                    break;
                default: // robe?
                    break;
            }
        }

        public void swapGloves()
        {
            switch (inventory.GloveSlot.TextureId)
            {
                case 0: //naked
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("gloves front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("gloves back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("gloves side"));
                    break;
                case 1: //leather
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("gloves front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("gloves back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("gloves side"));
                    break;
                case 2: // mail
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("gloves front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("gloves back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("gloves side"));
                    break;
                case 3: // plate
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("gloves front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("gloves back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("gloves side"));
                    break;
                default: // robe?
                    break;
            }
        }

        public void swapHead()
        {
            switch (inventory.HeadSlot.TextureId)
            {
                case 0: //naked
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("head front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("head back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("head side"));
                    break;
                case 1: //leather
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("head front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("head back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("head side"));
                    break;
                case 2: // mail
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("head front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("head back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("head side"));
                    break;
                case 3: // plate
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("head front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("head back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("head side"));
                    break;
                default: // robe?
                    break;
            }
        }

        public void swapLeftWrist()
        {
            switch (inventory.LeftWristSlot.TextureId)
            {
                case 0: //naked
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left wrist front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left wrist back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left wrist side"));
                    break;
                case 1: //leather
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left wrist front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left wrist back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left wrist side"));
                    break;
                case 2: // mail
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left wrist front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left wrist back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left wrist side"));
                    break;
                case 3: // plate
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left wrist front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left wrist back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("left wrist side"));
                    break;
                default: // robe?
                    break;
            }
        }

        public void swapRightWrist()
        {
            switch (inventory.RightWristSlot.TextureId)
            {
                case 0: //naked
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right wrist front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right wrist back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right wrist side"));
                    break;                                                 
                case 1: //leather                                          
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right wrist front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right wrist back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right wrist side"));
                    break;                                                 
                case 2: // mail                                            
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right wrist front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right wrist back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right wrist side"));
                    break;                                                 
                case 3: // plate                                           
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right wrist front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right wrist back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("right wrist side"));
                    break;
                default: // robe?
                    break;
            }
        }

        public void swapLegs()
        {
            switch (inventory.LegSlot.TextureId)
            {
                case 0: //naked
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("leg front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("leg back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("leg side"));
                    break;
                case 1: //leather
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("leg front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("leg back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("leg side"));
                    break;
                case 2: // mail
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("leg front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("leg back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("leg side"));
                    break;
                case 3: // plate
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("leg front"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("leg back"));
                    animator.SpriteProvider.Set(0, Resources.Load<Sprite>("leg side"));
                    break;
                default: // robe?
                    break;
            }
        }

        public void swapPrimary()
        {
            animator.SpriteProvider.Set(0, Resources.Load<Sprite>(EquipmentTextureSwapper.Instance.WeaponIndex[inventory.PrimarySlot.WeaponId][0]));
            animator.SpriteProvider.Set(0, Resources.Load<Sprite>(EquipmentTextureSwapper.Instance.WeaponIndex[inventory.PrimarySlot.WeaponId][1]));
            animator.SpriteProvider.Set(0, Resources.Load<Sprite>(EquipmentTextureSwapper.Instance.WeaponIndex[inventory.PrimarySlot.WeaponId][2]));
        }

        public void swapRanged()
        {
            animator.SpriteProvider.Set(0, Resources.Load<Sprite>(EquipmentTextureSwapper.Instance.WeaponIndex[inventory.RangedSlot.WeaponId][0]));
            animator.SpriteProvider.Set(0, Resources.Load<Sprite>(EquipmentTextureSwapper.Instance.WeaponIndex[inventory.RangedSlot.WeaponId][1]));
            animator.SpriteProvider.Set(0, Resources.Load<Sprite>(EquipmentTextureSwapper.Instance.WeaponIndex[inventory.RangedSlot.WeaponId][2]));
        }
        

        public void swapSecondary()
        {
            animator.SpriteProvider.Set(0, Resources.Load<Sprite>(EquipmentTextureSwapper.Instance.WeaponIndex[inventory.SecondarySlot.WeaponId][0]));
            animator.SpriteProvider.Set(0, Resources.Load<Sprite>(EquipmentTextureSwapper.Instance.WeaponIndex[inventory.SecondarySlot.WeaponId][1]));
            animator.SpriteProvider.Set(0, Resources.Load<Sprite>(EquipmentTextureSwapper.Instance.WeaponIndex[inventory.SecondarySlot.WeaponId][2]));
        }
    }
}