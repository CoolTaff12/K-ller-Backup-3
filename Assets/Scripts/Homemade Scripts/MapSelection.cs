using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapSelection : MonoBehaviour
{
    public UnityStandardAssets.Network.LobbyManager LobbyScenes;
    public Text OptionSelected;
    public Image MapFolder;
    public Sprite[] MapImages;

    /// <summary>
    /// When the script start, it will declair which text, image and script to OptionSelected, MapFolder and LobbyScenes.
    /// </summary>
    void Start ()
    {
        LobbyScenes = GameObject.Find("LobbyManager").GetComponent< UnityStandardAssets.Network.LobbyManager>();
        OptionSelected = GameObject.Find("OptionSelection").GetComponent<Text>();
        MapFolder = GameObject.Find("MapImage").GetComponent<Image>();
    }

    /// <summary>
    /// If the dropdown list text value changes to either of these choises, the image and player scene will change.
    /// </summary>
    void Update ()
    {
        switch(OptionSelected.text)
        {
            case "GYM_FORT":
                GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().playScene = "HermanGympasal";
                GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().matchMap = "GYM_FORT";
                MapFolder.sprite = MapImages[0];
                break;
            case "GYM_ARENA":
                GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().playScene = "SergejTestGym";
                GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().matchMap = "GYM_ARENA";
                MapFolder.sprite = MapImages[1];
                break;
            case "GYM_BRIDGETOWN":
                GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().playScene = "BridgeTown";
                GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().matchMap = "GYM_BRIDGETOWN";
                MapFolder.sprite = MapImages[2];
                break;
            case "GYM_FLOORISLAVA":
                GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().playScene = "TheFloorIsLava";
                GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().matchMap = "GYM_FLOORISLAVA";
                MapFolder.sprite = MapImages[3];
                break;
            case "Bonus_AcrossTheSky":
                GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().playScene = "RotateAcrossTheSky";
                GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().matchMap = "Bonus_AcrossTheSky";
                MapFolder.sprite = MapImages[4];
                break;

        }
    }
}
