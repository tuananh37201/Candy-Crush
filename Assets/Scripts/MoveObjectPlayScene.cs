using UnityEngine;
using DG.Tweening;

public class MoveObjectPlayScene : MonoBehaviour
{
    public GameObject panel, pausePanel;
    public Vector2 posAfter;
    public Vector2 posBefore;
    // Start is called before the first frame update
    void Start()
    { 
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PanelMove() {
        panel.transform.DOMove(posAfter,0.35f);
    }

    
}
