using UnityEngine;

public class UiTopCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chocolate")||
            collision.gameObject.CompareTag("Biscuit")||
            collision.gameObject.CompareTag("Breakable"))
        {
            Destroy(collision.gameObject);
            Level_Data.Instance.dSpecialBlockAmount--;
            if (Level_Data.Instance.dSpecialBlockAmount <= 0)
            {
                FindObjectOfType<Board>().currentState = GameState.pause;
                EndGameManager.instance.SetWinGame();
            }
        }
    }
}
