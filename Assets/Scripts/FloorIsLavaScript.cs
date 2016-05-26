using UnityEngine;
using System.Collections;

public class FloorIsLavaScript : MonoBehaviour
{

    /// <summary>
    /// When a player gameObject with Collider enters, it dies instantly.
    /// </summary>
    ///  <param name="other">The Player gameObject.</param>
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
