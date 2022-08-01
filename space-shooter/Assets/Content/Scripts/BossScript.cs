using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{

    Animator animator;
    public GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(attack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator attack()
    {
        while (GameManager.instance.isGameOn)
        {
            yield return new WaitForSecondsRealtime(Random.Range(2f, 6f));
            //animator.SetBool("canAttack", true);
            animator.Play("bossAttacking", 0, 0);
            Instantiate(weapon, transform.position, Quaternion.identity);
            //animator.SetBool("canAttack", false);
        }
    }
}
