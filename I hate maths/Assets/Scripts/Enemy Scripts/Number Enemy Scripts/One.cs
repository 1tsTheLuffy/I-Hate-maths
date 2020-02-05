using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One : MonoBehaviour
{
    private int health;
    private Vector2 target;
    private float distance;
    [SerializeField] float speed;

    [SerializeField] GameObject destroyParticle;

    private Transform bus;

    private Rigidbody2D rb;
    [SerializeField] DreamBusController controller;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bus = GameObject.FindGameObjectWithTag("DreamBus").transform;
        controller = GameObject.FindGameObjectWithTag("DreamBus").GetComponent<DreamBusController>();
        health = 1;
    }

    private void LateUpdate()
    {
        target = new Vector2(bus.position.x, bus.position.y);
        
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, bus.position);
        

        if (health == 0)
        {
            Destroy(gameObject);
            Instantiate(destroyParticle, transform.position, Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        if (distance < 24f && (bus.position.x < transform.position.x))
        {
            transform.Rotate(0f, 0f, 8f);
            transform.position = Vector2.MoveTowards(transform.position, target, (speed * speed * 1.2f) * Time.fixedDeltaTime);
        }
        else
        {
            float randomUp = Random.Range(10f,20f);
            rb.AddForce(Vector2.up * randomUp, ForceMode2D.Force);
            transform.Translate(Vector2.left * speed * 2 * Time.fixedDeltaTime);
            float randomDown = Random.Range(10f, 20f);
            rb.AddForce(Vector2.down * randomDown, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet1") || collision.CompareTag("TriangleBullet"))
        {
            health = 0;
            controller.elapsedTime = .1f;
            Destroy(collision.transform.gameObject);
        }
        if(collision.CompareTag("DreamBus"))
        {
            health = 0;
            controller.shakeAmplitude = 2f;
            controller.shakeFrequency = 1f;
            controller.elapsedTime = .1f;
            controller.health--;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        
    }
}
