using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {
    public GameObject dd;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(dd);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
