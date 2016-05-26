using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;



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
    public List<GameObject> Team1;
    public List<GameObject> Team2;
    public List<GameObject> Team3;
    public List<GameObject> Team4;
    public List<GameObject> Team5;
    public List<GameObject> Team6;
    public List<GameObject> Team7;
    public List<GameObject> Team8;
    public List<GameObject> Team9;
    public List<GameObject> Team10;
    [SerializeField]
    private GameObject Victory;
    [SerializeField]
    private GameObject Draw;
    [SerializeField]
    private List<GameObject> TeamPlayers;

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
        Team1 = new List<GameObject>();
        Team2 = new List<GameObject>();
        Team3 = new List<GameObject>();
        Team4 = new List<GameObject>();
        Team5 = new List<GameObject>();
        Team6 = new List<GameObject>();
        Team7 = new List<GameObject>();
        Team8 = new List<GameObject>();
        Team9 = new List<GameObject>();
        Team10 = new List<GameObject>();
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
                    CheckAvailablePlayers();
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
    public  void CheckAvailablePlayers()
    {
        foreach (GameObject GnT in TeamPlayers)
        {
            Debug.Log("Passed here again");
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 1)
            {
                if(!Team1.Contains(GnT))
                { Team1.Add(GnT); GnT.GetComponent<NetworkCharacterInfo>().Cmd_SpawnColors();
                    GnT.GetComponent<PlayerInfo>().playersColor = GnT.GetComponent<NetworkCharacterInfo>().color; }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 2)
            {
                if (!Team2.Contains(GnT))
                { Team2.Add(GnT); GnT.GetComponent<NetworkCharacterInfo>().Cmd_SpawnColors();
                    GnT.GetComponent<PlayerInfo>().playersColor = GnT.GetComponent<NetworkCharacterInfo>().color; }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 3)
            {
                if (!Team3.Contains(GnT))
                { Team3.Add(GnT); GnT.GetComponent<NetworkCharacterInfo>().Cmd_SpawnColors();
                    GnT.GetComponent<PlayerInfo>().playersColor = GnT.GetComponent<NetworkCharacterInfo>().color; }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 4)
            {
                if (!Team4.Contains(GnT))
                { Team4.Add(GnT); GnT.GetComponent<NetworkCharacterInfo>().Cmd_SpawnColors();
                    GnT.GetComponent<PlayerInfo>().playersColor = GnT.GetComponent<NetworkCharacterInfo>().color;
                }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 5)
            {
                if (!Team5.Contains(GnT))
                { Team5.Add(GnT); GnT.GetComponent<NetworkCharacterInfo>().Cmd_SpawnColors();
                    GnT.GetComponent<PlayerInfo>().playersColor = GnT.GetComponent<NetworkCharacterInfo>().color;
                }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 6)
            {
                if (!Team6.Contains(GnT))
                { Team6.Add(GnT); GnT.GetComponent<NetworkCharacterInfo>().Cmd_SpawnColors();
                    GnT.GetComponent<PlayerInfo>().playersColor = GnT.GetComponent<NetworkCharacterInfo>().color;
                }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 7)
            {
                if (!Team7.Contains(GnT))
                { Team7.Add(GnT); GnT.GetComponent<NetworkCharacterInfo>().Cmd_SpawnColors();
                    GnT.GetComponent<PlayerInfo>().playersColor = GnT.GetComponent<NetworkCharacterInfo>().color;
                }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 8)
            {
                if (!Team8.Contains(GnT))
                { Team8.Add(GnT); GnT.GetComponent<NetworkCharacterInfo>().Cmd_SpawnColors();
                    GnT.GetComponent<PlayerInfo>().playersColor = GnT.GetComponent<NetworkCharacterInfo>().color;
                }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 9)
            {
                if (!Team9.Contains(GnT))
                { Team9.Add(GnT); GnT.GetComponent<NetworkCharacterInfo>().Cmd_SpawnColors();
                    GnT.GetComponent<PlayerInfo>().playersColor = GnT.GetComponent<NetworkCharacterInfo>().color;
                }
            }
            if (GnT.GetComponent<NetworkCharacterInfo>().teamNumber == 10)
            {
                if (!Team10.Contains(GnT))
                { Team10.Add(GnT); GnT.GetComponent<NetworkCharacterInfo>().Cmd_SpawnColors();
                    GnT.GetComponent<PlayerInfo>().playersColor = GnT.GetComponent<NetworkCharacterInfo>().color;
                }
            }
        }
    }

	/// <summary>
	/// If the player has been hit and is dead, it wil remove the player from the list.
	/// It will then take the number away from the player and check the current teamstatus.
	/// </summary>
	/// <param name="isc_Dead">The Player GameObject</param>
    public void CheckingList(GameObject isc_Dead)
    {
        Debug.Log("isc_Dead name is " + isc_Dead);
        Debug.Log("isc_Dead team is" + isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber);
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 1)
        {
            Team1.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 2)
        {
            Team2.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 3)
        {
            Team3.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 4)
        {
            Team4.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 5)
        {
            Team5.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 6)
        {
            Team6.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 7)
        {
            Team7.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 8)
        {
            Team8.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 9)
        {
            Team9.Remove(isc_Dead);
        }
        if (isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber == 10)
        {
            Team10.Remove(isc_Dead);
        }
        isc_Dead.GetComponent<NetworkCharacterInfo>().teamNumber = 0;
        CheckforTeamStatus();
    }

	/// <summary>
	/// Checks with the current team status, who won and if its a draw.
	/// </summary>
	/// <param name="Winner">The Player GameObjects who won</param>
	/// <param name="Draw">The Player GameObjects who have both lost</param>
    private void CheckforTeamStatus()
    {
        Debug.Log("Here I go again on my own");
        if (Team1.Count != 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
            Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 1");
            foreach (GameObject Winners in Team1)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count != 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 2");
            foreach (GameObject Winners in Team2)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count != 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 3");
            foreach (GameObject Winners in Team3)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count != 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 4");
            foreach (GameObject Winners in Team4)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count != 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 5");
            foreach (GameObject Winners in Team5)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count != 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 6");
            foreach (GameObject Winners in Team6)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count != 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 7");
            foreach (GameObject Winners in Team7)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count != 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 8");
            foreach (GameObject Winners in Team8)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count != 0 && Team10.Count == 0)
        {
            Debug.Log("Victory for team 9");
            foreach (GameObject Winners in Team9)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
          Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count != 0)
        {
            Debug.Log("Victory for team 10");
            foreach (GameObject Winners in Team10)
            {
                Winners.GetComponent<NetworkCharacterInfo>().Victory.SetActive(true);
                Winners.GetComponent<NetworkCharacterInfo>().PlaySound(0);
                Winners.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
                Winners.GetComponent<NetworkCharacterInfo>().StartCoroutine(NLH.GoBacktoLobby(10f));
            }
        }
        if (Team1.Count == 0 && Team2.Count == 0 && Team3.Count == 0 && Team4.Count == 0 && Team5.Count == 0 &&
         Team6.Count == 0 && Team7.Count == 0 && Team8.Count == 0 && Team9.Count == 0 && Team10.Count == 0)
        {
            foreach (GameObject Draw in TeamPlayers)
            {
                Draw.GetComponent<NetworkCharacterInfo>().Draw.SetActive(true);
                Draw.GetComponent<NetworkCharacterInfo>().NLH.showResults = true;
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
