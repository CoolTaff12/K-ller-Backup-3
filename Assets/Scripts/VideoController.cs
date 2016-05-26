using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class VideoController : MonoBehaviour
{
    public float Timeduration;

    /// <summary>
    /// When the its start, it will immedially get the component of it's gameObject and start to play it.
    /// </summary>
	void Start ()
    {
        MovieTexture movie = GetComponent<RawImage>().material.mainTexture as MovieTexture;
        movie.Play();
	}

    /// <summary>
    /// After 9.5f, then it's going to load the next scene or the scene called "NetworkLobby".
    /// </summary>
	void Update ()
    {
        Timeduration += (Time.deltaTime * 2);
     if (Timeduration > 9.5f)
        {
            SceneManager.LoadScene("NetworkLobby");
        }
	}
}
