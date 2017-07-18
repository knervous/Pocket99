using UnityEngine;
using System.Collections.Generic;
using SocketIO;
using UnityEngine.SceneManagement;
using System;
using Assets.Scripts.Data_Models;
using Assets.Scripts.UI;
using UnityEngine.Events;
using UnityEngine.UI;
#if UNITY_5_3_OR_NEWER
using Noesis;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;
#endif



public class WorldConnect : MonoBehaviour {

    public static WorldConnect instance;
    public GameObject WorldConnectObject;
    public GameObject SelectedChar;
    public GameObject EntryPointUI;
    public Camera loginCam;
    public Camera gameCam;
    public SocketIOComponent socket;
    public GameObject zoneConnectSocket;
    public CharacterSelect CharSelect;
    public CharacterCreate CharCreate;
    public GameObject MainPlayer;
    public GameObject selectedPlayer;
    public bool loggedIn = false;

    public Account server_account;
    public List<Player> server_player = new List<Player>();
    public List<Inventory> server_inventory = new List<Inventory>();

    public string username;
    public string password;

  

    public void Awake()
    {
        if (instance)
            DestroyImmediate(WorldConnectObject);
        else
        {
            DontDestroyOnLoad(WorldConnectObject);
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        instance = this;
        //SceneManager.activeSceneChanged += SpawnPlayer;
        socket.On("login_success", CharacterSelectConnect);
        socket.On("login_connected", AuthenticateUser);
        socket.On("password_failed", PasswordFailed);
        socket.On("account_created", AccountCreated);
        socket.On("name_check", CreateCharacter);
        socket.On("character_created", CharacterCreated);
        socket.On("enter_world", EnterWorld);
       // socket.On("receive_inventory", ReceiveInventory);
    }

    private void AccountCreated(SocketIOEvent e)
    {
        AuthenticateUser(e);
        CharSelect.OpenPopupPanel("Account created: " + username);
    }

    private void PasswordFailed(SocketIOEvent e)
    {
        Debug.Log("password failed");
        socket.Close();
    }

    public void CheckName(string name)
    {
        JSONObject obj = new JSONObject();
        obj.AddField("name", name);
        obj.AddField("exists", new bool());
        socket.Emit("check_name", obj);
    }

    private void CreateCharacter(SocketIOEvent obj)
    {
        Debug.Log("name check hit");
        if (obj.data["exists"].b)
            CharSelect.OpenPopupPanel("Name already taken. Try another.");
        else
        {
            CharSelect.OpenPopupPanel("Name accepted. Creating character now.");
            socket.Emit("create_new_character", CharCreate.CreatedPlayer.CreateServerPlayer());
        }
    }

    private void CharacterCreated(SocketIOEvent obj)
    {
        server_player.Clear();
        server_inventory.Clear();
        CharCreate.page = 1;
        CharCreate.ResetValues();
        AuthenticateUser(obj);
    }

    private void CharacterSelectConnect(SocketIOEvent e)
    {
        server_account = gameObject.AddComponent<Account>();
        server_account.PopulateFromServer(e);
        for(int x = 0; x < e.data["characters"].list.Count; x++)
        {
            var ch = e.data["characters"].list[x];
            var inv = e.data["inventories"].list[x];
            var tempPlayer = gameObject.AddComponent<Player>();
            tempPlayer.PopulateFromServer(ch,inv);
            server_player.Add(tempPlayer);
        }
        if(server_player.Count > 0)
        {
            SpawnSelected(0);
        }
        CharSelect.ToCharList();
    }
    private void AuthenticateUser(SocketIOEvent e)
    {
        JSONObject login = new JSONObject();
        JSONObject account = new JSONObject();
        JSONObject characters = new JSONObject();
        JSONObject inventories = new JSONObject();
        login.AddField("username", username);
        login.AddField("password", password);
        login.AddField("account", account);
        login.AddField("characters", characters);
        login.AddField("inventories", inventories);

        socket.Emit("trylogin", login);
    }
    public void TryLogin(string server)
    {
        socket.UpdateWebSocket(server);
        socket.Connect();
    }

    public void SpawnSelected(int index)
    {
        Destroy(selectedPlayer);
        var p = server_player[index];

        selectedPlayer = Instantiate(Resources.Load("Textures/Character Models/Barbarian/Male/Barbarian Male")) as GameObject;
        selectedPlayer.transform.parent = MainPlayer.transform;
        selectedPlayer.transform.localScale = new UnityEngine.Vector3(8, 8, 1);
        selectedPlayer.transform.localPosition = new UnityEngine.Vector3(0, 0, -1);
        //MainPlayer.transform.position = new Vector3(1, 1, 1);
        selectedPlayer.GetComponent<Animation>().Player = MainPlayer;
        selectedPlayer.GetComponent<CollisionDetection>().player = MainPlayer;
        selectedPlayer.GetComponent<Animation>().inventory = p.inventory_;
        MainPlayer.name = p.name_;
        MainPlayer.GetComponent<PlayerAttributes>().player = p;
        
    }

    public void DoEnterWorld(int index)
    {
        var player = server_player[index];
        GameObject.Destroy(EntryPointUI);
        switch (player.race_)
        {
            //case (int)Player.RaceById.Barbarian:
            //    break;
            default:
                SceneManager.LoadScene("Qeynos2Scene");
                string newUrl = socket.url.Split(':')[2];

                //zoneConnectSocket.GetComponent<SocketIOComponent>().url = "ws://localhost:5998/socket.io/?EIO=4&transport=websocket";
                zoneConnectSocket.SetActive(true);
                socket.Emit("zone_into_world", player.CreateServerPlayer());
                MainPlayer.transform.position = new UnityEngine.Vector3(1, 1, 1);
                selectedPlayer.transform.localScale = new UnityEngine.Vector3(3, 3, 1);
                selectedPlayer.tag = "Player";
                DontDestroyOnLoad(selectedPlayer);
                zoneConnectSocket.GetComponent<Network>().myPlayer = MainPlayer;
                loginCam.gameObject.SetActive(false);
                gameCam.gameObject.SetActive(true);
                gameCam.tag = "MainCamera";
                gameCam.GetComponent<CameraFollow>().player = MainPlayer;
                gameCam.GetComponent<ScreenClicker>().player = MainPlayer;
                gameCam.GetComponent<CameraFollow>().StartCamera();
                UIInventory.Inv.UpdateEquip();
                break;
        }
    }

    public void EnterWorld(SocketIOEvent e)
    {

    }

    // Update is called once per frame
    void Update () {
       
    }


    public static GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }
}
