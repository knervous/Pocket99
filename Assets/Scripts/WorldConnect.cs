using UnityEngine;
using System.Collections;
using SocketIO;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class WorldConnect : MonoBehaviour {

    public static WorldConnect instance;
    public GameObject WorldConnectObject;
    public GameObject SelectedChar;
    public GameObject player;
    public Camera gameCam;
    public SocketIOComponent socket;
    public bool loggedIn = false;


    public InputField server;
    public InputField username;
    public InputField password;
    public Button connect;

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
        SceneManager.activeSceneChanged += SpawnPlayer;
        socket.On("loginsuccess", CharacterSelectConnect);
        socket.On("login_connected", (e) => { loggedIn = true; AuthenticateUser(e); });
        connect.onClick.AddListener(TryLogin);
    }

    private void CharacterSelectConnect(SocketIOEvent e)
    {
        Debug.Log("DATA: " + e.data["characters"]["character_one"]["name"].str);
        var chars = e.data["characters"];
        //chars["character_one"][]
    }
    private void AuthenticateUser(SocketIOEvent e)
    {
        JSONObject login = new JSONObject();
        JSONObject characters = new JSONObject();
        login.AddField("username", username.text);
        login.AddField("password", password.text);
        login.AddField("characters", characters);
        socket.Emit("trylogin", login);
    }
    private void TryLogin()
    {
        string serverurl = string.Empty;
        serverurl += @"ws://";
        serverurl += server.text.Length > 0 ? server.text : server.placeholder.GetComponent<Text>().text;
        serverurl += @":7000/socket.io/?EIO=4&transport=websocket";
        socket.UpdateWebSocket(serverurl);
        socket.Connect();
    }

    void SpawnPlayer(Scene previousScene, Scene newScene)
    {
        player = Instantiate(SelectedChar.GetComponent<SelectedCharacter>().PlayerObject) as GameObject;
       
        gameCam.GetComponent<CameraFollow>().player = player;
        gameCam.GetComponent<ScreenClicker>().player = player;
        gameCam.GetComponent<CameraFollow>().StartCamera();
        player.name = "My Player Character";
        player.tag = "Player";
        Network.instance.myPlayer = player;
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
