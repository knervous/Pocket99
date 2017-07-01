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

        npc.tag = "fire beetle";
        npc.transform.position = new Vector3(GetFloatFromJson(e.data["position"], "x"), GetFloatFromJson(e.data["position"], "y"), -1);

        npcs.Add(e.data["zoneId"].ToString(), npc);
    }



    void OnConnected(SocketIOEvent e)
    {
        Debug.Log("connected");
        
    }

    private void OnMove(SocketIOEvent e)
    {
        Debug.Log("player is moving" + e.data);
        var id = e.data["id"].ToString();
        var mover = npcs[id];
        var position = new Vector3(GetFloatFromJson(e.data, "x"), GetFloatFromJson(e.data, "y"), -1);
        Debug.Log("TRYING TO MOVE TO: " + position);
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
