using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winning : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<CaptureDaFlag>() != null) {
			GameObject.Find("Event Board").GetComponent<EventBoard>().ReceiveMessage("You Win!");
			other.GetComponent<CaptureDaFlag>().caught = false;
		}
	}
}
