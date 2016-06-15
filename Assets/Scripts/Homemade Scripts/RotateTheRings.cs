using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RotateTheRings : MonoBehaviour
{
    public float RotateSpeed;

    /// <summary>
    /// This rotates the object as Update is called once per frame
    /// </summary>
    void Update ()
    {
        transform.Rotate(new Vector3(0f, RotateSpeed, 0.0f));
    }
}
