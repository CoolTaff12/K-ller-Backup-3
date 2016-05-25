using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class AssignPlayerInfo : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		Physics.IgnoreLayerCollision (0, 10);
		Physics.IgnoreLayerCollision (11, 10);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Command to kill a given player.
	/// </summary>
	/// <param name="go">The Player GameObject</param>
    [Command]
	public void Cmd_KillAPlayer(GameObject go)
	{
        Debug.Log("CmD_KillAPlayer");
        go.GetComponent<PlayerInfo> ().Rpc_KillYourself ();
    }
	/// <summary>
	/// Request spawning of a ball when a player character dies.
	/// </summary>
	/// <param name="go">Prefab of the object that should spawn.</param>
	/// <param name="pos">spawnposition</param>
	/// <param name="p">Player that dies</param>
	[Command]
	public void Cmd_SpawnHead(GameObject go, GameObject pos, GameObject p, Color C)
	{
		GameObject HeadBall = Instantiate(go, pos.transform.position, Quaternion.identity) as GameObject;
		foreach (Material matt in HeadBall.GetComponent<Renderer>().materials)
		{
			if (matt.name == "Armor (Instance)")
			{
				Debug.Log("I'm here");
				matt.color = C;
			}
		}
		NetworkServer.Spawn(HeadBall);
	}
}
