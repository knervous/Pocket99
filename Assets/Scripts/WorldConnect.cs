using UnityEngine;
using System.Collections.Generic;
using SocketIO;
using UnityEngine.SceneManagement;
using System;
using Assets.Scripts.Data_Models;
using Assets.Scripts.UI;
using UnityEngine.Events;
using UnityEngine.UI;

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
        
        foreach (var char_ in e.data["characters"].list)
        {
            var tempPlayer = gameObject.AddComponent<Player>();
            tempPlayer.PopulateFromServer(char_);
            server_player.Add(tempPlayer);
        }

        //foreach (var inv_ in e.data["inventories"].list)
        //{
        //    server_inventory.Add(gameObject.AddComponent<Inventory>());
        //    //populate from server method needed
        //}

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
                zoneConnectSocket.SetActive(true);
                socket.Emit("zone_into_world", player.CreateServerPlayer());
                MainPlayer.SetActive(true);

                var newPlayer = Instantiate(Resources.Load("Textures/Character Models/Barbarian/Male/Barbarian Male")) as GameObject;
                newPlayer.transform.parent = MainPlayer.transform;
                newPlayer.transform.localScale = new Vector3(1, 1, 1);
                newPlayer.transform.position = new Vector3(1, 1, 1);
                MainPlayer.transform.position = new Vector3(1, 1, 1);
                newPlayer.GetComponent<Animation>().Player = MainPlayer;
                newPlayer.GetComponent<CollisionDetection>().player = MainPlayer;
                MainPlayer.GetComponent<ClickMove>().playerModel = newPlayer;
                MainPlayer.name = player.name_;
                newPlayer.tag = "Player";
                DontDestroyOnLoad(newPlayer);
                zoneConnectSocket.GetComponent<Network>().myPlayer = MainPlayer;
                loginCam.gameObject.SetActive(false);
                gameCam.gameObject.SetActive(true);
                gameCam.tag = "MainCamera";
                gameCam.GetComponent<CameraFollow>().player = MainPlayer;
                gameCam.GetComponent<ScreenClicker>().player = MainPlayer;
                gameCam.GetComponent<CameraFollow>().StartCamera();


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
