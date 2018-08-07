using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cpuTackle : MonoBehaviour {

	private void OnTriggerEnter(Collider collision)
	{
		Player player = collision.transform.parent.GetComponent<Player>();
		if (player != null) {
			player.SendToJail();
		}
	}
}
