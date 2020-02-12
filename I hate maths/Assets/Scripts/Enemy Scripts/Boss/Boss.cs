using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int stage;
    [SerializeField] float movementSpeed;
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    [SerializeField] Transform[] movePoints;

    Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        stage = Random.Range(1, 3);

        StartCoroutine(boss());
    }

    IEnumerator boss()
    {
        while(true)
        {
            if (stage == 1)
            {
                animator.SetBool("isGun", true);
                float delay = Random.Range(4f, 8f);
                yield return new WaitForSeconds(delay);
                stage = Random.Range(1, 3);
            }
            else if (stage == 2)
            {
                animator.SetBool("isThorn", true);
                float delay = Random.Range(4f, 8f);
                yield return new WaitForSeconds(delay);
                stage = Random.Range(1, 3);
            }
        }
    }
}
