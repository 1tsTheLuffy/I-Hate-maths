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
    PlayerScoreManager sm;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
        sm = GameObject.FindGameObjectWithTag("SM").GetComponent<PlayerScoreManager>();
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
      //  transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet1"))
        {
            Destroy(collision.transform.gameObject);
            shake.C_Shake(.1f, 2f, 1f);
            sm.score++;
            health = 0;
        }

        if(collision.CompareTag("Electric"))
        {
            health = 0;
            shake.C_Shake(.1f, 1f, 1f);
            sm.score++;
        }
    }

    private void OnDestroy()
    {
        GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(instance, 1f);
    }
}
