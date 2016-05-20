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
    [Command]
    public void Cmd_SpawnColors(GameObject go)
    {
        go.GetComponent<NetworkCharacterInfo>().Rpc_SpawnColors();
    }


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
	public void Cmd_SpawnHead(GameObject go, GameObject pos, GameObject p)
	{
		GameObject HeadBall = Instantiate(go, pos.transform.position, Quaternion.identity) as GameObject;
		foreach (Material matt in HeadBall.GetComponent<Renderer>().materials)
		{
			if (matt.name == "Armor (Instance)")
			{
				Debug.Log("I'm here");
				matt.color = p.GetComponent<NetworkCharacterInfo>().color;
			}
		}
//		HeadBall.GetComponent<Renderer> ().material.mainTexture = go.GetComponent<PlayerInfo>().bodyparts [0].GetComponent<Renderer> ().material.mainTexture;
		NetworkServer.Spawn(HeadBall);
	}
}
