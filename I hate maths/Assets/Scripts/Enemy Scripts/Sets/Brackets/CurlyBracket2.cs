using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlyBracket2 : MonoBehaviour
{
    private int health = 1;
    [SerializeField] float speed;
    [SerializeField] float frequency;
    [SerializeField] float magnitude;
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    [SerializeField] GameObject[] bullet;
    [SerializeField] GameObject[] destroyParticle;

    [SerializeField] Transform Point;
    [SerializeField] Transform[] shootPoint;

    Rigidbody2D rb;
    CameraShake shake;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        health = 1;
        timer = timeBtwSpawn;
    }

    private void Update()
    {
        if(timer <= 0)
        {
            Instantiate(bullet[0], shootPoint[0].position, shootPoint[0].rotation);
            Instantiate(bullet[1], shootPoint[1].position, shootPoint[1].rotation);
            timer = timeBtwSpawn;
        }else
        {
            timer -= Time.deltaTime;
        }

        if(transform.position.x < -21f)
        {
            Destroy(gameObject);
        }

        if(health <= 0)
        {
            shake.elapsedTime = .1f;
            shake.shakeAmplitude = 2f;
            shake.shakeFrequency = 1f;
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0f, 0f, 10f);
       // transform.RotateAround(transform, Vector3.forward, -25f * Time.fixedDeltaTime);
        Vector2 pos = transform.position;
        pos.y = Mathf.Tan(Time.time * frequency) * magnitude;
        transform.position = pos;
        //transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
        transform.position = Vector2.MoveTowards(transform.position, Point.position, speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet1"))
        {
            health = 0;
            shake.C_Shake(.01f,1f,1f);
        }
        if (collision.CompareTag("TriangleBullet"))
        {
            shake.C_Shake(.1f, 2.5f, 1f);
            health = 0;
        }
        if (collision.CompareTag("Electric"))
        {
            health = 0;
            shake.C_Shake(.1f, 1f, 1f);
        }

        if(collision.CompareTag("DreamBus"))
        {
            health = 0;
            shake.C_Shake();
        }
    }

    private void OnDestroy()
    {
        GameObject instance_1 = Instantiate(destroyParticle[0], transform.position,Quaternion.identity);
        GameObject instacne_2 = Instantiate(destroyParticle[1], transform.position, Quaternion.identity);
        Destroy(instance_1, 1.2f);
        Destroy(instacne_2, 1.2f);
    }
}
