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
            inventory = inv != null ? inv : new Inventory();
        }

        public Color32 GetColor(uint color) {
            byte[] byteArray = BitConverter.GetBytes(color);
            Color32 clr = new Color32();
            clr.a = byteArray[3];
            clr.r = byteArray[2];
            clr.b = byteArray[0];
            clr.g = byteArray[1];
            return clr; 
        }

        public void SetRenderColor(SpriteRenderer[] renderers, List<string> names, Color c)
        {
            foreach (var r in renderers)
            {
                var name = r.sprite != null ? (r.sprite as Sprite).name : String.Empty;
                foreach (var n in names)
                {
                    if (name.Equals(n))
                    {
                        r.color = c;
                    }
                }
            }
        }

        public UnityAnimator animator { get; set; }
        public Inventory inventory { get; set; }
        public bool rangedVisible { get; set; }

        public void swapArms()
        {
            string Folder = "Textures/Character Models/Barbarian/Male/";
            int m = inventory.ArmSlot != null ? inventory.ArmSlot.material : 0;
            switch (m)
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
            Color32 c = inventory.ArmSlot != null ? GetColor(inventory.ArmSlot.color) : new Color32(255,255,255,255);
            List<string> names = new List<string> {
                animator.SpriteProvider.Get(2,9).name,
                animator.SpriteProvider.Get(2,3).name,
                animator.SpriteProvider.Get(2,18).name,
                animator.SpriteProvider.Get(2,24).name,
                animator.SpriteProvider.Get(2,31).name,
                animator.SpriteProvider.Get(2,36).name,
            };
            SetRenderColor(animator.renderers, names, c);
            animator.SpriteProvider.Set(2, 9, Resources.Load<Sprite>(Folder + "bam_side_"+ m + "_frontarm"));
            animator.SpriteProvider.Set(2, 3, Resources.Load<Sprite>(Folder + "bam_side_" + m + "_backarm"));
            animator.SpriteProvider.Set(2, 18, Resources.Load<Sprite>(Folder + "bam_front_" + m + "_leftarm"));
            animator.SpriteProvider.Set(2, 24, Resources.Load<Sprite>(Folder + "bam_front_" + m + "_rightarm"));
            animator.SpriteProvider.Set(2, 31, Resources.Load<Sprite>(Folder + "bam_back_" + m + "_leftarm"));
            animator.SpriteProvider.Set(2, 36, Resources.Load<Sprite>(Folder + "bam_back_" + m + "_rightarm"));
        }

        public void swapChest()
        {
            string Folder = "Textures/Character Models/Barbarian/Male/";
            int m = inventory.ChestSlot != null ? inventory.ChestSlot.material : 0;
            switch (m)
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
            Color32 c = inventory.ChestSlot != null ? GetColor(inventory.ChestSlot.color) : new Color32(255, 255, 255, 255);
            List<string> names = new List<string> {
                animator.SpriteProvider.Get(2,1).name,
                animator.SpriteProvider.Get(2,15).name,
                animator.SpriteProvider.Get(2,28).name
            };
            SetRenderColor(animator.renderers, names, c);

            animator.SpriteProvider.Set(2, 1, Resources.Load<Sprite>(Folder + "bam_side_" + m + "_chest"));
            animator.SpriteProvider.Set(2, 15, Resources.Load<Sprite>(Folder + "bam_front_" + m + "_chest"));
            animator.SpriteProvider.Set(2, 28, Resources.Load<Sprite>(Folder + "bam_back_" + m + "_chest"));
            
        }

        public void swapFeet()
        {
            string Folder = "Textures/Character Models/Barbarian/Male/";
            int m = inventory.ArmSlot != null ? inventory.FeetSlot.material : 0;
            switch (m)
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
            Color32 c = inventory.FeetSlot != null ? GetColor(inventory.FeetSlot.color) : new Color32(255, 255, 255, 255);
            List<string> names = new List<string> {
                animator.SpriteProvider.Get(2,14).name,
                animator.SpriteProvider.Get(2,8).name,
                animator.SpriteProvider.Get(2,21).name,
                animator.SpriteProvider.Get(2,26).name,
                animator.SpriteProvider.Get(2,32).name,
                animator.SpriteProvider.Get(2,37).name,
            };
            SetRenderColor(animator.renderers, names, c);
            animator.SpriteProvider.Set(2, 14, Resources.Load<Sprite>(Folder + "bam_side_" + m + "_frontfoot"));
            animator.SpriteProvider.Set(2, 8, Resources.Load<Sprite>(Folder + "bam_side_" + m + "_backfoot"));
            animator.SpriteProvider.Set(2, 21, Resources.Load<Sprite>(Folder + "bam_front_" + m + "_leftfoot"));
            animator.SpriteProvider.Set(2, 26, Resources.Load<Sprite>(Folder + "bam_front_" + m + "_rightfoot"));
            animator.SpriteProvider.Set(2, 32, Resources.Load<Sprite>(Folder + "bam_back_" + m + "_leftfoot"));
            animator.SpriteProvider.Set(2, 37, Resources.Load<Sprite>(Folder + "bam_back_" + m + "_rightfoot"));
        }

        public void swapGloves()
        {
            string Folder = "Textures/Character Models/Barbarian/Male/";
            int m = inventory.GloveSlot != null ? inventory.GloveSlot.material : 0;
            switch (m)
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
            Color32 c = inventory.GloveSlot != null ? GetColor(inventory.GloveSlot.color) : new Color32(255, 255, 255, 255);
            List<string> names = new List<string> {
                animator.SpriteProvider.Get(2,11).name,
                animator.SpriteProvider.Get(2,5).name,
                animator.SpriteProvider.Get(2,19).name,
                animator.SpriteProvider.Get(2,23).name,
                animator.SpriteProvider.Get(2,33).name,
                animator.SpriteProvider.Get(2,38).name,
            };
            SetRenderColor(animator.renderers, names, c);
            animator.SpriteProvider.Set(2, 11, Resources.Load<Sprite>(Folder + "bam_side_" + m + "_fronthand"));
            animator.SpriteProvider.Set(2, 5, Resources.Load<Sprite>(Folder + "bam_side_" + m + "_backhand"));
            animator.SpriteProvider.Set(2, 19, Resources.Load<Sprite>(Folder + "bam_front_" + m + "_lefthand"));
            animator.SpriteProvider.Set(2, 23, Resources.Load<Sprite>(Folder + "bam_front_" + m + "_righthand"));
            animator.SpriteProvider.Set(2, 33, Resources.Load<Sprite>(Folder + "bam_back_" + m + "_lefthand"));
            animator.SpriteProvider.Set(2, 38, Resources.Load<Sprite>(Folder + "bam_back_" + m + "_righthand"));
        }

        public void swapHead()
        {
            switch (5)
            {
                case 0: //naked
                    break;
                case 1: //leather
                    break;
                case 2: // mail
                    break;
                case 3: // plate
                    break;
                default: // robe?
                    break;
            }
            Color32 c =  new Color32(255, 255, 255, 255);
            List<string> names = new List<string> {
                animator.SpriteProvider.Get(2,0).name,
                animator.SpriteProvider.Get(2,16).name,
                animator.SpriteProvider.Get(2,29).name
            };
            SetRenderColor(animator.renderers, names, c);
        }

        public void swapWrist()
        {
            string Folder = "Textures/Character Models/Barbarian/Male/";
            int m = inventory.LeftWristSlot != null ? inventory.LeftWristSlot.material : 0;
            switch (m)
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
            Color32 c = inventory.LeftWristSlot != null ? GetColor(inventory.LeftWristSlot.color) : new Color32(255, 255, 255, 255);
            List<string> names = new List<string> {
                animator.SpriteProvider.Get(2,10).name,
                animator.SpriteProvider.Get(2,4).name,
                animator.SpriteProvider.Get(2,20).name,
                animator.SpriteProvider.Get(2,22).name,
                animator.SpriteProvider.Get(2,35).name,
                animator.SpriteProvider.Get(2,40).name,
            };
            SetRenderColor(animator.renderers, names, c);
            animator.SpriteProvider.Set(2, 10, Resources.Load<Sprite>(Folder + "bam_side_" + m + "_frontwrist"));
            animator.SpriteProvider.Set(2, 4, Resources.Load<Sprite>(Folder + "bam_side_" + m + "_backwrist"));
            animator.SpriteProvider.Set(2, 20, Resources.Load<Sprite>(Folder + "bam_front_" + m + "_leftwrist"));
            animator.SpriteProvider.Set(2, 22, Resources.Load<Sprite>(Folder + "bam_front_" + m + "_rightwrist"));
            animator.SpriteProvider.Set(2, 35, Resources.Load<Sprite>(Folder + "bam_back_" + m + "_leftwrist"));
            animator.SpriteProvider.Set(2, 40, Resources.Load<Sprite>(Folder + "bam_back_" + m + "_rightwrist"));
        }

        public void swapLegs()
        {
            string Folder = "Textures/Character Models/Barbarian/Male/";
            int m = inventory.LegSlot != null ? inventory.LegSlot.material : 0;
            switch (m)
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

            Color32 c = inventory.LegSlot != null ? GetColor(inventory.LegSlot.color) : new Color32(255, 255, 255, 255);
            List<string> names = new List<string> {
                animator.SpriteProvider.Get(2,2).name,
                animator.SpriteProvider.Get(2,17).name,
                animator.SpriteProvider.Get(2,30).name,
                animator.SpriteProvider.Get(2,12).name,
                animator.SpriteProvider.Get(2,13).name,
                animator.SpriteProvider.Get(2,6).name,
                animator.SpriteProvider.Get(2,7).name,
                animator.SpriteProvider.Get(2,25).name,
                animator.SpriteProvider.Get(2,27).name,
                animator.SpriteProvider.Get(2,34).name,
                animator.SpriteProvider.Get(2,39).name
            };
            SetRenderColor(animator.renderers, names, c);

            animator.SpriteProvider.Set(2, 2, Resources.Load<Sprite>(Folder + "bam_side_" + m + "_kilt"));
            animator.SpriteProvider.Set(2, 17, Resources.Load<Sprite>(Folder + "bam_front_" + m + "_kilt"));
            animator.SpriteProvider.Set(2, 30, Resources.Load<Sprite>(Folder + "bam_back_" + m + "_kilt"));
            animator.SpriteProvider.Set(2, 12, Resources.Load<Sprite>(Folder + "bam_side_" + m + "_frontlegup"));
            animator.SpriteProvider.Set(2, 13, Resources.Load<Sprite>(Folder + "bam_side_" + m + "_frontleglow"));
            animator.SpriteProvider.Set(2, 6, Resources.Load<Sprite>(Folder + "bam_side_" + m + "_backlegup"));
            animator.SpriteProvider.Set(2, 7, Resources.Load<Sprite>(Folder + "bam_side_" + m + "_backleglow"));
            animator.SpriteProvider.Set(2, 25, Resources.Load<Sprite>(Folder + "bam_front_" + m + "_leftleg"));
            animator.SpriteProvider.Set(2, 27, Resources.Load<Sprite>(Folder + "bam_front_" + m + "_rightleg"));
            animator.SpriteProvider.Set(2, 34, Resources.Load<Sprite>(Folder + "bam_back_" + m + "_leftleg"));
            animator.SpriteProvider.Set(2, 39, Resources.Load<Sprite>(Folder + "bam_back_" + m + "_rightleg"));
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
            swapHead();
            swapLegs();
            swapWrist();
            //swapPrimary();
            //swapRanged();
            //swapSecondary
        }
    }
}