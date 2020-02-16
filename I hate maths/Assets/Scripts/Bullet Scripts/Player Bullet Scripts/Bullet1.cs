using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    [SerializeField] float speed;

    private GameObject temp;
    [SerializeField] GameObject destroyParticle;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if(transform.position.x > 9.8f)
        {
            Destroy(gameObject);
        }

        Destroy(gameObject, .7f);

    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }

    private void OnDestroy()
    {
        temp = Instantiate(destroyParticle, transform.position,transform.rotation);
        Destroy(temp, 1.5f);
    }
}
