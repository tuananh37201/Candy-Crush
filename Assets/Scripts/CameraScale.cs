using UnityEngine;

public class CameraScale : MonoBehaviour
{
    private Board board;
    public float cameraOffset = -1;
    public float aspectRatio = 0.625f;
    public float padding = 2;
    public float yOffset = 1;
    public int boardWidth;
    public int boardHeight;
    private bool hasRunOnce = false;

    private void Awake()
    {
        
    }
    void Start()
    {

    }

    private void Update()
    {
        if (!hasRunOnce)
        {
            boardWidth = Board.Instance.width;
            boardHeight = Board.Instance.height;
            Debug.Log(boardWidth);
            RepositionCamera(Board.Instance.width - 1, Board.Instance.height - 1);
            hasRunOnce = true;
        }
    }

    void RepositionCamera(float x, float y)
    {
        Vector3 tempPosition = new Vector3(x / 2, y / 2 + yOffset, cameraOffset);
        transform.position = tempPosition;
        if (boardWidth >= boardHeight   )
        {
            Camera.main.orthographicSize = (boardWidth / 2 + padding) / aspectRatio;
        }
        else
        {
            Camera.main.orthographicSize = (boardWidth / 2 + padding) + 2 * yOffset;
        }
    }
}
