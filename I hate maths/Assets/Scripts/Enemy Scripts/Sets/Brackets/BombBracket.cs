using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBracket : MonoBehaviour
{
    private int health = 1;
    [SerializeField] float speed;
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    [SerializeField] GameObject bomb;
    [SerializeField] GameObject destroyParticle;

    [SerializeField] Transform spawnPoint;

    CameraShake shake;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }

    private void Update()
    {
        if(timer <= 0)
        {
            Instantiate(bomb, spawnPoint.position, Quaternion.identity);
            timer = timeBtwSpawn;
        }else
        {
            timer -= Time.deltaTime;
        }

        if(transform.position.x < -25f)
        {
            Destroy(gameObject);
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet1"))
        {
            shake.elapsedTime = .5f;
            shake.shakeAmplitude = 2f;
            shake.shakeFrequency = .5f;
            Destroy(collision.transform.gameObject);
            health = 0;
        }
    }

    private void OnDestroy()
    {
        GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(instance, 1f);
    }
}
