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
			switch (SpecialAbilities.special) {
			case Special.waveoff:
				controller.pieceEmpty = true;
				controller.SpecialChecks ();
				SpecialAbilities.special = Special.none;
				break;
			default:
				controller.OnClickInChild ();
				break;
			}
		}
	}
}
