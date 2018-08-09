using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EventBoard : NetworkBehaviour {

	[SyncVar(hook = "OnEvent")]
	string evt = "";


	public void ReceiveMessage(string str) {
		evt = str;
	}

	void OnEvent(string str) {
		if (str != "") {
			Camera.main.transform.GetComponentInChildren<Text>().text = str;
            Camera.main.transform.GetComponentInParent<Player>().Reset();
		}
		evt = "";
	} 
}
