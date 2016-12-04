using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;       //Public variable to store a reference to the player game object
    public float perspectiveZoomSpeed = .5f;
    public float orthoZoomSpeed = .5f;

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        if(Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Camera.main.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
            Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, .1f);

        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetKey(KeyCode.Alpha2))
        {
            Camera.main.orthographicSize -= 5;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetKey(KeyCode.Alpha1))
        {
            Camera.main.orthographicSize += 5;
        }

    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
    }
}