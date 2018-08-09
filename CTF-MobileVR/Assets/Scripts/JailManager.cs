using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JailManager : NetworkBehaviour {

	public List<Transform> _inJail;

	[SyncVar(hook = "OnMoreJail")]
	public int jailCount;

	public Transform _spawn;

	private void Start()
	{
		_inJail = new List<Transform>();
	}

	private void OnTriggerEnter(Collider collision)
	{
		Player player = collision.transform.parent.GetComponent<Player>();
		if (player != null && player._jailed == false)
			EmptyJail();
		
	}

	public void EmptyJail() {
		foreach (Transform player in _inJail)
        {
            player.GetComponent<Player>()._jailed = false;
			player.GetComponent<Player>().body.position = _spawn.position;
			player.GetComponent<Player>().body.rotation = _spawn.rotation;
        }

		_inJail.Clear();
	}

	public void AddPlayerToJail(Transform player) {
		_inJail.Add(player);
		CmdAddToJail();

	}

    [Command]
	void CmdAddToJail() {
		jailCount += 1;
	}

	void OnMoreJail (int prisoners) {
		int count = GameObject.FindGameObjectsWithTag("Player").Length;
        if (prisoners == count)
        {
            GameObject.Find("Event Board").GetComponent<EventBoard>().ReceiveMessage("You Lose! :(");
            foreach (Transform perp in _inJail)
            {
                perp.GetComponent<Player>().Reset();
            }
            _inJail.Clear();
			jailCount = 0;
        }
	}
}
