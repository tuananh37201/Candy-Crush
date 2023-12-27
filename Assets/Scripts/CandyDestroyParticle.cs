using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroyParticle : MonoBehaviour
{
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isDestroyed", true);
    }


}
