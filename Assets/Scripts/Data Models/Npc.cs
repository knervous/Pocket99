using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets.Scripts.Data_Models
{
    [System.Serializable]
    public class Npc
    {
        
        public string name = String.Empty;
        public string lastName = String.Empty;
        public string texture = String.Empty;
        public int textureId, level, race, class_, bodyType, hp, mana, gender, size, hpRegenRate, manaRegenRate,
            lootTableId, merchantId, npcSpellsId, npcFactionId, minDmg, maxDmg, aggroRadius,
            dMeleeTexture1, dMeleeTexture2, primMeleeType, secMeleeType, rangedType, runSpeed,
            mr, cr, dr, fr, pr, seeInvis, seeInvisUndead, ac, npcAggro, attackSpeed, attackDelay,
            str, sta, dex, agi, int_, wis, cha, atk, accuracy, avoidance, version, maxLevel, scaleRate,
            isQuest, light = 0;
        public float x, y;
        public Dictionary<String, object> position;

        public static Npc CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<Npc>(jsonString);
        }

    }
}
