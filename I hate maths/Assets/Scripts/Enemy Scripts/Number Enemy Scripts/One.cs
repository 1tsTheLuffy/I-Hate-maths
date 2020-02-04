using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One : MonoBehaviour
{
    private Vector2 target;
    private float distance;
    [SerializeField] float speed;

    private Transform bus;

    private Rigidbody2D rb;
    [SerializeField] DreamBusController controller;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bus = GameObject.FindGameObjectWithTag("DreamBus").transform;
        controller = GameObject.FindGameObjectWithTag("DreamBus").GetComponent<DreamBusController>();
    }

    private void LateUpdate()
    {
        target = new Vector2(bus.position.x, bus.position.y);
        
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, bus.position);
        if(distance < 24f && (bus.position.x < transform.position.x))
        {
            transform.Rotate(0f, 0f, 8f);
            transform.position = Vector2.MoveTowards(transform.position, target, (speed * speed) * Time.fixedDeltaTime);
        }
        else
            transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet1") || collision.CompareTag("TriangleBullet"))
        {
            Destroy(gameObject);
            Destroy(collision.transform.gameObject);
        }
        if(collision.CompareTag("DreamBus"))
        {
            Destroy(gameObject);
            controller.health--;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
