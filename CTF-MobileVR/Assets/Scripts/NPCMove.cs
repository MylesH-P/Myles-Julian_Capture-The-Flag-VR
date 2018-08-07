﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class NPCMove : NetworkBehaviour {

	[SerializeField]
	Transform[] _destination;
	private int numDestinations;
	private int cur;
	private Transform target;

	private bool chasing;

	NavMeshAgent _navMeshAgent;


	private void Start()
	{
		_navMeshAgent = GetComponentInChildren<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.LogError("this isn't a nav mesh agent");
        }
        else
        {
            SetDestination();
        }

        numDestinations = _destination.Length;
        cur = 0;
        chasing = false;
        target = null;
	}

	private void Update()
    {
		if (_navMeshAgent.remainingDistance < 0.5f && !chasing)
        {
            _navMeshAgent.SetDestination(_destination[(cur++) % numDestinations].transform.position);
        }

		if (chasing) {
			NavMeshPath path = new NavMeshPath();
			_navMeshAgent.CalculatePath(target.position, path);
			if (path.status != NavMeshPathStatus.PathComplete)
			{
				chasing = false;
				_navMeshAgent.SetDestination(_destination[(cur++) % numDestinations].transform.position);
			} else {
				_navMeshAgent.SetPath(path);
			}
		}


    }

	public void SetDestination() {
		if (_destination != null) {
			Vector3 pos = _destination[cur++].transform.position;
			_navMeshAgent.SetDestination(pos);
		}
	}



	public void ChaseTrigger(Collider col)
	{
		GameObject other = col.gameObject;
		Player game = other.transform.parent.GetComponent<Player>();
		if (game != null) {
			chasing = true;
			_navMeshAgent.SetDestination(other.transform.position);
			target = other.transform;
		}
	}
    
	public void ChaseEnd(Collider other)
	{
		if (target == other.transform) {
			chasing = false;
			_navMeshAgent.SetDestination(_destination[(cur++) % numDestinations].transform.position);
		}
	}
}
