using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SevenBullet : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] GameObject destroyParticle;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Destroy(gameObject, 2f);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
    }

    private void OnDestroy()
    {
        GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(instance, 1f);
    }
}
