using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlyBracket1 : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    [SerializeField] GameObject Bullet;

    [SerializeField] Transform shootPoint;
    private Transform Bus;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Bus = GameObject.FindGameObjectWithTag("DreamBus").transform;
    }

    private void Update()
    {
        if (Bus == null)
            return;

        Vector2 direction = new Vector2(Bus.position.x - transform.position.x, Bus.position.y - transform.position.y);
        transform.right = -direction;

        if(timer <= 0)
        {
            Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
            timer = timeBtwSpawn;
        }else
        {
            timer -= Time.deltaTime; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet1"))
        {
            Destroy(collision.transform.gameObject);
            Destroy(gameObject);
        }
    }
}
