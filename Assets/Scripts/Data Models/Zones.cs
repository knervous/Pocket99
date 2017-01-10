using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data_Models
{
    public class Zones : MonoBehaviour
    {
        public static string GetZoneById(int index)
        {
            string temp = string.Empty;
            switch(index)
            {
                case (int) ZoneById.Blackburrow:
                    temp = "Blackburrow";
                    break;
                case (int)ZoneById.NorthQeynos:
                    temp = "North Qeynos";
                    break;
                case (int)ZoneById.QeynosCatacombs:
                    temp = "Qeynos Aqueducts";
                    break;
                case (int)ZoneById.QeynosHills:
                    temp = "Qeynos Hills";
                    break;
                case (int)ZoneById.SouthQeynos:
                    temp = "South Qeynos";
                    break;
                case (int)ZoneById.Akanon:
                    temp = "Ak'anon";
                    break;
                case (int)ZoneById.CabilisEast:
                    temp = "East Cabilis";
                    break;
                case (int)ZoneById.CabilisWest:
                    temp = "West Cabilis";
                    break;
                case (int)ZoneById.Erudin:
                    temp = "Erudin";
                    break;
                case (int)ZoneById.FelwitheA:
                    temp = "Felwithe";
                    break;
                case (int)ZoneById.FelwitheB:
                    temp = "Old Felwithe";
                    break;
                case (int)ZoneById.GreaterFaydark:
                    temp = "Greater Faydark";
                    break;
                case (int)ZoneById.Grobb:
                    temp = "Grobb";
                    break;
                case (int)ZoneById.Halas:
                    temp = "Halas";
                    break;
                case (int)ZoneById.KaladimA:
                    temp = "North Kaladim";
                    break;
                case (int)ZoneById.KaladimB:
                    temp = "South Kaladim";
                    break;
                case (int)ZoneById.Neriak:
                    temp = "Neriak";
                    break;
                case (int)ZoneById.EastFreeport:
                    temp = "East Freeport";
                    break;
                case (int)ZoneById.WestFreeport:
                    temp = "West Freeport";
                    break;
                case (int)ZoneById.NorthFreeport:
                    temp = "North Freeport";
                    break;
                case (int)ZoneById.Rivervale:
                    temp = "Rivervale";
                    break;
                default:
                    temp = "Not Implemented ID: " + index;
                    break;
            }
            return temp;
        }

        public enum ZoneById
        {
            NorthQeynos = 0,
            SouthQeynos = 1,
            QeynosHills = 2,
            QeynosCatacombs = 3,
            Blackburrow = 4,
            Erudin = 5,
            Halas = 6,
            Rivervale = 7,
            NorthFreeport = 8,
            EastFreeport = 9,
            WestFreeport = 10,
            Neriak = 11,
            Grobb = 12,
            Oggok = 13,
            KaladimA = 14,
            KaladimB = 15,
            GreaterFaydark = 16,
            FelwitheA = 17,
            FelwitheB = 18,
            Akanon = 19,
            CabilisEast = 20,
            CabilisWest = 21
        }
    }
}
