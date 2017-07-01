using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {

    // Use this for initialization

    public bool collided = false;
    public Collision2D collidedObject;
    public GameObject player;

    public bool Collided
    {
        get { return collided; }
        set { collided = value; }
    }

	void Start () {
       
	}
	


    void OnCollisionEnter2D(Collision2D collider)
    {
        collided = true;
        if (!(collider.gameObject.tag == "Player"))
        {
            Debug.Log("Trying mags " + player.GetComponent<ClickMove>().XMag + " " + player.GetComponent<ClickMove>().YMag);
            player.transform.Translate(-player.GetComponent<ClickMove>().XMag * Time.deltaTime, -player.GetComponent<ClickMove>().YMag * Time.deltaTime, 0);
            player.GetComponent<ClickMove>().isMoving = false;
            
        }
    }


    void OnCollisionStay2D(Collision2D collider)
    {
        collided = true;
        //if(!(collider.gameObject.tag == "Player"))
        if(collider.gameObject.GetComponent<PolygonCollider2D>() != null)
        {
            player.transform.Translate(-player.GetComponent<ClickMove>().XMag * Time.deltaTime * player.GetComponent<PlayerAttributes>().PlayerSpeed, -player.GetComponent<ClickMove>().YMag * Time.deltaTime * player.GetComponent<PlayerAttributes>().PlayerSpeed, 0);
            player.GetComponent<ClickMove>().isMoving = false;
        }

    }

    void OnCollisionExit2D(Collision2D collider)
    {
      //  player.GetComponent<BoxCollider2D>().size = player.GetComponent<PlayerAttributes>().HitBoxSize;

        //collided = false;
        //player.GetComponent<ClickMove>().isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {


    }

    void LateUpdate()
    {
    }




}
