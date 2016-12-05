using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {

    // Use this for initialization

    public bool collided = false;
    public Collision2D collidedObject;

    public bool Collided
    {
        get { return collided; }
        set { collided = value; }
    }


    Vector3 lastPos;

	void Start () {
        lastPos = new Vector3();
	}
	
	

    void Awake()
    {
        lastPos = transform.position;
    }


    void OnCollisionEnter2D(Collision2D collider)
    {
        

        collided = true;
        if (!(collider.gameObject.tag == "Player"))
        {
            Debug.Log("Trying mags " + GetComponent<ClickMove>().XMag + " " + GetComponent<ClickMove>().YMag);
            transform.Translate(-GetComponent<ClickMove>().XMag * Time.deltaTime, -GetComponent<ClickMove>().YMag * Time.deltaTime, 0);
            GetComponent<ClickMove>().isMoving = false;
        }



    }


    void OnCollisionStay2D(Collision2D collider)
    {
        collided = true;
        
        if(!(collider.gameObject.tag == "Player"))
        {
            transform.Translate(-GetComponent<ClickMove>().XMag * Time.deltaTime * GetComponent<PlayerAttributes>().PlayerSpeed, -GetComponent<ClickMove>().YMag * Time.deltaTime * GetComponent<PlayerAttributes>().PlayerSpeed, 0);
            GetComponent<ClickMove>().isMoving = false;
        }

    }

    void OnCollisionExit2D(Collision2D collider)
    {
      //  GetComponent<BoxCollider2D>().size = GetComponent<PlayerAttributes>().HitBoxSize;

        //collided = false;
        //GetComponent<ClickMove>().isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {


    }

    void LateUpdate()
    {
    }




}
