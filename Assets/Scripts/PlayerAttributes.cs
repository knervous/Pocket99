using UnityEngine;
using System.Collections;

public class PlayerAttributes : MonoBehaviour {

    public PlayerAttributes instance;
    public GameObject gamePlayerObject;
    public float PlayerSpeed = 100f;
    public Vector2 HitBoxSize = new Vector2(2.5f, 4);

    // Use this for initialization
    public void Awake()
    {
        if (instance)
            DestroyImmediate(gamePlayerObject);
        else
        {
            DontDestroyOnLoad(gamePlayerObject);
            instance = this;
        }
    }

    void Start () {
        tag = "Player";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
