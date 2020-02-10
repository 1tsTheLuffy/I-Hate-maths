using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBullet : MonoBehaviour
{
    [SerializeField] float speed;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }
}
