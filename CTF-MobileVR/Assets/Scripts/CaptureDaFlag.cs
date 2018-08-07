using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CaptureDaFlag : NetworkBehaviour {
    
	public  Vector3 position;
	public Vector3 rotation;

	[SyncVar(hook = "OnCatchStatusChange")]
	public bool caught;

	private void Start()
	{
		position = transform.position;
		rotation = transform.rotation.eulerAngles;
		caught = false;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.parent.GetComponent<Player>() != null) {
			Capture(other.transform.parent.gameObject);
		}
 	}

	public void Capture(GameObject captor) {
		captor.GetComponent<Player>()._hasFlag = true;
		captor.GetComponent<Player>()._flag = gameObject;
		caught = true;
		transform.parent = captor.transform;
		transform.localPosition = new Vector3(0, 2, 0);
	}
    
	private void Reset()
	{
		transform.parent = null;
		transform.position = position;
		transform.rotation = Quaternion.Euler(rotation);
	}

	void OnCatchStatusChange(bool gotIt) {
		if (gotIt == false) {
			this.Reset();
		}
	}

}
