using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Two : MonoBehaviour
{
    private int health = 2;
    [SerializeField] float speed;

    [SerializeField] GameObject one;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet1") || collision.CompareTag("TriangleBullet"))
        {
            Destroy(gameObject);
        }
    }
           
}
