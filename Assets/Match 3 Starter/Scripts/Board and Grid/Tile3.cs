using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class Tile3 : MonoBehaviour
{
    private static Color selectedColor = new Color(1f, 0.6f, 0.8f, 0.8f);
    private static Tile3 previousSelected = null;
    private static Vector3 startScale;
    private static Quaternion startRotation;

    private SpriteRenderer render;
    private bool isSelected = false;
    private bool matchFound = false;

    private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        startScale = transform.localScale;
        startRotation = transform.rotation;
    }

    private void Select()
    {
        isSelected = true;
        render.color = selectedColor;
        transform.localScale *= 1.1f;
        StartCoroutine(SelectedMovement());
        previousSelected = gameObject.GetComponent<Tile3>();
        SFXManager.instance.PlaySFX(Clip.Select);
    }

    private void Deselect()
    {
        isSelected = false;
        render.color = Color.white;
        transform.localScale = startScale;
        StopCoroutine(SelectedMovement());
        previousSelected = null;
    }

    void OnMouseDown()
    {
        // Not Selectable conditions
        if (render.sprite == null || BoardManager3.instance.IsShifting)
        {
            return;
        }

        if (isSelected)
        { // Is it already selected?
            Deselect();
        }
        else
        {
            if (previousSelected == null)
            { // Is it the first tile selected?
                Select();
            }
            else
            {
                if (GetAllAdjacentTiles().Contains(previousSelected.gameObject))
                { // Is it an adjacent tile?
                    SwapSprite(previousSelected.render);
                    float startX = transform.position.x;
                    float startY = transform.position.y;
                    previousSelected.ClearAllMatches();
                    previousSelected.Deselect();
                    ClearAllMatches();
                }
                else
                {
                    previousSelected.GetComponent<Tile3>().Deselect();
                    Select();
                }
            }
        }
    }

    public void SwapSprite(SpriteRenderer render2)
    {
        if (render.sprite == render2.sprite)
        {
            return;
        }

        Sprite tempSprite = render2.sprite;
        render2.sprite = render.sprite;
        render.sprite = tempSprite;
        SFXManager.instance.PlaySFX(Clip.Swap);
        GUIManager3.instance.MoveCounter--;
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

    private List<GameObject> FindMatch(Vector2 castDir)
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
        {
            matchingTiles.Add(hit.collider.gameObject);
            hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
        }
        return matchingTiles;
    }

        void ClearMatch(Vector2[] paths)
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        for (int i = 0; i < paths.Length; i++)
            matchingTiles.AddRange(FindMatch(paths[i]));

        if (matchingTiles.Count >= 3)
        {
            StartCoroutine(BoardManager3.instance.Add_Score(matchingTiles));
            for (int i = 0; i < matchingTiles.Count; i++)
            {

                matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
            }
            render.sprite = null;
            matchFound = false;

            StartCoroutine(BoardManager3.instance.CreateBomb(transform.position.x, transform.position.y));

            SFXManager.instance.PlaySFX(Clip.Clear);

        }
        else if (matchingTiles.Count == 2)
        {
            StartCoroutine(BoardManager3.instance.Add_Score(matchingTiles));
            for (int i = 0; i < matchingTiles.Count; i++)
            {
                matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
            }
            render.sprite = null;
            matchFound = false;

            StartCoroutine(BoardManager3.instance.FindNullTiles());


            SFXManager.instance.PlaySFX(Clip.Clear);
        }

    }


    public void ClearAllMatches()
    {
        if (render.sprite == null)
            return;

        ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
        ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
    }

    public IEnumerator SelectedMovement()
    {
        while (isSelected)
        {
            transform.Rotate(0, 0, Mathf.Cos(Time.time) / 2);
            yield return new WaitForSeconds(0.01f);
        }
        if (!isSelected)
        {
            transform.SetPositionAndRotation(transform.position, startRotation);
        }

    }

}