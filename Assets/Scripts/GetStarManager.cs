using UnityEngine;

public class GetStarManager : MonoBehaviour {
    public int scoreToGetOneStar, scoreToGetTwoStar, scoreToGetThreeStar;
    public GameObject yellowStar1, yellowStar2, yellowStar3;
    public Transform yellowStar1Pos, yellowStar2Pos, yellowStar3Pos;
    private bool isSpawingStar1 = false;
    private bool isSpawingStar2 = false;
    private bool isSpawingStar3 = false;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        SpawnStar();
    }

    private void SpawnStar() {
        if (FindObjectOfType<ScoreManager>().score >= scoreToGetOneStar) {
            SpawnStar1();
        }
        if (FindObjectOfType<ScoreManager>().score >= scoreToGetTwoStar) {
            SpawnStar2();
        }
        if (FindObjectOfType<ScoreManager>().score >= scoreToGetThreeStar) {
            SpawnStar3();
        }
    }

    private void SpawnStar1() {
        if (!isSpawingStar1) {
            GameObject YellowStar1 = Instantiate(yellowStar1, yellowStar1Pos.position, Quaternion.identity);
            YellowStar1.transform.parent = yellowStar1Pos.transform;
            isSpawingStar1 = true;
        }
    }

    private void SpawnStar2() {
        if (!isSpawingStar2) {
            GameObject YellowStar2 = Instantiate(yellowStar2, yellowStar2Pos.position, Quaternion.identity);
            YellowStar2.transform.parent = yellowStar2Pos.transform;
            isSpawingStar2 = true;
        }
    }
    private void SpawnStar3() {
        if (!isSpawingStar3) {
            GameObject YellowStar3 = Instantiate(yellowStar3, yellowStar3Pos.position, Quaternion.identity);
            YellowStar3.transform.parent = yellowStar3Pos.transform;
            isSpawingStar3 = true;
        }
    }
}
