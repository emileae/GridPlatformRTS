using UnityEngine;
using System.Collections;

public class PackagedResource : MonoBehaviour {

	private bool follow = false;
	private GameObject followObject;
	private GameObject foundation;

	// Use this for initialization
	void Start () {
	
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

	public void UnFollowPlayer(GameObject foundationObject){
		foundation = foundationObject;
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
