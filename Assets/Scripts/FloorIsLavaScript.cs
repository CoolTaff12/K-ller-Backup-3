using UnityEngine;
using System.Collections;

public class FloorIsLavaScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        //If the object is the player
        if (other.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>())
        {
            GameObject target = other.gameObject;
            Vector3 OldSet = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
            target.GetComponent<PlayerInfo>().Cmd_KillYourself(target);
            target.transform.position = OldSet;
        }
    }
}
