using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Candy : MonoBehaviour
{
    public static Candy instance;
    private SpriteRenderer spriteRenderer;
    public bool isClickRowBomb;
    public bool isClickColorBomb;
    private int doubleClickCount = 0;

    [Header("Board Variables")]
    public int column;
    public int row;
    public int targetX;
    public int targetY;
    public int previousRow;
    public int previousColumn;
    public bool isMatched = false;

    private EndGameManager endGameManager;
    private HintManager hintManager;
    private FindMatches findMatches;
    private Board board;
    public GameObject otherCandy;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;

    [Header("Swipe Stuff")]
    public float swipeAngle = 0;
    public float swipeResist = 1f;

    [Header("Power Stuff")]
    public bool isRowBomb;
    public bool isColumnBomb;
    public bool isColorBomb;
    public bool isAdjacentBomb;
    public GameObject rowSugar;
    public GameObject columnSugar;
    public GameObject colorBomb;
    public GameObject adjacentMarker;


    private void Awake()
    {
        DOTween.SetTweensCapacity(1000,100);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isColumnBomb = false;
        isRowBomb = false;
        isAdjacentBomb = false;
        endGameManager = FindObjectOfType<EndGameManager>();

        hintManager = FindObjectOfType<HintManager>();
        board = FindObjectOfType<Board>();
        findMatches = FindObjectOfType<FindMatches>();
    }

    // Testing
    private void OnMouseOver()
    {
        Color colorAlpha = new(){a = 0};


        if (isClickRowBomb && Input.GetMouseButtonDown(0))
        {
            doubleClickCount++;
            if (doubleClickCount == 2)
            {
                isRowBomb = true;
                GameObject arrow = Instantiate(rowSugar, transform.position, Quaternion.identity);
                arrow.transform.parent = this.transform;
                arrow.transform.parent.GetComponent<SpriteRenderer>().color = colorAlpha;
                ItemPriceManager.Instance.bombAmount -= 1;
            }
        }
        else if (isClickColorBomb && Input.GetMouseButtonDown(0))
        {
            doubleClickCount++;
            if (doubleClickCount == 2)
            {
                isColorBomb = true;
                GameObject arrow = Instantiate(colorBomb, transform.position, Quaternion.identity);
                arrow.transform.parent = this.transform;
                arrow.transform.parent.GetComponent<SpriteRenderer>().color = colorAlpha;
                ItemPriceManager.Instance.colorBombAmount -= 1;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        findMatches.FindAllMatches();
        if (GameObjectLV1.Instance.isClickBuyColorBomb == true)
        {
            isClickColorBomb = true;
            isClickRowBomb = false;
        }
        if (GameObjectLV1.Instance.isClickBuyRowBomb == true)
        {
            isClickRowBomb = true;
            isClickColorBomb = false;
        }
        if (ItemPriceManager.Instance.bombAmount <= 0) isClickRowBomb = false;
        if (ItemPriceManager.Instance.colorBombAmount <= 0) isClickColorBomb = false;
        targetX = column;
        targetY = row;

        #region Move towards the target
        
        // ( TRÁI || PHẢI )
        if (Mathf.Abs(targetX - transform.position.x) > .1) // Lấy trị tuyệt đối để di chuyển sang trái hay bên phải ( + hoặc - )
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.DOMove(tempPosition, .4f);

            if (board.allCandys[column, row] != this.gameObject)
            {
                board.allCandys[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();
        }
        // Directly set the position
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
        }
        // ( LÊN || XUỐNG )
        // Move Towards the target
        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.DOMove(tempPosition, .4f);

            if (board.allCandys[column, row] != this.gameObject)
            {
                board.allCandys[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();

        }
        // Directly set the position
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
        }
#endregion
    }

    private void OnMouseDown()
    {
        // Destroy the hint
        if (hintManager != null)
        {
            hintManager.DestroyHints();
        }

        if (board.currentState == GameState.move)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp()
    {
        if (board.currentState == GameState.move)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CaculateAngele();
        }
    }

    void CaculateAngele()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            board.currentState = GameState.wait;
            // Tính hàm Tan để đưa ra góc người chơi kéo chuột
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();

            board.currentCandy = this;
        }
        else
        {
            board.currentState = GameState.move;
        }
    }

    void MovePiecesActual(Vector2 direction)
    {
        otherCandy = board.allCandys[column + (int)direction.x, row + (int)direction.y];  // Lấy vị trí kẹo 
        previousRow = row;
        previousColumn = column;
        if (board.lockTiles[column, row] == null && board.lockTiles[column + (int)direction.x, row + (int)direction.y] == null)
        {
            if (otherCandy != null)
            {
                otherCandy.GetComponent<Candy>().column += -1 * (int)direction.x; // Chuyển viên kẹo other về vị trí viên kẹo hiện tại
                otherCandy.GetComponent<Candy>().row += -1 * (int)direction.y;
                column += (int)direction.x;                                       // Chuyển viên kẹo hiện tại sang vị trí other
                row += (int)direction.y;
                StartCoroutine(CheckMoveCor());
            }
            else
            {
                board.currentState = GameState.move;
            }
        }
        else
        {
            board.currentState = GameState.move;
        }
    }

    void MovePieces()
    {
        // Right Swipe
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1)
        {
            MovePiecesActual(Vector2.right);
        }
        // Left Swipe
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            MovePiecesActual(Vector2.left);
        }
        // Up Swipe
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        {
            MovePiecesActual(Vector2.up);
        }
        // Down Swipe
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            MovePiecesActual(Vector2.down);
        }
        else
        {
            board.currentState = GameState.move;
        }
    }

    public IEnumerator CheckMoveCor()
    {
        if (isColorBomb)
        {
            // This piece is a color bomb, the other piece is the color to destroy
            findMatches.MacthPiecesColor(otherCandy.tag);
            isMatched = true;
        }
        else if (otherCandy.GetComponent<Candy>().isColorBomb)
        {
            // The other piece is a color bomb, this piece has the color to destroy
            findMatches.MacthPiecesColor(this.gameObject.tag);
            otherCandy.GetComponent<Candy>().isMatched = true;
        }

        yield return new WaitForSeconds(.5f);
        if (otherCandy != null)
        {
            if (!isMatched && !otherCandy.GetComponent<Candy>().isMatched)
            {
                otherCandy.GetComponent<Candy>().row = row;
                otherCandy.GetComponent<Candy>().column = column;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                board.currentCandy = null;
                board.currentState = GameState.move;
            }
            else
            {
                if (endGameManager != null)
                {
                    if (endGameManager.requirements.gameType == GameType.Moves)
                    {
                        endGameManager.DecreaseCountervalue();
                    }
                }
                board.DestroyMatches();
            }
            //otherCandy = null;
        }
    }

    public void MakeRowBomb()
    {
        isRowBomb = true;

        Color alpha = spriteRenderer.color;
        alpha.a = 0;
        spriteRenderer.color = alpha;

        GameObject sugar = Instantiate(rowSugar, transform.position, Quaternion.identity);
        sugar.transform.parent = this.transform;

    }
    public void MakeColumnBomb()
    {
        isColumnBomb = true;

        Color alpha = spriteRenderer.color;
        alpha.a = 0;
        spriteRenderer.color = alpha;

        GameObject sugar = Instantiate(columnSugar, transform.position, Quaternion.identity);
        sugar.transform.parent = this.transform;
    }
    public void MakeColorBomb()
    {
        isColorBomb = true;

        Color alpha = spriteRenderer.color;
        alpha.a = 0;
        spriteRenderer.color = alpha;

        GameObject color = Instantiate(colorBomb, transform.position, Quaternion.identity);
        color.transform.parent = this.transform;
    }
    public void MakeAdjacentBomb()
    {
        isAdjacentBomb = true;

        Color alpha = spriteRenderer.color;
        alpha.a = 0;
        spriteRenderer.color = alpha;

        GameObject maker = Instantiate(adjacentMarker, transform.position, Quaternion.identity);
        maker.transform.parent = this.transform;
    }
}
