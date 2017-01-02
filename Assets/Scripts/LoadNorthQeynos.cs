//using UnityEngine;
//using System.Collections;
//using UnityEngine.SceneManagement;
//using SocketIO;

//public class LoadNorthQeynos : MonoBehaviour {


//    public GameObject placeHolder;
//    public Camera inf_camera;

//    bool netLoaded = false;
//    bool playerLoaded = false;
//    bool allLoaded = false;

//    private GameObject sock = null;
//    // Use this for initialization
//    void Start () {
       
//        SceneManager.LoadSceneAsync("Network", LoadSceneMode.Additive);
//        SceneManager.LoadSceneAsync("PlayerModels", LoadSceneMode.Additive);

//	}

//    void LoadPlayerModel()

//    {
//        var go = SceneManager.GetSceneByName("PlayerModels").GetRootGameObjects();
//        foreach (GameObject g in go)
//        {
//            if (g.name == "Barb Male Model")
//            {
//                placeHolder = Instantiate(g) as GameObject;
//                placeHolder.GetComponent<NetworkMove>().socket = sock.GetComponent<SocketIOComponent>();
//                placeHolder.tag = "Player";
//                sock.GetComponent<Network>().myPlayer = placeHolder;
//                sock.GetComponent<Network>().playerPrefab = placeHolder;
//                inf_camera.GetComponent<ScreenClicker>().player = placeHolder;
//                inf_camera.GetComponent<CameraFollow>().player = placeHolder;
//                //  placeHolder.GetComponent<NetworkMove>().socket =  SceneManager.GetSceneByName("Network").GetRootGameObjects()[0].GetComponent<SocketIOComponent>();
//            }
//        }

//        playerLoaded = true;
//    }
//	void LoadNet()
//    {
//        var network = SceneManager.GetSceneByName("Network").GetRootGameObjects();
//        foreach (GameObject obj in network)
//        {
//            if (obj.name == "NetworkComponent")
//            {
//                sock = obj;
//            }
//        }

//        netLoaded = true;
//    }
//	// Update is called once per frame


//	void Update () {

//        if (SceneManager.GetSceneByName("Network").isLoaded && !netLoaded) LoadNet();

//        if (SceneManager.GetSceneByName("PlayerModels").isLoaded && !playerLoaded && netLoaded) LoadPlayerModel();



//    }
//}
