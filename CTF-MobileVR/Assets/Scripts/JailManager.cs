using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailManager : MonoBehaviour {

	public List<Transform> _inJail;
	public Transform _spawn;

	private void Start()
	{
		_inJail = new List<Transform>();
	}

	private void OnTriggerEnter(Collider collision)
	{
		Debug.Log("there");
		Player player = collision.transform.parent.GetComponent<Player>();
		if (player != null && player._jailed == false)
			EmptyJail();
		
	}

	public void EmptyJail() {
		foreach (Transform player in _inJail)
        {
            player.GetComponent<Player>()._jailed = false;
            player.position = _spawn.position;
            player.rotation = _spawn.rotation;
        }
	}

	public void AddPlayerToJail(Transform player) {
		_inJail.Add(player);
	}
}
