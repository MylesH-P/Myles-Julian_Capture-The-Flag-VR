using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour {
    
	NPCMove npc;
	// Use this for initialization
	void Start () {
		npc = transform.parent.GetComponent<NPCMove>();

	}

	private void OnTriggerEnter(Collider other)
	{
		npc.ChaseTrigger(other);
	}

	private void OnTriggerExit(Collider other)
	{
		npc.ChaseEnd(other);
	}
}
