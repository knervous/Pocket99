using UnityEngine;
using System.Collections;
using System;

public class ClickMove : MonoBehaviour {

    public float duration = 20.0f;
    private Vector3 position;
    public bool isMoving = false;
    public static float colliderCoefficient = 1;
    public bool moveBackWards;
    private float xMag = 0.0f;
    private float yMag = 0.0f;

    public float XMag
    {
        get { return xMag; }
        set { xMag = value; }
    }

    public float YMag
    {
        get { return yMag; }
        set { yMag = value; }
    }

    void Start () {
	}
	
	public void OnClick (Vector3 infposition) {
        position = infposition;
        var navPos = gameObject.GetComponent<NavigatePosition>();
        var netMove = gameObject.GetComponent<NetworkMove>();
        Debug.Log("Navigating to: " + position);
        netMove.OnMove(position);

        gameObject.GetComponent<CollisionDetection>().Collided = false;
        isMoving = true;
     
        Debug.Log("PLAYER POSITION: "+ gameObject.transform.position);
        //navPos.NavigateTo(position);
	}

    public void Move(Vector3 infposition) {

        position = infposition;
        isMoving = true;
        gameObject.GetComponent<CollisionDetection>().Collided = false;
    }

    void Update()
    {
        if (isMoving)
        {
            var moveTotalX = position.x - gameObject.transform.position.x;
            var moveTotalY = position.y - gameObject.transform.position.y;
            var total = Math.Abs(moveTotalX) + Math.Abs(moveTotalY);
            xMag = moveTotalX / total;
            yMag = moveTotalY / total;

            var moveSpeed = gameObject.GetComponent<PlayerAttributes>().PlayerSpeed;

            for (float f = 0; f < moveSpeed; f++)
            {
                if (Math.Abs(total) > 10)
                {
                    gameObject.transform.Translate(xMag * Time.deltaTime, yMag * Time.deltaTime, 0);
                }
                else
                {
                    //gameObject.transform.Translate( -100 * xMag * Time.deltaTime , -100 * yMag * Time.deltaTime , 0);
                    isMoving = false;
                    break;
                }
            }

        }
        else
        {

        }
        
    }
}
