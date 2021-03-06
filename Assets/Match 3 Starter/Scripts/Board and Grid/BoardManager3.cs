using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager3 : MonoBehaviour
{
    public static BoardManager3 instance;
    public static List<Sprite> pussy_use;
    public GameObject tilePrefab;
    public GameObject rockPrefab; public GameObject rockPrefab2;
    public GameObject bombPrefab;
    public int xSize, ySize;

    private GameObject[,] tiles;
    private GameObject[,] exTiles;

    public bool IsShifting { get; set; }
    public bool IsBombCreating { get; set; }
    public Image[] Aim;

    void Start()
    {
        instance = GetComponent<BoardManager3>();
        Vector2 offset = tilePrefab.GetComponent<SpriteRenderer>().bounds.size + new Vector3(0.03f, 0.03f, 0.03f);
        CreateBoard(offset.x, offset.y);
        IsBombCreating = false;
        Aim[0].sprite = pussy_use[0];
        Aim[1].sprite = pussy_use[0];
    }

    public IEnumerator Add_Score(List<GameObject> matchingTiles)
    {

        for (int i = 0; i < matchingTiles.Count; i++)
        {
             if (matchingTiles[i].GetComponent<SpriteRenderer>().sprite == pussy_use[0])
                GUIManager3.instance.Score += 1;
        }
        if (matchingTiles[1].GetComponent<SpriteRenderer>().sprite == pussy_use[0])
            GUIManager3.instance.Score += 1;

        yield return null;
    }

    private void CreateBoard(float xOffset, float yOffset)
    {
        tiles = new GameObject[xSize, ySize];
        exTiles = new GameObject[xSize, ySize];
        float startX = transform.position.x;
        float startY = transform.position.y;

        Sprite[] previousLeft = new Sprite[ySize];
        Sprite previousBelow = null;

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if ((x != 3 || y != 2) &&(x != 3 || y != 3))
                {
                    tiles[x, y] = Instantiate(tilePrefab, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tilePrefab.transform.rotation);
                    tiles[x, y].transform.parent = transform;

                    List<Sprite> possibleCharacters = new List<Sprite>();
                    possibleCharacters.AddRange(pussy_use);

                    possibleCharacters.Remove(previousLeft[y]);
                    possibleCharacters.Remove(previousBelow);

                    Sprite newSprite = possibleCharacters[Random.Range(0, possibleCharacters.Count - 1)];
                    tiles[x, y].GetComponent<SpriteRenderer>().sprite = newSprite;
                    previousLeft[y] = newSprite;
                    previousBelow = newSprite;
                }
            }
        }
       
        GameObject rock = Instantiate(rockPrefab, new Vector3(startX + (xOffset * 3), startY + (yOffset * 2), 0), rockPrefab.transform.rotation);
        tiles[3, 2] = rock;
       
       
         GameObject rock2 = Instantiate(rockPrefab2, new Vector3(startX + (xOffset * 3), startY  + (yOffset * 3), 0), rockPrefab.transform.rotation);
        tiles[3, 3] = rock2;

    }



    public IEnumerator FindNullTiles()
    {
        Vector2 offset = tilePrefab.GetComponent<SpriteRenderer>().bounds.size + new Vector3(0.03f, 0.03f, 0.03f);


        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if ((x == 3 && y == 2) || (x == 3 && y == 3) || tiles[x, y].GetComponent<Bomb3>() != null)
                {

                }
                else
                {
                    float xx = transform.position.x + (offset.x * x);
                    float yy = transform.position.y + (offset.y * y);
                    if (tiles[x, y].GetComponent<SpriteRenderer>().sprite == null)
                    {
                        Debug.Log("??????? 1.2" + tiles[x, y].GetComponent<Bomb3>());
                        yield return StartCoroutine(ShiftTilesDown(x, y));
                        break;
                    }
                }
            }
        }

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {

                if ((x != 3 || y != 2) && (x != 3 || y != 3) && tiles[x, y].GetComponent<Tile3>() != null)
                    tiles[x, y].GetComponent<Tile3>().ClearAllMatches();
            }
        }
        yield return null;
    }

    private IEnumerator ShiftTilesDown(int x, int yStart, float shiftDelay = .06f)
    {
        IsShifting = true;
        List<SpriteRenderer> renders = new List<SpriteRenderer>();
        int nullCount = 0;
        Vector2 offset = tilePrefab.GetComponent<SpriteRenderer>().bounds.size + new Vector3(0.03f, 0.03f, 0.03f);
        float xOffset = offset.x; float yOffset = offset.y;

        for (int y = yStart; y < ySize; y++)
        {

            if ((x != 3 || y != 2) && (x != 3 || y != 3) && tiles[x, y].GetComponent<Tile3>() != null)
            {
                SpriteRenderer render = tiles[x, y].GetComponent<SpriteRenderer>();
                if (render.sprite == null)
                {
                    nullCount++;
                }
                if (y == ySize - 1)
                {
                    render.sprite = GetNewSprite(x, ySize - 1);
                }
                else if (tiles[x, y + 1].GetComponent<SpriteRenderer>() == null)
                {
                    if (tiles[x, y + 1].GetComponent<Bomb3>() != null && y == ySize - 2)
                    {
                        render.sprite = GetNewSprite(x, ySize - 1);
                    }
                    else if (tiles[x, y + 1].GetComponent<Bomb3>() != null && y < ySize - 2)
                    {
                        render.sprite = tiles[x, y + 2].GetComponent<SpriteRenderer>().sprite;
                    }

                }

                renders.Add(render);

            }
        }

        for (int i = 0; i < nullCount; i++)
        {

            yield return new WaitForSeconds(shiftDelay);
            for (int k = 0; k < renders.Count - 1; k++)
            {
                renders[k].sprite = renders[k + 1].sprite;
                renders[k + 1].sprite = GetNewSprite(x, ySize - 1);
            }
        }
        IsShifting = false;
        yield return null;
    }


    public IEnumerator FindBomb(float startX, float startY)
    {
        Vector2 offset = tilePrefab.GetComponent<SpriteRenderer>().bounds.size + new Vector3(0.03f, 0.03f, 0.03f);

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {

                if (tiles[x, y].GetComponent<Bomb2>() != null
                && (Mathf.Abs(tiles[x, y].transform.position.x - startX) > 0.001f && Mathf.Abs(tiles[x, y].transform.position.y - startY) > 0.001f))
                {

                }
                else
                {
                    if (Mathf.Abs(tiles[x, y].transform.position.x - startX) <= 0.001f && Mathf.Abs(tiles[x, y].transform.position.y - startY) <= 0.001f)
                    {

                        Destroy(tiles[x, y]);
                        tiles[x, y] = exTiles[x, y];

                        if (x + 1 < xSize && (x != 2 || y != 2) && (x != 2 || y != 3)) tiles[x + 1, y].GetComponent<SpriteRenderer>().sprite = null;
                        if (x + 1 < xSize && y + 1 < ySize && (x != 2 || y != 1) && (x != 2 || y != 2)) tiles[x + 1, y + 1].GetComponent<SpriteRenderer>().sprite = null;
                        if (x + 1 < xSize && y - 1 > 0 && (x != 2 || y != 3) && (x != 2 || y != 4)) tiles[x + 1, y - 1].GetComponent<SpriteRenderer>().sprite = null;

                        if (y + 1 < ySize && (x != 3 || y != 1) && (x != 3 || y != 2)) tiles[x, y + 1].GetComponent<SpriteRenderer>().sprite = null;
                        if (y - 1 >= 0 && (x != 3 || y != 4) && (x != 3 || y != 3)) tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite = null;

                        if (x - 1 >= 0 && (x != 4 || y != 2) && (x != 4 || y != 3)) tiles[x - 1, y].GetComponent<SpriteRenderer>().sprite = null;
                        if (x - 1 >= 0 && y - 1 >= 0 && (x != 4 || y != 4) && (x != 4 || y != 3)) tiles[x - 1, y - 1].GetComponent<SpriteRenderer>().sprite = null;
                        if (y + 1 < ySize && x - 1 >= 0 && (x != 4 || y != 1) && (x != 4 || y != 2)) tiles[x - 1, y + 1].GetComponent<SpriteRenderer>().sprite = null;


                        StartCoroutine(FindNullTiles());
                    }

                }

            }
        }

        yield return null;
    }

    public IEnumerator CreateBomb(float startXX, float startYY)
    {
        Vector2 offset = tilePrefab.GetComponent<SpriteRenderer>().bounds.size + new Vector3(0.03f, 0.03f, 0.03f);
        IsBombCreating = true;
        bool isCreated = false;

        for (int x = 0; x < xSize; x++)
        {

            for (int y = 0; y < ySize; y++)
            {
                float xx = transform.position.x + (offset.x * x);
                float yy = transform.position.y + (offset.y * y);

                if (Mathf.Abs(xx - startXX) <= 0.001f && Mathf.Abs(yy - startYY) <= 0.001f)
                {
                    exTiles[x, y] = tiles[x, y];
                    tiles[x, y] = Instantiate(bombPrefab, new Vector3(xx, yy, -1), transform.rotation);

                    isCreated = true;
                    break;
                }
            }
            if (isCreated)
                break;
        }

        StartCoroutine(FindNullTiles());
        IsBombCreating = false;
        yield return null;
    }


    private Sprite GetNewSprite(int x, int y)
    {
        List<Sprite> possibleCharacters = new List<Sprite>();
        possibleCharacters.AddRange(pussy_use);

        if (x > 0)
        {
            possibleCharacters.Remove(tiles[x - 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (x < xSize - 1)
        {
            possibleCharacters.Remove(tiles[x + 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (y > 0)
        {
            possibleCharacters.Remove(tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite);
        }

        return possibleCharacters[Random.Range(0, possibleCharacters.Count - 1)];
    }

}
