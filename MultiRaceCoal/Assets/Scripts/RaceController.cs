using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceController : MonoBehaviour
{
    public static bool racePending = false;
    public static int totalLaps = 1;
    public int timer = 5;

    CheckpointController[] cars;

    public Text startText;
    AudioSource audioSource;
    public AudioClip count;
    public AudioClip start;

    public GameObject endPanel;

    void Start()
    {
        endPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        startText.gameObject.SetActive(false);

        InvokeRepeating(nameof(CountDown), 3, 1);

        GameObject[] carObjects = GameObject.FindGameObjectsWithTag("Car");
        cars = new CheckpointController[carObjects.Length];

        for(int i =0; i<cars.Length; i++)
        {
            cars[i] = carObjects[i].GetComponent<CheckpointController>();
        }
    }

    void LateUpdate()
    {
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
