using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
    {

        Player player = other.transform.parent.GetComponent<Player>();
        if (player != null)
        {
            player.Bounce();
        }
    }
}
