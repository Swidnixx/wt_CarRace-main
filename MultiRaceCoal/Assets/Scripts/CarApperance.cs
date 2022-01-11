using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarApperance : MonoBehaviour
{
	public string playerName;
	public Color carColor;

	public Text nameText;
	public Renderer carRenderer;

	int playerNumber;
	bool numberSet;
	public CheckpointController checkpointController;

    private void LateUpdate()
    {
        if(!numberSet)
        {
			playerNumber = Leaderboard.Register(playerName);
			numberSet = true;
        }
		else
        {
			Leaderboard.SetPosition(playerName, 
				checkpointController.lap, 
				checkpointController.lastCheckpoint
				);
        }
    }

    public void SetNameAndColor(string name, Color color)
    {
		playerName = name;
		nameText.text = name;
		carRenderer.material.color = color;
		nameText.color = color;
    }
	public void SetLocalPlayer()
    {
		FindObjectOfType<CameraController>().SetCamera(this.gameObject);
		playerName = PlayerPrefs.GetString("PlayerName");
		carColor = MenuController.IntToColor(PlayerPrefs.GetInt("red"),
			PlayerPrefs.GetInt("green"),
			PlayerPrefs.GetInt("blue"));
		SetNameAndColor(playerName, carColor);
    }
}
