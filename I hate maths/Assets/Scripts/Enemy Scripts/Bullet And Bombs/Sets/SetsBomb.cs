using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetsBomb : MonoBehaviour
{
    private int health = 1;
    [Range(0f,5f)]
    [SerializeField] float radius;
    [SerializeField] float timer;
    [SerializeField] float timeBtw;

    [SerializeField] GameObject[] objects;
    [SerializeField] GameObject[] destroyParticle;

    private DreamBusController controller;

    [SerializeField] Transform[] point;

    [SerializeField] LayerMask Enemy;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        controller = GameObject.FindGameObjectWithTag("DreamBus").GetComponent<DreamBusController>();

        timer = timeBtw;
        health = 1;
    }

    private void Update()
    {
        if (controller == null)
        {
            return;
        }

        if(health <= 0)
        {
            //Instantiate(destroyParticle[0], transform.position, Quaternion.identity);
            //Instantiate(destroyParticle[1], transform.position, Quaternion.identity);

            

            Destroy(gameObject);
        }

        Blast();
    }

    private void Blast()
    {
        if (timer <= 0)
        {
            Collider2D[] Obj = Physics2D.OverlapCircleAll(transform.position, radius, Enemy);
            for (int i = 0; i < Obj.Length; i++)
            {
                Destroy(Obj[i].transform.gameObject);
                Destroy(gameObject);
            }
            health = 0;
            for (int i = 0; i < 2; i++)
            {
                int x = Random.Range(0, objects.Length);
                Instantiate(objects[x], point[i].position, Quaternion.identity);
            }
            Instantiate(destroyParticle[0], transform.position, Quaternion.identity);
            Instantiate(destroyParticle[1], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }else
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DreamBus"))
        {
            Destroy(gameObject);
            Instantiate(destroyParticle[0], transform.position, Quaternion.identity);
            Instantiate(destroyParticle[1], transform.position, Quaternion.identity);
            controller.health -= 1;   
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
