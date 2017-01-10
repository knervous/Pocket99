using Assets.Scripts.Data_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class CharacterCreate : MonoBehaviour
    {
        //page number for char creation
        public int page = 1;

        //player to be created
        public Player CreatedPlayer;


        //world connect object
        public WorldConnect WorldConnection;

        //ui panels
        public GameObject LoginPanel;
        public GameObject CharacterSelectPanel;
        public GameObject CharacterCreatePanel;

        //inner char create panels
        public GameObject CharCreatePageOne;
        public GameObject CharCreatePageTwo;
        public GameObject CharCreatePageThree;

        //character stats
        public Int32 _ClassSelection = 0;
        public Int32 _RaceSelection = 0;
        public Int32 _DeitySelection = 0;
        public Int32 ZoneSelection = 0;
        public Int32 GenderSelection = 0;

        public Text STR;
        public Text STA;
        public Text DEX;
        public Text AGI;
        public Text INT;
        public Text WIS;
        public Text CHA;

        //back next buttons
        public GameObject Back1;
        public GameObject Next1;

        //name
        public InputField CreationName;

        #region Gender Select Objects
        public GameObject Male;
        public Text MaleText;
        public GameObject Female;
        public Text FemaleText;
        #endregion

        #region Race Select Objects
        public GameObject Barbarian;
        public Text BarbarianText;
        public GameObject DarkElf;
        public Text DarkElfText;
        public GameObject Dwarf;
        public Text DwarfText;
        public GameObject Erudite;
        public Text EruditeText;
        public GameObject Gnome;
        public Text GnomeText;
        public GameObject HalfElf;
        public Text HalfElfText;
        public GameObject Halfling;
        public Text HalflingText;
        public GameObject HighElf;
        public Text HighElfText;
        public GameObject Human;
        public Text HumanText;
        public GameObject Iksar;
        public Text IksarText;
        public GameObject Ogre;
        public Text OgreText;
        public GameObject Troll;
        public Text TrollText;
        public GameObject WoodElf;
        public Text WoodElfText;
        #endregion

        #region Class Select Objects
        public GameObject Bard;
        public Text BardText;
        public GameObject Cleric;
        public Text ClericText;
        public GameObject Druid;
        public Text DruidText;
        public GameObject Enchanter;
        public Text EnchanterText;
        public GameObject Magician;
        public Text MagicianText;
        public GameObject Monk;
        public Text MonkText;
        public GameObject Necromancer;
        public Text NecromancerText;
        public GameObject Paladin;
        public Text PaladinText;
        public GameObject Ranger;
        public Text RangerText;
        public GameObject Rogue;
        public Text RogueText;
        public GameObject ShadowKnight;
        public Text ShadowKnightText;
        public GameObject Shaman;
        public Text ShamanText;
        public GameObject Warrior;
        public Text WarriorText;
        public GameObject Wizard;
        public Text WizardText;
        #endregion

        #region Deity Select Objects
        public GameObject Agnostic;
        public Text AgnosticText;
        public GameObject Bertoxxulous;
        public Text BertoxxulousText;
        public GameObject Bristlebane;
        public Text BristlebaneText;
        public GameObject CazicThule;
        public Text CazicThuleText;
        public GameObject Innoruuk;
        public Text InnoruukText;
        public GameObject Karana;
        public Text KaranaText;
        public GameObject Erollisi;
        public Text ErollisiText;
        public GameObject Mithaniel;
        public Text MithanielText;
        public GameObject Rodcet;
        public Text RodcetText;
        public GameObject Prexus;
        public Text PrexusText;
        public GameObject Quellious;
        public Text QuelliousText;
        public GameObject Solusek;
        public Text SolusekText;
        public GameObject Brell;
        public Text BrellText;
        public GameObject Tribunal;
        public Text TribunalText;
        public GameObject Tunare;
        public Text TunareText;
        public GameObject Veeshan;
        public Text VeeshanText;
        public GameObject Rallos;
        public Text RallosText;
        #endregion

        #region CitySelect Objects
        //cityselect
        public GameObject Erudin;
        public Text ErudinText;
        public GameObject Qeynos;
        public Text QeynosText;
        public GameObject Halas;
        public Text HalasText;
        public GameObject Rivervale;
        public Text RivervaleText;
        public GameObject Freeport;
        public Text FreeportText;
        public GameObject Neriak;
        public Text NeriakText;
        public GameObject Grobb;
        public Text GrobbText;
        public GameObject Oggok;
        public Text OggokText;
        public GameObject Kaladim;
        public Text KaladimText;
        public GameObject Kelethin;
        public Text KelethinText;
        public GameObject Felwithe;
        public Text FelwitheText;
        public GameObject Akanon;
        public Text AkanonText;
        public GameObject Cabilis;
        public Text CabilisText;
        //
        #endregion

        private Text RaceTextSelection;
        private Text ClassTextSelection;
        private Text DeityTextSelection;
        private Text CityTextSelection;
        
        public void Start()
        {
            Init();

        }

        public void ResetValues()
        {
            if(!CreatedPlayer)
            CreatedPlayer = gameObject.AddComponent<Player>();
            RaceClick(BarbarianText, (int)Player.RaceById.Barbarian);
            ClassClick(WarriorText, (int)Player.ClassById.Warrior);
            DeityClick(RallosText, (int)Player.DeityById.Rallos);
            CityClick(HalasText, (int)Zones.ZoneById.Halas);
            CreationName.text = "";
        }
        private void Init()
        {
            ResetValues();
            Back1.GetComponent<Button>().onClick.AddListener(delegate { BackClick(); });
            Next1.GetComponent<Button>().onClick.AddListener(delegate { NextClick(); });

            //gender
            Male.GetComponent<Button>().onClick.AddListener(delegate { MaleClick(); });
            Female.GetComponent<Button>().onClick.AddListener(delegate { FemaleClick(); });

            //race
            Barbarian.GetComponent<Button>().onClick.AddListener(delegate { RaceClick(BarbarianText, (int)Player.RaceById.Barbarian); });
            DarkElf.GetComponent<Button>().onClick.AddListener(delegate { RaceClick(DarkElfText, (int)Player.RaceById.DarkElf); });
            Dwarf.GetComponent<Button>().onClick.AddListener(delegate { RaceClick(DwarfText, (int)Player.RaceById.Dwarf); });
            Erudite.GetComponent<Button>().onClick.AddListener(delegate { RaceClick(EruditeText, (int)Player.RaceById.Erudite); });
            Gnome.GetComponent<Button>().onClick.AddListener(delegate { RaceClick(GnomeText, (int)Player.RaceById.Gnome); });
            HalfElf.GetComponent<Button>().onClick.AddListener(delegate { RaceClick(HalfElfText, (int)Player.RaceById.HalfElf); });
            Halfling.GetComponent<Button>().onClick.AddListener(delegate { RaceClick(HalflingText, (int)Player.RaceById.Halfling); });
            HighElf.GetComponent<Button>().onClick.AddListener(delegate { RaceClick(HighElfText, (int)Player.RaceById.HighElf); });
            Human.GetComponent<Button>().onClick.AddListener(delegate { RaceClick(HumanText, (int)Player.RaceById.Human); });
            Iksar.GetComponent<Button>().onClick.AddListener(delegate { RaceClick(IksarText, (int)Player.RaceById.Iksar); });
            Ogre.GetComponent<Button>().onClick.AddListener(delegate { RaceClick(OgreText, (int)Player.RaceById.Ogre); });
            Troll.GetComponent<Button>().onClick.AddListener(delegate { RaceClick(TrollText, (int)Player.RaceById.Troll); });
            WoodElf.GetComponent<Button>().onClick.AddListener(delegate { RaceClick(WoodElfText, (int)Player.RaceById.WoodElf); });

            //class
            Bard.GetComponent<Button>().onClick.AddListener(delegate { ClassClick(BardText, (int)Player.ClassById.Bard); });
            Cleric.GetComponent<Button>().onClick.AddListener(delegate { ClassClick(ClericText, (int)Player.ClassById.Cleric); });
            Druid.GetComponent<Button>().onClick.AddListener(delegate { ClassClick(DruidText, (int)Player.ClassById.Druid); });
            Enchanter.GetComponent<Button>().onClick.AddListener(delegate { ClassClick(EnchanterText, (int)Player.ClassById.Enchanter); });
            Magician.GetComponent<Button>().onClick.AddListener(delegate { ClassClick(MagicianText, (int)Player.ClassById.Magician); });
            Monk.GetComponent<Button>().onClick.AddListener(delegate { ClassClick(MonkText, (int)Player.ClassById.Monk); });
            Necromancer.GetComponent<Button>().onClick.AddListener(delegate { ClassClick(NecromancerText, (int)Player.ClassById.Necromancer); });
            Paladin.GetComponent<Button>().onClick.AddListener(delegate { ClassClick(PaladinText, (int)Player.ClassById.Paladin); });
            Ranger.GetComponent<Button>().onClick.AddListener(delegate { ClassClick(RangerText, (int)Player.ClassById.Ranger); });
            Rogue.GetComponent<Button>().onClick.AddListener(delegate { ClassClick(RogueText, (int)Player.ClassById.Rogue); });
            ShadowKnight.GetComponent<Button>().onClick.AddListener(delegate { ClassClick(ShadowKnightText, (int)Player.ClassById.Shadowknight); });
            Shaman.GetComponent<Button>().onClick.AddListener(delegate { ClassClick(ShamanText, (int)Player.ClassById.Shaman); });
            Warrior.GetComponent<Button>().onClick.AddListener(delegate { ClassClick(WarriorText, (int)Player.ClassById.Warrior); });
            Wizard.GetComponent<Button>().onClick.AddListener(delegate { ClassClick(WizardText, (int)Player.ClassById.Wizard); });

            //deity
            Agnostic.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(AgnosticText, (int)Player.DeityById.Agnostic); });
            Bertoxxulous.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(BertoxxulousText, (int)Player.DeityById.Bertoxxulous); });
            Bristlebane.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(BristlebaneText, (int)Player.DeityById.Bristbane); });
            CazicThule.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(CazicThuleText, (int)Player.DeityById.CazicThule); });
            Innoruuk.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(InnoruukText, (int)Player.DeityById.Innoruuk); });
            Karana.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(KaranaText, (int)Player.DeityById.Karana); });
            Erollisi.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(ErollisiText, (int)Player.DeityById.Erollisi); });
            Mithaniel.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(MithanielText, (int)Player.DeityById.Mithaniel); });
            Rodcet.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(RodcetText, (int)Player.DeityById.Rodcet); });
            Prexus.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(PrexusText, (int)Player.DeityById.Prexus); });
            Quellious.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(QuelliousText, (int)Player.DeityById.Quellious); });
            Solusek.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(SolusekText, (int)Player.DeityById.Solusek); });
            Brell.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(BrellText, (int)Player.DeityById.Brell); });
            Tribunal.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(TribunalText, (int)Player.DeityById.Tribunal); });
            Tunare.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(TunareText, (int)Player.DeityById.Tunare); });
            Veeshan.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(VeeshanText, (int)Player.DeityById.Veeshan); });
            Rallos.GetComponent<Button>().onClick.AddListener(delegate { DeityClick(RallosText, (int)Player.DeityById.Rallos); });

            //city
            Erudin.GetComponent<Button>().onClick.AddListener(delegate { CityClick(ErudinText, (int)Zones.ZoneById.Erudin); });
            Qeynos.GetComponent<Button>().onClick.AddListener(delegate { CityClick(QeynosText, (int)Zones.ZoneById.NorthQeynos); });
            Halas.GetComponent<Button>().onClick.AddListener(delegate { CityClick(HalasText, (int)Zones.ZoneById.Halas); });
            Rivervale.GetComponent<Button>().onClick.AddListener(delegate { CityClick(RivervaleText, (int)Zones.ZoneById.Rivervale); });
            Freeport.GetComponent<Button>().onClick.AddListener(delegate { CityClick(FreeportText, (int)Zones.ZoneById.NorthFreeport); });
            Neriak.GetComponent<Button>().onClick.AddListener(delegate { CityClick(NeriakText, (int)Zones.ZoneById.Neriak); });
            Grobb.GetComponent<Button>().onClick.AddListener(delegate { CityClick(GrobbText, (int)Zones.ZoneById.Grobb); });
            Oggok.GetComponent<Button>().onClick.AddListener(delegate { CityClick(OggokText, (int)Zones.ZoneById.Oggok); });
            Kaladim.GetComponent<Button>().onClick.AddListener(delegate { CityClick(KaladimText, (int)Zones.ZoneById.KaladimA); });
            Kelethin.GetComponent<Button>().onClick.AddListener(delegate { CityClick(KelethinText, (int)Zones.ZoneById.GreaterFaydark); });
            Felwithe.GetComponent<Button>().onClick.AddListener(delegate { CityClick(FelwitheText, (int)Zones.ZoneById.GreaterFaydark); });
            Akanon.GetComponent<Button>().onClick.AddListener(delegate { CityClick(AkanonText, (int)Zones.ZoneById.Akanon); });
            Cabilis.GetComponent<Button>().onClick.AddListener(delegate { CityClick(CabilisText, (int)Zones.ZoneById.CabilisWest); });
        }

        public void BackClick()
        {
            switch (page)
            {
                case 2:
                    CharCreatePageOne.SetActive(true);
                    CharCreatePageTwo.SetActive(false);
                    CharCreatePageThree.SetActive(false);
                    page = 1;
                    break;
                case 3:
                    CharCreatePageOne.SetActive(false);
                    CharCreatePageTwo.SetActive(true);
                    CharCreatePageThree.SetActive(false);
                    page = 2;
                    break;
                default:
                    CharacterSelectPanel.SetActive(true);
                    CharacterCreatePanel.SetActive(false);
                    CreationName.text = "";
                    LoginPanel.SetActive(false);
                    break;
            } 
        }
        public void NextClick()
        {
            switch(page)
            {
                case 1:
                    CharCreatePageOne.SetActive(false);
                    CharCreatePageTwo.SetActive(true);
                    CharCreatePageThree.SetActive(false);

                    int.TryParse(STR.text, out CreatedPlayer.str_);
                    
                    int.TryParse(STA.text, out CreatedPlayer.sta_);
                    int.TryParse(DEX.text, out CreatedPlayer.dex_);
                    int.TryParse(AGI.text, out CreatedPlayer.agi_);
                    int.TryParse(WIS.text, out CreatedPlayer.wis_);
                    int.TryParse(INT.text, out CreatedPlayer.int_);
                    int.TryParse(CHA.text, out CreatedPlayer.cha_);

                    CreatedPlayer.level_ = 1;
                    CreatedPlayer.name_ = TidyCase(CreationName.text);

                    page = 2;
                    break;
                case 2:
                    CharCreatePageOne.SetActive(false);
                    CharCreatePageTwo.SetActive(false);
                    CharCreatePageThree.SetActive(true);
                    page = 3;
                    break;
                case 3:
                    if (CreationName.text.All(char.IsLetter) && CreationName.text.Length > 3)
                    {
                        CreationName.text = TidyCase(CreationName.text);
                        WorldConnection.CheckName(CreationName.text);
                        
                    }
                    else
                    {
                        WorldConnection.CharSelect.OpenPopupPanel("Name cannot contain numbers and must be at least 4 letters long.");

                    }
                    break;
                default:
                    page = 1;
                    break;
            }
        }

        public void MaleClick()
        {
            CreatedPlayer.gender_ = 0;
            MaleText.color = Color.green;
            FemaleText.color = Color.white;
        }
        public void FemaleClick()
        {
            CreatedPlayer.gender_ = 1;
            FemaleText.color = Color.green;
            MaleText.color = Color.white;
        }

        public void RaceClick(Text selection, int raceId)
        {
            CreatedPlayer.race_ = raceId;
            if (RaceTextSelection)
                RaceTextSelection.color = Color.white;
            selection.color = Color.green;
            RaceTextSelection = selection;
        }
        
        public void ClassClick(Text selection, int classId)
        {
            CreatedPlayer.class_ = classId;
            if (ClassTextSelection)
                ClassTextSelection.color = Color.white;
            selection.color = Color.green;
            ClassTextSelection = selection;
        }

        public void DeityClick(Text selection, int deityId)
        {
            CreatedPlayer.deity_ = deityId;
            if (DeityTextSelection)
                DeityTextSelection.color = Color.white;
            selection.color = Color.green;
            DeityTextSelection = selection;
        }

        public void CityClick(Text selection, int zoneId)
        {
            CreatedPlayer.zone_id_ = zoneId;
            if (CityTextSelection)
                CityTextSelection.color = Color.white;
            selection.color = Color.green;
            CityTextSelection = selection;
        }

        public string TidyCase(string sourceStr)
        {
            sourceStr.Trim();
            if (!string.IsNullOrEmpty(sourceStr))
            {
                char[] allCharacters = sourceStr.ToCharArray();

                for (int i = 0; i < allCharacters.Length; i++)
                {
                    char character = allCharacters[i];
                    if (i == 0)
                    {
                        if (char.IsLower(character))
                        {
                            allCharacters[i] = char.ToUpper(character);
                        }
                    }
                    else
                    {
                        if (char.IsUpper(character))
                        {
                            allCharacters[i] = char.ToLower(character);
                        }
                    }
                }
                return new string(allCharacters);
            }
            return sourceStr;
        } // Clean string case to first case upper
    }
}
