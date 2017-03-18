using UnityEngine;
using System.Collections;
using UnityEditor;

public class StatePatternNPC : MonoBehaviour {

	public Board blackboard;

	public bool toIdle = false;

	public bool busy = false;

	public Transform target;
	public NPCMove moveController;
	public bool arrived = false;
	public int foundationIndex;// current foundation the NPC is standing on
	public int platform = 0;// the platform NPC is standing on

	[HideInInspector] public INPCState currentState;
	[HideInInspector] public IdleState idleState;
	[HideInInspector] public GetInstructionsState getInstructionsState;
	[HideInInspector] public ProcessState processState;
	[HideInInspector] public BuildState buildState;
	[HideInInspector] public FishState fishState;
	[HideInInspector] public GoState goState;
	[HideInInspector] public OffLoadState offLoadState;

	private void Awake(){
		idleState = new IdleState(this);
		getInstructionsState = new GetInstructionsState(this);
		processState = new ProcessState(this);
		buildState = new BuildState(this);
		fishState = new FishState(this);
		goState = new GoState(this);
		offLoadState = new OffLoadState(this);
	}

	// Use this for initialization
	void Start ()
	{
		if (blackboard == null) {
			blackboard = GameObject.Find("Blackboard").GetComponent<Board>();
		}
		moveController = GetComponent<NPCMove>();
		currentState = idleState;
	}
	
	// Update is called once per frame
	void Update () {
		currentState.UpdateState();
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		currentState.OnTriggerEnter2D(col);
	}

	public void DeactivateTarget (){
		// remove the target form the worklist so that no more NPCs are called to a target that isn't active
		blackboard.RemoveFromWorkList(target.gameObject);
		target.gameObject.SetActive(false);
		target = null;
	}

	public void PlayProcessAnimation (int resourceType){
		Debug.Log ("Play the necessary animations for type: " + resourceType);
	}

	public void CreateProcessedResource (int resourceType){
		Debug.Log ("Instantiate a package for type: " + resourceType);
		GameObject prefab = blackboard.GetProcessedPrefab(resourceType);
		GameObject package = Instantiate(prefab, target.position, Quaternion.identity) as GameObject;
		PayTarget payScript = package.GetComponent<PayTarget>();
		payScript.foundationIndex = target.GetComponent<PayTarget>().foundationIndex;
	}

	public void Build(NPCInstructions instructions){
		Debug.Log ("Play the necessary animations for building: ");
		Debug.Log ("Building with resource: " + instructions.resourceType + " and foundation: " + instructions.foundationType);
		GameObject prefab = blackboard.GetBuildingPrefab(instructions.resourceType, instructions.foundationType, platform);
		StartCoroutine(BuildStructure(prefab, instructions));
	}

	IEnumerator BuildStructure (GameObject prefab, NPCInstructions instructions)
	{
		yield return new WaitForSeconds (prefab.GetComponent<Structure> ().buildTime);
		GameObject instantiatedPrefab = Instantiate(prefab, target.position, Quaternion.identity) as GameObject;// cast it as a gameobject... otherwise seems to be trnasform.position bugs

		Debug.Log("Direction facing??? " + instructions.facing);

		// TODO: clean this up... all models need to face left, then only flip if they're facing right... might want to make this more flexible
		if (instructions.facing == 1) {
			Vector3 theScale = instantiatedPrefab.transform.localScale;
			theScale.x *= -1;
			instantiatedPrefab.transform.localScale = theScale;
		}
		Debug.Log("Structure located at: " + instantiatedPrefab.transform.position);

		// deactivate the current package, since its now been replaced with the new building
		DeactivateTarget();

		toIdle = true;
		// activate the structure's functionality
		Structure structureScript = instantiatedPrefab.GetComponent<Structure>();
		structureScript.Activate();

	}

}
