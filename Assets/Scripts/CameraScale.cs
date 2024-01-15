using UnityEngine;

public class CameraScale : MonoBehaviour
{
    private Board board;
    public float cameraOffset = -1;
    public float aspectRatio = 0.625f;
    public float padding = 2;
    public float yOffset = 1;

    private void Awake()
    {
        if (Board.Instance != null)
        {
            RepositionCamera(board.width - 1, board.height - 1);
        }
    }
    void Start()
    {

    }

    void RepositionCamera(float x, float y)
    {
        Vector3 tempPosition = new Vector3(x / 2, y / 2 + yOffset, cameraOffset);
        transform.position = tempPosition;
        if (board.width >= board.height)
        {
            Camera.main.orthographicSize = (board.width / 2 + padding) / aspectRatio;
        }
        else
        {
            Camera.main.orthographicSize = (board.height / 2 + padding) + 2 * yOffset;
        }
    }
}
