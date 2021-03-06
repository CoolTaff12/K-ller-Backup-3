﻿using UnityEngine;
using UnityStandardAssets.Network;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook
{
    public bool isinLevel = false;
    public bool showResults = false;
    public GameObject DodgeballSpawner;
    public GameObject OurPlayer;

    public LobbyManager LM;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController MouseLocking;

    /// <summary>
	/// When LobbySever loads the scene, the player object who gets spawn recive the values and name from the players own LobbyPlayer.
	/// </summary>
    /// <param name="manager">The NetworkManager</param>
    /// <param name="lobbyPlayer">the lobbyPlayer that gives out values to NetworkCharacterInfo</param>
    /// <param name="gameplayer">Makes Ourplayer the same as the selected gamePlayer</param>
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        if (gamePlayer.GetComponent<NetworkCharacterInfo>())
        {
            OurPlayer = gamePlayer;
            MouseLocking = OurPlayer.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
            NetworkCharacterInfo characterInfo = gamePlayer.GetComponent<NetworkCharacterInfo>();
            

            characterInfo.playerName = lobby.name;
            characterInfo.name = characterInfo.playerName;
            characterInfo.color = lobby.playerColor;
            characterInfo.teamNumber = (lobby.setTeamNumber + 1);
            characterInfo.gameObject.GetComponent<NetworkCharacterInfo>().teamNumber = (lobby.setTeamNumber + 1);
            isinLevel = true;
        }
    }

    /// <summary>
    /// If the player presses Escape and expected to exit the game
    /// </summary>
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && showResults == false)
        {
            if(OurPlayer != null)
            {
                LM.Exiting(MouseLocking, true);
            }
        }
        else if(Input.GetKey(KeyCode.Escape) && showResults == false && isinLevel == false)
        {
            LM.GoBackButton();
        }
    }


    /// <summary>
    /// This turns bools to false and commands its own LobbyManager to exit the match
    /// </summary>
    /// <param name="waitTime">Float that waits before it beings to run</param>
    public IEnumerator GoBacktoLobby(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isinLevel = false;
        showResults = false;
        LM.Exiting(MouseLocking, true);
    }

    
}
