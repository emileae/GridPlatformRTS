using UnityEngine;
using System.Collections;

public class BuildState : INPCState {

	private readonly StatePatternNPC npc;

	private bool building = false;
	private NPCInstructions instructions;

	public BuildState (StatePatternNPC statePatternNPC)
	{
		npc = statePatternNPC;
	}

	public void UpdateState ()
	{
		npc.busy = true;
		Debug.Log("Is building?!?!?!?!?");
		if (!building) {
			Build ();
		}

		if (npc.toIdle) {
			ToIdleState();
			building = false;
		}
	}

	public void OnTriggerEnter2D(Collider2D col){
	}

	public void ToIdleState(){
		npc.currentState = npc.idleState;
	}

	public void ToGoState(){
	}

	public void ToGetInstructionsState (){
	}

	public void ToProcessState (){
	}

	public void ToBuildState(){
	}

	public void ToFishState(){
	}

	public void ToOffloadState(){
	}

	void Build(){
		building = true;
		// target here is the package
		instructions = npc.target.GetComponent<NPCInstructions>();
		Debug.Log("Got instructions..... " + instructions.facing);
		npc.Build(instructions);
	}
}
