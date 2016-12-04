using UnityEngine;
using System.Collections;

public class PlayerAttributes : MonoBehaviour {


    public float PlayerSpeed = 100f;
    public Vector2 HitBoxSize = new Vector2(2.5f, 4);

	// Use this for initialization
	void Start () {

     
        tag = "Player";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
