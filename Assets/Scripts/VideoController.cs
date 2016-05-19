using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class VideoController : MonoBehaviour
{
    public float Timeduration;

	// Use this for initialization
	void Start ()
    {
        MovieTexture movie = GetComponent<RawImage>().material.mainTexture as MovieTexture;
        Debug.Log("movie name is " + movie.name);
        movie.Play();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Timeduration += (Time.deltaTime * 2);
     if (Timeduration > 9.5f)
        {
            SceneManager.LoadScene("NetworkLobby");
        }
	}
}
