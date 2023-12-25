using System.Collections;
using UnityEngine;

public enum GameState
{
    wait,
    move
}

public enum TileKind
{
    Breakable,
    Blank,
    Normal
}

[System.Serializable]
public class TileType
{
    public int x;
    public int y;
    public TileKind tileKind;
}

public class Board : MonoBehaviour
{
    public GameState currentState = GameState.move;

    public int width;
    public int height;
    public int offSet;

    public GameObject boardTilePrefab;
    public GameObject[] candys;
    public GameObject destroyEffect;
    public TileType[] boardLayout;
    private bool[,] blankSpaces;
    public GameObject[,] allCandys;
    public Candy currentCandy;
    private FindMatches findMatches;

    void Start()
    {
        findMatches = FindObjectOfType<FindMatches>();

        blankSpaces = new bool[width, height];
        allCandys = new GameObject[width, height];
        Setup();
    }

    public void GenerateBlankSpace()
    {
        for (int i = 0; i < boardLayout.Length; i++)
        {
            if (boardLayout[i].tileKind == TileKind.Blank)
            {
                blankSpaces[boardLayout[i].x, boardLayout[i].y] = true;
            }
        }
    }

    private void Setup()
    {
        GenerateBlankSpace();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (!blankSpaces[i, j])
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
    }

    private bool MatchesAt(int column, int row, GameObject piece)
    {
        if (column > 1 && row > 1)
        {
            if (allCandys[column - 1, row] != null && allCandys[column - 2, row] != null)
            {
                if (piece.CompareTag(allCandys[column - 1, row].tag) && piece.CompareTag(allCandys[column - 2, row].tag))
                {
                    return true;
                }
            }

            if (allCandys[column, row - 1] != null && allCandys[column, row - 2] != null)
            {
                if (piece.CompareTag(allCandys[column, row - 1].tag) && piece.CompareTag(allCandys[column, row - 2].tag))
                {
                    return true;
                }
            }
        }
        else if (column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (allCandys[column, row - 1] != null && allCandys[column, row - 2])
                {
                    if (piece.CompareTag(allCandys[column, row - 1].tag) && piece.CompareTag(allCandys[column, row - 2].tag))
                    {
                        return true;
                    }
                }
            }
            if (column > 1)
            {
                if (allCandys[column - 1, row] != null && allCandys[column - 2, row])
                {
                    if (piece.CompareTag(allCandys[column - 1, row].tag) && piece.CompareTag(allCandys[column - 2, row].tag))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private bool ColumnOrBow()
    {
        int numberHorizontal = 0;
        int numberVertical = 0;
        Candy firstPiece = findMatches.currentMatches[0].GetComponent<Candy>();
        if (firstPiece != null)
        {
            foreach (GameObject currentPiece in findMatches.currentMatches)
            {
                Candy candy = currentPiece.GetComponent<Candy>();
                if (candy.row == firstPiece.row)
                {
                    numberHorizontal++;
                }
                if (candy.column == firstPiece.column)
                {
                    numberVertical++;
                }
            }
        }
        return (numberVertical == 5 || numberHorizontal == 5);
    }

    private void CheckToMakeBombs()
    {
        if (findMatches.currentMatches.Count == 4 || findMatches.currentMatches.Count == 7)
        {
            findMatches.CheckBomb();
        }
        if (findMatches.currentMatches.Count == 5 || findMatches.currentMatches.Count == 8)
        {
            if (ColumnOrBow())
            {
                // Make a color bomb
                //Is thr current candy matched?
                if (currentCandy != null)
                {
                    if (currentCandy.isMatched)
                    {
                        if (!currentCandy.isColorBomb)
                        {
                            currentCandy.isMatched = false;
                            currentCandy.MakeColorBomb();
                        }
                    }
                    else
                    {
                        if (currentCandy.otherCandy != null)
                        {
                            Candy otherCandy = currentCandy.otherCandy.GetComponent<Candy>();
                            if (otherCandy.isMatched)
                            {
                                if (!otherCandy.isColorBomb)
                                {
                                    otherCandy.isMatched = false;
                                    otherCandy.MakeColorBomb();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // Make a adjcent bomb
                //Is thr current candy matched?
                if (currentCandy != null)
                {
                    if (currentCandy.isMatched)
                    {
                        if (!currentCandy.isAdjacentBomb)
                        {
                            currentCandy.isMatched = false;
                            currentCandy.MakeAdjacentBomb();
                        }
                    }
                    else
                    {
                        if (currentCandy.otherCandy != null)
                        {
                            Candy otherCandy = currentCandy.otherCandy.GetComponent<Candy>();
                            if (otherCandy.isMatched)
                            {
                                if (!otherCandy.isAdjacentBomb)
                                {
                                    otherCandy.isMatched = false;
                                    otherCandy.MakeAdjacentBomb();
                                }
                            }
                        }
                    }
                }

            }
        }
    }

    // Destroy viên kẹo và đặt ô đó thành null
    private void DestroyMatchesAt(int column, int row)
    {
        if (allCandys[column, row].GetComponent<Candy>().isMatched)
        {

            // How many elements are in the matched pieces list from findmatches?
            if (findMatches.currentMatches.Count >= 4)
            {
                CheckToMakeBombs();
            }

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
        findMatches.currentMatches.Clear();
        StartCoroutine(DecreaseRowCor2());
    }

    private IEnumerator DecreaseRowCor2()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                // If the cureent spot isn't blank and is empty
                if (!blankSpaces[i, j] && allCandys[i, j] == null)
                {
                    // Loop from the space abpve to ther top of the column
                    for(int k = j + 1; k < height; k++)
                    {
                        // If a dot is found
                        if (allCandys[i, k] != null)
                        {
                            // Move that do to this empty space
                            allCandys[i, k].GetComponent<Candy>().row = j;
                            // Set that spot to be null
                            allCandys[i, k] = null;
                            /// Break out of the loop
                            break;
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoardCor());
    }

    private IEnumerator DecreaseRowCor()
    {
        yield return new WaitForSeconds(.5f);
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
                if (allCandys[i, j] == null && !blankSpaces[i,j])
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
        currentState = GameState.move;
    }
}
