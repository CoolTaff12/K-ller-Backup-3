using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnPointActivity : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider player)
    {
        if(player.tag == "Player")
        {
        }
    }
    void OnTriggerExit(Collider player)
    {
        NetworkStartPosition.Destroy(gameObject);
    }
}
