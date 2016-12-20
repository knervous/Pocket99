using UnityEngine;
using System.Collections;
using System;

public class SelectedCharacter : MonoBehaviour {


    private SelectedCharacter instance;
    public GameObject PlayerObject;
    public GameObject PlayerGameObject;

    void Awake()
    {
        if(!instance)instance = this;
       // DontDestroyOnLoad(PlayerObject);
        DontDestroyOnLoad(PlayerGameObject);
    }

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
