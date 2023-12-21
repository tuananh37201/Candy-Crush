using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
                    if (i > 0 && i < board.width - 1)
                    {
                        GameObject leftCandy = board.allCandys[i - 1, j];
                        GameObject rightCandy = board.allCandys[i + 1, j];
                        if (leftCandy != null && rightCandy != null)
                        {
                            if (currentCandy.CompareTag(leftCandy.tag) && currentCandy.CompareTag(rightCandy.tag))
                            {
                                if(currentCandy.GetComponent<Candy>().isRowBomb || leftCandy.GetComponent<Candy>().isRowBomb || rightCandy.GetComponent<Candy>().isRowBomb)
                                {
                                    currentMatches.Union(GetRowPieces(j));
                                }

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
                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upCandy = board.allCandys[i, j + 1];
                        GameObject downCandy = board.allCandys[i, j - 1];
                        if (upCandy != null && downCandy != null)
                        {
                            if (currentCandy.CompareTag(upCandy.tag) && currentCandy.CompareTag(downCandy.tag))
                            {
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

}
