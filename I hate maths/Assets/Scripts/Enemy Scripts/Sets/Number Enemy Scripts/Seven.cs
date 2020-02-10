using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seven : MonoBehaviour
{
    private int health = 1;
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject destroyParticle;

    private Transform bus;
    [SerializeField] Transform shootPoint;


    PlayerScoreManager sm;

    Rigidbody2D rb;
    Animator animator;
    CameraShake shake;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sm = GameObject.FindGameObjectWithTag("SM").GetComponent<PlayerScoreManager>();
        bus = GameObject.FindGameObjectWithTag("DreamBus").transform;
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        health = 1;
    }

    private void Update()
    {
        if (bus == null)
            return;

        Vector2 direction = new Vector2(bus.position.x - transform.position.x, bus.position.y - transform.position.y);
        transform.right = -direction;

        if(timer <= 0)
        {
            animator.SetBool("isFire", true);
            shake.C_Shake(.1f, .5f, .5f);
            Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            timer = timeBtwSpawn;
        }else
        {
            timer -= Time.deltaTime;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet1"))
        {
            Destroy(collision.transform.gameObject);
            health = 0;
            sm.score++;
            shake.C_Shake(.1f, .5f, .5f);
        }
        if (collision.CompareTag("TriangleBullet"))
        {
            shake.C_Shake(.1f, 2.5f, 1f);
            health = 0;
        }
        if (collision.CompareTag("Electric"))
        {
            health = 0;
            sm.score++;
            shake.C_Shake(.1f, 1f, 1f);
        }
    }

    private void OnDestroy()
    {
        GameObject instance = Instantiate(destroyParticle ,transform.position, Quaternion.identity);
        Destroy(instance, 1f);
    }
}
