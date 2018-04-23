using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour {

	[SerializeField] Text main, secondary;
	static string mainString, secString;
	static bool textUpdate = false;
	static bool winGame;
	[SerializeField] GameObject panel;
	// Use this for initialization
	void Start () {
		panel.SetActive(false);
	}
	void Update()
	{
		if (textUpdate)
		{
			main.text = mainString;
			secondary.text = secString;
			panel.SetActive(true);
			textUpdate = false;
		}
	}

	public static void DisplayCondition(int piecesLeft, bool win)
	{
		if (!win)
		{
			mainString = "Too Bad";
			secString = "You had " + piecesLeft + " pieces remaining";
			winGame = false;
		}
		else
		{
			mainString = "Congratulations";
			secString = "You win!";
			winGame = true;
		}
		textUpdate = true;
	}

	public void Restart()
	{
		int scene = SceneManager.GetActiveScene().buildIndex;
		if (winGame)
		{
			SceneManager.LoadScene(scene+1);
		}
		else
		{
			SceneManager.LoadScene(scene);
		}		
	}
}
