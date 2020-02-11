using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VD_1_Bullet : MonoBehaviour
{
    [SerializeField] float speed;

    private Vector2 moveDirection;

    [SerializeField] GameObject destroyParticle;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

       // InvokeRepeating("Disable", 0, 3f);
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

        if(collision.CompareTag("ShieldBullet") || collision.CompareTag("Electric"))
        {
            gameObject.SetActive(false);
            Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Debug.Log("HIT!!");
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
