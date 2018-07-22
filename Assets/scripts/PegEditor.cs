using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PegEditor : MonoBehaviour {
	const int gridSizeX = 5;
	const int gridSizeY = 9;

	public static Vector2Int GetGridSize()
	{
		return new Vector2Int(gridSizeX, gridSizeY);
	}

	void Update()
	{
		SnapToGrid();
		UpdateLabel();
	}

	private void SnapToGrid()
	{
		transform.position = new Vector3(
			GetGridPos().x * gridSizeX,			
			GetGridPos().y * gridSizeY,
			0f
			);
	}

	private Vector2Int GetGridPos()
	{
		return new Vector2Int(
			Mathf.RoundToInt(transform.position.x / gridSizeX),
			Mathf.RoundToInt(transform.position.y / gridSizeY)
			);
	}

	private float OffsetPosY()
	{
		if (Mathf.RoundToInt(transform.position.x / gridSizeX) % 2 == 0)
		{
			return transform.position.y;
		}
		else
		{
			return transform.position.y;
		}
	}

	private void UpdateLabel()
	{
		string labelText = GetGridPos().x + "," + GetGridPos().y;
		gameObject.name = "Peg (" + labelText + ")";
	}
}
