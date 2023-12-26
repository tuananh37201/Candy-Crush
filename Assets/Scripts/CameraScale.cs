using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    private Board board;
    public float cameraOffset;
    public float aspectRatio = 0.625f;
    public float padding = 2;

    void Start()
    {
        board = FindAnyObjectByType<Board>();
        if(board != null)
        {
            RepositionCamera(board.width - 1, board.height - 1); 
        }
    }

    void RepositionCamera(float x, float y)
    {
        Vector2 tempPosition = new Vector3(x/2, y/2, cameraOffset);
        transform.position = tempPosition;
        if(board.width >= board.height)
        {
            Camera.main.orthographicSize = (board.width /2 + padding) / aspectRatio;
        }
        else
        {
            Camera.main.orthographicSize = (board.height / 2 + padding);
        }
    }
}
