using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpriteAnimation : MonoBehaviour
{
    public Image myImage;
    public Sprite[] Circles;
    public float framesPerSecond;

    /// <summary>
    /// Calls the object the animation going to take place.
    /// </summary>
    void Start ()
    {
        myImage = GameObject.Find("Load Circle").GetComponent<Image>();
	}
   
    /// <summary>
    /// Updates and make sprite switch once per frame, thus creating an animation.
    /// </summary>
    void Update ()
    {
        //The Animation
        if (myImage != null)
        {
            int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
            index = index % Circles.Length;
            myImage.sprite = Circles[index];
        }
    }
}
