using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketIO;
using UnityEngine;


namespace Assets.Scripts.Data_Models
{
    public class Player : MonoBehaviour
    {
        
        public Int32 zone_id_ = 0;
        public Inventory inventory_;
        public float x_ = 0;
        public float y_ = 0;
        public float z_ = -1;
        public Int32 gender_ = 0;
        public Int32 class_ = 0;
        public Int32 race_ = 0;
        public Int32 level_ = 1;
        public Int32 deity_ = 1;
        public Int32 anon_ = 0;
        public DateTime last_login_;
        public DateTime time_played_;
        public Int32 gm_ = 0;
        public Int32 exp_ = 0;
        public Int32 current_hp_ = 1;
        public Int32 current_mana_ = 0;
        public Int32 max_hp_ = 1;
        public Int32 max_mana_ = 0;
        public Int32 current_endurance_ = 0;
        public Int32 max_endurance_ = 1;
        public Int32 intoxication_ = 0;
        public Int32 str_ = 0;
        public Int32 sta_ = 0;
        public Int32 agi_ = 0;
        public Int32 dex_ = 0;
        public Int32 wis_ = 0;
        public Int32 int_ = 0;
        public Int32 cha_ = 0;
        public Int32 hunger_level_ = 0;
        public Int32 thirst_level_ = 0;
        public Int32 pvp_status_ = 0;
        public Int32 show_helm_ = 0;
        public Int32 air_remaining_ = 100;
        public Int32 lfg_ = 0;
        public string name_ = "";
        public string lastName_ = "";
        public string title_ = "";
        public string account_id_ = "";
        public Int32 char_id_ = -1;

        public JSONObject CreateServerPlayer()
        {
            JSONObject obj = new JSONObject();
            obj.AddField("zoneId", zone_id_);
            obj.AddField("x", x_);
            obj.AddField("y", y_);
            obj.AddField("z", -10);
            obj.AddField("gender", gender_);
            obj.AddField("class", class_);
            obj.AddField("race", race_);
            obj.AddField("level", level_);
            obj.AddField("deity", deity_);
            obj.AddField("anon", anon_);
            obj.AddField("gm", gm_);
            obj.AddField("exp", exp_);
            obj.AddField("curHp", 10);
            obj.AddField("mana", 10);
            obj.AddField("endurance", 10);
            obj.AddField("intoxication", 0);
            obj.AddField("str", str_);
            obj.AddField("sta", sta_);
            obj.AddField("cha", cha_);
            obj.AddField("dex", dex_);
            obj.AddField("int", int_);
            obj.AddField("agi", agi_);
            obj.AddField("wis", wis_);
            obj.AddField("hungerLevel", hunger_level_);
            obj.AddField("thirstLevel", thirst_level_);
            obj.AddField("pvpStatus", pvp_status_);
            obj.AddField("showHelm", show_helm_);
            obj.AddField("airRemaining", air_remaining_);
            obj.AddField("lfg", lfg_);
            obj.AddField("name", name_);
            obj.AddField("title", title_);

            return obj;
        }

        public void PopulateFromServer(JSONObject e, JSONObject inventory)
        {
            last_login_ = new DateTime((long)e["lastLogin"].n);
            time_played_ = new DateTime((long)e["timePlayed"].n);

            zone_id_ = (int) e["zoneId"].n;
            x_ = e["x"].n;
            y_ = e["y"].n;
            z_ = e["z"].n;
            gender_ = (int)e["gender"].n;
            class_ = (int)e["class"].n;
            level_ = (int)e["level"].n;
            deity_ = (int)e["deity"].n;
            race_ = (int)e["race"].n;
            gm_ = (int)e["gm"].n;
            exp_ = (int)e["exp"].n;
            anon_ = (int)e["anon"].n;
            current_hp_ = (int)e["curHp"].n;
            current_mana_ = (int)e["mana"].n;
            max_hp_ = 1;
            max_mana_ = 0;
            current_endurance_ = (int)e["endurance"].n;
            max_endurance_ = 1;
            intoxication_ = (int)e["intoxication"].n;
            str_ = (int)e["str"].n;
            sta_ = (int)e["sta"].n;
            agi_ = (int)e["agi"].n;
            dex_ = (int)e["dex"].n;
            wis_ = (int)e["wis"].n;
            int_ = (int)e["int"].n;
            cha_ = (int)e["cha"].n;
            hunger_level_ = (int)e["hungerLevel"].n;
            thirst_level_ = (int)e["thirstLevel"].n;
            pvp_status_ = (int)e["pvpStatus"].n;
            show_helm_ = (int)e["showHelm"].n;
            air_remaining_ = (int)e["airRemaining"].n;
            lfg_ = (int)e["lfg"].n;

            account_id_ = e["accountId"].str;
            char_id_ = (int) e["charId"].n;
            name_ = e["name"].str;
            lastName_ = e["lastName"].str;
            title_ = e["title"].str;

            inventory_ = Inventory.Populate(inventory);
        }

        public static string GetClassById(int index)
        {
            string temp = String.Empty;
            switch(index)
            {
                case (int)ClassById.Bard:
                    temp = "Bard";
                    break;
                case (int)ClassById.Cleric:
                    temp = "Cleric";
                    break;
                case (int)ClassById.Druid:
                    temp = "Bard";
                    break;
                case (int)ClassById.Enchanter:
                    temp = "Enchanter";
                    break;
                case (int)ClassById.Magician:
                    temp = "Magician";
                    break;
                case (int)ClassById.Monk:
                    temp = "Monk";
                    break;
                case (int)ClassById.Necromancer:
                    temp = "Necromancer";
                    break;
                case (int)ClassById.Paladin:
                    temp = "Paladin";
                    break;
                case (int)ClassById.Ranger:
                    temp = "Ranger";
                    break;
                case (int)ClassById.Shadowknight:
                    temp = "Shadowknight";
                    break;
                case (int)ClassById.Shaman:
                    temp = "Shaman";
                    break;
                case (int)ClassById.Warrior:
                    temp = "Warrior";
                    break;
                case (int)ClassById.Wizard:
                    temp = "Bard";
                    break;
            }

            return temp;
        }

        public static string GetRaceById(int index)
        {
            string temp = String.Empty;
            switch (index)
            {
                case (int)RaceById.Barbarian:
                    temp = "Barbarian";
                    break;
                case (int)RaceById.DarkElf:
                    temp = "Dark Elf";
                    break;
                case (int)RaceById.Dwarf:
                    temp = "Dwarf";
                    break;
                case (int)RaceById.Erudite:
                    temp = "Erudite";
                    break;
                case (int)RaceById.Gnome:
                    temp = "Gnome";
                    break;
                case (int)RaceById.HalfElf:
                    temp = "Half Elf";
                    break;
                case (int)RaceById.Halfling:
                    temp = "Halfling";
                    break;
                case (int)RaceById.HighElf:
                    temp = "High Elf";
                    break;
                case (int)RaceById.Human:
                    temp = "Human";
                    break;
                case (int)RaceById.Iksar:
                    temp = "Iksar";
                    break;
                case (int)RaceById.Ogre:
                    temp = "Ogre";
                    break;
                case (int)RaceById.Troll:
                    temp = "Troll";
                    break;
                case (int)RaceById.WoodElf:
                    temp = "Wood Elf";
                    break;
            }
            return temp;
        }

        public enum ClassById
        {
            Bard = 0,
            Cleric = 1,
            Druid = 2,
            Enchanter = 3,
            Magician = 4,
            Monk = 5,
            Necromancer = 6,
            Paladin = 7,
            Ranger = 8,
            Rogue = 9,
            Shadowknight = 10,
            Shaman = 11,
            Warrior = 12,
            Wizard = 13
        }

        public enum RaceById
        {
            Barbarian = 0,
            DarkElf = 1,
            Dwarf = 2,
            Erudite = 3,
            Gnome = 4,
            HalfElf = 5,
            Halfling = 6,
            HighElf = 7,
            Human = 8,
            Iksar = 9,
            Ogre = 10,
            Troll = 11,
            WoodElf = 12
        }

        public enum DeityById
        {
            Agnostic = 0,
            Bertoxxulous = 1,
            Bristbane = 2,
            CazicThule = 3,
            Innoruuk = 4,
            Karana = 5,
            Erollisi = 6,
            Mithaniel = 7,
            Rodcet = 8,
            Prexus = 9,
            Quellious = 10,
            Solusek = 11,
            Brell = 12,
            Tribunal = 13,
            Tunare = 14,
            Veeshan = 15,
            Rallos = 16
        }


    }
}
