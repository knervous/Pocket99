using UnityEngine;
using System.Collections;
using System;

public class ScreenClicker : MonoBehaviour {


    public float speed = 1.5f;
    
    public GameObject floor;
    public GameObject player;
    private Vector3 target;
    private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
        target = transform.position;
        rbody = player.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(1))
        {
            var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TryMove(target);
        }
        if(Input.touchCount == 1 )
        {
            var target = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            TryMove(target);
        }

        var playerSpeed = 100;
        if (Input.GetKey("up"))//Press up arrow key to move forward on the Y AXIS
        {
            player.transform.Translate(0, playerSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey("down"))//Press up arrow key to move forward on the Y AXIS
        {
            player.transform.Translate(0, -playerSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey("left"))//Press up arrow key to move forward on the Y AXIS
        {
            player.transform.Translate(-playerSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("right"))//Press up arrow key to move forward on the Y AXIS
        {
            player.transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
        }


    }

    void TryMove(Vector3 target)
    {


      //  var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var clickMove = player.GetComponent<ClickMove>();

#if UNITY_EDITOR
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //for touch device
#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
   target = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif


        clickMove.OnClick(new Vector3(target.x, target.y, 0));

        //clickMove.OnClick(ray);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            clickMove = hit.collider.gameObject.GetComponent<ClickMove>();
            Debug.Log("Clicked, ray found, trying to go to position: " + hit.point);
            clickMove.OnClick(hit.point);
        }
    }
}
