using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Two : MonoBehaviour
{
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
            float randomTime = Random.Range(.3f, .6f);
            timer = randomTime;
        }else
        {
            timer -= Time.deltaTime;
        }

        if(transform.position.x < -25)
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
            Destroy(gameObject);
            Destroy(collision.transform.gameObject);
            shake.elapsedTime = .1f;
            shake.shakeAmplitude = 1f;
            shake.shakeFrequency = 1f;
        }
    }

    private void OnDestroy()
    {
        GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(instance, 1f);
    }

}
