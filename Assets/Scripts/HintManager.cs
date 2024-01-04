using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    private Board board;
    public float hintDelay;
    private float hintDelaySeconds;
    public GameObject hintPartical;
    public List<GameObject> currentHints;

    void Start()
    {
        board = FindObjectOfType<Board>();
        hintDelaySeconds = hintDelay;
        currentHints = new List<GameObject>();
    }

    void Update()
    {
        hintDelaySeconds -= Time.deltaTime;
        if (hintDelaySeconds <= 0 && currentHints.Count == 0)
        {
            MarkHints();
            hintDelaySeconds = hintDelay;
        }
    }

    List<GameObject> FindAllMatches()
    {
        List<GameObject> possibleMoves = new List<GameObject>();
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                if (board.allCandys[i, j] != null)
                {

                }
            }
        }

        return possibleMoves;
    }

    private void MarkHints()
    {
        List<GameObject> moves = FindAllMatches();
        foreach (GameObject move in moves) 
        {
            GameObject hint = Instantiate(hintPartical, move.transform.position, Quaternion.identity);
            hint.transform.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
            currentHints.Add(hint);
        }
    }
    public void DestroyHints()
    {
        foreach (GameObject hint in currentHints)
        {
            Destroy(hint);
        }
        currentHints.Clear();
        hintDelaySeconds = hintDelay;
    }
}
