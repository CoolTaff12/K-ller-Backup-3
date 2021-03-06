﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DodgeBallBehaviour : NetworkBehaviour {
	[SerializeField]
	private AudioClip[] audioClips = null;
	[SerializeField]
	private AudioClip[] HitAudioClips;
	private GrabAndToss gat;
	private Rigidbody rb;
	private Collider coll;
	[SyncVar][SerializeField]
	private int thrownByTeam = 1;
	[HideInInspector]
	public int b_ThrownByTeam{get{return thrownByTeam;}}
	[SerializeField]
	private GameObject Sparks = null;
	[SerializeField]
	private Transform myTransform;
	[SerializeField]
	private bool pickedUp;
	[HideInInspector]
	public bool b_PickedUp{get{return pickedUp;}}
	private GameObject currentPlayer;
	[SyncVar][SerializeField]
	private int bounces = 10;
	[SerializeField]
	private int bouncesToReset = 10;
    [SyncVar][SerializeField]
    private GameObject ParticlesAwareness;

	// Use this for initialization
	void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody> ();
		coll = gameObject.GetComponent<SphereCollider> ();
		Physics.IgnoreLayerCollision (11, 10);
		if (!isLocalPlayer) {
			return;
		}
		}

	// Update is called once per frame;
	void Update ()
	{
		if (pickedUp) {
			gameObject.transform.position = gat.c_HoldPos.transform.position;
			gameObject.transform.rotation = gat.c_FPC.transform.rotation;
		}
		if (bounces <= 0) {
			thrownByTeam = 0;
		}
        if(gameObject.transform.position.y < -2.0f)
        {
            GameObject.Find("OoB with Script").GetComponent<OutofBound>().Teleport(gameObject);
        }

	}
	//-----------------Play Audio------------------------
	//This will take the gameobjects AudioSource to switch the audioclips
	public void PlaySound(int clip)
	{
		GetComponent<AudioSource>().clip = audioClips[clip];
		GetComponent<AudioSource>().Play();
	}

	void OnCollisionEnter(Collision col)
	{
		bounces--;

		if (col.gameObject.tag == "ForceField")
		{
			GameObject Sparked = (GameObject) Instantiate(Sparks, transform.position, Quaternion.identity);
			Destroy(Sparked, 3f);
		}
		else if (col.gameObject.tag != "Player" || col.gameObject.tag != "ForceField")
		{
			PlaySound(0);
		}

	}
	/// <summary>
	/// Rpc for ball to get picked up.
	/// </summary>
	/// <param name="go">Player character picking up the ball</param>
	[ClientRpc]
	public void Rpc_GetPickedUp (GameObject go){
		coll.enabled = false;
		rb.detectCollisions = false;
		rb.isKinematic = true;
		currentPlayer = go;
		gat = currentPlayer.GetComponent<GrabAndToss> ();
		pickedUp = true;
        ParticlesAwareness.SetActive(false);
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;

	
	}
	/// <summary>
	/// Rpc for setting the ball in motion, Shooting/Tossing the ball.
	/// </summary>
	[ClientRpc]
	public void Rpc_Shoot (float force){
		pickedUp = false;
		gameObject.transform.position = gat.c_ThrowFrom.transform.position;
		bounces = bouncesToReset;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.isKinematic = false;
		thrownByTeam = currentPlayer.GetComponent<NetworkCharacterInfo> ().teamNumber;
		coll.enabled = true;
		rb.detectCollisions = true;
		rb.AddForce(gat.c_Head.transform.forward * force);
		gat = null;
        ParticlesAwareness.SetActive(true);
	}

}
