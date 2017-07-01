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
            string Folder = "Textures/Character Models/Barbarian/Male/";
            switch (inventory.ArmSlot.TextureId)
            {
                case 0: // naked
                    Folder += "None/";
                    break;
                case 1: // leather
                    Folder += "Leather/";
                    break;
                case 2: // mail
                    Folder += "Chain/";
                    break;
                case 3: // plate
                    Folder += "Plate/";
                    break;
                default: // robe?
                    break;
            }
            animator.SpriteProvider.Set(2, 9, Resources.Load<Sprite>(Folder + "bam_side_"+ inventory.ArmSlot.TextureId + "_frontarm"));
            animator.SpriteProvider.Set(2, 3, Resources.Load<Sprite>(Folder + "bam_side_" + inventory.ArmSlot.TextureId + "_backarm"));
            animator.SpriteProvider.Set(2, 18, Resources.Load<Sprite>(Folder + "bam_front_" + inventory.ArmSlot.TextureId + "_leftarm"));
            animator.SpriteProvider.Set(2, 24, Resources.Load<Sprite>(Folder + "bam_front_" + inventory.ArmSlot.TextureId + "_rightarm"));
            animator.SpriteProvider.Set(2, 31, Resources.Load<Sprite>(Folder + "bam_back_" + inventory.ArmSlot.TextureId + "_leftarm"));
            animator.SpriteProvider.Set(2, 36, Resources.Load<Sprite>(Folder + "bam_back_" + inventory.ArmSlot.TextureId + "_rightarm"));
        }

        public void swapChest()
        {
            string Folder = "Textures/Character Models/Barbarian/Male/";
            switch (inventory.ChestSlot.TextureId)
            {
                case 0: // naked
                    Folder += "None/";
                    break;
                case 1: // leather
                    Folder += "Leather/";
                    break;
                case 2: // mail
                    Folder += "Chain/";
                    break;
                case 3: // plate
                    Folder += "Plate/";
                    break;
                default: // robe?
                    break;
            }
            animator.SpriteProvider.Set(2, 1, Resources.Load<Sprite>(Folder + "bam_side_" + inventory.ChestSlot.TextureId + "_chest"));
            animator.SpriteProvider.Set(2, 15, Resources.Load<Sprite>(Folder + "bam_front_" + inventory.ChestSlot.TextureId + "_chest"));
            animator.SpriteProvider.Set(2, 28, Resources.Load<Sprite>(Folder + "bam_back_" + inventory.ChestSlot.TextureId + "_chest"));
            
        }

        public void swapFeet()
        {
            string Folder = "Textures/Character Models/Barbarian/Male/";
            switch (inventory.FeetSlot.TextureId)
            {
                case 0: // naked
                    Folder += "None/";
                    break;
                case 1: // leather
                    Folder += "Leather/";
                    break;
                case 2: // mail
                    Folder += "Chain/";
                    break;
                case 3: // plate
                    Folder += "Plate/";
                    break;
                default: // robe?
                    break;
            }
            animator.SpriteProvider.Set(2, 14, Resources.Load<Sprite>(Folder + "bam_side_" + inventory.FeetSlot.TextureId + "_frontfoot"));
            animator.SpriteProvider.Set(2, 8, Resources.Load<Sprite>(Folder + "bam_side_" + inventory.FeetSlot.TextureId + "_backfoot"));
            animator.SpriteProvider.Set(2, 21, Resources.Load<Sprite>(Folder + "bam_front_" + inventory.FeetSlot.TextureId + "_leftfoot"));
            animator.SpriteProvider.Set(2, 26, Resources.Load<Sprite>(Folder + "bam_front_" + inventory.FeetSlot.TextureId + "_rightfoot"));
            animator.SpriteProvider.Set(2, 32, Resources.Load<Sprite>(Folder + "bam_back_" + inventory.FeetSlot.TextureId + "_leftfoot"));
            animator.SpriteProvider.Set(2, 37, Resources.Load<Sprite>(Folder + "bam_back_" + inventory.FeetSlot.TextureId + "_rightfoot"));
        }

        public void swapGloves()
        {
            string Folder = "Textures/Character Models/Barbarian/Male/";
            switch (inventory.GloveSlot.TextureId)
            {
                case 0: // naked
                    Folder += "None/";
                    break;
                case 1: // leather
                    Folder += "Leather/";
                    break;
                case 2: // mail
                    Folder += "Chain/";
                    break;
                case 3: // plate
                    Folder += "Plate/";
                    break;
                default: // robe?
                    break;
            }
            animator.SpriteProvider.Set(2, 11, Resources.Load<Sprite>(Folder + "bam_side_" + inventory.GloveSlot.TextureId + "_fronthand"));
            animator.SpriteProvider.Set(2, 5, Resources.Load<Sprite>(Folder + "bam_side_" + inventory.GloveSlot.TextureId + "_backhand"));
            animator.SpriteProvider.Set(2, 19, Resources.Load<Sprite>(Folder + "bam_front_" + inventory.GloveSlot.TextureId + "_lefthand"));
            animator.SpriteProvider.Set(2, 23, Resources.Load<Sprite>(Folder + "bam_front_" + inventory.GloveSlot.TextureId + "_righthand"));
            animator.SpriteProvider.Set(2, 33, Resources.Load<Sprite>(Folder + "bam_back_" + inventory.GloveSlot.TextureId + "_lefthand"));
            animator.SpriteProvider.Set(2, 38, Resources.Load<Sprite>(Folder + "bam_back_" + inventory.GloveSlot.TextureId + "_righthand"));
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

        public void swapWrist()
        {
            string Folder = "Textures/Character Models/Barbarian/Male/";
            switch (inventory.LeftWristSlot.TextureId)
            {
                case 0: // naked
                    Folder += "None/";
                    break;
                case 1: // leather
                    Folder += "Leather/";
                    break;
                case 2: // mail
                    Folder += "Chain/";
                    break;
                case 3: // plate
                    Folder += "Plate/";
                    break;
                default: // robe?
                    break;
            }
            animator.SpriteProvider.Set(2, 10, Resources.Load<Sprite>(Folder + "bam_side_" + inventory.LeftWristSlot.TextureId + "_frontwrist"));
            animator.SpriteProvider.Set(2, 4, Resources.Load<Sprite>(Folder + "bam_side_" + inventory.LeftWristSlot.TextureId + "_backwrist"));
            animator.SpriteProvider.Set(2, 20, Resources.Load<Sprite>(Folder + "bam_front_" + inventory.LeftWristSlot.TextureId + "_leftwrist"));
            animator.SpriteProvider.Set(2, 22, Resources.Load<Sprite>(Folder + "bam_front_" + inventory.LeftWristSlot.TextureId + "_rightwrist"));
            animator.SpriteProvider.Set(2, 35, Resources.Load<Sprite>(Folder + "bam_back_" + inventory.LeftWristSlot.TextureId + "_leftwrist"));
            animator.SpriteProvider.Set(2, 40, Resources.Load<Sprite>(Folder + "bam_back_" + inventory.LeftWristSlot.TextureId + "_rightwrist"));
        }

        public void swapLegs()
        {
            string Folder = "Textures/Character Models/Barbarian/Male/";
            switch (inventory.LegSlot.TextureId)
            {
                case 0: // naked
                    Folder += "None/";
                    break;
                case 1: // leather
                    Folder += "Leather/";
                    break;
                case 2: // mail
                    Folder += "Chain/";
                    break;
                case 3: // plate
                    Folder += "Plate/";
                    break;
                default: // robe?
                    break;
            }
            animator.SpriteProvider.Set(2, 2, Resources.Load<Sprite>(Folder + "bam_side_" + inventory.LegSlot.TextureId + "_kilt"));
            animator.SpriteProvider.Set(2, 17, Resources.Load<Sprite>(Folder + "bam_front_" + inventory.LegSlot.TextureId + "_kilt"));
            animator.SpriteProvider.Set(2, 30, Resources.Load<Sprite>(Folder + "bam_back_" + inventory.LegSlot.TextureId + "_kilt"));
            animator.SpriteProvider.Set(2, 12, Resources.Load<Sprite>(Folder + "bam_side_" + inventory.LegSlot.TextureId + "_frontlegup"));
            animator.SpriteProvider.Set(2, 13, Resources.Load<Sprite>(Folder + "bam_side_" + inventory.LegSlot.TextureId + "_frontleglow"));
            animator.SpriteProvider.Set(2, 6, Resources.Load<Sprite>(Folder + "bam_side_" + inventory.LegSlot.TextureId + "_backlegup"));
            animator.SpriteProvider.Set(2, 7, Resources.Load<Sprite>(Folder + "bam_side_" + inventory.LegSlot.TextureId + "_backleglow"));
            animator.SpriteProvider.Set(2, 25, Resources.Load<Sprite>(Folder + "bam_front_" + inventory.LegSlot.TextureId + "_leftleg"));
            animator.SpriteProvider.Set(2, 27, Resources.Load<Sprite>(Folder + "bam_front_" + inventory.LegSlot.TextureId + "_rightleg"));
            animator.SpriteProvider.Set(2, 34, Resources.Load<Sprite>(Folder + "bam_back_" + inventory.LegSlot.TextureId + "_leftleg"));
            animator.SpriteProvider.Set(2, 39, Resources.Load<Sprite>(Folder + "bam_back_" + inventory.LegSlot.TextureId + "_rightleg"));
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

        public void refresh()
        {
            swapArms();
            swapChest();
            swapFeet();
            swapGloves();
            //swapHead();
            swapLegs();
            swapWrist();
            //swapPrimary();
            //swapRanged();
            //swapSecondary
        }
    }
}