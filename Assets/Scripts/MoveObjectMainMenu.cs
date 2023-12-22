using UnityEngine;
using DG.Tweening;

public class MoveObjectMainMenu : MonoBehaviour
{
    public static MoveObjectMainMenu instance;
    public GameObject ExitPanel;
    public Vector3 exitPanelAfter;
    public Vector3 exitPanelBefore;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ExitPanel.transform.position = exitPanelBefore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void exitPanelMoveAfter() {
        ExitPanel.transform.DOMove(exitPanelAfter, 0.75f);
    }

    public void exitPanelMoveBefore() {
        ExitPanel.transform.DOMove(exitPanelBefore, 0.75f);
    }
}
