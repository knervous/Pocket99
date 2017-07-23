using UnityEngine;
using System.Collections.Generic;
using SocketIO;
using Assets.Scripts.Data_Models;


public class ChatNetwork : MonoBehaviour
{
    public static SocketIOComponent socket;
    public static ChatNetwork instance;
    public GameObject SocketObject;

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
        socket.On("item_search_result", ItemSearchResult);
        socket.On("summon_item", SummonItem);

    }

    void SummonItem(SocketIOEvent e)
    {
        var d = e.data;
        var inv = MainPlayer.instance.GetComponent<PlayerAttributes>().player.inventory_;
        Debug.Log(e.data["slot"]);
        Debug.Log("Before switch");
        switch ((int)e.data["slot"].n)
        {
            case 1:
                inv.Slot1 = Item.CreateFromJSON(e.data["summoned"].ToString());
                break;
            case 2:
                inv.Slot2 = Item.CreateFromJSON(e.data["summoned"].ToString());
                break;
            case 3:
                inv.Slot3 = Item.CreateFromJSON(e.data["summoned"].ToString());
                break;
            case 4:
                inv.Slot4 = Item.CreateFromJSON(e.data["summoned"].ToString());
                break;
            case 5:
                inv.Slot5 = Item.CreateFromJSON(e.data["summoned"].ToString());
                break;
            case 6:
                inv.Slot6 = Item.CreateFromJSON(e.data["summoned"].ToString());
                break;
            case 7:
                inv.Slot7 = Item.CreateFromJSON(e.data["summoned"].ToString());
                break;
            case 8:
                inv.Slot8 = Item.CreateFromJSON(e.data["summoned"].ToString());
                break;
            default:
                break;
        }
        Debug.Log("After switch");
        UserInterface.Chat.InsertChatMessage(e.data["item"].str);
        UserInterface.Inv.UpdateEquip();
    }

    void ItemSearchResult(SocketIOEvent e)
    {
        foreach(var a in e.data.list)
        {
            foreach(var b in a.list)
            {
                string s = b.GetField("id").n + " - " + b.GetField("name").str;
                UserInterface.Chat.InsertChatMessage(s);
            }
            UserInterface.Chat.InsertChatMessage(System.String.Format("\r\n Found {0} or more Results \r\n", a.list.Count ));
        }
    }

    void OnConnected(SocketIOEvent e)
    {
        Debug.Log("connected");
        
    }

}
