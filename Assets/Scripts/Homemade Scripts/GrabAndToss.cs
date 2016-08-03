using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class GrabAndToss : NetworkBehaviour
{

	RaycastHit hit;
	[SerializeField]
	private float rayDistance = 4f; //Length of Ray. Default set to 5.
	[SerializeField]
	private float rayRadius = 0.75f; //Radius of Ray. Default set to 0.75
	[SerializeField]
	private float tossForce = 20f; //Force added to ball when tossed. Default set to 20;
	public float c_TossForce{get{return tossForce;}set{tossForce = value;}}
	[SyncVar][SerializeField]
	private bool holdingBall; //Is this character holding a ball?
	[HideInInspector]
	public bool c_HoldingBall{get{return holdingBall;}}
	private bool throwing = false;
	private DodgeBallBehaviour ballScript; //Script on the ball hit by the players' raycast.
	private AssignPlayerInfo assignInfo; //Script to set initial info such as teamNumber.
	private Animator anim;//Animator attached to the player.
	[SerializeField]
	private GameObject fpc = null; //FirstPersonController connected to the player.
	public GameObject c_FPC {get{return fpc;}}
	[SerializeField]
	private GameObject head  = null; //Head of the player;
	public GameObject c_Head {get{return head;}}
	private GameObject currentBall; //Ball that is currently being held.
	public GameObject c_CurrentBall{get{return currentBall;}}
	[SerializeField]
	private GameObject holdPos  = null; //Position of the held ball.
	public GameObject c_HoldPos{get{return holdPos;}}
	[SerializeField]
	private GameObject throwFrom  = null; //Position of the held ball.
	public GameObject c_ThrowFrom{get{return throwFrom;}}

	private PlayerInfo playerInfo;
	private NetworkCharacterInfo charInfo;

    public AudioClip[] audioClips = null;

    //-----------------Play Audio------------------------
    //This will take the gameobjects AudioSource to switch the audioclips
    public void PlaySound(int clip)
    {
        gameObject.transform.FindChild("FirstPersonCharacter").GetComponent<AudioSource>().clip = audioClips[clip];
        gameObject.transform.FindChild("FirstPersonCharacter").GetComponent<AudioSource>().Play();
    }


    // Use this for initialization
    void Start ()
	{
		anim = GetComponent<Animator>();
		playerInfo = gameObject.GetComponent<PlayerInfo> ();
    }



    // Update is called once per frame
    void Update ()
	{

		//Is the player looking at a ball?
		if (Physics.SphereCast (c_Head.transform.position, rayRadius, c_Head.transform.forward, out hit, rayDistance)) {
			if (!isLocalPlayer) {
				return;
			}
			if (hit.collider.GetComponent<DodgeBallBehaviour> () != null) {
				//Pick up the ball
				if (CrossPlatformInputManager.GetButton ("Fire1") && !holdingBall && !playerInfo.c_Dead) {
					if (!hit.collider.GetComponent<DodgeBallBehaviour> ().b_PickedUp) {
					currentBall = hit.collider.gameObject;
					Cmd_GetPickedUp (currentBall , gameObject);
					holdingBall = true;

					}

				}
			}
		}
//--THROW BALL--//
		if (Input.GetButton ("Fire2")) {
			if (!isLocalPlayer) {
				return;
			}
			if (!holdingBall) {
				anim.SetBool ("isThrowing", false);
				return;
			}
			if (!throwing && holdingBall) {
				throwing = true;
				anim.SetBool ("isThrowing", true);
			} 
			holdingBall = false;
			Cmd_Shoot (currentBall, tossForce);
			currentBall = null;
			ballScript = null;

		}
		if (Input.GetButtonUp ("Fire2") && throwing) {
			throwing = false;
			anim.SetBool ("isThrowing", false);
		}

	}
		/// <summary>
		/// Command that requests that the ball will shoot/get tossed.
		/// </summary>
		/// <param name="bs">Script attached to the ball</param>
		/// <param name="dir">What direction will the ball be tossed </param>
	[Command]
	public void Cmd_Shoot(GameObject bs, float force){
		ballScript = bs.GetComponent<DodgeBallBehaviour> ();
		ballScript.Rpc_Shoot (force);
        PlaySound(Random.Range(0, 2));
    }
	/// <summary>
	/// Command that requests the ball to get picked up.
	/// </summary>
	/// <param name="bs">Script attached to the ball</param>
	/// <param name="go">The ball that is affected</param>
	[Command]
	void Cmd_GetPickedUp(GameObject bs, GameObject go){
		ballScript = bs.GetComponent<DodgeBallBehaviour> ();
		ballScript.Rpc_GetPickedUp (go);
	}
}
