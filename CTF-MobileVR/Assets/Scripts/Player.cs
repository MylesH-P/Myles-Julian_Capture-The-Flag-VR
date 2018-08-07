using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    
    public float speed;
	public Transform _jail;

    [SyncVar]
	public bool _jailed;

	[SyncVar]
	public bool _hasFlag;

	public GameObject _flag;

    public Transform cameraTransform;
    public Transform cameraContainerTransform;
	//public Transform body;

	// Use this for initialization
	public override void OnStartLocalPlayer() {
        
        //we now check to see if the player is local. If it is not then
        //turn off the camera, audiolistener, and script of the other player
        //then return
        //set the speed to a comfortable 2.0
        speed = 2.0f;

        cameraTransform = Camera.main.transform;
        cameraContainerTransform = cameraTransform.parent;

		//transform.position = new Vector3(0, 2, 0);
		_jail = GameObject.FindWithTag("Jail").transform;

        cameraContainerTransform.position = transform.position;
		cameraContainerTransform.parent = transform;
        //cameraContainerTransform.parent = body;
		cameraContainerTransform.localPosition = Vector3.zero;
	}
    

	public void Translate(Vector3 translation)
    {
        
        transform.Translate(translation, Space.World);
    }


	// Update is called once per frame
	void Update () {
		
		if (!isLocalPlayer) {
			return;
		}           
        //if the cardboard button is pressed down move the player forward towards
        //where the user is looking, but do not change the y position value
        if (Input.GetMouseButton(0) && !_jailed)
        {

            //first get the forward vector from the camera
            Vector3 forward = GetComponentInChildren<Camera>().transform.forward;
            //set the y to 0
            forward.y = 0;
            //translate the player gameobject
            gameObject.GetComponent<Player>().Translate(forward * speed * Time.deltaTime);
            
        }
	}
    
	public void SendToJail() {
		_jailed = true;

		if (_hasFlag && _flag != null)
        {
			_flag.GetComponent<CaptureDaFlag>().caught = false;
            _hasFlag = false;
            _flag = null;
        }

		if (_jail == null) {
			_jail = GameObject.FindWithTag("Jail").transform;
		}

		transform.position = _jail.GetChild(3).position;
		transform.rotation = _jail.GetChild(3).rotation;

	    _jail.gameObject.GetComponent<JailManager>().AddPlayerToJail(transform);
	}
}
