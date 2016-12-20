using UnityEngine;
using System.Collections;
using SocketIO;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

public class WorldConnect : MonoBehaviour {

    public static WorldConnect instance;
    public GameObject WorldConnectObject;
    public GameObject SelectedChar;
    public GameObject player;
    public Camera gameCam;
    public SocketIOComponent socket;
    public bool loggedIn = false;

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
        socket = Instantiate(socket);
        socket.transform.parent = gameObject.transform;
        socket.name = "Socket Connection";
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
       
        if (Network.socket.IsConnected && loggedIn == false && SceneManager.GetActiveScene().name != "Qeynos2Scene")
        {
            SceneManager.LoadScene("Qeynos2Scene");
            loggedIn = true;
        }

    }


    public static GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }
}
