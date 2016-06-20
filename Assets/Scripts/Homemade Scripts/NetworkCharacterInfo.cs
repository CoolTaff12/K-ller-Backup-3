using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;



[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class NetworkCharacterInfo : NetworkBehaviour
{

    //Network syncvar
    [SyncVar]
    public Color color;
    [SyncVar]
    public string playerName;
    [SyncVar]
    public int teamNumber;

    [SyncVar]
    public int checkingTexture = 6;
    public int checkingPlayers;

    public GameObject[] ChoosenMaterials;
    [SerializeField]
    private GameObject Victory;
    [SerializeField]
    private GameObject Draw;
    [SerializeField]
    private List<GameObject> TeamPlayers;
    [SerializeField]
    private List<int> PlayerNumber;

    public NetworkLobbyHook NLH;
    public UnityStandardAssets.Network.LobbyManager LM;

    public AudioClip[] audioClips;

    protected bool CheckedPlayers = false;

    /// <summary>
    /// This will take the gameobjects AudioSource to switch the audioclips-
    /// </summary>
    /// <param name="clip">the number in the Audioclip</param>
    public void PlaySound(int clip)
    {
        gameObject.transform.FindChild("FirstPersonCharacter").GetComponent<AudioSource>().clip = audioClips[clip];
        gameObject.transform.FindChild("FirstPersonCharacter").GetComponent<AudioSource>().Play();
    }

	/// <summary>
	/// Creates new GameObject lists for the script to register on Awake.
	/// </summary>
    void Awake()
    {
        TeamPlayers = new List<GameObject>();
    }

    // Use this for initialization
    void Start ()
    {
        NLH = GameObject.Find("LobbyManager").GetComponent<NetworkLobbyHook>();
        LM = GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>();
        //Tells the player what its name is
        checkingPlayers = LM.PlayersOnline.Count;
        gameObject.name = playerName;
        //Renderar the colour and the texture player had choosen ealier
    }

	/// <summary>
	/// Since checkingPlayers have the same Count as LobbyManagers PlayeOnline list, this
	/// will check add every new game object tagged "Player" and add them to TeamPlayerslist
	/// </summary>
	/// <param name="NewPlayer">The Player GameObject</param>
    void Update()
    {   
        if (checkingPlayers > 0)
        {
            GameObject[] AvalibleEntries = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject NewPlayer in AvalibleEntries)
            {
                if(TeamPlayers.Contains(NewPlayer))
                {

                }
                else
                {
                    TeamPlayers.Add(NewPlayer);
                    CheckAvailablePlayers(NewPlayer);
                    checkingPlayers--;
                }
            }
        }
    }

	/// <summary>
	/// Command the players to find the right material by name and change it by it's color
	/// </summary>
	/// <param name="matt">The Player Material</param>
    private void Rpc_SpawnColors()
    {
        for (int i = 0; i < ChoosenMaterials.Length; i++)
        {
            foreach (Material matt in ChoosenMaterials[i].GetComponent<Renderer>().materials)
            {
                if (matt.name == "Armor2 (Instance)" || matt.name == "Armor2")
                {
                    Debug.Log("I'm here");
                    matt.color = color;
                    checkingTexture--;
                }
            }
        }
    }

	/// <summary>
	/// Tells the method to start spawn the players color
	/// </summary>
    public void Cmd_SpawnColors()
    {
        Debug.Log("Colour set");
        Rpc_SpawnColors();
    }

	/// <summary>
	/// Adding player from TeamPLayers list to a sepret list based on his teamNumber
	/// </summary>
	/// <param name="GnT">The Player GameObject</param>
    public  void CheckAvailablePlayers(GameObject GnT)
    {
        Debug.Log("Passed here again");
        GnT.GetComponent<NetworkCharacterInfo>().Cmd_SpawnColors();
        GnT.GetComponent<PlayerInfo>().playersColor = GnT.GetComponent<NetworkCharacterInfo>().color;
        PlayerNumber[(GnT.GetComponent<NetworkCharacterInfo>().teamNumber - 1)]++;
    }

	/// <summary>
	/// If the player has been hit and is dead, it wil remove the player from the list.
	/// It will then take the number away from the player and check the current teamstatus.
	/// </summary>
	/// <param name="isc_Dead">The Player GameObject</param>
    public void CheckingList(GameObject isc_Dead)
    {
        if (TeamPlayers.Contains(isc_Dead))
        {
            Debug.Log("isc_Dead name is " + isc_Dead);
            Debug.Log("isc_Dead team is " + isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber);
            PlayerNumber[(isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber - 1)]--;
            isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber = 0;
            CheckforTeamStatus();
            TeamPlayers.Remove(isc_Dead);
        }
            
    }

	/// <summary>
	/// Checks with the current team status, who won and if its a draw.
	/// </summary>
	/// <param name="Winner">The Player GameObjects who won</param>
	/// <param name="Draw">The Player GameObjects who have both lost</param>
    private void CheckforTeamStatus()
    {
        Debug.Log("Here I go again on my own");
        if (PlayerNumber[0] != 0 && PlayerNumber[1] == 0 && PlayerNumber[2] == 0 && PlayerNumber[3] == 0 && PlayerNumber[4] == 0 &&
            PlayerNumber[5] == 0 && PlayerNumber[6] == 0 && PlayerNumber[7] == 0 && PlayerNumber[8] == 0 && PlayerNumber[9] == 0)
        {
            Debug.Log("Victory for team 1");
            foreach (GameObject Winners in TeamPlayers)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (PlayerNumber[0] == 0 && PlayerNumber[1] != 0 && PlayerNumber[2] == 0 && PlayerNumber[3] == 0 && PlayerNumber[4] == 0 &&
          PlayerNumber[5] == 0 && PlayerNumber[6] == 0 && PlayerNumber[7] == 0 && PlayerNumber[8] == 0 && PlayerNumber[9] == 0)
        {
            Debug.Log("Victory for team 2");
            foreach (GameObject Winners in TeamPlayers)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (PlayerNumber[0] == 0 && PlayerNumber[1] == 0 && PlayerNumber[2] != 0 && PlayerNumber[3] == 0 && PlayerNumber[4] == 0 &&
          PlayerNumber[5] == 0 && PlayerNumber[6] == 0 && PlayerNumber[7] == 0 && PlayerNumber[8] == 0 && PlayerNumber[9] == 0)
        {
            Debug.Log("Victory for team 3");
            foreach (GameObject Winners in TeamPlayers)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (PlayerNumber[0] == 0 && PlayerNumber[1] == 0 && PlayerNumber[2] == 0 && PlayerNumber[3] != 0 && PlayerNumber[4] == 0 &&
          PlayerNumber[5] == 0 && PlayerNumber[6] == 0 && PlayerNumber[7] == 0 && PlayerNumber[8] == 0 && PlayerNumber[9] == 0)
        {
            Debug.Log("Victory for team 4");
            foreach (GameObject Winners in TeamPlayers)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (PlayerNumber[0] == 0 && PlayerNumber[1] == 0 && PlayerNumber[2] == 0 && PlayerNumber[3] == 0 && PlayerNumber[4] != 0 &&
          PlayerNumber[5] == 0 && PlayerNumber[6] == 0 && PlayerNumber[7] == 0 && PlayerNumber[8] == 0 && PlayerNumber[9] == 0)
        {
            Debug.Log("Victory for team 5");
            foreach (GameObject Winners in TeamPlayers)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (PlayerNumber[0] == 0 && PlayerNumber[1] == 0 && PlayerNumber[2] == 0 && PlayerNumber[3] == 0 && PlayerNumber[4] == 0 &&
          PlayerNumber[5] != 0 && PlayerNumber[6] == 0 && PlayerNumber[7] == 0 && PlayerNumber[8] == 0 && PlayerNumber[9] == 0)
        {
            Debug.Log("Victory for team 6");
            foreach (GameObject Winners in TeamPlayers)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (PlayerNumber[0] == 0 && PlayerNumber[1] == 0 && PlayerNumber[2] == 0 && PlayerNumber[3] == 0 && PlayerNumber[4] == 0 &&
          PlayerNumber[5] == 0 && PlayerNumber[6] != 0 && PlayerNumber[7] == 0 && PlayerNumber[8] == 0 && PlayerNumber[9] == 0)
        {
            Debug.Log("Victory for team 7");
            foreach (GameObject Winners in TeamPlayers)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (PlayerNumber[0] == 0 && PlayerNumber[1] == 0 && PlayerNumber[2] == 0 && PlayerNumber[3] == 0 && PlayerNumber[4] == 0 &&
          PlayerNumber[5] == 0 && PlayerNumber[6] == 0 && PlayerNumber[7] != 0 && PlayerNumber[8] == 0 && PlayerNumber[9] == 0)
        {
            Debug.Log("Victory for team 8");
            foreach (GameObject Winners in TeamPlayers)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (PlayerNumber[0] == 0 && PlayerNumber[1] == 0 && PlayerNumber[2] == 0 && PlayerNumber[3] == 0 && PlayerNumber[4] == 0 &&
          PlayerNumber[5] == 0 && PlayerNumber[6] == 0 && PlayerNumber[7] == 0 && PlayerNumber[8] != 0 && PlayerNumber[9] == 0)
        {
            Debug.Log("Victory for team 9");
            foreach (GameObject Winners in TeamPlayers)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (PlayerNumber[0] == 0 && PlayerNumber[1] == 0 && PlayerNumber[2] == 0 && PlayerNumber[3] == 0 && PlayerNumber[4] == 0 &&
          PlayerNumber[5] == 0 && PlayerNumber[6] == 0 && PlayerNumber[7] == 0 && PlayerNumber[8] == 0 && PlayerNumber[9] != 0)
        {
            Debug.Log("Victory for team 10");
            foreach (GameObject Winners in TeamPlayers)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (PlayerNumber[0] == 0 && PlayerNumber[1] == 0 && PlayerNumber[2] == 0 && PlayerNumber[3] == 0 && PlayerNumber[4] == 0 &&
         PlayerNumber[5] == 0 && PlayerNumber[6] == 0 && PlayerNumber[7] == 0 && PlayerNumber[8] == 0 && PlayerNumber[9] == 0)
        {
            foreach (GameObject Draw in TeamPlayers)
            {
                Draw.GetComponent<NetworkCharacterInfo>().Draw.SetActive(true);
                Draw.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Draw.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
                Draw.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
    }

    /// <summary>
    /// Calls client with the gameObject that is dead.
    /// </summary>
    /// <param name="go">The Player GameObjects who have died</param>
    [ClientRpc]
    public void Rpc_CheckingList(GameObject go) {
        CheckingList(go);
    }
}
