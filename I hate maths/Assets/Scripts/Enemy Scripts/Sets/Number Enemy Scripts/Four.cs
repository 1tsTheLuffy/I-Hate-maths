using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Four : MonoBehaviour
{
    private int health;
    [SerializeField] float speed;
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    [SerializeField] GameObject Bomb;
    [SerializeField] GameObject destroyParticle;

    [SerializeField] Transform spawnPoint;

    PlayerScoreManager sm;

    Rigidbody2D rb;
    CameraShake shake;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
        sm = GameObject.FindGameObjectWithTag("SM").GetComponent<PlayerScoreManager>();

        health = 1;

        timer = timeBtwSpawn;
    }

    private void Update()
    {
        if (timer <= 0)
        {
            Instantiate(Bomb, spawnPoint.position, Quaternion.identity);
            timer = timeBtwSpawn;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if(transform.position.x < -22f)
        {
            Destroy(gameObject);
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
            shake.C_Shake(.1f, 1f, 1f);
        }   
        if(collision.CompareTag("TriangleBullet") || collision.CompareTag("ShieldBullet"))
        {
            health = 0;
            sm.score++;
            shake.C_Shake(.1f, 1f, 1f);
        }
        if(collision.CompareTag("Electric"))
        {
            health = 0;
            shake.C_Shake(.1f, 1f, 1f);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        Debug.Log("OUT!!");
    }

    private void OnDestroy()
    {
        GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(instance, 1.2f);
    }
}
