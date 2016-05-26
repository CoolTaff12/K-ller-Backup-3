using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour
{
	public Vector3 force = new Vector3(0, 10, 0);
    [SerializeField]
    private float TrampolineForce;
    public int AppliedForce;
    public int NormalForce;
    public int StrongForce;

    Vector3 _overwrite = Vector3.zero;

    /// <summary>
    /// The first if-statement runs if the object isTrigger or doesn't have a rigidbody
    /// The second to the forth if-statementruns runs if the object is not the player but have rigidbody
    /// The fith if-statement runs if the object is the player
    /// </summary>
    /// /// <param name="other">The objects Collider.</param>
    void OnTriggerEnter(Collider other)
	{
        if (other.isTrigger || !other.GetComponent<Rigidbody>())
			return;
		
		_overwrite = -other.GetComponent<Rigidbody>().velocity;
		if (force.x < Mathf.Epsilon)
			_overwrite.x = 0F;
		if (force.y < Mathf.Epsilon)
			_overwrite.y = 0F;
		if (force.z < Mathf.Epsilon)
			_overwrite.z = 0F;
		
            other.GetComponent<Rigidbody>().AddForce(force - other.GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);


        //If the object is the player
        if (other.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>())
        {
            AppliedForce = Random.Range(1, 3);
            if(AppliedForce == 2)
            {
                TrampolineForce = StrongForce;
            }
            else
            {
                TrampolineForce = NormalForce;
            }
            other.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_JumpSpeed = TrampolineForce;
            other.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_Jump = true;
        }
     }
}
