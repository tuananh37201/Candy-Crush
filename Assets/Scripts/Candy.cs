using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    [Header("Board Variables")]
    public int column;
    public int row;
    public int targetX;
    public int targetY;
    public int previousRow;
    public int previousColumn;
    public bool isMatched = false;

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
    public bool isColumnBomb;
    public bool isRowBomb;
    public GameObject rowSugar;
    public GameObject columnSugar;


    // Start is called before the first frame update
    void Start()
    {
        isColumnBomb = false;
        isRowBomb = false;

        board = FindObjectOfType<Board>();
        findMatches = FindObjectOfType<FindMatches>();
        //targetX = (int)transform.position.x;
        //targetY = (int)transform.position.y;
        //row = targetY;
        //column = targetX;
        //previousRow = row;
        //previousColumn = column;

    }

    // Testing
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            isRowBomb = true;
            GameObject sugar = Instantiate(rowSugar, transform.position, Quaternion.identity);
            sugar.transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

        targetX = column;
        targetY = row;

        // ( TRÁI || PHẢI )
        // Move Towards the target 
        if (Mathf.Abs(targetX - transform.position.x) > .1) // Lấy trị tuyệt đối để di chuyển sang trái hay bên phải ( + hoặc - )
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f); // Tạo chuyển động giữa 2 đối tượng trong 1 khoảng thời gian
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
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
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
    }



    private void OnMouseDown()
    {
        if(board.currentStage == GameState.move)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    private void OnMouseUp()
    {
        if(board.currentStage == GameState.move)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CaculateAngele();
        }
    }

    void CaculateAngele()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            // Tính hàm Tan để đưa ra góc người chơi kéo chuột
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();
            board.currentStage = GameState.wait;

            board.currentCandy = this;
        }
        else
        {
            board.currentStage = GameState.move;
        }
    }

    void MovePieces()
    {
        // Right Swipe
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1)
        {
            otherCandy = board.allCandys[column + 1, row];  // Lấy vị trí kẹo bên phải
            previousRow = row;
            previousColumn = column;
            otherCandy.GetComponent<Candy>().column -= 1;   // Chuyển viên kẹo bên phải sang bên trái
            column += 1;                                    // Chuyển viên kẹo được kéo sang bên phải
        }
        // Left Swipe
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            otherCandy = board.allCandys[column - 1, row];  // Lấy vị trí kẹo bên trái
            previousRow = row;
            previousColumn = column;
            otherCandy.GetComponent<Candy>().column += 1;   // Chuyển viên kẹo bên phải sang bên phải
            column -= 1;                                    // Chuyển viên kẹo được kéo sang bên trái
        }
        // Up Swipe
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        {
            otherCandy = board.allCandys[column, row + 1];  // Lấy vị trí kẹo bên trái
            previousRow = row;
            previousColumn = column;
            otherCandy.GetComponent<Candy>().row -= 1;   // Chuyển viên kẹo bên phải sang bên phải
            row += 1;                                    // Chuyển viên kẹo được kéo sang bên trái
        }
        // Down Swipe
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            otherCandy = board.allCandys[column, row - 1];  // Lấy vị trí kẹo bên trái
            previousRow = row;
            previousColumn = column;
            otherCandy.GetComponent<Candy>().row += 1;   // Chuyển viên kẹo bên phải sang bên phải
            row -= 1;                                    // Chuyển viên kẹo được kéo sang bên trái
        }

        StartCoroutine(CheckMoveCor());
    }

    public IEnumerator CheckMoveCor()
    {
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
                board.currentStage = GameState.move;
                board.currentCandy = null;
            }
            else
            {
                board.DestroyMatches();
            }
            //otherCandy = null;
        }
    }

    void FindMatches()
    {
        // Kiểm tra xem viên kẹo bên trái và bên phải có cùng tag với viên kẹo hiện tại không
        if (column > 0 && column < board.width - 1)
        {
            GameObject leftCandy1 = board.allCandys[column - 1, row];
            GameObject rightCandy1 = board.allCandys[column + 1, row];
            if (leftCandy1 != null && rightCandy1 != null)
            {
                if (gameObject.CompareTag(leftCandy1.tag) && gameObject.CompareTag(rightCandy1.tag))
                {
                    leftCandy1.GetComponent<Candy>().isMatched = true;
                    rightCandy1.GetComponent<Candy>().isMatched = true;
                    isMatched = true;
                }
            }

        }
        // Kiểm tra xem viên kẹo bên trên và bên dưới có cùng tag với viên kẹo hiện tại không
        if (row > 0 && row < board.height - 1)
        {
            GameObject downCandy1 = board.allCandys[column, row - 1];
            GameObject upCandy1 = board.allCandys[column, row + 1];
            if (upCandy1 != null && downCandy1 != null)
            {
                if (gameObject.CompareTag(downCandy1.tag) && gameObject.CompareTag(upCandy1.tag))
                {
                    downCandy1.GetComponent<Candy>().isMatched = true;
                    upCandy1.GetComponent<Candy>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }


    public void MakeRowBomb()
    {
        isRowBomb = true;
        GameObject sugar = Instantiate(rowSugar, transform.position, Quaternion.identity);
        sugar.transform.parent = this.transform;
    }
    public void MakeColumnBomb()
    {
        isColumnBomb = true;
        GameObject sugar = Instantiate(columnSugar, transform.position, Quaternion.identity);
        sugar.transform.parent = this.transform;
    }
}
