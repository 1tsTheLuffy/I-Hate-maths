using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float force;

    [SerializeField] GameObject destroyParticle;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Destroy(gameObject, 8f);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet1") || collision.CompareTag("Electric"))
        {
            Destroy(gameObject);
            Destroy(collision.transform.gameObject);
        }
    }

    private void OnDestroy()
    {
        GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(instance, 1f);
    }
}
