using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

public class FindMatches : MonoBehaviour
{
    private Board board;
    public List<GameObject> currentMatches = new List<GameObject>();

    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCor());
    }

    private IEnumerator FindAllMatchesCor()
    {
        yield return new WaitForSeconds(.1f);
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                GameObject currentCandy = board.allCandys[i, j];
                if (currentCandy != null)
                {
                    // Check hàng
                    if (i > 0 && i < board.width - 1)
                    {
                        GameObject leftCandy = board.allCandys[i - 1, j];
                        GameObject rightCandy = board.allCandys[i + 1, j];
                        if (leftCandy != null && rightCandy != null)
                        {
                            // Check 2 viên bên phải và trái cùng màu hay ko
                            if (currentCandy.CompareTag(leftCandy.tag) && currentCandy.CompareTag(rightCandy.tag))
                            {
                                // Check 1 trong 3 viên có Row Bomb không
                                if (currentCandy.GetComponent<Candy>().isRowBomb || leftCandy.GetComponent<Candy>().isRowBomb || rightCandy.GetComponent<Candy>().isRowBomb)
                                {
                                    currentMatches.Union(GetRowPieces(j));
                                }
                                // Nếu là ColumnBomb
                                if (currentCandy.GetComponent<Candy>().isColumnBomb)
                                {
                                    currentMatches.Union(GetColumnPieces(i));
                                }
                                if (leftCandy.GetComponent<Candy>().isColumnBomb)
                                {
                                    currentMatches.Union(GetColumnPieces(i - 1));
                                }
                                if (rightCandy.GetComponent<Candy>().isColumnBomb)
                                {
                                    currentMatches.Union(GetColumnPieces(i + 1));
                                }

                                //----------------------------------------------------
                                if (!currentMatches.Contains(leftCandy))
                                {
                                    currentMatches.Add(leftCandy);
                                }
                                leftCandy.GetComponent<Candy>().isMatched = true;

                                if (!currentMatches.Contains(rightCandy))
                                {
                                    currentMatches.Add(rightCandy);
                                }
                                rightCandy.GetComponent<Candy>().isMatched = true;

                                if (!currentMatches.Contains(currentCandy))
                                {
                                    currentMatches.Add(currentCandy);
                                }
                                currentCandy.GetComponent<Candy>().isMatched = true;
                            }
                        }
                    }

                    // Check cột
                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upCandy = board.allCandys[i, j + 1];
                        GameObject downCandy = board.allCandys[i, j - 1];
                        if (upCandy != null && downCandy != null)
                        {
                            // Check 2 viên bên trên và dưới cùng màu hay ko
                            if (currentCandy.CompareTag(upCandy.tag) && currentCandy.CompareTag(downCandy.tag))
                            {
                                // Check 1 trong 3 viên có Column Bomb không
                                if (currentCandy.GetComponent<Candy>().isColumnBomb || upCandy.GetComponent<Candy>().isColumnBomb || downCandy.GetComponent<Candy>().isColumnBomb)
                                {
                                    currentMatches.Union(GetColumnPieces(i));
                                }
                                // Nếu là Row
                                if (currentCandy.GetComponent<Candy>().isRowBomb)
                                {
                                    currentMatches.Union(GetRowPieces(j));
                                }
                                if (upCandy.GetComponent<Candy>().isRowBomb)
                                {
                                    currentMatches.Union(GetRowPieces(j + 1));
                                }
                                if (downCandy.GetComponent<Candy>().isRowBomb)
                                {
                                    currentMatches.Union(GetRowPieces(i - 1));
                                }

                                //----------------------------------------------------
                                if (!currentMatches.Contains(upCandy))
                                {
                                    currentMatches.Add(upCandy);
                                }
                                upCandy.GetComponent<Candy>().isMatched = true;

                                if (!currentMatches.Contains(downCandy))
                                {
                                    currentMatches.Add(downCandy);
                                }
                                downCandy.GetComponent<Candy>().isMatched = true;

                                if (!currentMatches.Contains(currentCandy))
                                {
                                    currentMatches.Add(currentCandy);
                                }
                                currentCandy.GetComponent<Candy>().isMatched = true;
                            }
                        }
                    }
                }
            }
        }
    }

    public void MacthPiecesColor(string color)
    {
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                // Check if that piece exists
                if (board.allCandys[i, j] != null)
                {
                    // Check the tag on that candy
                    if (board.allCandys[i, j].tag == color)
                    {
                        // Set that dot to be matched
                        board.allCandys[i, j].GetComponent<Candy>().isMatched = true;
                    }
                }
            }
        }
    }

    // Cho tất cả cột thành isMatched
    List<GameObject> GetColumnPieces(int column)
    {
        List<GameObject> candys = new List<GameObject>();
        for (int i = 0; i < board.height; i++)
        {
            if (board.allCandys[column, i] != null)
            {
                candys.Add(board.allCandys[column, i]);
                board.allCandys[column, i].GetComponent<Candy>().isMatched = true;
            }
        }
        return candys;
    }

    // Cho tất cả hàng thành isMatched
    List<GameObject> GetRowPieces(int row)
    {
        List<GameObject> candys = new List<GameObject>();
        for (int i = 0; i < board.width; i++)
        {
            if (board.allCandys[i, row] != null)
            {
                candys.Add(board.allCandys[i, row]);
                board.allCandys[i, row].GetComponent<Candy>().isMatched = true;
            }
        }
        return candys;
    }

    public void CheckBomb()
    {
        // Did player move somthing?
        if (board.currentCandy != null)
        {
            // Is the piece they move mathed?
            if (board.currentCandy.isMatched)
            {
                // Make it unmatched
                board.currentCandy.isMatched = false;

                // Decide what kind of bomb to make
                if ((board.currentCandy.swipeAngle > -45 && board.currentCandy.swipeAngle <= 45)
                || (board.currentCandy.swipeAngle < -135 || board.currentCandy.swipeAngle >= 135))
                {
                    board.currentCandy.MakeRowBomb();
                }
                else
                {
                    board.currentCandy.MakeColumnBomb();
                }
            }
            // Is the other piece matched?
            else if (board.currentCandy.otherCandy != null)
            {
                Candy otherCandy = board.currentCandy.otherCandy.GetComponent<Candy>();
                // Is other Candy unmatched?
                if (otherCandy.isMatched)
                {
                    // Make it unmatched
                    otherCandy.isMatched = false;

                    // Decide what kind of bomb make
                    if ((board.currentCandy.swipeAngle > -45 && board.currentCandy.swipeAngle <= 45)
                    || (board.currentCandy.swipeAngle < -135 || board.currentCandy.swipeAngle >= 135))
                    {
                        otherCandy.MakeRowBomb();
                    }
                    else
                    {
                        otherCandy.MakeColumnBomb();
                    }
                }
            }
        }
    }
}
