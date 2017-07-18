using UnityEngine;

public class MainPlayer : MonoBehaviour {

    public static MainPlayer instance;
    public GameObject MainPlayerObject;

    public void Awake()
    {
        if (instance)
            DestroyImmediate(MainPlayerObject);
        else
        {
            DontDestroyOnLoad(MainPlayerObject);
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        instance = this;
    }

    // Update is called once per frame
    void Update () {
       
    }


}
