using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data_Models
{
    [Serializable]
    public class Item
    {
        public int TextureId { get; set; }
        public int WeaponId { get; set; }

        public int id, ac, aagi, acha, adex, aint, asta, astr, awis, bagsize, bagslots, bagtype, bagwr,
            book, price, damage, delay, clicktype, clicklevel, recastdelay, recasttype, proceffect, proctype,
            proclevel, worneffect, wornlevel, maxcharges, haste, hp, mana, regen, manaregen, icon, magic,
            nodrop, norent, range, size, weight, cr, dr, fr, mr, pr, questitemflag, backstabdmg, itemtype, material;
        public uint color;

        public string Name, idfile, combateffects, lore, clickname, procname, wornname = String.Empty;
        public List<string> classes, races, slots;

        public Item()
        {

        }

        public static Item CreateFromJSON(string jsonString)
        {
            if(jsonString.Length < 10)
            {
                return null;
            }else
            {
                return JsonUtility.FromJson<Item>(jsonString);
            }
            
        }

    }
}
