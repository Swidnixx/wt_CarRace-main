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

	public int playerNumber;

	void Start()
	{
		//if(playerNumber == 0)
  //      {
		//	playerName = PlayerPrefs.GetString("PlayerName");
		//	carColor = MenuController.IntToColor(
		//		PlayerPrefs.GetInt("red"),
		//		PlayerPrefs.GetInt("green"),
		//		PlayerPrefs.GetInt("blue")
		//	);
  //      }
		//else
  //      {
		//	playerName = "Random " + playerNumber;
		//	float r = Random.Range(0f, 1f);
		//	carColor = new Color(r, r, r);
  //      }

		//nameText.text = playerName;
		//carRenderer.material.color = carColor;
		//nameText.color = carColor;
	}

	public void SetLocalPlayer()
    {

    }
}
