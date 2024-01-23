using System.Collections;
using UnityEngine;

public class GetStarManager : MonoBehaviour {
    public static GetStarManager instance;
    public int scoreToGetOneStar, scoreToGetTwoStar, scoreToGetThreeStar;
    public GameObject yellowStar1, yellowStar2, yellowStar3;
    public Transform yellowStar1Pos, yellowStar2Pos, yellowStar3Pos;
    private bool spawnStar = false;
    private int[] yellowStarAmounts = new int[15];

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start() {
        LoadYellowStarAmountsFromPlayerPrefs();
    }

    // Update is called once per frame
    void Update() {
        if (GameObjectLV1.Instance.isWinPanel == true) {
            if (!spawnStar) {
                SpawnStar();
                spawnStar = true;
            }
        }
        SetAmountOfReachedStars();
    }

    private void LoadYellowStarAmountsFromPlayerPrefs() {
        for (int i = 0; i < yellowStarAmounts.Length; i++) {
            int level = i + 1;
            yellowStarAmounts[i] = PlayerPrefs.GetInt($"YellowStarLv{level}", 0);
        }
    }

    public void SetAmountOfReachedStars() {
        int levelToLoad = Level_Data.Instance.levelToLoad;

        if (levelToLoad >= 1 && levelToLoad <= 15) {
            GameObject[] yellowStarPrefabs = GameObject.FindGameObjectsWithTag("YellowStar");
            yellowStarAmounts[levelToLoad - 1] = yellowStarPrefabs.Length;
            PlayerPrefs.SetInt($"YellowStarLv{levelToLoad}", yellowStarAmounts[levelToLoad - 1]);
            //Debug.Log(yellowStarAmounts[levelToLoad - 1]);
        }
    }


    public void SpawnStar() {
        if (ScoreManager.Instance.score >= Level_Data.Instance.dScore1) {
            StartCoroutine(SpawnStar1());
        }
        if (ScoreManager.Instance.score >= Level_Data.Instance.dScore2) {
            StartCoroutine(SpawnStar2());
        }
        if (ScoreManager.Instance.score >= Level_Data.Instance.dScore3) {
            StartCoroutine(SpawnStar3());
        }
    }

    private IEnumerator SpawnStar1() {
        yield return new WaitForSeconds(1f);
        GameObject YellowStar1 = Instantiate(yellowStar1, yellowStar1Pos.position, Quaternion.identity);
        YellowStar1.transform.parent = yellowStar1Pos.transform;
    }

    private IEnumerator SpawnStar2() {
        yield return new WaitForSeconds(2f);
        GameObject YellowStar2 = Instantiate(yellowStar2, yellowStar2Pos.position, Quaternion.identity);
        YellowStar2.transform.parent = yellowStar2Pos.transform;
    }
    private IEnumerator SpawnStar3() {
        yield return new WaitForSeconds(3f);
        GameObject YellowStar3 = Instantiate(yellowStar3, yellowStar3Pos.position, Quaternion.identity);
        YellowStar3.transform.parent = yellowStar3Pos.transform;
    }
}
