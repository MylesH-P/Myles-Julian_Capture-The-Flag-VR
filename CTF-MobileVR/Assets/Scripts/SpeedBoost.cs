using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {

	public float multiplier = 3.0f;
	public float length = 3.0f;
	private void OnTriggerEnter(Collider other)
	{
		
		Player player = other.transform.parent.GetComponent<Player>();
		NPCMove npc = other.GetComponent<NPCMove>();
		if (player != null) {
			player.Boost(multiplier, length);
		} else if (npc != null) {
			npc.Boost(multiplier, length);
		}
	}


}
