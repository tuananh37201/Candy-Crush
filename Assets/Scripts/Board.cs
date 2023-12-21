using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    wait,
    move
}

public class Board : MonoBehaviour
{
    public GameState currentStage = GameState.move;

    public int width;
    public int height;
    public int offSet;

    public GameObject boardTilePrefab;
    public GameObject[] candys;
    public GameObject destroyEffect;
    private BoardTile[,] allTiles;
    public GameObject[,] allCandys;
    public Candy currentCandy;
    private FindMatches findMatches;

    void Start()
    {
        findMatches = FindObjectOfType<FindMatches>();

        allTiles = new BoardTile[width, height];
        allCandys = new GameObject[width, height];
        Setup();
    }

    private void Setup()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i, j + offSet);
                GameObject boardTile = Instantiate(boardTilePrefab, tempPosition, Quaternion.identity);
                boardTile.transform.parent = this.transform;
                boardTile.name = "( " + i + " , " + j + " )";

                int candyToUse = Random.Range(0, candys.Length);

                int maxIterations = 0;
                while (MatchesAt(i, j, candys[candyToUse]) && maxIterations < 100)
                {
                    candyToUse = Random.Range(0, candys.Length);
                    maxIterations++;
                }
                maxIterations = 0;

                GameObject candy = Instantiate(candys[candyToUse], tempPosition, Quaternion.identity);
                candy.GetComponent<Candy>().row = j;
                candy.GetComponent<Candy>().column = i;
                candy.transform.parent = this.transform;
                candy.name = "( " + i + " , " + j + " )";
                allCandys[i, j] = candy;
            }
        }
    }

    private bool MatchesAt(int column, int row, GameObject piece)
    {
        if (column > 1 && row > 1)
        {
            if (piece.CompareTag(allCandys[column - 1, row].tag) && piece.CompareTag(allCandys[column - 2, row].tag))
            {
                return true;
            }
            if (piece.CompareTag(allCandys[column, row - 1].tag) && piece.CompareTag(allCandys[column, row - 2].tag))
            {
                return true;
            }
        }
        else if (column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (piece.CompareTag(allCandys[column, row - 1].tag) && piece.CompareTag(allCandys[column, row - 2].tag))
                {
                    return true;
                }
            }
            if (column > 1)
            {
                if (piece.CompareTag(allCandys[column - 1, row].tag) && piece.CompareTag(allCandys[column - 2, row].tag))
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Destroy viên kẹo và đặt ô đó thành null
    private void DestroyMatchesAt(int column, int row)
    {
        if (allCandys[column, row].GetComponent<Candy>().isMatched)
        {
            // How many elements are in the matched pieces list from findmatches?
            if(findMatches.currentMatches.Count == 4 || findMatches.currentMatches.Count == 7)
            {
                // Gerenating bomb
                findMatches.CheckBomb();
            }
            findMatches.currentMatches.Remove(allCandys[column, row]);
            GameObject particle = Instantiate(destroyEffect, allCandys[column, row].transform.position, Quaternion.identity);
            Destroy(particle, .5f);
            Destroy(allCandys[column, row]);
            allCandys[column, row] = null;
        }
    }

    // Check có ô nào trên bảng bị null ko, nếu ko null thì gọi hàm DestroyMatchesAt()
    public void DestroyMatches()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allCandys[i, j] != null)
                {
                    DestroyMatchesAt(i, j);
                }
            }
        }
        StartCoroutine(DecreaseRowCor());
    }

    private IEnumerator DecreaseRowCor()
    {
        int nullCount = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allCandys[i, j] == null)
                {
                    nullCount++;
                }
                else if (nullCount > 0)
                {
                    allCandys[i, j].GetComponent<Candy>().row -= nullCount;
                    allCandys[i, j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoardCor());
    }

    private void RefillBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allCandys[i, j] == null)
                {
                    Vector2 tempPosition = new Vector2(i, j + offSet);
                    int candyToUse = Random.Range(0, candys.Length);
                    GameObject piece = Instantiate(candys[candyToUse], tempPosition, Quaternion.identity);
                    allCandys[i, j] = piece;
                    piece.GetComponent<Candy>().row = j;
                    piece.GetComponent<Candy>().column = i;

                }
            }
        }
    }

    private bool MathesOnBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allCandys[i, j] != null)
                {
                    if (allCandys[i, j].GetComponent<Candy>().isMatched)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator FillBoardCor()
    {
        RefillBoard();
        yield return new WaitForSeconds(.5f);
        while (MathesOnBoard())
        {
            yield return new WaitForSeconds(.5f);
            DestroyMatches();
        }
        findMatches.currentMatches.Clear();
        currentCandy = null;
        yield return new WaitForSeconds(.5f);
        currentStage = GameState.move;
    }
}
