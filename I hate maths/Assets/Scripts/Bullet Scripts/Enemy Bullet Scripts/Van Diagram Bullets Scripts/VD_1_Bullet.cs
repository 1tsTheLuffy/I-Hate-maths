using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VD_1_Bullet : MonoBehaviour
{
    [SerializeField] float speed;

    private Vector2 moveDirection;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("Disable", 0, 3f);
    }

    private void FixedUpdate()
    {
        transform.Translate(moveDirection * speed * Time.fixedDeltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DreamBus"))
        {
            gameObject.SetActive(false);
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
