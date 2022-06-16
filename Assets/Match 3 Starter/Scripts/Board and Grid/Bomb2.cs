using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb2 : MonoBehaviour
{

	private static Color selectedColor = new Color(0.0f, 0.5f, 0.5f, 1.0f);
	private static Bomb2 previousSelected = null; private static Bomb2 bomb = null;
	private SpriteRenderer render;
	private bool isSelected = false; public List<Sprite> characters = new List<Sprite>();

	private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

	void Awake()
	{
		render = GetComponent<SpriteRenderer>();
	}

	public IEnumerator Wait()
	{
		yield return new WaitForSeconds(0.1f);
		Destroy(gameObject.GetComponent<Bomb2>());
		StartCoroutine(BoardManager2.instance.FindBomb(transform.position.x, transform.position.y));
	}

	private void Select()
	{
		isSelected = true;
		render.color = selectedColor;
		SFXManager.instance.PlaySFX(Clip.Select);
		previousSelected = gameObject.GetComponent<Bomb2>();
	}

	private void Deselect()
	{
		isSelected = false;
		render.color = Color.white;
	}

	void OnMouseDown()
	{
		if (render.sprite == null || BoardManager2.instance.IsShifting)
		{
			return;
		}

		if (isSelected)
		{
			SFXManager.instance.PlaySFX(Clip.Bomb);
			render.sprite = characters[0];
			StartCoroutine(Wait());
		}
		else
		{
			if (previousSelected == null)
				Select();

		}
	}



	private GameObject GetAdjacent(Vector2 castDir)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
		if (hit.collider != null)
		{
			return hit.collider.gameObject;
		}
		return null;
	}

	private List<GameObject> GetAllAdjacentTiles()
	{
		List<GameObject> adjacentTiles = new List<GameObject>();
		for (int i = 0; i < adjacentDirections.Length; i++)
		{
			adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
		}
		return adjacentTiles;
	}

	public void ClearAllMatches(float startX, float startY) { }
}

