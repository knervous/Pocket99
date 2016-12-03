using UnityEngine;
using System.Collections;

public class ClickMove : MonoBehaviour {

    public float duration = 20.0f;
    private Vector3 position;
    private bool isMoving = false;

	void Start () {
	}
	
	public void OnClick (Vector3 infposition) {
        position = infposition;
        var navPos = gameObject.GetComponent<NavigatePosition>();
        var netMove = gameObject.GetComponent<NetworkMove>();
        Debug.Log("Navigating to: " + position);
        netMove.OnMove(position);

        //player.transform.position = Vector3.Lerp(player.transform.position, position, 1 / (duration * (Vector3.Distance(gameObject.transform.position, position))));
        //player.transform.position = position;
  
        isMoving = true;
     
        Debug.Log("PLAYER POSITION: "+ gameObject.transform.position);
        //navPos.NavigateTo(position);
	}

    public void Move(Vector3 infposition) {

        position = infposition;
        isMoving = true;


        //player.transform.position = Vector3.Lerp(player.transform.position, position, .5f); // / (duration * (Vector3.Distance(gameObject.transform.position, position))));
        //player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(position.x, position.y, -1), 1);
       // Vector3.MoveTowards(player.transform.position, position, 500);
    }

    void Update()
    {
        if (isMoving && !Mathf.Approximately(gameObject.transform.position.magnitude, position.magnitude))
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, position, .2f);// / (duration * (Vector3.Distance(gameObject.transform.position, position))));
        }

        else if (isMoving && Mathf.Approximately(gameObject.transform.position.magnitude, position.magnitude))
        {
            isMoving = false;
            Debug.Log("I am here");
        }
    }
}
