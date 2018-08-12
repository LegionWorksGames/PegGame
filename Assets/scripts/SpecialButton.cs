using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpecialButton : MonoBehaviour {

	[SerializeField] Text usesTxt;
	[SerializeField] Special thisSpecial;
	int uses = 3;

	void Update()
	{
		usesTxt.text = "x" + uses;
	}

	public void ActivateSpecial()
	{
		if (uses > 0) {
			SpecialAbilities.special = thisSpecial;
			uses--;
		}
	}
}
