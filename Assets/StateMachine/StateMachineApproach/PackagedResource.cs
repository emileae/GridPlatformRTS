using UnityEngine;
using System.Collections;

public class PackagedResource : MonoBehaviour {

	private bool follow = false;
	private GameObject followObject;
	private GameObject foundation;
	private NPCInstructions instructions;
	private PayTarget payScript;

	// Use this for initialization
	void Start () {
		instructions = GetComponent<NPCInstructions>();
		payScript = GetComponent<PayTarget>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (follow) {
			transform.position = followObject.transform.position;
		} else {
			if (foundation != null) {
				transform.position = foundation.transform.position;
			}
		}
	}

	public void FollowPlayer(GameObject playerCarryLocation){
		follow = true;
		followObject = playerCarryLocation;
	}

	// This handles setting up the package according to where its dropped, so if wood is dropped on the shore then it builds a jetty
	public void UnFollowPlayer(GameObject foundationObject){
		// set the foundation type for the packaged resource instructions
		foundation = foundationObject;
		Foundation foundationScript = foundation.GetComponent<Foundation>();
		payScript.foundationIndex = foundationScript.foundationIndex;// set the foundation index for the package, so that it can be a target
		instructions.foundationType = foundationScript.foundationType;
		instructions.facing = foundationScript.facing;
		follow = false;
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			Debug.Log("Ola Player");
			PlayerController playerScript = col.gameObject.GetComponent<PlayerController>();
			playerScript.nearPackage = true;
		}
	}
}
