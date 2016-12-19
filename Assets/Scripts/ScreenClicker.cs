using UnityEngine;
using System.Collections;
using System;

public class ScreenClicker : MonoBehaviour {


    public float speed = 1.5f;
       
    public GameObject player;
    private Rigidbody2D rbody;
    private int counter = 0;

	// Use this for initialization
	void Start () {
        
        rbody = player.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (counter > 30)
        {
            if (Input.GetMouseButtonDown(1))
            {
                var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                TryMove(target);
            }
            if (Input.touchCount == 1)
            {
                var target = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                TryMove(target);
            }
        }
        else { counter++; }


    }

    void TryMove(Vector3 target)
    {
        counter = 0;
        var clickMove = player.GetComponent<ClickMove>();

#if UNITY_EDITOR
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //for touch device
#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
   target = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
        clickMove.OnClick(new Vector3(target.x, target.y, 0));
    }
}
