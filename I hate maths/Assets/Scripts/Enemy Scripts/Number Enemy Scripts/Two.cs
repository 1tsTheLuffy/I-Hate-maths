using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Two : MonoBehaviour
{
    private int health = 1;
    [SerializeField] float speed;
    [SerializeField] float frequency;
    [SerializeField] float magnitude;

    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject destroyParticle;

    [SerializeField] Transform shootPoint;

    private CameraShake shake;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        timer = timeBtwSpawn;
    }

    private void Update()
    {
        if (shake == null)
            return;

        if(timer <= 0)
        {
            Shoot();
            float randomTime = Random.Range(.05f,.1f);
            timer = randomTime;
        }else
        {
            timer -= Time.deltaTime;
        }

        if(transform.position.x < -25)
        {
            Destroy(gameObject);
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    private GameObject Shoot()
    {
        GameObject instance = Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
        return instance;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.y = Mathf.Sin(Time.time * frequency) * magnitude;
        transform.position = pos;
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet1") || collision.CompareTag("TriangleBullet"))
        {
            Destroy(collision.transform.gameObject);
            ThisShake(.1f, 1f, 1f);
            health = 0;
        }

        if(collision.CompareTag("Electric"))
        {
            health = 0;
            ThisShake(.1f, 1f, 1f);
        }
    }

    void ThisShake(float duration, float amp, float frequency = 1f)
    {
        shake.elapsedTime = duration;
        shake.shakeAmplitude = amp;
        shake.shakeFrequency = frequency;
    }

    private void OnDestroy()
    {
        GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(instance, 1f);
    }

}
