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
			0f,
			GetGridPos().y * gridSizeY
			);
	}

	private Vector2Int GetGridPos()
	{
		return new Vector2Int(
			Mathf.RoundToInt(transform.position.x / gridSizeX),
			Mathf.RoundToInt(transform.position.z / gridSizeY)
			);
	}

	private float OffsetPosY()
	{
		if (Mathf.RoundToInt(transform.position.x / gridSizeX) % 2 == 0)
		{
			return transform.position.z;
		}
		else
		{
			return transform.position.z;
		}
	}

	private void UpdateLabel()
	{
		string labelText = GetGridPos().x + "," + GetGridPos().y;
		gameObject.name = "Peg (" + labelText + ")";
	}
}
