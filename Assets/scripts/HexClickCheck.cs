using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexClickCheck : MonoBehaviour {

	PieceController controller;

	private void Start()
	{
		controller = GetComponentInParent<PieceController>();
	}

	void OnMouseDown()
	{
		if (!PieceController.gameOver)
		{
			print("Clicked");
			controller.OnClickInChild();
		}
	}
}
