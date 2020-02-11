using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float destroyWaitTime;

    [SerializeField] GameObject shockParticle;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Destroy(gameObject, destroyWaitTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject instance = Instantiate(shockParticle, collision.transform.position, Quaternion.identity);
        Destroy(instance, 1f);

        if(collision.CompareTag("EnemyBullet") || collision.CompareTag("Enemy"))
        {
            Destroy(collision.transform.gameObject);
        }

        if(collision.CompareTag("EnemyPoolBullet"))
        {
            collision.transform.gameObject.SetActive(false);
        }
    }
}
