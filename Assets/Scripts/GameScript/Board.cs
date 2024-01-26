using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    wait,
    move,
    win,
    lose,
    pause
}

public class Board : MonoBehaviour
{

    public static Board Instance;
    public AudioSource audioSource;
    public AudioClip destroyEffectsound;
    public GameState currentState = GameState.move;

    [Header("Level")]
    //public int level;
    //public TextAsset levelJson; // Drag and drop your JSON file in the inspector
    public Level_Data levelData;


    [Header("Board Stuff")]
    public int goalScore;
    public int width;
    public int height;
    public int offSet;
    private HintManager hintManager;
    public TileType[] boardLayout;
    private bool[,] blankSpaces;

    [Header("Math Stuff")]
    public Candy currentCandy;
    public FindMatches findMatches;
    public int basePieceValue = 20;
    private int streakValue = 1;
    private ScoreManager scoreManager;
    private bool makeChocolate = true;

    [Header("Prefabs")]
    public GameObject boardTilePrefab;
    public GameObject breakableTilePrefab;
    public GameObject chocolatePrefab;
    public GameObject lockTilePrefab;
    public GameObject biscuitTilePrefab;

    public GameObject[] candys;
    private BoardTile[,] breakableTiles;
    private BoardTile[,] chocolateTiles;
    public BoardTile[,] lockTiles;
    public BoardTile[,] biscuitTiles;
    public GameObject[,] allCandys;

    [Header("Destroy Effect")]
    public GameObject blueDestroyEffect;
    public GameObject greenDestroyEffect;
    public GameObject orangeDestroyEffect;
    public GameObject purpleDestroyEffect;
    public GameObject redDestroyEffect;
    public GameObject yellowDestroyEffect;

    [Header("Animation Time Scale")]
    [Tooltip("Chỉnh thời gian bằng thời gian của animation")]
    [Range(0, 1)]
    public float CandyDestroyTime;
    [Tooltip("Thời gian viên kẹo rơi")]
    [Range(0, 1)]
    public float refillDelay;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        GetLevelData();
        scoreManager = FindObjectOfType<ScoreManager>();
        breakableTiles = new BoardTile[width, height];
        chocolateTiles = new BoardTile[width, height];
        lockTiles = new BoardTile[width, height];
        biscuitTiles = new BoardTile[width, height];

        findMatches = FindObjectOfType<FindMatches>();
        hintManager = FindObjectOfType<HintManager>();

        blankSpaces = new bool[width, height];
        allCandys = new GameObject[width, height];
        audioSource = GetComponent<AudioSource>();
        Setup();
    } 

    // GetLevelData
    private void GetLevelData(){
        goalScore = levelData.dGoalScore;
        width = levelData.dWidth;
        height = levelData.dHeight;
        boardLayout = levelData.dBoardLayout;
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

    public void GenerateBreakableTiles()
    {
        // Look at all the tilse in the layout
        for (int i = 0; i < boardLayout.Length; i++)
        {
            // If a tiles is a "Breakable" tile
            if (boardLayout[i].tileKind == TileKind.Breakable)
            {
                // Create a "Breakable tiles at that position
                Vector2 tempPosition = new(boardLayout[i].x, boardLayout[i].y);
                GameObject tile = Instantiate(breakableTilePrefab, tempPosition, Quaternion.identity);
                breakableTiles[boardLayout[i].x, boardLayout[i].y] = tile.GetComponent<BoardTile>();
            }
        }
    }

    public void GenerateChocolateTiles()
    {
        // Look at all the tilse in the layout
        for (int i = 0; i < boardLayout.Length; i++)
        {
            // If a tiles is a "Chocolate" tile
            if (boardLayout[i].tileKind == TileKind.Chocolate)
            {
                // Create a "Chocolate" tiles at that position
                Vector2 tempPosition = new(boardLayout[i].x, boardLayout[i].y);
                GameObject tile = Instantiate(chocolatePrefab, tempPosition, Quaternion.identity);
                chocolateTiles[boardLayout[i].x, boardLayout[i].y] = tile.GetComponent<BoardTile>();
            }
        }
    }

    private void GenerateBiscuitTiles()
    {
        // Look at all the tilse in the layout
        for (int i = 0; i < boardLayout.Length; i++)
        {
            // If a tiles is a "Biscuit" tile
            if (boardLayout[i].tileKind == TileKind.Biscuit)
            {
                // Create a "Biscuit" tiles at that position
                Vector2 tempPosition = new(boardLayout[i].x, boardLayout[i].y);
                GameObject tile = Instantiate(biscuitTilePrefab, tempPosition, Quaternion.identity);
                biscuitTiles[boardLayout[i].x, boardLayout[i].y] = tile.GetComponent<BoardTile>();
            }
        }
    }

    private void GenerateLockTiles()
    {
        // Look at all the tilse in the layout
        for (int i = 0; i < boardLayout.Length; i++)
        {
            // If a tiles is a "Lock" tile
            if (boardLayout[i].tileKind == TileKind.Lock)
            {
                // Create a "Lock" tiles at that position
                Vector2 tempPosition = new(boardLayout[i].x, boardLayout[i].y);
                GameObject tile = Instantiate(lockTilePrefab, tempPosition, Quaternion.identity);
                lockTiles[boardLayout[i].x, boardLayout[i].y] = tile.GetComponent<BoardTile>();
            }
        }
    }

    private void Setup()
    {
        GenerateBlankSpace();
        GenerateBreakableTiles();
        GenerateChocolateTiles();
        GenerateLockTiles();
        GenerateBiscuitTiles();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (chocolateTiles[i, j] || biscuitTiles[i, j])
                {
                    Vector2 tempPosition = new Vector2(i, j + offSet);
                    Vector2 tilePosition = new Vector2(i, j);
                    GameObject boardTile = Instantiate(boardTilePrefab, tilePosition, Quaternion.identity);
                }
                if (!blankSpaces[i, j] && !chocolateTiles[i, j] && !biscuitTiles[i, j])
                {
                    Vector2 tempPosition = new Vector2(i, j + offSet);
                    Vector2 tilePosition = new Vector2(i, j);
                    GameObject boardTile = Instantiate(boardTilePrefab, tilePosition, Quaternion.identity);
                    boardTile.transform.parent = this.transform;
                    boardTile.name = "( " + i + " , " + j + " )";

                    int candyToUse = UnityEngine.Random.Range(0, candys.Length);

                    int maxIterations = 0;
                    while (MatchesAt(i, j, candys[candyToUse]) && maxIterations < 100)
                    {
                        candyToUse = UnityEngine.Random.Range(0, candys.Length);
                        maxIterations++;
                    }

                    GameObject candy = Instantiate(candys[candyToUse], tempPosition, Quaternion.identity);
                    candy.GetComponent<Candy>().row = j;
                    candy.GetComponent<Candy>().column = i;
                    candy.transform.parent = this.transform;
                    candy.name = "( " + i + " , " + j + " )";
                    allCandys[i, j] = candy;
                }
            }
        }

        if (IsDeadlocked())
        {
            StartCoroutine(ShuffleBoard());
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

    private bool ColumnOrRow()
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
            if (ColumnOrRow())
            {
                // Make a color bomb
                //Is the current candy matched?
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

    public void BomRow(int row)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (biscuitTiles[i, j])
                {
                    biscuitTiles[i, row].TakeDamage(1);
                    if(biscuitTiles[i, row].hitPoints <= 0)
                    {
                        biscuitTiles[i, row] = null;
                    }
                }
            }
        }
    }
    public void BombColumn(int column)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (biscuitTiles[i, j])
                {
                    biscuitTiles[column, i].TakeDamage(1);
                    if(biscuitTiles[column, i].hitPoints <= 0)
                    {
                        biscuitTiles[column, i] = null;
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

            DameBiscuit(column, row);
            DameChocolate(column, row);
            //DameBreakable(column, row);
            
            // How many elements are in the matched pieces list from findmatches?
            if (findMatches.currentMatches.Count >= 4)
            {
                CheckToMakeBombs();
            }

            // Does tile need to break?
            if (breakableTiles[column, row] != null)
            {
                // If it does, take damege
                breakableTiles[column, row].TakeDamage(1);
                if (breakableTiles[column, row].hitPoints <= 0)
                {
                    breakableTiles[column, row] = null;
                }
            }

            if (lockTiles[column, row] != null)
            {
                // If it does, take damege
                lockTiles[column, row].TakeDamage(1);
                if (lockTiles[column, row].hitPoints <= 0)
                {
                    lockTiles[column, row] = null;
                }
            }

            #region Destroy Effect
                if (allCandys[column, row].CompareTag("Blue Candy"))
                {
                    GameObject particle = Instantiate(blueDestroyEffect, allCandys[column, row].transform.position, Quaternion.identity);
                    Destroy(particle, CandyDestroyTime);
                }
                if (allCandys[column, row].CompareTag("Green Candy"))
                {
                    GameObject particle = Instantiate(greenDestroyEffect, allCandys[column, row].transform.position, Quaternion.identity);
                    Destroy(particle, CandyDestroyTime);
                }
                if (allCandys[column, row].CompareTag("Orange Candy"))
                {
                    GameObject particle = Instantiate(orangeDestroyEffect, allCandys[column, row].transform.position, Quaternion.identity);
                    Destroy(particle, CandyDestroyTime);
                }
                if (allCandys[column, row].CompareTag("Red Candy"))
                {
                    GameObject particle = Instantiate(redDestroyEffect, allCandys[column, row].transform.position, Quaternion.identity);
                    Destroy(particle, CandyDestroyTime);
                }
                if (allCandys[column, row].CompareTag("Purple Candy"))
                {
                    GameObject particle = Instantiate(purpleDestroyEffect, allCandys[column, row].transform.position, Quaternion.identity);
                    Destroy(particle, CandyDestroyTime);
                }
                if (allCandys[column, row].CompareTag("Yellow Candy"))
                {
                    GameObject particle = Instantiate(yellowDestroyEffect, allCandys[column, row].transform.position, Quaternion.identity);
                    Destroy(particle, CandyDestroyTime);
                }
            #endregion

            Destroy(allCandys[column, row]);
            scoreManager.IncreaseScore(basePieceValue * streakValue);
            allCandys[column, row] = null;
        }
    }

    // Check có ô nào trên bảng bị null ko, nếu ko null thì gọi hàm DestroyMatchesAt()
    public void DestroyMatches()
    {
        if (hintManager != null)
        {
            hintManager.DestroyHints();
        }

        if (findMatches.currentMatches.Count >= 4)
        {
            CheckToMakeBombs();
        }
        findMatches.currentMatches.Clear();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allCandys[i, j] != null)
                {
                    DestroyMatchesAt(i, j);
                    DOTween.Clear();
                }
            }
        }
        ItemPriceManager.Instance.myMoney += 100;
        StartCoroutine(DecreaseRowCor2());
        if(EndGameManager.instance.setWinGame == true)
        {
            PlayerPrefs.SetInt("MyMoney", ItemPriceManager.Instance.myMoney);
            PlayerPrefs.SetInt("BombAmount", ItemPriceManager.Instance.bombAmount);
            PlayerPrefs.SetInt("ExtraStepAmount", ItemPriceManager.Instance.extraStepAmount);
            PlayerPrefs.SetInt("BombAmount", ItemPriceManager.Instance.colorBombAmount);
            PlayerPrefs.Save();
        }
    }

    private void DameBiscuit(int column, int row)
    {
        if (column > 0)
        {
            if (biscuitTiles[column - 1, row])
            {
                biscuitTiles[column - 1, row].TakeDamage(1);
                if (biscuitTiles[column - 1, row].hitPoints <= 0)
                {
                    biscuitTiles[column - 1, row] = null;
                }
            }
        }
        if (column < width - 1)
        {
            if (biscuitTiles[column + 1, row])
            {
                biscuitTiles[column + 1, row].TakeDamage(1);
                if (biscuitTiles[column + 1, row].hitPoints <= 0)
                {
                    biscuitTiles[column + 1, row] = null;
                }
            }
        }
        if (row > 0)
        {
            if (biscuitTiles[column, row - 1])
            {
                biscuitTiles[column, row - 1].TakeDamage(1);
                if (biscuitTiles[column, row - 1].hitPoints <= 0)
                {
                    biscuitTiles[column, row - 1] = null;
                }
            }
        }
        if (row < height - 1)
        {
            if (biscuitTiles[column, row + 1])
            {
                biscuitTiles[column, row + 1].TakeDamage(1);
                if (biscuitTiles[column, row + 1].hitPoints <= 0)
                {
                    biscuitTiles[column, row + 1] = null;
                }
            }
        }
    }

    private void DameChocolate(int column, int row)
    {
        if (column > 0)
        {
            if (chocolateTiles[column - 1, row])
            {
                chocolateTiles[column - 1, row].TakeDamage(1);
                if (chocolateTiles[column - 1, row].hitPoints <= 0)
                {
                    chocolateTiles[column - 1, row] = null;
                }
                makeChocolate = false;
            }
        }
        if (column < width - 1)
        {
            if (chocolateTiles[column + 1, row])
            {
                chocolateTiles[column + 1, row].TakeDamage(1);
                if (chocolateTiles[column + 1, row].hitPoints <= 0)
                {
                    chocolateTiles[column + 1, row] = null;
                }
                makeChocolate = false;
            }
        }
        if (row > 0)
        {
            if (chocolateTiles[column, row - 1])
            {
                chocolateTiles[column, row - 1].TakeDamage(1);
                if (chocolateTiles[column, row - 1].hitPoints <= 0)
                {
                    chocolateTiles[column, row - 1] = null;
                }
                makeChocolate = false;
            }
        }
        if (row < height - 1)
        {
            if (chocolateTiles[column, row + 1])
            {
                chocolateTiles[column, row + 1].TakeDamage(1);
                if (chocolateTiles[column, row + 1].hitPoints <= 0)
                {
                    chocolateTiles[column, row + 1] = null;
                }
                makeChocolate = false;
            }
        }
    }

    private IEnumerator DecreaseRowCor2()
    {
        yield return new WaitForSeconds(refillDelay * 0.5f);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // If the cureent spot isn't blank and is empty
                if (!blankSpaces[i, j] && allCandys[i, j] == null && !chocolateTiles[i, j] && !biscuitTiles[i, j])
                {
                    // Loop from the space abpve to ther top of the column
                    for (int k = j + 1; k < height; k++)
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
        //yield return new WaitForSeconds(refillDelay * 0.5f);
        StartCoroutine(FillBoardCor());
    }

    private void RefillBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allCandys[i, j] == null && !blankSpaces[i, j] && !chocolateTiles[i, j] && !biscuitTiles[i, j])
                {
                    Vector2 tempPosition = new Vector2(i, j + offSet);
                    int candyToUse = UnityEngine.Random.Range(0, candys.Length);
                    int maxIterations = 0;
                    while (MatchesAt(i, j, candys[candyToUse]) && maxIterations < 100)
                    {
                        maxIterations++;
                        candyToUse = UnityEngine.Random.Range(0, candys.Length);
                    }
                    maxIterations = 0;

                    GameObject piece = Instantiate(candys[candyToUse], tempPosition, Quaternion.identity);
                    allCandys[i, j] = piece;
                    piece.GetComponent<Candy>().row = j;
                    piece.GetComponent<Candy>().column = i;

                }
            }
        }
    }

    private bool MatchesOnBoard()
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
        yield return new WaitForSeconds(refillDelay);
        RefillBoard();
        yield return new WaitForSeconds(refillDelay);

        while (MatchesOnBoard())
        {
            streakValue++;
            DestroyMatches();
            yield return new WaitForSeconds(2 * refillDelay);
            yield break;
        }
        currentCandy = null;
        CheckToMakeChocolate();

        if (IsDeadlocked())
        {
            StartCoroutine(ShuffleBoard());
        }
        yield return new WaitForSeconds(refillDelay);
        System.GC.Collect();
        currentState = GameState.move;
        makeChocolate = true;
        streakValue = 1;
    }

    private void CheckToMakeChocolate()
    {
        // Check the choclotate tiles array
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (chocolateTiles[i, j] != null && makeChocolate)
                {
                    // Call another method to make new chocolate
                    MakeNewChocolate();
                    return;
                }
            }
        }
    }

    private Vector2 CheckForAdjacent(int column, int row)
    {
        if (column < width - 1 && allCandys[column + 1, row])
        {
            return Vector2.right;
        }
        if (column > 0 && allCandys[column - 1, row])
        {
            return Vector2.left;
        }
        if (row < height - 1 && allCandys[column, row + 1])
        {
            return Vector2.up;
        }
        if (row > 0 && allCandys[column, row - 1])
        {
            return Vector2.down;
        }
        return Vector2.zero;
    }

    private void MakeNewChocolate()
    {
        bool chocolate = false;
        int loops = 0;

        while (!chocolate && loops < 200)
        {
            int newX = UnityEngine.Random.Range(0, width);
            int newY = UnityEngine.Random.Range(0, height);
            if (chocolateTiles[newX, newY])
            {
                Vector2 adjacent = CheckForAdjacent(newX, newY);
                if (adjacent != Vector2.zero)
                {
                    Destroy(allCandys[newX + (int)adjacent.x, newY + (int)adjacent.y]);
                    Vector2 tempPosition = new Vector2(newX + (int)adjacent.x, newY + (int)adjacent.y);
                    GameObject tile = Instantiate(chocolatePrefab, tempPosition, Quaternion.identity);
                    chocolateTiles[newX + (int)adjacent.x, newY + (int)adjacent.y] = tile.GetComponent<BoardTile>();
                    chocolate = true;
                }
            }

            loops++;
        }
    }

    private void SwitchPieces(int column, int row, Vector2 direction)
    {
        if (allCandys[column + (int)direction.x, row + (int)direction.y] != null)
        {
            // Take the secend piece and save it in a holder
            GameObject holder = allCandys[column + (int)direction.x, row + (int)direction.y];
            // Switching the first candy to be the second popsition
            allCandys[column + (int)direction.x, row + (int)direction.y] = allCandys[column, row];
            // Set the first candy to be the second candy
            allCandys[column, row] = holder;
        }
    }

    private bool CheckForMathces()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allCandys[i, j] != null)
                {
                    // Make sure that one and two to the right in the board{
                    if (i < width - 2)
                    {
                        // Check if the candys to the rinht and two to the right exist
                        if (allCandys[i + 1, j] != null && allCandys[i + 2, j] != null)
                        {
                            if (allCandys[i, j].CompareTag(allCandys[i + 1, j].tag) && allCandys[i, j].CompareTag(allCandys[i + 2, j].tag))
                            {
                                return true;
                            }
                        }
                    }
                    if (j < height - 2)
                    {
                        // Check if candys above exist
                        if (allCandys[i, j + 1] != null && allCandys[i, j + 2] != null)
                        {
                            if (allCandys[i, j].CompareTag(allCandys[i, j + 1].tag) && allCandys[i, j].CompareTag(allCandys[i, j + 2].tag))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    public bool SwitchAndCheck(int column, int row, Vector2 direction)
    {
        SwitchPieces(column, row, direction);
        if (CheckForMathces())
        {
            SwitchPieces(column, row, direction);
            return true;
        }
        SwitchPieces(column, row, direction);
        return false;
    }

    private bool IsDeadlocked()
    {
        if (hintManager != null)
        {
            hintManager.DestroyHints();
        }
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allCandys[i, j] != null)
                {
                    if (i < width - 1)
                    {
                        if (allCandys[i + 1, j] != null)
                        {
                            if (SwitchAndCheck(i, j, Vector2.right))
                            {
                                return false;
                            }
                        }
                    }
                    if (j < height - 1)
                    {
                        if (SwitchAndCheck(i, j, Vector2.up))
                            {
                                return false;
                            }
                        // if (allCandys[i, j + 1] != null)
                        // {
                        //     if (SwitchAndCheck(i, j, Vector2.up))
                        //     {
                        //         return false;
                        //     }
                        // }
                    }
                    // if (allCandys[i, j].GetComponent<Candy>().isColorBomb)
                    // {
                    //     return false;
                    // }
                }
            }
        }
        return true;
    }

    public IEnumerator ShuffleBoard()
    {
        yield return new WaitForSeconds(0.5f);

        List<GameObject> newBoard = new List<GameObject>();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allCandys[i, j] != null)
                {
                    newBoard.Add(allCandys[i, j]);
                }
            }
        }
        yield return new WaitForSeconds(0.5f);
        // For every spot on the board
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // If this spot shouldn't be blank
                if (!blankSpaces[i, j] && !chocolateTiles[i, j] && !biscuitTiles[i, j])
                {
                    // Pick a random number
                    int pieceToUse = UnityEngine.Random.Range(0, newBoard.Count);

                    // Assign the column and row to the piece
                    int maxIterations = 0;
                    while (MatchesAt(i, j, newBoard[pieceToUse]) && maxIterations < 100)
                    {
                        pieceToUse = UnityEngine.Random.Range(0, newBoard.Count);
                        maxIterations++;
                    }
                    // Make a container for the piece
                    Candy piece = newBoard[pieceToUse].GetComponent<Candy>();
                    maxIterations = 0;
                    piece.column = i;
                    piece.row = j;

                    //Fill in the candys array with this new piece
                    allCandys[i, j] = newBoard[pieceToUse];
                    //Remove it from list
                    newBoard.Remove(newBoard[pieceToUse]);
                }
            }
        }

        // Check if it's still deadlocked
        if (IsDeadlocked())
        {
            StartCoroutine(ShuffleBoard());
        }

        StartCoroutine(findMatches.FindAllMatchesCor());
    }
}
