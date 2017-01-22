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
    public GameObject playerModel;

    public float XMag { get; set; }

    public float YMag { get; set; }

    void Start () {
	}
	
	public void OnClick (Vector3 infposition) {
        position = infposition;
        Debug.Log("Navigating to: " + position);
        if(Network.socket)
        Network.socket.Emit("move", new JSONObject(Network.VectorToJson(new Vector3(position.x, position.y, -1))));
        //gameObject.GetComponent<CollisionDetection>().Collided = false;
        isMoving = true;
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
            XMag = moveTotalX / total;
            YMag = moveTotalY / total;

            var moveSpeed = gameObject.GetComponent<PlayerAttributes>().PlayerSpeed;

            for (float f = 0; f < moveSpeed; f++)
            {
                if (Math.Abs(total) > 10)
                {
                    gameObject.transform.Translate(XMag * Time.deltaTime, YMag * Time.deltaTime, 0);
                    
                }
                else
                {
                    XMag = YMag = 0;
                    isMoving = false;
                    break;
                }
            }

        }
        
    }
}
