using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    public int hitPoints;
    private SpriteRenderer sprite;
    public GameObject target;
    //public GameObject breakEffect;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        target = GameObject.Find("Target");
    }

    private void Update()
    {
        if (hitPoints <= 0)
        {
            StartCoroutine(BreakalbeDestroyEffect());

        }
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
    }
                        
    public IEnumerator BreakalbeDestroyEffect()
    {
        transform.DOMove(target.transform.position, 2f, false);
        //transform.DORotate(new Vector3(0f, 0f, 360f), 1.5f, RotateMode.FastBeyond360);
        yield return new WaitForSeconds(2.5f);
        DOTween.Kill(transform);
        Destroy(gameObject);
    }
}
