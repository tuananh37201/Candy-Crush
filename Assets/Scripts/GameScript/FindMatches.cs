using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

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

    private List<GameObject> IsAdjacentBomb(Candy candy1, Candy candy2, Candy candy3)
    {
        List<GameObject> currentCandys = new List<GameObject>();

        if (candy1.isAdjacentBomb)
        {
            currentMatches.Union(GetAdjacentPieces(candy1.column, candy1.row));
        }
        if (candy2.isAdjacentBomb)
        {
            currentMatches.Union(GetAdjacentPieces(candy2.column, candy2.row));
        }
        if (candy3.isAdjacentBomb)
        {
            currentMatches.Union(GetAdjacentPieces(candy3.column, candy3.row));
        }
        return currentCandys;
    }

    private List<GameObject> IsRowBomb(Candy candy1, Candy candy2, Candy candy3)
    {
        List<GameObject> currentCandys = new List<GameObject>();

        if (candy1.isRowBomb)
        {
            currentMatches.Union(GetRowPieces(candy1.row));
            board.BomRow(candy1.row);
        }
        if (candy2.isRowBomb)
        {
            currentMatches.Union(GetRowPieces(candy2.row));
            board.BomRow(candy2.row);
        }
        if (candy3.isRowBomb)
        {
            currentMatches.Union(GetRowPieces(candy3.row));
            board.BomRow(candy3.row);
        }
        return currentCandys;
    }

    private List<GameObject> IsColumnBomb(Candy candy1, Candy candy2, Candy candy3)
    {
        List<GameObject> currentCandys = new List<GameObject>();

        if (candy1.isColumnBomb)
        {
            currentMatches.Union(GetColumnPieces(candy1.column));
            board.BomRow(candy1.column);
        }
        if (candy2.isColumnBomb)
        {
            currentMatches.Union(GetColumnPieces(candy2.column));
            board.BomRow(candy2.column);
        }
        if (candy3.isColumnBomb)
        {
            currentMatches.Union(GetColumnPieces(candy3.column));
            board.BomRow(candy3.column);
        }
        return currentCandys;
    }

    private void AddToListAndMatch(GameObject candy)
    {
        if (!currentMatches.Contains(candy))
        {
            currentMatches.Add(candy);
        }
        candy.GetComponent<Candy>().isMatched = true;
    }

    private void GetNearbyPieces(GameObject candy1, GameObject candy2, GameObject candy3)
    {
        AddToListAndMatch(candy1);
        AddToListAndMatch(candy2);
        AddToListAndMatch(candy3);
    }

    public IEnumerator FindAllMatchesCor()
    {
//=======================  
      //yield return new WaitForSeconds(.1f);
//=======================     
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                GameObject currentCandy = board.allCandys[i, j];
                if (currentCandy != null)
                {
                    Candy _currentCandy = currentCandy.GetComponent<Candy>();
                    // Check column
                    if (i > 0 && i < board.width - 1)
                    {
                        GameObject leftCandy = board.allCandys[i - 1, j];
                        GameObject rightCandy = board.allCandys[i + 1, j];
                        if (leftCandy != null && rightCandy != null)
                        {
                            Candy _leftCandy = leftCandy.GetComponent<Candy>();
                            Candy _rightCandy = rightCandy.GetComponent<Candy>();
                            if (leftCandy != null && rightCandy != null)
                            {
                                // Check 2 viên bên phải và trái cùng màu hay ko
                                if (currentCandy.CompareTag(leftCandy.tag) && currentCandy.CompareTag(rightCandy.tag))
                                {
                                    // If is a Row Bomb
                                    currentMatches.Union(IsRowBomb(_leftCandy, _currentCandy, _rightCandy));
                                    // If is a Column Bomb
                                    currentMatches.Union(IsColumnBomb(_leftCandy, _currentCandy, _rightCandy));
                                    // If is a Adjacent Bomb
                                    currentMatches.Union(IsAdjacentBomb(_leftCandy, _currentCandy, _rightCandy));


                                    // Get nearby candys
                                    GetNearbyPieces(leftCandy, currentCandy, rightCandy);
                                }
                            }
                        }
                    }

                    // Check row
                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upCandy = board.allCandys[i, j + 1];
                        GameObject downCandy = board.allCandys[i, j - 1];
                        if (upCandy != null && downCandy != null)
                        {
                            Candy _upCandy = upCandy.GetComponent<Candy>();
                            Candy _downCandy = downCandy.GetComponent<Candy>();
                            if (upCandy != null && downCandy != null)
                            {
                                // Check 2 viên bên trên và dưới cùng màu hay ko
                                if (currentCandy.CompareTag(upCandy.tag) && currentCandy.CompareTag(downCandy.tag))
                                {
                                    // If is a Column Bomb
                                    currentMatches.Union(IsColumnBomb(_upCandy, _currentCandy, _downCandy));
                                    // If is a Row Bomb
                                    currentMatches.Union(IsRowBomb(_upCandy, _currentCandy, _downCandy));
                                    // If is a Adjacent Bomb
                                    currentMatches.Union(IsAdjacentBomb(_upCandy, _currentCandy, _downCandy));

                                    // Get nearby candys
                                    GetNearbyPieces(upCandy, currentCandy, downCandy);
                                }
                            }
                        }
                    }
                }
//=======================                
                yield return null;
//=======================                
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
                    if (board.allCandys[i, j].CompareTag(color))
                    {
                        // Set that dot to be matched
                        board.allCandys[i, j].GetComponent<Candy>().isMatched = true;
                    }
                }
            }
        }
    }

    List<GameObject> GetAdjacentPieces(int column, int row)
    {
        List<GameObject> candys = new List<GameObject>();
        for (int i = column - 1; i <= column + 1; i++)
        {
            for (int j = row - 1; j <= row + 1; j++)
            {
                // Check if the piece is inside board
                if (i >= 0 && i < board.width && j >= 0 && j < board.height)
                {
                    if (board.allCandys[i, j] != null)
                    {
//=======================                       
                        // Candy candy = board.allCandys[column, i].GetComponent<Candy>();
                        // if (candy.isRowBomb)
                        // {
                        //     candys.Union(GetRowPieces(i)).ToList();
                        // }
//=======================                     
                        candys.Add(board.allCandys[i, j]);
                        board.allCandys[i, j].GetComponent<Candy>().isMatched = true;
                    }
                }
            }
        }
        return candys;
    }

    // Cho tất cả cột thành isMatched
    List<GameObject> GetColumnPieces(int column)
    {
        List<GameObject> candys = new List<GameObject>();
        for (int i = 0; i < board.height; i++)
        {
//=======================            
            // if (board.allCandys[column, i] != null)
            // {
            //     candys.Add(board.allCandys[column, i]);
            //     board.allCandys[column, i].GetComponent<Candy>().isMatched = true;
            // }

            Candy candy = board.allCandys[column, i].GetComponent<Candy>();
            if(candy.isRowBomb)
            {
                candys.Union(GetRowPieces(i)).ToList();
            }

            candys.Add(board.allCandys[column, i]);
            candy.isMatched = true;
//=======================            
        }
        return candys;
    }

    // Cho tất cả hàng thành isMatched
    List<GameObject> GetRowPieces(int row)
    {
        List<GameObject> candys = new List<GameObject>();
        for (int i = 0; i < board.width; i++)
        {
//=======================            
            // if (board.allCandys[i, row] != null)
            // {
            //     candys.Add(board.allCandys[i, row]);
            //     board.allCandys[i, row].GetComponent<Candy>().isMatched = true;
            // }

            Candy candy = board.allCandys[i, row].GetComponent<Candy>();
            if(candy.isColumnBomb)
            {
                candys.Union(GetColumnPieces(i)).ToList();
            }

            candys.Add(board.allCandys[i, row]);
            candy.isMatched = true;
//=======================            
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
