﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour {

	[SerializeField]
	Transform _destination;

	NavMeshAgent _navMeshAgent;
    
	// Use this for initialization
	void Start () {
		_navMeshAgent = GetComponent<NavMeshAgent>();

		if (_navMeshAgent == null) {
			Debug.LogError("this isn't a nav mesh agent");
		} else {
			SetDestination();
		}
	}

	public void SetDestination() {
		if (_destination != null) {
			Vector3 pos = _destination.transform.position;
			_navMeshAgent.SetDestination(pos);
		}
	}
}