using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class ClickMove : MonoBehaviour {

    public float duration = 20.0f;
    public Vector3 position;
    public bool isMoving = false;
    public static float colliderCoefficient = 1;
    public bool moveBackWards;
    private float xMag = 0.0f;
    private float yMag = 0.0f;
    public event EventHandler Changed;

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
        Debug.Log("Navigating to: " + position);
        //Network.socket.Emit("move", new JSONObject(Network.VectorToJson(new Vector3(position.x, position.y, -1))));
        gameObject.GetComponent<CollisionDetection>().Collided = false;
        //isMoving = true;
        GetComponent<NavMeshAgent>().SetDestination(infposition);

    }

    public void Move(Vector3 infposition) {

        position = infposition;
        isMoving = true;
        
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
                    xMag = yMag = 0;
                    isMoving = false;
                    break;
                }
            }

        }
        
    }
}
