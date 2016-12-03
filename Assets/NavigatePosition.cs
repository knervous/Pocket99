using UnityEngine;
using System.Collections;

public class NavigatePosition : MonoBehaviour {

    NavMeshAgent agent;

	void Start () {

        agent = GetComponent<NavMeshAgent>();
        Debug.Log("Agent is: " + agent);
	}

	    public void NavigateTo (Vector3 position) {
        //Debug.Log("Clicked, trying to go to position: " + position);
        agent.SetDestination(position);
	}

    void Update()
    {
       // GetComponent<Animator>().SetFloat("Distance", agent.remainingDistance);
    }
}
