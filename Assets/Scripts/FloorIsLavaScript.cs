using UnityEngine;
using System.Collections;

public class FloorIsLavaScript : MonoBehaviour
{

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
            target.GetComponent<PlayerInfo>().Cmd_KillYourself(target);
            target.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_Jumping = false;
            target.transform.position = new Vector3 (69, 20, -30);
        }
    }
}
