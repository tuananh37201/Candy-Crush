using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    public int hitPoints;
    private SpriteRenderer sprite;
    public GameObject breakEffect;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (hitPoints <= 0)
        {
            StartCoroutine(breakalbeDestroyEffect());
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
    }

    public IEnumerator breakalbeDestroyEffect()
    {
        this.gameObject.SetActive(false);
        GameObject Effect = Instantiate(breakEffect, transform.position, Quaternion.identity);
        Destroy(Effect, .3f);
        yield return new WaitForSeconds(.1f);
    }
}
