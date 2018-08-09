using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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
	public Transform body;

    public Transform canvas;

	public float _boostTimer = 0f;
	private float _multiplier;
	private float _resetCounter;
	private float _miscCounter;
	private IEnumerator _coroutine;
	// Use this for initialization
	public override void OnStartLocalPlayer() {
        
        //we now check to see if the player is local. If it is not then
        //turn off the camera, audiolistener, and script of the other player
        //then return
        //set the speed to a comfortable 2.0
        speed = 5.0f;

        cameraTransform = Camera.main.transform;
        cameraContainerTransform = cameraTransform.parent;
        canvas = transform.GetChild(1);

        Debug.Log(canvas.ToString());
		//transform.position = new Vector3(0, 2, 0);
		_jail = GameObject.FindWithTag("Jail").transform;

        canvas.gameObject.SetActive(true);
        canvas.parent = cameraTransform;
        cameraContainerTransform.position = transform.position;
		cameraContainerTransform.parent = transform;
        cameraContainerTransform.parent = body;
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
		//transform.position = body.transform.position;

        if (Input.GetMouseButton(0) && !_jailed)
        {

            //first get the forward vector from the camera
            Vector3 forward = GetComponentInChildren<Camera>().transform.forward;
            //set the y to 0
            forward.y = 0;
            //translate the player gameobject
            gameObject.GetComponent<Player>().Translate(forward * speed * Time.deltaTime);
            
        }


		if (_boostTimer > 0) {
			_boostTimer -= Time.deltaTime;
			if (_boostTimer <=0 ) {
				_boostTimer = 0;
                speed /= _multiplier;
			}
		}
	}
    
	public void SendToJail() {
		_jailed = true;

		_coroutine = ClearMessage();
		StartCoroutine(_coroutine);
		transform.GetComponentInChildren<Text>().text = "You were tagged";

        
		if (_hasFlag && _flag != null)
        {
			_flag.GetComponent<CaptureDaFlag>().caught = false;
            _hasFlag = false;
            _flag = null;
        }

		if (_jail == null) {
			_jail = GameObject.FindWithTag("Jail").transform;
		}

		body.transform.position = _jail.GetChild(3).position;
		body.transform.rotation = _jail.GetChild(3).rotation;

	    _jail.gameObject.GetComponent<JailManager>().AddPlayerToJail(transform);
	}

	public void Boost(float mult, float length) {
		if (_boostTimer <= 0)
		{
			speed *= mult;
			_multiplier = mult;
		}

		_boostTimer += length;
	}

	public void Bounce() {
		Rigidbody rb = transform.GetComponentInChildren<Rigidbody>();
		rb.AddForce(Vector3.up * 1000 + cameraTransform.forward * 500);
	}

	public void Reset()
	{
		_coroutine = ResetAfterMatch();
		StartCoroutine(_coroutine);
	}
    
    IEnumerator ClearMessage()
	{
		_miscCounter = 0;
		while (_miscCounter < 5.0f) {
			_miscCounter += Time.deltaTime;
			yield return null;
		}
		Text text = transform.GetComponentInChildren<Text>();
        if (text != null)
			text.text = "";
        EndCoroutine();
	}

	IEnumerator ResetAfterMatch() {
		_resetCounter = 0;
		while (_resetCounter < 10.0f) {
			_resetCounter += Time.deltaTime;
			yield return null;
		}
		transform.GetChild(0).position = GameObject.Find("Blue Player Spawn1").transform.position + Vector3.up * 5;
		_jailed = false;
		Text text = transform.GetComponentInChildren<Text>();
        if (text != null)
			text.text = "";
		EndCoroutine();
	}

	void EndCoroutine() {
		StopCoroutine(_coroutine);
	}

	public void CatchHandle() {
		_coroutine = ClearMessage();
        StartCoroutine(_coroutine);
		Text text = transform.GetComponentInChildren<Text>();
        if (text != null)
            text.text = "You have the flag!";
	}

	public void Warn() {
		_coroutine = ClearMessage();
		StartCoroutine(_coroutine);
		Text text = transform.GetComponentInChildren<Text>();
        if (text != null)
		   text.text = "You are being chased!";

	}

}
