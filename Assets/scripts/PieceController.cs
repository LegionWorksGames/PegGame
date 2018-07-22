using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour {

	public bool pieceEmpty, jumpAvalible;
	public static bool gameOver;
	bool thisPieceClicked;
	public Vector2Int location;
	public Vector2Int[] checks;
	List<PieceController> placesToLand = new List<PieceController>();
	List<PieceController> piecesToJump = new List<PieceController>();
	static PieceController[] pieces;
	[SerializeField] SpriteRenderer frogRender;
	[SerializeField] Sprite[] frogSprites;
	// Use this for initialization
	void Start () {
		gameOver = false;
		pieces = FindObjectsOfType<PieceController>();
		location = new Vector2Int(
			Mathf.RoundToInt(transform.position.x * HexBoardEditor.GetGridSize().x),
			Mathf.RoundToInt(transform.position.y * HexBoardEditor.GetGridSize().y)
			);
		checks = new Vector2Int[] {
			new Vector2Int (1,1),
			new Vector2Int (2,0),
			new Vector2Int (1,-1),
			new Vector2Int (-1,-1),
			new Vector2Int (-2,0),
			new Vector2Int (-1,1)
		};
		Invoke("DelayedStart", 0.5f);
	}
	void DelayedStart()
	{
		CheckAllPossibleMoves();
	}

	private void Update()
	{
		if (frogRender.gameObject.activeSelf && pieceEmpty)
		{
			frogRender.gameObject.SetActive(false);
		}
		if (!frogRender.gameObject.activeSelf && !pieceEmpty)
		{
			frogRender.gameObject.SetActive(true);
			frogRender.sprite = frogSprites[0];
			frogRender.transform.localPosition = Vector3.zero;
		}
	}

	public void OnClickInChild()
	{
		if (!pieceEmpty)
		{
			if (thisPieceClicked)
			{
				frogRender.sprite = frogSprites[0];
				thisPieceClicked = false;
				return;
			}
			foreach (PieceController piece in pieces)
			{
				if (piece.thisPieceClicked)
				{
					return;
				}
			}
			if (!thisPieceClicked)
			{
				frogRender.sprite = frogSprites[1];
				thisPieceClicked = true;
				return;
			}

		}
		else
		{
			CheckForJump();
		}
	}


	private void CheckForJump()
	{
		foreach (PieceController piece in pieces)
		{
			if (piece.thisPieceClicked)
			{
				for (int i = 0; i < piece.placesToLand.Count; i++)
				{
					if (piece.placesToLand[i] == this)
					{
						frogRender.sprite = frogSprites[0];
						piece.thisPieceClicked = false;
						piece.pieceEmpty = true;
						print(piece.piecesToJump[i].pieceEmpty);
						piece.piecesToJump[i].pieceEmpty = true;
						pieceEmpty = false;
						RecheckAllJumps();
						CheckWinLosesConditions();
						return;
					}
				}				
			}
		}		
	}

	private void CheckWinLosesConditions()
	{
		int movesLeft = 0;
		int piecesLeft = 0;
		foreach (PieceController piece in pieces)
		{
			if (piece.jumpAvalible)
			{
				movesLeft++;
			}
			if (!piece.pieceEmpty)
			{
				piecesLeft++;
			}
		}
		if (movesLeft <= 0)
		{
			if (piecesLeft <= 1)
			{
				TextDisplay.DisplayCondition(piecesLeft, true);
				print("win");
			}
			else
			{
				TextDisplay.DisplayCondition(piecesLeft, false);				
			}
			gameOver = true;
		}
	}

	void RecheckAllJumps()
	{
		foreach (PieceController piece in pieces)
		{
			piece.CheckAllPossibleMoves();
		}
	}

	void CheckAllPossibleMoves()
	{
		jumpAvalible = false;
		// For all pieces that aren't empty
		if(!pieceEmpty)
		{
			for (int i = 0; i < pieces.Length; i++)
			{
				// Go through all 6 sides of a piece
				for (int j = 0; j < 6; j++)
				{
					// If a board exists
					if (LocalCheck(j) == pieces[i].GetComponent<HexBoardEditor>().GetGridPos())
					{
						// and the piece on that board isn't empty.
						if (!pieces[i].GetComponentInChildren<PieceController>().pieceEmpty)
						{
							// look through all pieces again
							for (int k = 0; k < pieces.Length; k++)
							{
								// If the piece in the same direction as the pervious direction 
								// is empty then add both pieces to a list.
								if (pieces[k].pieceEmpty &&
									pieces[i].LocalCheck(j) == pieces[k].GetComponent<HexBoardEditor>().GetGridPos())
								{
									placesToLand.Add(pieces[k]);
									piecesToJump.Add(pieces[i]);
									jumpAvalible = true;
								}
							}
						}				
					}
				}
			}
		}
	}

	public Vector2Int LocalCheck(int j)
	{
		return new Vector2Int(
			GetComponentInParent<HexBoardEditor>().GetGridPos().x + checks[j].x,
			GetComponentInParent<HexBoardEditor>().GetGridPos().y + checks[j].y
			);
	}
}
