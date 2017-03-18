using UnityEngine;
using System.Collections;

public class Foundation : MonoBehaviour {

	public int foundationType = 0;
	public int foundationIndex = 0;

	[Range(-1,1)]
	public int facing = 0;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.CompareTag ("Resource")) {
			Debug.Log ("Resource over the foundation...");
			NPCInstructions instructions = col.gameObject.GetComponent<NPCInstructions>();
			instructions.foundationType = foundationType;
		}
		if (col.CompareTag ("Player")) {
			Debug.Log ("Player over the foundation...");
			PlayerController playerScript = col.gameObject.GetComponent<PlayerController>();
			playerScript.foundation = gameObject;
//			PlayerController playerScript = col.gameObject.GetComponent<PlayerController> ();
//			if (playerScript.carryingPackage) {
//				Debug.Log("The Player is carrying a package");
//			}
		}

		if (col.CompareTag ("NPC")) {
			Debug.Log ("NPC over the foundation...");
			StatePatternNPC npcScript = col.gameObject.GetComponent<StatePatternNPC>();
			npcScript.foundationIndex = foundationIndex;
		}

	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.CompareTag ("Resource")) {
			Debug.Log("This is a resource, so indicate that its leaving a foundation");
			NPCInstructions instructions = col.gameObject.GetComponent<NPCInstructions>();
			instructions.ResetFoundationType();
		}
	}

}
