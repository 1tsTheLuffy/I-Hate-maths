using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cb4Bullet2 : MonoBehaviour
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
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }

    private void OnDestroy()
    {
        GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(gameObject, 1f);
    }
}
