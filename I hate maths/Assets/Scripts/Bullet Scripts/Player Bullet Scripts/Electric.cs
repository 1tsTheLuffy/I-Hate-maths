using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric : MonoBehaviour
{
    [SerializeField] float speed;

    private GameObject instance;
    [SerializeField] GameObject shockParticles;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Destroy(instance, .6f);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            instance = Instantiate(shockParticles, collision.transform.position, Quaternion.identity);
        }

        if (collision.CompareTag("EnemyBullet"))
        {
            instance = Instantiate(shockParticles, collision.transform.position, Quaternion.identity);
            Destroy(collision.transform.gameObject);
        }
    }
}
