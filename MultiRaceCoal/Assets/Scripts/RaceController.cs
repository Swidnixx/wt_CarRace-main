using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class RaceController : MonoBehaviourPunCallbacks
{
    #region RaceFields
    public static bool racePending = false;
    public static int totalLaps = 1;
    public int timer = 5;

    CheckpointController[] cars;

    public Text startText;
    AudioSource audioSource;
    public AudioClip count;
    public AudioClip start;

    public GameObject endPanel;


    public GameObject carPrefab;
    public Transform[] spawnPositions;
    public int playerCount;
    #endregion

    #region Photon Logic
    public GameObject StartBurtton;
    public GameObject WaitText;

    [PunRPC]
    public void StartGame()
    {
        WaitText.SetActive(false);
        StartBurtton.SetActive(false);

        InvokeRepeating(nameof(CountDown), 3, 1);
        GameObject[] carObjects = GameObject.FindGameObjectsWithTag("Car");
        cars = new CheckpointController[carObjects.Length];

        for (int i = 0; i < cars.Length; i++)
        {
            cars[i] = carObjects[i].GetComponent<CheckpointController>();
        }
    }

    public void BeginGame()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            photonView.RPC(nameof(StartGame), RpcTarget.All, null);
        }
    }
    #endregion

    void Awake()
    {
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        endPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        startText.gameObject.SetActive(false);

        StartBurtton.SetActive(false);
        WaitText.SetActive(false);

        int randStartPos = Random.Range(0, spawnPositions.Length);
        Vector3 startPos = spawnPositions[randStartPos].position;
        Quaternion startRot = spawnPositions[randStartPos].rotation;
        GameObject playerCar = null;

        if(PhotonNetwork.IsConnected)
        {
            startPos = spawnPositions[playerCount - 1].position;
            startRot = spawnPositions[playerCount - 1].rotation;

            object[] instanceData = new object[4];
            instanceData[0] = PlayerPrefs.GetString("PlayerName");
            instanceData[1] = PlayerPrefs.GetInt("red");
            instanceData[2] = PlayerPrefs.GetInt("green");
            instanceData[3] = PlayerPrefs.GetInt("blue");

            if(OnlinePlayer.LocalPlayer == null)
            {
                playerCar = PhotonNetwork.Instantiate(
                    carPrefab.name,
                    startPos, startRot, 0, instanceData
                    );
                playerCar.GetComponent<CarApperance>().SetLocalPlayer();
            }

            if(PhotonNetwork.IsMasterClient)
            {
                StartBurtton.SetActive(true);
            }
            else
            {
                WaitText.SetActive(true);
            }
        }

        playerCar.GetComponent<PlayerController>().enabled = true;
    }

    void LateUpdate()
    {
        if (cars == null) return;


        int finishers = 0;
        foreach(CheckpointController c in cars)
        {
            if (c.lap == totalLaps + 1)
                finishers++;
        }
        if (finishers >= cars.Length && racePending)
        {
            endPanel.SetActive(true);
            racePending = false;

        }

    }

    public void RestartRace()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CountDown()
    {
        startText.gameObject.SetActive(true);
        if(timer != 0)
        {
            startText.text = timer.ToString();
            audioSource.PlayOneShot(count);
            timer--;
        }
        else
        {
            startText.text = "Start!";
            audioSource.PlayOneShot(start);
            racePending = true;
            CancelInvoke("CountDown");

            Invoke(nameof(HideStartText),1);
        }
    }
    void HideStartText()
    {
        startText.gameObject.SetActive(false);
    }
    // Start is called before the first frame update

}
