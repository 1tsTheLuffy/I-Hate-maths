using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CbBullet1 : MonoBehaviour
{
    [SerializeField] float fireSpeed;

    private GameObject instance;
    [SerializeField] GameObject destroyParticle;

    [SerializeField] Vector2 MoveDirection;


    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

     //   InvokeRepeating("Disable", 0, 2);
    }

    private void Update()
    {
        Destroy(instance, 2f);
    }

    private void FixedUpdate()
    {
        transform.Translate(MoveDirection * fireSpeed * Time.fixedDeltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        MoveDirection = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DreamBus"))
        {
            instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    //private void OnDisable()
    //{
    //    GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
    //    Destroy(instance, 1f);
    //}
}
