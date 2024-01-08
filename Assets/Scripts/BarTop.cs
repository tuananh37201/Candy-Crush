using UnityEngine;

public class BarTop : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        // Kiểm tra xem đối tượng va chạm có tag là "Choco" không
        if (collision.CompareTag("Chocolate")) {
            EndGameManagerChocolate.instance.movedChoco++;
            EndGameManagerChocolate.instance.chocoAmonutToGet -=
                EndGameManagerChocolate.instance.movedChoco;
            EndGameManagerChocolate.instance.movedChoco = 0;
        }
    }
}
