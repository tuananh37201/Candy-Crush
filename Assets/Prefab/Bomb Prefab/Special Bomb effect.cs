using System.Collections;
using UnityEngine;


public class SpecialBombeffect : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Effect());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Effect()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("OnBoard", true);
    }
}
