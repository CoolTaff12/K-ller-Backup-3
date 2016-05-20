using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OutofBound : MonoBehaviour
{

    public List<GameObject> SpawningPositions = new List<GameObject>();
    public GameObject[] respawns;
    public GameObject BallsHere;

    // Use this for initialization
    void Start ()
    {
        //Finds all spawn points on the map
        respawns = GameObject.FindGameObjectsWithTag("Spawn");

        foreach (GameObject spawns in respawns)
        {
            SpawningPositions.Add(spawns);
        }
    }
	
    //If object has fallen out of bound, they get send to one of the spawn points on the map
    void OnTriggerStay(Collider target)
    {
        GameObject GotCaught = target.gameObject;
        Teleport(GotCaught);
    }

    public void Teleport(GameObject location)
    {
        Debug.Log("I'm here now!");
        int Selected = Random.Range(0, SpawningPositions.Count);
        location.GetComponent<Rigidbody>().velocity = Vector3.zero;
        location.transform.position = SpawningPositions[Selected].transform.position;
        BallsHere.transform.position = new Vector3(location.transform.position.x, (location.transform.position.y + 5.0f),location.transform.position.z);
    }
}
