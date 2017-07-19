using UnityEngine;
using System.Collections;
using System;
using Noesis;
using Vector3 = UnityEngine.Vector3;

public class ScreenClicker : MonoBehaviour {


    public float speed = 1.5f;
       
    public GameObject player;
    private Rigidbody2D rbody;
    private int counter = 0;
    private Visual _root;

    // Use this for initialization
    void Start () {
        var gui = GetComponent<NoesisView>();
        _root = (Visual)gui.Content;
        DontDestroyOnLoad(this);
    }
	
	// Update is called once per frame
	void Update () {
        if (counter > 15)
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

        Point point = new Point(target.x, Screen.height - target.y);
        HitTestResult hit = VisualTreeHelper.HitTest(this._root, point);
        if (hit.VisualHit != null)
            return;

        var hitCollider = Physics2D.OverlapPoint(target);
        if(hitCollider)
        {
            switch(hitCollider.gameObject.tag)
            {
                case "Player":
                    Debug.Log("Player clicked");
                    break;
            }
        }

        clickMove.OnClick(new Vector3(target.x, target.y, 0));
    }
}
