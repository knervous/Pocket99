using UnityEngine;
using System.Collections.Generic;
using SocketIO;
using Assets.Scripts.Data_Models;


public class Network : MonoBehaviour
{
    public static SocketIOComponent socket;
    public GameObject SocketObject;
    public GameObject playerPrefab;
    public GameObject myPlayer;
    public static Network instance;

    Dictionary<string, GameObject> players;
    Dictionary<string, GameObject> npcs;

    public void Awake()
    {
        if (instance)
            DestroyImmediate(SocketObject);
        else
        {
            DontDestroyOnLoad(SocketObject);
            instance = this;
        }
    }


    void Start()
    {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);
        socket.On("spawn", OnSpawn);
        socket.On("move", OnMove);
        socket.On("disconnected", OnDisconnected);
        socket.On("requestPosition", RequestPosition);
        socket.On("updatePosition", UpdatePosition);
        socket.On("npcSpawn", NpcSpawn);
        socket.On("itemSwapOkay", SwapOkay);

        players = new Dictionary<string, GameObject>();
        npcs = new Dictionary<string, GameObject>();
    }

    

    void OnSpawn(SocketIOEvent e)
    {
        Debug.Log("spawned" + e.data);
        
        var player = Instantiate(playerPrefab);
       
        player.tag = "Player";

        players.Add(e.data["id"].ToString(), player);
        Debug.Log("count: " + players.Count);
    }

    void NpcSpawn(SocketIOEvent e)
    {
        Debug.Log("npc spawned" + e.data);
        var n = Npc.CreateFromJSON(e.data.ToString());
        var npc = Instantiate(Resources.Load(n.texture)) as GameObject;

        //npc.tag = "fire beetle";
        npc.transform.position = new Vector3(GetFloatFromJson(e.data["position"], "x"), GetFloatFromJson(e.data["position"], "y"), -1);

        npcs.Add(e.data["zoneId"].ToString(), npc);
    }

    void SwapOkay(SocketIOEvent e)
    {
        Debug.Log("Item Swapped");
        string to = e.data["to"].str;
        string from = e.data["from"].str;
        var inv = MainPlayer.instance.GetComponent<PlayerAttributes>().player.inventory_;
        Item tempOne = new Item();
        Item tempTwo = new Item();


        if (from.StartsWith("Inventory_Slot"))
        {
            switch (from[from.Length - 1])
            {
                case '1':
                    tempOne = inv.Slot1;
                    break;
                case '2':
                    tempOne = inv.Slot2;
                    break;
                case '3':
                    tempOne = inv.Slot3;
                    break;
                case '4':
                    tempOne = inv.Slot4;
                    break;
                case '5':
                    tempOne = inv.Slot5;
                    break;
                case '6':
                    tempOne = inv.Slot6;
                    break;
                case '7':
                    tempOne = inv.Slot7;
                    break;
                case '8':
                    tempOne = inv.Slot8;
                    break;
            }
        }
        else
        {
            switch (from)
            {
                case "Inventory_LeftEar":
                    tempOne = inv.LeftEarSlot;
                    break;
                case "Inventory_Neck":
                    tempOne = inv.NeckSlot;
                    break;
                case "Inventory_Face":
                    tempOne = inv.FaceSlot;
                    break;
                case "Inventory_Head":
                    tempOne = inv.HeadSlot;
                    break;
                case "Inventory_RightEar":
                    tempOne = inv.RightEarSlot;
                    break;
                case "Inventory_LeftFinger":
                    tempOne = inv.LeftFingerSlot;
                    break;
                case "Inventory_LeftWrist":
                    tempOne = inv.LeftWristSlot;
                    break;
                case "Inventory_Arms":
                    tempOne = inv.ArmSlot;
                    break;
                case "Inventory_Hands":
                    tempOne = inv.GloveSlot;
                    break;
                case "Inventory_RightWrist":
                    tempOne = inv.RightWristSlot;
                    break;
                case "Inventory_RightFinger":
                    tempOne = inv.RightFingerSlot;
                    break;
                case "Inventory_Shoulders":
                    tempOne = inv.ShoulderSlot;
                    break;
                case "Inventory_Chest":
                    tempOne = inv.ChestSlot;
                    break;
                case "Inventory_Back":
                    tempOne = inv.BackSlot;
                    break;
                case "Inventory_Belt":
                    tempOne = inv.WaistSlot;
                    break;
                case "Inventory_Legs":
                    tempOne = inv.LegSlot;
                    break;
                case "Inventory_Feet":
                    tempOne = inv.FeetSlot;
                    break;
                case "Inventory_Primary":
                    tempOne = inv.PrimarySlot;
                    break;
                case "Inventory_Offhand":
                    tempOne = inv.SecondarySlot;
                    break;
                case "Inventory_Ranged":
                    tempOne = inv.RangedSlot;
                    break;
                case "Inventory_Ammo":
                    tempOne = inv.RangedSlot;
                    break;
            }
        }

        if (to.StartsWith("Inventory_Slot"))
        {
            switch (to[to.Length - 1])
            {
                case '1':
                    tempTwo = inv.Slot1;
                    break;
                case '2':
                    tempTwo = inv.Slot2;
                    break;
                case '3':
                    tempTwo = inv.Slot3;
                    break;
                case '4':
                    tempTwo = inv.Slot4;
                    break;
                case '5':
                    tempTwo = inv.Slot5;
                    break;
                case '6':
                    tempTwo = inv.Slot6;
                    break;
                case '7':
                    tempTwo = inv.Slot7;
                    break;
                case '8':
                    tempTwo = inv.Slot8;
                    break;
            }
        }
        else
        {
            switch (to)
            {
                case "Inventory_LeftEar":
                    tempTwo = inv.LeftEarSlot;
                    break;
                case "Inventory_Neck":
                    tempTwo = inv.NeckSlot;
                    break;
                case "Inventory_Face":
                    tempTwo = inv.FaceSlot;
                    break;
                case "Inventory_Head":
                    tempTwo = inv.HeadSlot;
                    break;
                case "Inventory_RightEar":
                    tempTwo = inv.RightEarSlot;
                    break;
                case "Inventory_LeftFinger":
                    tempTwo = inv.LeftFingerSlot;
                    break;
                case "Inventory_LeftWrist":
                    tempTwo = inv.LeftWristSlot;
                    break;
                case "Inventory_Arms":
                    tempTwo = inv.ArmSlot;
                    break;
                case "Inventory_Hands":
                    tempTwo = inv.GloveSlot;
                    break;
                case "Inventory_RightWrist":
                    tempTwo = inv.RightWristSlot;
                    break;
                case "Inventory_RightFinger":
                    tempTwo = inv.RightFingerSlot;
                    break;
                case "Inventory_Shoulders":
                    tempTwo = inv.ShoulderSlot;
                    break;
                case "Inventory_Chest":
                    tempTwo = inv.ChestSlot;
                    break;
                case "Inventory_Back":
                    tempTwo = inv.BackSlot;
                    break;
                case "Inventory_Belt":
                    tempTwo = inv.WaistSlot;
                    break;
                case "Inventory_Legs":
                    tempTwo = inv.LegSlot;
                    break;
                case "Inventory_Feet":
                    tempTwo = inv.FeetSlot;
                    break;
                case "Inventory_Primary":
                    tempTwo = inv.PrimarySlot;
                    break;
                case "Inventory_Offhand":
                    tempTwo = inv.SecondarySlot;
                    break;
                case "Inventory_Ranged":
                    tempTwo = inv.RangedSlot;
                    break;
                case "Inventory_Ammo":
                    tempTwo = inv.RangedSlot;
                    break;
            }
        }

        if (from.StartsWith("Inventory_Slot"))
        {
            switch (from[from.Length - 1])
            {
                case '1':
                    inv.Slot1 = tempTwo; 
                    break;
                case '2':
                     inv.Slot2 = tempTwo;
                    break;
                case '3':
                    inv.Slot3 = tempTwo;
                    break;
                case '4':
                    inv.Slot4 = tempTwo;
                    break;
                case '5':
                     inv.Slot5 = tempTwo;
                    break;
                case '6':
                     inv.Slot6 = tempTwo;
                    break;
                case '7':
                    inv.Slot7 = tempTwo;
                    break;
                case '8':
                    inv.Slot8 = tempTwo;
                    break;
            }
        }
        else
        {
            switch (from)
            {
                case "Inventory_LeftEar":
                    inv.LeftEarSlot = tempTwo;
                    break;
                case "Inventory_Neck":
                    inv.NeckSlot = tempTwo;
                    break;
                case "Inventory_Face":
                    inv.FaceSlot  = tempTwo;
                    break;
                case "Inventory_Head":
                    inv.HeadSlot  = tempTwo;
                    break;
                case "Inventory_RightEar":
                    inv.RightEarSlot  = tempTwo;
                    break;
                case "Inventory_LeftFinger":
                    inv.LeftFingerSlot  = tempTwo;
                    break;
                case "Inventory_LeftWrist":
                    inv.LeftWristSlot  = tempTwo;
                    break;
                case "Inventory_Arms":
                    inv.ArmSlot  = tempTwo;
                    break;
                case "Inventory_Hands":
                    inv.GloveSlot  = tempTwo;
                    break;
                case "Inventory_RightWrist":
                    inv.RightWristSlot  = tempTwo;
                    break;
                case "Inventory_RightFinger":
                    inv.RightFingerSlot  = tempTwo;
                    break;
                case "Inventory_Shoulders":
                    inv.ShoulderSlot  = tempTwo;
                    break;
                case "Inventory_Chest":
                    inv.ChestSlot  = tempTwo;
                    break;
                case "Inventory_Back":
                    inv.BackSlot  = tempTwo;
                    break;
                case "Inventory_Belt":
                    inv.WaistSlot  = tempTwo;
                    break;
                case "Inventory_Legs":
                    inv.LegSlot  = tempTwo;
                    break;
                case "Inventory_Feet":
                    inv.FeetSlot  = tempTwo;
                    break;
                case "Inventory_Primary":
                    inv.PrimarySlot  = tempTwo;
                    break;
                case "Inventory_Offhand":
                    inv.SecondarySlot  = tempTwo;
                    break;
                case "Inventory_Ranged":
                    inv.RangedSlot  = tempTwo;
                    break;
                case "Inventory_Ammo":
                    inv.RangedSlot  = tempTwo;
                    break;
            }
        }

        if (to.StartsWith("Inventory_Slot"))
        {
            switch (to[to.Length - 1])
            {
                case '1':
                    inv.Slot1 = tempOne;
                    break;
                case '2':
                    inv.Slot2 = tempOne;
                    break;
                case '3':
                    inv.Slot3 = tempOne;
                    break;
                case '4':
                    inv.Slot4 = tempOne;
                    break;
                case '5':
                    inv.Slot5 = tempOne;
                    break;
                case '6':
                    inv.Slot6 = tempOne;
                    break;
                case '7':
                    inv.Slot7 = tempOne;
                    break;
                case '8':
                    inv.Slot8 = tempOne;
                    break;
            }
        }
        else
        {
            switch (to)
            {
                case "Inventory_LeftEar":
                    inv.LeftEarSlot = tempOne;
                    break;
                case "Inventory_Neck":
                    inv.NeckSlot = tempOne;
                    break;
                case "Inventory_Face":
                    inv.FaceSlot = tempOne;
                    break;
                case "Inventory_Head":
                    inv.HeadSlot = tempOne;
                    break;
                case "Inventory_RightEar":
                    inv.RightEarSlot = tempOne;
                    break;
                case "Inventory_LeftFinger":
                    inv.LeftFingerSlot = tempOne;
                    break;
                case "Inventory_LeftWrist":
                    inv.LeftWristSlot = tempOne;
                    break;
                case "Inventory_Arms":
                    inv.ArmSlot = tempOne;
                    break;
                case "Inventory_Hands":
                    inv.GloveSlot = tempOne;
                    break;
                case "Inventory_RightWrist":
                    inv.RightWristSlot = tempOne;
                    break;
                case "Inventory_RightFinger":
                    inv.RightFingerSlot = tempOne;
                    break;
                case "Inventory_Shoulders":
                    inv.ShoulderSlot = tempOne;
                    break;
                case "Inventory_Chest":
                    inv.ChestSlot = tempOne;
                    break;
                case "Inventory_Back":
                    inv.BackSlot = tempOne;
                    break;
                case "Inventory_Belt":
                    inv.WaistSlot = tempOne;
                    break;
                case "Inventory_Legs":
                    inv.LegSlot = tempOne;
                    break;
                case "Inventory_Feet":
                    inv.FeetSlot = tempOne;
                    break;
                case "Inventory_Primary":
                    inv.PrimarySlot = tempOne;
                    break;
                case "Inventory_Offhand":
                    inv.SecondarySlot = tempOne;
                    break;
                case "Inventory_Ranged":
                    inv.RangedSlot = tempOne;
                    break;
                case "Inventory_Ammo":
                    inv.RangedSlot = tempOne;
                    break;
            }
        }

        UserInterface.Inv.UpdateEquip();
    }



    void OnConnected(SocketIOEvent e)
    {
        Debug.Log("connected");
        
    }

    private void OnMove(SocketIOEvent e)
    {

        var id = e.data["id"].ToString();
        var mover = npcs[id];
        var position = new Vector3(GetFloatFromJson(e.data, "x"), GetFloatFromJson(e.data, "y"), -1);
    
        var clickMove = mover.GetComponent<ClickMove>();
        clickMove.Move(position);

    }

    private void RequestPosition(SocketIOEvent e)
    {
        socket.Emit("updatePosition", new JSONObject(VectorToJson(myPlayer.transform.position)));
    }

    private void UpdatePosition(SocketIOEvent e)
    {
        var player = players[e.data["id"].ToString()];

        var position = new Vector3(GetFloatFromJson(e.data, "x"), GetFloatFromJson(e.data, "y"), -1);

        player.transform.position = position;
    }


    private void OnDisconnected(SocketIOEvent e)
    {
        var id = e.data["id"].ToString();
        var player = players[id];
        Destroy(player);

        players.Remove(id);
    }

    public static string VectorToJson(Vector3 vector)
    {
        return string.Format(@"{{""x"":""{0}"",""y"":""{1}""}}", vector.x, vector.y);
    }

    float GetFloatFromJson(JSONObject data, string key)
    {
        return float.Parse(data[key].ToString().Replace("\"", ""));
    }


}
