using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class MenuController : MonoBehaviourPunCallbacks
{

    #region Unity Fields
    public Renderer carRenderer;

    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public InputField playerName;
    #endregion

    #region Photon Fields
    public Text networkText;
    int maxPlayers = 4;
    bool isConnecting;
    #endregion

    #region Photon Callbacks
    public void Connect()
    {
        networkText.text = "";
        isConnecting = true;
        PhotonNetwork.NickName = playerName.text;
        if(PhotonNetwork.IsConnected)
        {
            networkText.text += "Joining room...\n";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            networkText.text += "Connecting...\n";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        if(isConnecting)
        {
            networkText.text += "Connected to master";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        networkText.text += "Failed to join room...\n";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = (byte)maxPlayers});
    }

    public override void OnJoinedRoom()
    {
        networkText.text += "Joined room with "
            + PhotonNetwork.CurrentRoom.PlayerCount
            + " players\n";

        PhotonNetwork.LoadLevel("MainScene");
    }
    #endregion

    #region Menu Script
    public void SetName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
    }

    public static Color IntToColor(int red, int green, int blue)
    {
        float r = (float)red / 255;
        float g = (float)green / 255;
        float b = (float)blue / 255;

        return new Color(r, g, b);
    }

    void SetCarColor(int red, int green, int blue)
    {
        Color color = IntToColor(red, green, blue);
        carRenderer.material.color = color;

        PlayerPrefs.SetInt("red", red);
        PlayerPrefs.SetInt("green", green);
        PlayerPrefs.SetInt("blue", blue);
    }

    public void JoinRoom()
    {
        SceneManager.LoadScene(0);
    }

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        redSlider.value = PlayerPrefs.GetInt("red");
        greenSlider.value = PlayerPrefs.GetInt("green");
        blueSlider.value = PlayerPrefs.GetInt("blue");

        playerName.text = PlayerPrefs.GetString("PlayerName");
    }

    // Update is called once per frame
    void Update()
    {
        SetCarColor((int)redSlider.value, (int)greenSlider.value, (int)blueSlider.value);
    }
    #endregion
}
